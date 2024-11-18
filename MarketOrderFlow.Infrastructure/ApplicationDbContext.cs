using MarketOrderFlow.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketOrderFlow.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<MarketModel> Markets => Set<MarketModel>();
    public DbSet<LogisticsCenterModel> LogisticsCenters => Set<LogisticsCenterModel>();
    public DbSet<ProductModel> Products => Set<ProductModel>();
    public DbSet<OrderModel> Orders => Set<OrderModel>();
    public DbSet<MarketProductStockModel> MarketProductStocks => Set<MarketProductStockModel>();
    public DbSet<ConfirmedOrderModel> ConfirmedOrders => Set<ConfirmedOrderModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ProductModel>()
           .HasMany(e => e.Orders)
           .WithMany(e => e.Products)
           .UsingEntity("ProductToOrder");

        modelBuilder.Entity<ConfirmedOrderModel>()
        .HasOne(co => co.Market)
        .WithMany()
        .HasForeignKey(co => co.MarketId);

        modelBuilder.Entity<ConfirmedOrderModel>()
            .HasOne(co => co.Product)
            .WithMany()
            .HasForeignKey(co => co.ProductId);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleModel>().HasData(new { Id = 1, Name = "User" });
        modelBuilder.Entity<RoleModel>().HasData(new { Id = 2, Name = "Manager"});
        modelBuilder.Entity<UserModel>().HasData(new { Id = 1, Name = "Sercan"});

    }
        public async Task<Result> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int countOfEntry = await SaveChangesAsync(cancellationToken);

            return countOfEntry == 0 ?
                Result.Failed() :
                Result.Success();
        }
        catch (Exception e) { return Result.Failed(e.Message); }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<BaseModel> insertedEntries = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Added && x.Entity is BaseModel)
            .Select(x => (BaseModel)x.Entity);

        IEnumerable<BaseModel> modifiedEntries = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Modified && x.Entity is BaseModel)
            .Select(x => (BaseModel)x.Entity);

        // Silinmiş öğeleri almak (soft delete)
        IEnumerable<BaseModel> deletedEntries = ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Deleted && x.Entity is BaseModel)
            .Select(x => (BaseModel)x.Entity);

        // Soft delete için: Eğer kaydın silinmesi gerekiyorsa, onu gerçekten silmek yerine 'IsDeleted' bayrağını true yapıyoruz.
        foreach (var entry in deletedEntries)
        {
            entry.IsDeleted = true;
            entry.DeletedAt = DateTimeOffset.UtcNow; // Silinme tarihini kaydediyoruz
        }

        foreach (var entry in insertedEntries)
            entry.CreatedAt = DateTimeOffset.UtcNow;

        foreach (var entry in modifiedEntries)
            entry.UpdatedAt = DateTimeOffset.UtcNow;

        return base.SaveChangesAsync(cancellationToken);
    }
}
