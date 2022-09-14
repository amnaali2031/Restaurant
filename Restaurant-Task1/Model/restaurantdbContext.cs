using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Restaurant_Task1.Model
{
    public partial class restaurantdbContext : DbContext
    {

        public bool IgnoreFilter { get; set; }

        public restaurantdbContext()
        {
        }

        public restaurantdbContext(DbContextOptions<restaurantdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Restaurant> Restaurants { get; set; }
        public virtual DbSet<RestaurantMenu> RestaurantMenus { get; set; }
        public virtual DbSet<RestaurantView> RestaurantViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-R1REQ4G;Database=restaurantdb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(55)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Customer");

                entity.HasOne(d => d.RestaurantMenu)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.RestaurantMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_RestaurantMenu");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("float");

                entity.Property(e => e.Archived)
                    .HasColumnType("int")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");


                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurant");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RestaurantMenu>(entity =>
            {
                entity.ToTable("RestaurantMenu");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MealName)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantMenus)
                    .HasForeignKey(d => d.RestaurantId)
                    .HasConstraintName("FK_RestaurantMenu_Restaurant");
            });

            modelBuilder.Entity<RestaurantView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RestaurantView");

                entity.Property(e => e.Expr1)
                    .HasColumnName("MostPurchasedCustomer")
                    .IsRequired()
                    .HasMaxLength(111);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(55);

                entity.Property(e => e.TheBestSellingMeal).HasMaxLength(55);
            });

            modelBuilder.Entity<Customer>().HasQueryFilter(a => (a.Archived == 0) || IgnoreFilter);
            modelBuilder.Entity<RestaurantMenu>().HasQueryFilter(a => (a.Archived == 0) || IgnoreFilter);
            modelBuilder.Entity<Restaurant>().HasQueryFilter(a => (a.Archived == 0) || IgnoreFilter);
            modelBuilder.Entity<Order>().HasQueryFilter(a => (a.Archived == 0) || IgnoreFilter);


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
