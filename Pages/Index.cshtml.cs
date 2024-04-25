using System.Security.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mindee;
using Mindee.Input;
using Mindee.Product.Receipt;
using Recidol.Migrations;
using Recidol.Models;
using System.Web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Recidol.Pages;

// uncomment to make website start on log in page
[Authorize] 
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly Recidol.Data.ApplicationDbContext _db;
    public string UserId { get; set;}
    public string Text { get; set; }
    public string Text2 { get; set; }
    public receiptInfo receiptInfo {get;set;}
    public lineItems Litems {get;set;}
    public List<receiptInfo> receipts {get;set;}
    public List<lineItems> items {get;set;}
    public List<receiptInfo> receiptsByShop {get;set;}
    public List<receiptInfo> receiptsByCountry {get;set;}
    public List<receiptInfo> receiptsByCategory {get;set;}
    public List<receiptInfo> receiptsByDate {get;set;}

    public IList<receiptInfo> ChartData { get; set; }
    public IndexModel(ILogger<IndexModel> logger, Recidol.Data.ApplicationDbContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        UserId = user.Id;
        receipts = _db.receiptInfo.Where(i => i.userId == UserId).ToList();
        items = _db.lineItems.ToList();
        receiptsByShop = _db.receiptInfo.Where(j => j.userId == UserId).OrderBy(i => i.SupplierName).ToList();
        receiptsByCategory = _db.receiptInfo.Where(j => j.userId == UserId).OrderBy(i => i.Category).ToList();
        receiptsByCountry = _db.receiptInfo.Where(j => j.userId == UserId).OrderBy(i => i.Country).ToList();
        receiptsByDate = _db.receiptInfo.Where(j => j.userId == UserId).OrderBy(i => i.date).ToList();
    }
    
    public async Task<IActionResult> OnPost(IFormFile postedImage)
    {
        if(postedImage != null && postedImage.Length > 0)
        {
            // Gets the file path of uploaded receipt 
            var fileName = Path.GetFileName(postedImage.FileName);
            Text = fileName;
            // Saves image to wwwroot folder
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await postedImage.CopyToAsync(fileStream);
            }
            
            // Api connection 
            string apiKey = "7360fb089233be90acf3f2c702b7eb71";
            MindeeClient mindeeClient = new MindeeClient(apiKey);
            
            var inputSource = new LocalInputSource(filePath);
            // Send image to api
            var response = await mindeeClient.ParseAsync<ReceiptV5>(inputSource);
            // Get response
            string responseToString = response.Document.ToString();

            Text2 = response.Document.Inference.Prediction.ToString();

            LOCALE = response.Document.Inference.Prediction.Locale.Currency.ToString();
            TOTAL = response.Document.Inference.Prediction.TotalAmount.ToString();
            
            if (response.Document.Inference.Prediction.SupplierName.Value != null)
            {
                SUPPLIERNAME = response.Document.Inference.Prediction.SupplierName.Value.ToString();
            }
            else
            {
                SUPPLIERNAME = "Unknown";
            }
            
            if (response.Document.Inference.Prediction.Time.Value != null)
            {
                TIME = response.Document.Inference.Prediction.Time.Value.ToString();
            }
            else
            {
                TIME = "Unknown";
            }
            if (response.Document.Inference.Prediction.Date.Value != null)
            {
                DATE = response.Document.Inference.Prediction.Date.Value.ToString();
            }
            else
            {
                DATE = "Unknown";
            }
            if(response.Document.Inference.Prediction.Locale.Country != null)
            {
                COUNTRY = response.Document.Inference.Prediction.Locale.Country.ToString();
            }
            else 
            {
                COUNTRY = LOCALE;
            }
            if (response.Document.Inference.Prediction.Subcategory.Value == null)
            {
                CATEGORY = response.Document.Inference.Prediction.Category.Value.ToString();
            }
            else{

                CATEGORY = response.Document.Inference.Prediction.Subcategory.Value.ToString();
            }
            
            
            
            if (receiptInfo == null)
            {
                receiptInfo = new receiptInfo();
            }
            receiptInfo.SupplierName = SUPPLIERNAME;
            receiptInfo.Currency = LOCALE;
            receiptInfo.time = TIME;
            receiptInfo.totalAmount = double.Parse(TOTAL);
            receiptInfo.Category = CATEGORY;
            receiptInfo.imagePath = fileName;
            receiptInfo.id = response.Document.Id;
            receiptInfo.Country = COUNTRY;
            receiptInfo.date = DATE;
            var user = await _userManager.GetUserAsync(User);
            receiptInfo.userId = user.Id;

            foreach (var item in response.Document.Inference.Prediction.LineItems)
            {
                LineItem lineitem = new LineItem();
                lineitem.TotalAmount = item.TotalAmount;
                lineitem.UnitPrice = item.UnitPrice;
                lineitem.Quantity = item.Quantity;
                lineitem.Description = item.Description.ToString();

                lineItems Litems = new lineItems();

                Litems.price = item.UnitPrice;
                Litems.description = item.Description.ToString();
                if (item.Quantity != null)
                {
                    Litems.quantity = item.Quantity;
                }
                else
                {
                    Litems.quantity = 1;
                }
                
                Litems.receiptId = response.Document.Id;
                Litems.totalAmount = item.TotalAmount;
                _db.lineItems.Add(Litems);
            }
            
            _db.receiptInfo.Add(receiptInfo);
            await _db.SaveChangesAsync();
        }

        return RedirectToPage("/Index");
        
    }
    
    // Delete Receipt function
    public async Task<IActionResult> OnPostDeleteAsync(string receiptId)
    {
        // Finding receipt based on ID
        var receipt = await _db.receiptInfo.FindAsync(receiptId);
        if (receipt != null)
        {
            // Remove associated line items
            var lineItems = _db.lineItems.Where(l => l.receiptId == receiptId);
            _db.lineItems.RemoveRange(lineItems);

            // Delete receipt from db
            _db.receiptInfo.Remove(receipt);
            await _db.SaveChangesAsync();
        }

        // Redirect back to the home page
        return RedirectToPage("/Index");
    }




    public List<LineItem> lineItems { get; set; }
    


    public string CATEGORY {get;set;}
    public string TOTAL {get;set;}
    public string? DATE {get;set;}
    public string LOCALE {get;set;}
    public string SUPPLIERNAME {get;set;}
    public string TIME {get;set;}
    public string QUANTITY {get;set;}
    public string COUNTRY {get;set;}

    
    public class LineItem
    {
        public double Confidence { get; set; }
        public string? Description { get; set; }
        public List<List<double>> Polygon { get; set; }
        public double? Quantity { get; set; }
        public double? TotalAmount { get; set; }
        public double? UnitPrice { get; set; }
    }
}
