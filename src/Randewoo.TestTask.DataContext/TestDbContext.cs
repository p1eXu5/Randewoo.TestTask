using Randewoo.TestTask.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Randewoo.TestTask.DataContext
{
    public partial  class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext (DbContextOptions< TestDbContext > options )
            : base(options)
        {
        }

        public virtual DbSet<Distributor> Distributors { get; set; }
        public virtual DbSet< Link > Links { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<PriceRecord> PricesRecords { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=test_test;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            modelBuilder.Entity< Distributor >(entity =>
            {
                entity.ToTable("DISTRIBUTORS");

                entity.HasIndex(e => e.Id)
                    .HasName("DISTRIBUTORSUNIQUEID")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("DISTRIBUTORSUNIQUENAME")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Active).HasColumnName("ACTIVE");

                entity.Property(e => e.Address)
                    .HasColumnName("ADDRESS")
                    .HasMaxLength(1024);

                entity.Property(e => e.Comment)
                    .HasColumnName("COMMENT")
                    .HasMaxLength(4000);

                entity.Property(e => e.Dealerorder).HasColumnName("DEALERORDER");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(1024);

                entity.Property(e => e.Firstalways).HasColumnName("FIRSTALWAYS");

                entity.Property(e => e.Goinpurchaselist).HasColumnName("GOINPURCHASELIST");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasMaxLength(64);

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.Sendmail).HasColumnName("SENDMAIL");
            });

            modelBuilder.Entity< Link >(entity =>
            {
                entity.ToTable("LINKS");

                entity.HasIndex(e => e.Id)
                    .HasName("LINKSUNIQUEID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductId).HasColumnName("CATALOGPRODUCTID");

                entity.Property(e => e.Pricerecordindex).HasColumnName("PRICERECORDINDEX");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("LINKSFOREIGNCATALOGPRODUCT");

                entity.HasOne(d => d.PriceRecord)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.Pricerecordindex)
                    .HasConstraintName("LINKSFOREIGNPRICERECORD");
            });

            modelBuilder.Entity< Manufacturers >(entity =>
            {
                entity.ToTable("MANUFACTURERS");

                entity.HasIndex(e => e.Id)
                    .HasName("MANUFACTURERSUNIQUEID")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("MANUFACTURERSUNIQUENAME")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Extra).HasColumnName("EXTRA");

                entity.Property(e => e.Extraused).HasColumnName("EXTRAUSED");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Published)
                    .IsRequired()
                    .HasColumnName("PUBLISHED")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity< Price >(entity =>
            {
                entity.ToTable("PRICES");

                entity.HasIndex(e => e.Id)
                    .HasName("PRICESUNIQUEID")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("PRICESUNIQUENAME")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("COMMENT")
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.DefaultCurrency).HasColumnName("DEFAULTCURRENCY");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.DistributorId).HasColumnName("DISID");

                entity.Property(e => e.Filedate)
                    .HasColumnName("FILEDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .HasColumnName("FILENAME")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Filesheets)
                    .HasColumnName("FILESHEETS")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Firstrow).HasColumnName("FIRSTROW");

                entity.Property(e => e.Instockrange)
                    .IsRequired()
                    .HasColumnName("INSTOCKRANGE")
                    .HasMaxLength(16)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasColumnName("ISACTIVE")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Maxpricechange).HasColumnName("MAXPRICECHANGE");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Namerange)
                    .IsRequired()
                    .HasColumnName("NAMERANGE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Pricerange)
                    .IsRequired()
                    .HasColumnName("PRICERANGE")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Sheet)
                    .IsRequired()
                    .HasColumnName("SHEET")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Skurange)
                    .IsRequired()
                    .HasColumnName("SKURANGE")
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Stockrange)
                    .IsRequired()
                    .HasColumnName("STOCKRANGE")
                    .HasMaxLength(16)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Stopwords)
                    .HasColumnName("STOPWORDS")
                    .HasMaxLength(6000)
                    .IsUnicode(false);

                entity.Property(e => e.Turndollarsrate).HasColumnName("TURNDOLLARSRATE");

                entity.HasOne(d => d.Distributor)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.DistributorId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRICESFOREIGNDISTRIBUTOR");
            });

            modelBuilder.Entity< PriceRecord >(entity =>
            {
                entity.HasKey(e => e.RecordIndex)
                    .HasName("PRICESRECORDSPRIMARY");

                entity.ToTable("PRICESRECORDS");

                entity.HasIndex(e => e.RecordIndex)
                    .HasName("PRICESRECORDSUNIQUEINDEX")
                    .IsUnique();

                entity.Property(e => e.RecordIndex).HasColumnName("RECORDINDEX");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("COMMENT")
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.Instock)
                    .HasColumnName("INSTOCK")
                    .HasDefaultValueSql("((10000))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(1024);

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Priceid).HasColumnName("PRICEID");

                entity.Property(e => e.Sku)
                    .IsRequired()
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.State).HasColumnName("STATE");

                entity.Property(e => e.Stock).HasColumnName("STOCK");

                entity.Property(e => e.Used).HasColumnName("USED");

                entity.HasOne(d => d.PriceNavigation)
                    .WithMany(p => p.PriceRecords)
                    .HasForeignKey(d => d.Priceid)
                    .HasConstraintName("PRICESRECORDSFOREIGNPRICE");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCTS");

                entity.HasIndex(e => e.Id)
                    .HasName("PRODUCTSUNIQUEID")
                    .IsUnique();

                entity.HasIndex(e => e.Label)
                    .HasName("PRODUCTSUNIQUELABEL")
                    .IsUnique();

                entity.HasIndex(e => e.Sku)
                    .HasName("PRODUCTSUNIQUESKU")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Autoupdate).HasColumnName("AUTOUPDATE");

                entity.Property(e => e.Autoupdatetests).HasColumnName("AUTOUPDATETESTS");

                entity.Property(e => e.Baseprice).HasColumnName("BASEPRICE");

                entity.Property(e => e.Changedate)
                    .HasColumnName("CHANGEDATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ChildName)
                    .IsRequired()
                    .HasColumnName("CHILDNAME")
                    .HasMaxLength(1024)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("COMMENT")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Correctedstatus).HasColumnName("CORRECTEDSTATUS");

                entity.Property(e => e.Createdate)
                    .HasColumnName("CREATEDATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Defaultcurrency)
                    .HasColumnName("DEFAULTCURRENCY")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Deleted).HasColumnName("DELETED");

                entity.Property(e => e.Extra).HasColumnName("EXTRA");

                entity.Property(e => e.Extraused).HasColumnName("EXTRAUSED");

                entity.Property(e => e.Instock)
                    .HasColumnName("INSTOCK")
                    .HasDefaultValueSql("((10000))");

                entity.Property(e => e.IsPromo).HasColumnName("IS_PROMO");

                entity.Property(e => e.Isnew).HasColumnName("ISNEW");

                entity.Property(e => e.Isnewstartdate)
                    .HasColumnName("ISNEWSTARTDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Isprobirka).HasColumnName("ISPROBIRKA");

                entity.Property(e => e.Label)
                    .IsRequired()
                    .HasColumnName("LABEL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Manid).HasColumnName("MANID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Parentlabel)
                    .HasColumnName("PARENTLABEL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Photoexists).HasColumnName("PHOTOEXISTS");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.Productexists).HasColumnName("PRODUCTEXISTS");

                entity.Property(e => e.Published)
                    .IsRequired()
                    .HasColumnName("PUBLISHED")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Sku).HasColumnName("SKU");

                entity.Property(e => e.State).HasColumnName("STATE");

                entity.Property(e => e.Templateid).HasColumnName("TEMPLATEID");

                entity.Property(e => e.Tester).HasColumnName("TESTER");

                entity.Property(e => e.Userchanged).HasColumnName("USERCHANGED");

                entity.Property(e => e.Viewstyleid)
                    .HasColumnName("VIEWSTYLEID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Weight)
                    .HasColumnName("WEIGHT")
                    .HasDefaultValueSql("((100))");

                entity.HasOne(d => d.Man)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Manid)
                    .HasConstraintName("PRODUCTSFOREIGNMANUFACTURER");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
