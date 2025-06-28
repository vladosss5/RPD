using Microsoft.EntityFrameworkCore;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Data;

public class DataContext : DbContext
{
    /// <summary>
    /// Конструктор для миграций.
    /// </summary>
    public DataContext() { }
    
    /// <summary>
    /// Конструктор.
    /// </summary>
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlite($"Data Source=C:\\Users\\VPC\\Documents\\Projects\\RentalPoint.Desktop\\RentalPoint.Data\\DataBase.sqlite");
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Dictionary> Dictionaries { get; set; }
    public DbSet<DictionaryValue> DictionaryValues { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderInventory> OrderInventories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Deposit>(entity =>
        {
            entity.Property(e => e.IsReturned)
                .HasDefaultValue(false);
            entity.Property(e => e.DepositDate)
                .HasColumnType("datetime");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime");
            entity.Property(e => e.Comment)
                .HasMaxLength(500);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
        });

        // Настройка связей и ключей
        modelBuilder.Entity<OrderInventory>()
            .HasKey(oi => oi.Id);

        modelBuilder.Entity<OrderInventory>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderInventories)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderInventory>()
            .HasOne(oi => oi.Inventory)
            .WithMany(i => i.OrderInventories)
            .HasForeignKey(oi => oi.InventoryId);

        modelBuilder.Entity<DictionaryValue>()
            .HasOne(dv => dv.Dictionary)
            .WithMany(d => d.DictionaryValues)
            .HasForeignKey(dv => dv.DictionaryId);

        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Type)
            .WithMany()
            .HasForeignKey(i => i.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Status)
            .WithMany()
            .HasForeignKey(i => i.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Status)
            .WithMany()
            .HasForeignKey(o => o.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}