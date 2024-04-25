using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recidol.Models;

namespace Recidol.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    // DbSets for the models
    public DbSet<Recidol.Models.lineItems> lineItems {get;set;}
    public DbSet<Recidol.Models.receiptInfo> receiptInfo {get;set;}
}
