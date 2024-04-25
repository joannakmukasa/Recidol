using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Recidol.Models
{
    public class receiptInfo
    {
        [Key]
        public string id { get; set; }
        public string userId { get; set; }
        public string SupplierName { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
        public double totalAmount { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string imagePath { get; set; }
        public string? Country { get; set; }
    }
}