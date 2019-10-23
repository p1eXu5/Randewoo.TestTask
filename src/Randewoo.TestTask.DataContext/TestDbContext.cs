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

        public virtual DbSet<Distributors> Distributors { get; set; }
        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<Manufacturers> Manufacturers { get; set; }
        public virtual DbSet<Prices> Prices { get; set; }
        public virtual DbSet<Pricesrecords> Pricesrecords { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=test_test;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Distributors>(entity =>
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

            modelBuilder.Entity<Links>(entity =>
            {
                entity.ToTable("LINKS");

                entity.HasIndex(e => e.Id)
                    .HasName("LINKSUNIQUEID")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Catalogproductid).HasColumnName("CATALOGPRODUCTID");

                entity.Property(e => e.Pricerecordindex).HasColumnName("PRICERECORDINDEX");

                entity.HasOne(d => d.Catalogproduct)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.Catalogproductid)
                    .HasConstraintName("LINKSFOREIGNCATALOGPRODUCT");

                entity.HasOne(d => d.PricerecordindexNavigation)
                    .WithMany(p => p.Links)
                    .HasForeignKey(d => d.Pricerecordindex)
                    .HasConstraintName("LINKSFOREIGNPRICERECORD");
            });

            modelBuilder.Entity<Manufacturers>(entity =>
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

            modelBuilder.Entity<Prices>(entity =>
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

                entity.Property(e => e.Defaultcurrency).HasColumnName("DEFAULTCURRENCY");

                entity.Property(e => e.Discount).HasColumnName("DISCOUNT");

                entity.Property(e => e.Disid).HasColumnName("DISID");

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

                entity.Property(e => e.Isactive)
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

                entity.HasOne(d => d.Dis)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.Disid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("PRICESFOREIGNDISTRIBUTOR");
            });

            modelBuilder.Entity<Pricesrecords>(entity =>
            {
                entity.HasKey(e => e.Recordindex)
                    .HasName("PRICESRECORDSPRIMARY");

                entity.ToTable("PRICESRECORDS");

                entity.HasIndex(e => e.Recordindex)
                    .HasName("PRICESRECORDSUNIQUEINDEX")
                    .IsUnique();

                entity.Property(e => e.Recordindex).HasColumnName("RECORDINDEX");

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
                    .WithMany(p => p.Pricesrecords)
                    .HasForeignKey(d => d.Priceid)
                    .HasConstraintName("PRICESRECORDSFOREIGNPRICE");
            });

            modelBuilder.Entity<Products>(entity =>
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

                entity.Property(e => e.Childname)
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
