﻿using MarketOrderFlow.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketOrderFlow.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<MarketModel> Markets { get; set; }
    public DbSet<LogisticsCenterModel> LogisticsCenters { get; set; }
    public DbSet<ProductModel> Products { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderItemModel> OrderItems { get; set; }


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