using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace NEXT.DB.Models
{
    public partial class NEXTContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=localhost,1433;Database=NEXT;User ID=NEXT;Password=password31!;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasIndex(e => e.AttributeTypeID).HasName("AttributetypeAttribute_FK");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.AttributeType).WithMany(p => p.Attribute).HasForeignKey(d => d.AttributeTypeID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeGroup>(entity =>
            {
                entity.Property(e => e.AttributeGroupName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<AttributeGroupAttribute>(entity =>
            {
                entity.HasKey(e => new { e.AttributeGroupID, e.AttributeID });

                entity.HasIndex(e => e.AttributeGroupID).HasName("Attribute_AttributeGroup_FK");

                entity.HasIndex(e => e.AttributeID).HasName("AttributeGroup_Attribute_FK");

                entity.HasOne(d => d.AttributeGroup).WithMany(p => p.AttributeGroupAttribute).HasForeignKey(d => d.AttributeGroupID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeGroupAttribute).HasForeignKey(d => d.AttributeID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeGroupTranslation>(entity =>
            {
                entity.HasKey(e => new { e.LanguageID, e.AttributeGroupID });

                entity.HasIndex(e => e.AttributeGroupID).HasName("AttributegroupAttributegrouptranslation_FK");

                entity.HasIndex(e => e.LanguageID).HasName("LanguageAttributegroup_FK");

                entity.Property(e => e.AttributeGroupTranslationValue)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.AttributeGroup).WithMany(p => p.AttributeGroupTranslation).HasForeignKey(d => d.AttributeGroupID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Language).WithMany(p => p.AttributeGroupTranslation).HasForeignKey(d => d.LanguageID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeOption>(entity =>
            {
                entity.HasKey(e => new { e.AttributeID, e.Value });

                entity.HasIndex(e => e.AttributeID).HasName("AttributeAttributeoption_FK");

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeOption).HasForeignKey(d => d.AttributeID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeOptionTranslation>(entity =>
            {
                entity.HasKey(e => new { e.AttributeID, e.Value, e.LanguageID });

                entity.HasIndex(e => e.LanguageID).HasName("LanuageAttributeoptiontranslation_FK");

                entity.HasIndex(e => new { e.AttributeID, e.Value }).HasName("AttributeoptionAttributeoptiontranslation_FK");

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.Property(e => e.TranslationValue)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Language).WithMany(p => p.AttributeOptionTranslation).HasForeignKey(d => d.LanguageID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.AttributeOption).WithMany(p => p.AttributeOptionTranslation).HasForeignKey(d => new { d.AttributeID, d.Value }).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeTranslation>(entity =>
            {
                entity.HasKey(e => new { e.AttributeID, e.LanguageID });

                entity.HasIndex(e => e.AttributeID).HasName("AttributeAttributetranslation_FK");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Attribute).WithMany(p => p.AttributeTranslation).HasForeignKey(d => d.AttributeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Language).WithMany(p => p.AttributeTranslation).HasForeignKey(d => d.LanguageID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<AttributeType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<BarcodeType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ChanneProductPrice>(entity =>
            {
                entity.HasKey(e => new { e.ChannelID, e.ProductID, e.PriceTypeID, e.VATID, e.CurrencyID });

                entity.HasIndex(e => new { e.ChannelID, e.ProductID }).HasName("ChannelproductChannelproductprice_FK");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Currency).WithMany(p => p.ChanneProductPrice).HasForeignKey(d => d.CurrencyID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PriceType).WithMany(p => p.ChanneProductPrice).HasForeignKey(d => d.PriceTypeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.VAT).WithMany(p => p.ChanneProductPrice).HasForeignKey(d => d.VATID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ChannelProduct).WithMany(p => p.ChanneProductPrice).HasForeignKey(d => new { d.ChannelID, d.ProductID }).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.HasIndex(e => e.ParentChannelID).HasName("ChannelChannels_FK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ParentChannel).WithMany(p => p.InverseParentChannel).HasForeignKey(d => d.ParentChannelID);
            });

            modelBuilder.Entity<ChannelProduct>(entity =>
            {
                entity.HasKey(e => new { e.ChannelID, e.ProductID });

                entity.HasIndex(e => e.ChannelID).HasName("ChannelChannelproduct_FK");

                entity.HasIndex(e => e.ProductID).HasName("ProductChannelproduct_FK");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Channel).WithMany(p => p.ChannelProduct).HasForeignKey(d => d.ChannelID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Product).WithMany(p => p.ChannelProduct).HasForeignKey(d => d.ProductID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ChannelProductStock>(entity =>
            {
                entity.HasKey(e => new { e.ChannelID, e.ProductID, e.StockTypeID });

                entity.HasIndex(e => e.StockTypeID).HasName("StockStocktype_FK");

                entity.HasIndex(e => new { e.ChannelID, e.ProductID }).HasName("ChannelproductChannelproductstock_FK");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.HasOne(d => d.StockType).WithMany(p => p.ChannelProductStock).HasForeignKey(d => d.StockTypeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ChannelProduct).WithMany(p => p.ChannelProductStock).HasForeignKey(d => new { d.ChannelID, d.ProductID }).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ChannelSettings>(entity =>
            {
                entity.HasIndex(e => e.ChannelID).HasName("ChannelChannelsettings_FK");

                entity.HasOne(d => d.Channel).WithMany(p => p.ChannelSettings).HasForeignKey(d => d.ChannelID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("CompanyName").IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Currrency>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("varchar");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.ISOCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PriceType>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("varchar");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.BrandID).HasName("Brand_FK");

                entity.HasIndex(e => e.ExternalProductIdentifier).HasName("ExternalProductIdentifier").IsUnique();

                entity.HasIndex(e => e.ParentProductID).HasName("ChildProducts_FK");

                entity.HasIndex(e => e.ProductTypeID).HasName("ProducttypeProduct_FK");

                entity.HasIndex(e => e.SKU).HasName("SKU").IsUnique();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.ExternalProductIdentifier).HasMaxLength(255);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.SKU)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Brand).WithMany(p => p.Product).HasForeignKey(d => d.BrandID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ParentProduct).WithMany(p => p.InverseParentProduct).HasForeignKey(d => d.ParentProductID);

                entity.HasOne(d => d.ProductType).WithMany(p => p.Product).HasForeignKey(d => d.ProductTypeID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductAttributeOption>(entity =>
            {
                entity.HasKey(e => new { e.AttributeID, e.ProductID, e.VendorID, e.Value });

                entity.HasIndex(e => e.ProductID).HasName("ProductProductattributeoption_FK");

                entity.HasIndex(e => e.VendorID).HasName("VendorProductattributeoption_FK");

                entity.HasIndex(e => new { e.AttributeID, e.Value }).HasName("AttributeoptionProductattributeoption_FK");

                entity.Property(e => e.Value).HasMaxLength(255);

                entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributeOption).HasForeignKey(d => d.ProductID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Vendor).WithMany(p => p.ProductAttributeOption).HasForeignKey(d => d.VendorID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.AttributeOption).WithMany(p => p.ProductAttributeOption).HasForeignKey(d => new { d.AttributeID, d.Value }).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductAttributeValue>(entity =>
            {
                entity.HasKey(e => new { e.AttributeID, e.ProductID, e.LanguageID, e.VendorID });

                entity.HasIndex(e => e.AttributeID).HasName("AttributeProductattributevalue_FK");

                entity.HasIndex(e => e.ProductID).HasName("ProductProductattributevalue_FK");

                entity.HasIndex(e => e.VendorID).HasName("VendorProductattributevalue_FK");

                entity.Property(e => e.Value).HasColumnType("text");

                entity.HasOne(d => d.Attribute).WithMany(p => p.ProductAttributeValue).HasForeignKey(d => d.AttributeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Language).WithMany(p => p.ProductAttributeValue).HasForeignKey(d => d.LanguageID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Product).WithMany(p => p.ProductAttributeValue).HasForeignKey(d => d.ProductID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Vendor).WithMany(p => p.ProductAttributeValue).HasForeignKey(d => d.VendorID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductBarcode>(entity =>
            {
                entity.HasKey(e => new { e.BarcodeTypeID, e.Barcode, e.ProductID, e.VendorID });

                entity.HasIndex(e => e.BarcodeTypeID).HasName("BarcodetypeBarcode_FK");

                entity.HasIndex(e => new { e.ProductID, e.VendorID }).HasName("VendorProductbarcode_FK");

                entity.Property(e => e.Barcode).HasMaxLength(50);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.HasOne(d => d.BarcodeType).WithMany(p => p.ProductBarcode).HasForeignKey(d => d.BarcodeTypeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.VendorProduct).WithMany(p => p.ProductBarcode).HasForeignKey(d => new { d.ProductID, d.VendorID }).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductRelationType>(entity =>
            {
                entity.HasKey(e => e.RelatedProductTypeID);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RelatedProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.RelatedProductID, e.RelatedProductTypeID });

                entity.HasIndex(e => e.ProductID).HasName("ProductRelatedproduct_FK");

                entity.HasIndex(e => e.RelatedProductTypeID).HasName("ProductrelationtypeRelatedproduct_FK");

                entity.HasOne(d => d.Product).WithMany(p => p.RelatedProduct).HasForeignKey(d => d.ProductID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RelatedProductNavigation).WithMany(p => p.RelatedProductNavigation).HasForeignKey(d => d.RelatedProductID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.RelatedProductType).WithMany(p => p.RelatedProduct).HasForeignKey(d => d.RelatedProductTypeID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StockType>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.CompanyID).HasName("CompanyUser_FK");

                entity.HasIndex(e => e.Username).HasName("IX_CompanyID_UserName");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Company).WithMany(p => p.User).HasForeignKey(d => d.CompanyID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.roleID).IsRequired();

            });

            modelBuilder.Entity<VAT>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Percentage).HasColumnType("decimal");
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.HasIndex(e => e.CompanyID).HasName("CompanyVendor_FK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Company).WithMany(p => p.Vendor).HasForeignKey(d => d.CompanyID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VendorProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.VendorID });

                entity.HasIndex(e => e.ProductID).HasName("ProductVendorproduct_FK");

                entity.HasIndex(e => e.VendorID).HasName("VendorVendorproduct_FK");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.HasOne(d => d.Product).WithMany(p => p.VendorProduct).HasForeignKey(d => d.ProductID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Vendor).WithMany(p => p.VendorProduct).HasForeignKey(d => d.VendorID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VendorProductPrice>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.VendorID, e.CurrencyID, e.PriceTypeID, e.VATID });

                entity.HasIndex(e => new { e.ProductID, e.VendorID }).HasName("VendorproductVendorproductprice_FK");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Currency).WithMany(p => p.VendorProductPrice).HasForeignKey(d => d.CurrencyID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PriceType).WithMany(p => p.VendorProductPrice).HasForeignKey(d => d.PriceTypeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.VAT).WithMany(p => p.VendorProductPrice).HasForeignKey(d => d.VATID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.VendorProduct).WithMany(p => p.VendorProductPrice).HasForeignKey(d => new { d.ProductID, d.VendorID }).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<VendorStock>(entity =>
            {
                entity.HasKey(e => new { e.ProductID, e.VendorID, e.StockTypeID });

                entity.HasIndex(e => e.StockTypeID).HasName("StocktypeVendorstock_FK");

                entity.HasIndex(e => new { e.ProductID, e.VendorID }).HasName("VendorproductVendorstock_FK");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.HasOne(d => d.StockType).WithMany(p => p.VendorStock).HasForeignKey(d => d.StockTypeID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.VendorProduct).WithMany(p => p.VendorStock).HasForeignKey(d => new { d.ProductID, d.VendorID }).OnDelete(DeleteBehavior.Restrict);
            });
        }

        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<AttributeGroup> AttributeGroup { get; set; }
        public virtual DbSet<AttributeGroupAttribute> AttributeGroupAttribute { get; set; }
        public virtual DbSet<AttributeGroupTranslation> AttributeGroupTranslation { get; set; }
        public virtual DbSet<AttributeOption> AttributeOption { get; set; }
        public virtual DbSet<AttributeOptionTranslation> AttributeOptionTranslation { get; set; }
        public virtual DbSet<AttributeTranslation> AttributeTranslation { get; set; }
        public virtual DbSet<AttributeType> AttributeType { get; set; }
        public virtual DbSet<BarcodeType> BarcodeType { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<ChanneProductPrice> ChanneProductPrice { get; set; }
        public virtual DbSet<Channel> Channel { get; set; }
        public virtual DbSet<ChannelProduct> ChannelProduct { get; set; }
        public virtual DbSet<ChannelProductStock> ChannelProductStock { get; set; }
        public virtual DbSet<ChannelSettings> ChannelSettings { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Currrency> Currrency { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<PriceType> PriceType { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductAttributeOption> ProductAttributeOption { get; set; }
        public virtual DbSet<ProductAttributeValue> ProductAttributeValue { get; set; }
        public virtual DbSet<ProductBarcode> ProductBarcode { get; set; }
        public virtual DbSet<ProductRelationType> ProductRelationType { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<RelatedProduct> RelatedProduct { get; set; }
        public virtual DbSet<StockType> StockType { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<VAT> VAT { get; set; }
        public virtual DbSet<Vendor> Vendor { get; set; }
        public virtual DbSet<VendorProduct> VendorProduct { get; set; }
        public virtual DbSet<VendorProductPrice> VendorProductPrice { get; set; }
        public virtual DbSet<VendorStock> VendorStock { get; set; }
    }
}