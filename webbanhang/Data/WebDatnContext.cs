using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webbanhang.Data;

public partial class WebDatnContext : DbContext
{
    public WebDatnContext()
    {
    }

    public WebDatnContext(DbContextOptions<WebDatnContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountAddress> AccountAddresses { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Disscount> Disscounts { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<OderDetail> OderDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderAddress> OrderAddresses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<ReplyFeedback> ReplyFeedbacks { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PhanDuyHieu\\SQLEXPRESS;Initial Catalog=web_DATN;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK_table_name1");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId)
                .HasMaxLength(20)
                .HasColumnName("account_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .HasColumnType("text")
                .HasColumnName("avatar");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Districs).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.RandomKey)
                .HasMaxLength(10)
                .HasColumnName("Random_Key");
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.Ward).HasMaxLength(50);
        });

        modelBuilder.Entity<AccountAddress>(entity =>
        {
            entity.HasKey(e => e.AccountAddressId).HasName("PK__Account___056FBBEF20814EAE");

            entity.ToTable("Account_address");

            entity.Property(e => e.AccountAddressId)
                .ValueGeneratedNever()
                .HasColumnName("account_address_ID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(20)
                .HasColumnName("account_ID");
            entity.Property(e => e.AccountPhonenumber)
                .HasMaxLength(30)
                .HasColumnName("account_phonenumber");
            entity.Property(e => e.AccountUsername)
                .HasMaxLength(20)
                .HasColumnName("account_username");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
            entity.Property(e => e.DistrictId).HasColumnName("district_ID");
            entity.Property(e => e.Isdefault).HasColumnName("isdefault");
            entity.Property(e => e.ProvinceId).HasColumnName("province_ID");
            entity.Property(e => e.WardId).HasColumnName("ward_ID");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountAddresses)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_address_id");

            entity.HasOne(d => d.District).WithMany(p => p.AccountAddresses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account_a__distr__5EBF139D");

            entity.HasOne(d => d.Province).WithMany(p => p.AccountAddresses)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account_a__provi__5CD6CB2B");

            entity.HasOne(d => d.Ward).WithMany(p => p.AccountAddresses)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account_a__ward___5DCAEF64");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brand__5E5B8BAF825B4999");

            entity.ToTable("brand");

            entity.Property(e => e.BrandId)
                .ValueGeneratedNever()
                .HasColumnName("brand_ID");
            entity.Property(e => e.BrandName)
                .HasMaxLength(50)
                .HasColumnName("brand_name");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK__contact__024F7EDECF09A6D2");

            entity.ToTable("contact");

            entity.Property(e => e.ContactId)
                .ValueGeneratedNever()
                .HasColumnName("contact_ID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(50)
                .HasColumnName("create_by");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(30)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__Delivery__1CA3F90D8564A3D3");

            entity.ToTable("Delivery");

            entity.Property(e => e.DeliveryId)
                .ValueGeneratedNever()
                .HasColumnName("delivery_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<Disscount>(entity =>
        {
            entity.HasKey(e => e.DisscountId).HasName("PK__Disscoun__7012EA087DB6660C");

            entity.ToTable("Disscount");

            entity.Property(e => e.DisscountId)
                .ValueGeneratedNever()
                .HasColumnName("Disscount_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.DisscountCode)
                .HasMaxLength(50)
                .HasColumnName("Disscount_code");
            entity.Property(e => e.DisscountEnd)
                .HasColumnType("datetime")
                .HasColumnName("Disscount_end");
            entity.Property(e => e.DisscountName)
                .HasMaxLength(100)
                .HasColumnName("Disscount_name");
            entity.Property(e => e.DisscountPrice).HasColumnName("Disscount_price");
            entity.Property(e => e.DisscountStart)
                .HasColumnType("datetime")
                .HasColumnName("Disscount_start");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__District__25203623DDD1D6B2");

            entity.ToTable("District");

            entity.Property(e => e.DistrictId)
                .ValueGeneratedNever()
                .HasColumnName("district_ID");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(30)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_ID");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__District__provin__412EB0B6");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK_Employees_ID");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId)
                .HasMaxLength(20)
                .HasColumnName("feedback_ID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.DisscountId).HasColumnName("disscount_ID");
            entity.Property(e => e.GenreId).HasColumnName("genre_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.RateStar).HasColumnName("rate_star");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");

            entity.HasOne(d => d.Disscount).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.DisscountId)
                .HasConstraintName("FK__Feedback__dissco__76969D2E");

            entity.HasOne(d => d.Genre).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Feedback__genre___75A278F5");

            entity.HasOne(d => d.Product).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ChildTable_ParentTable11");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genre__964A20061D4505AB");

            entity.ToTable("Genre");

            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("Genre_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasColumnName("Genre_name");
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<OderDetail>(entity =>
        {
            entity.HasKey(e => e.OderDetailId).HasName("PK__Oder_det__E4C86E20E5354A10");

            entity.ToTable("Oder_detail");

            entity.Property(e => e.OderDetailId).HasColumnName("oder_detail_ID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(20)
                .HasColumnName("account_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.OrderId).HasColumnName("order_ID");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .HasColumnName("size");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Transection)
                .HasMaxLength(50)
                .HasColumnName("transection");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");

            entity.HasOne(d => d.Account).WithMany(p => p.OderDetails)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("fk_parent22222");

            entity.HasOne(d => d.Order).WithMany(p => p.OderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("Ten_RangBuoc_FK55555");

            entity.HasOne(d => d.Product).WithMany(p => p.OderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ChildTable_ParentTable111");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Ten_Bang_NewColumn");

            entity.Property(e => e.OrderId).HasColumnName("order_ID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(20)
                .HasColumnName("account_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.Delivery)
                .HasMaxLength(50)
                .HasColumnName("delivery");
            entity.Property(e => e.DisscountId).HasColumnName("Disscount_ID");
            entity.Property(e => e.District)
                .HasMaxLength(20)
                .HasColumnName("district");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderNote)
                .HasMaxLength(200)
                .HasColumnName("order_note");
            entity.Property(e => e.Payment)
                .HasMaxLength(50)
                .HasColumnName("payment");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Province)
                .HasMaxLength(20)
                .HasColumnName("province");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
            entity.Property(e => e.Ward)
                .HasMaxLength(20)
                .HasColumnName("ward");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_address_id1");

            entity.HasOne(d => d.Disscount).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DisscountId)
                .HasConstraintName("constraint_name222");
        });

        modelBuilder.Entity<OrderAddress>(entity =>
        {
            entity.HasKey(e => e.OrderAddressId).HasName("PK__Order_ad__9A9E375F5AF858F7");

            entity.ToTable("Order_address");

            entity.Property(e => e.OrderAddressId)
                .ValueGeneratedNever()
                .HasColumnName("order_address_ID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.DistrictId).HasColumnName("district_ID");
            entity.Property(e => e.OrderPhonenumber)
                .HasMaxLength(20)
                .HasColumnName("order_phonenumber");
            entity.Property(e => e.OrderUsername)
                .HasMaxLength(20)
                .HasColumnName("order_username");
            entity.Property(e => e.ProvinceId).HasColumnName("province_ID");
            entity.Property(e => e.Timeedit).HasColumnName("timeedit");
            entity.Property(e => e.WardId).HasColumnName("ward_ID");

            entity.HasOne(d => d.District).WithMany(p => p.OrderAddresses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_add__distr__6477ECF3");

            entity.HasOne(d => d.Province).WithMany(p => p.OrderAddresses)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_add__provi__628FA481");

            entity.HasOne(d => d.Ward).WithMany(p => p.OrderAddresses)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Order_add__ward___6383C8BA");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__ED10C442A47BDCB2");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("payment_ID");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.PaymentName)
                .HasMaxLength(50)
                .HasColumnName("payment_name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(50)
                .HasColumnName("update_by");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_TableName222");

            entity.ToTable("product");

            entity.Property(e => e.ProductId).HasColumnName("Product_ID");
            entity.Property(e => e.BrandId).HasColumnName("brand_ID");
            entity.Property(e => e.Byturn).HasColumnName("byturn");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.CreateBy)
                .HasMaxLength(20)
                .HasColumnName("create_by");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GenreId).HasColumnName("Genre_ID");
            entity.Property(e => e.Image)
                .HasMaxLength(50)
                .HasColumnName("image");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Specifications).HasColumnName("specifications");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UpdateBy)
                .HasMaxLength(20)
                .HasColumnName("update_by");
            entity.Property(e => e.Views).HasColumnName("views");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__brand_I__4CA06362");

            entity.HasOne(d => d.Genre).WithMany(p => p.Products)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__product__Genre_I__4D94879B");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImgId).HasName("PK__Product___C6E137DF9E9B8942");

            entity.ToTable("Product_Image");

            entity.Property(e => e.ProductImgId)
                .ValueGeneratedNever()
                .HasColumnName("product_img_ID");
            entity.Property(e => e.DisscountId).HasColumnName("disscount_ID");
            entity.Property(e => e.GenreId).HasColumnName("genre_ID");
            entity.Property(e => e.Images)
                .HasMaxLength(100)
                .HasColumnName("images");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Disscount).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.DisscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_I__dissc__71D1E811");

            entity.HasOne(d => d.Genre).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product_I__genre__70DDC3D8");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ChildTable_ParentTable1");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PK__Province__08DFB2C7D1060DE0");

            entity.ToTable("Province");

            entity.Property(e => e.ProvinceId)
                .ValueGeneratedNever()
                .HasColumnName("province_ID");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(30)
                .HasColumnName("province_name");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        modelBuilder.Entity<ReplyFeedback>(entity =>
        {
            entity.HasKey(e => e.ReplyFeedbackId).HasName("PK__Reply_fe__F015072FA6A5D36E");

            entity.ToTable("Reply_feedback");

            entity.Property(e => e.ReplyFeedbackId)
                .ValueGeneratedNever()
                .HasColumnName("reply_feedback_ID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(20)
                .HasColumnName("account_ID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.FeedbackId).HasColumnName("feedback_ID");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithMany(p => p.ReplyFeedbacks)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_account_address_id12");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.WardId).HasName("PK__Ward__396CB5D574BC5078");

            entity.ToTable("Ward");

            entity.Property(e => e.WardId)
                .ValueGeneratedNever()
                .HasColumnName("ward_ID");
            entity.Property(e => e.DistrictId).HasColumnName("district_ID");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.WardName)
                .HasMaxLength(30)
                .HasColumnName("ward_name");

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ward__district_I__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
