using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SIM_Application.Models;

public partial class StockInventoryManagementSystemContext : DbContext
{
    public StockInventoryManagementSystemContext()
    {
    }

    public StockInventoryManagementSystemContext(DbContextOptions<StockInventoryManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<BrokerDetail> BrokerDetails { get; set; }

    public virtual DbSet<CityTable> CityTables { get; set; }

    public virtual DbSet<CountryTable> CountryTables { get; set; }

    public virtual DbSet<GenderTable> GenderTables { get; set; }

    public virtual DbSet<InvestmentTypeTable> InvestmentTypeTables { get; set; }

    public virtual DbSet<MaritalStatusTable> MaritalStatusTables { get; set; }

    public virtual DbSet<NationalityTable> NationalityTables { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<RoleTable> RoleTables { get; set; }

    public virtual DbSet<StateTable> StateTables { get; set; }

    public virtual DbSet<StockDetail> StockDetails { get; set; }

    public virtual DbSet<StockExchangeTable> StockExchangeTables { get; set; }

    public virtual DbSet<StockProvider> StockProviders { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    public virtual DbSet<UserStockDetail> UserStockDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-UL6LIPC1\\SQLEXPRESS;Database=Stock_Inventory_Management_System;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Admin");

            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(445)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.HasKey(e => e.BankDetailsId).HasName("PK__Bank_Det__AE325372C1D74EE2");

            entity.ToTable("Bank_Details");

            entity.Property(e => e.BankDetailsId).HasColumnName("Bank_Details_Id");
            entity.Property(e => e.AccountBalance)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("Account_Balance");
            entity.Property(e => e.AccountNumber)
                .HasColumnType("decimal(15, 0)")
                .HasColumnName("Account_Number");
            entity.Property(e => e.BankName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Bank_Name");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.Ifsc)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("IFSC");
            entity.Property(e => e.Mcir)
                .HasColumnType("decimal(15, 0)")
                .HasColumnName("MCIR");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
        });

        modelBuilder.Entity<BrokerDetail>(entity =>
        {
            entity.HasKey(e => e.BrokerId).HasName("PK__Broker_D__999DF1DBD80CD929");

            entity.ToTable("Broker_Details");

            entity.Property(e => e.BrokerId).HasColumnName("Broker_Id");
            entity.Property(e => e.BrokerName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Broker_Name");
            entity.Property(e => e.CommissionPercentage)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("Commission_Percentage");
        });

        modelBuilder.Entity<CityTable>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City_Tab__DE9DE000A95AB847");

            entity.ToTable("City_Table");

            entity.Property(e => e.CityId).HasColumnName("City_Id");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CountryTable>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country___8036CBAE4FD99C82");

            entity.ToTable("Country_Table");

            entity.Property(e => e.CountryId).HasColumnName("Country_Id");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GenderTable>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__Gender_t__AF750E44853CEA35");

            entity.ToTable("Gender_table");

            entity.Property(e => e.GenderId).HasColumnName("Gender_Id");
            entity.Property(e => e.Gender)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InvestmentTypeTable>(entity =>
        {
            entity.HasKey(e => e.InvestmentTypeId).HasName("PK__Investme__D52DA4C699DEEF00");

            entity.ToTable("Investment_Type_table");

            entity.Property(e => e.InvestmentTypeId).HasColumnName("Investment_Type_Id");
            entity.Property(e => e.InvestmentType)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Investment_Type");
        });

        modelBuilder.Entity<MaritalStatusTable>(entity =>
        {
            entity.HasKey(e => e.MaritalStatusId).HasName("PK__Marital___6BE37BE3DB781311");

            entity.ToTable("Marital_Status_table");

            entity.Property(e => e.MaritalStatusId).HasColumnName("Marital_Status_Id");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Marital_Status");
        });

        modelBuilder.Entity<NationalityTable>(entity =>
        {
            entity.HasKey(e => e.NationalityId).HasName("PK__National__C8E3386550C5C174");

            entity.ToTable("Nationality_table");

            entity.Property(e => e.NationalityId).HasColumnName("Nationality_Id");
            entity.Property(e => e.Nationality)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order_De__F1E4607B6F333C00");

            entity.ToTable("Order_Details");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.BarCode)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Bar_Code");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            entity.Property(e => e.InvestmentTypeId).HasColumnName("Investment_Type_Id");
            entity.Property(e => e.OrderDate).HasColumnName("Order_Date");
            entity.Property(e => e.PurchasedQuantity).HasColumnName("Purchased_Quantity");
            entity.Property(e => e.TotalInvestment)
                .HasColumnType("numeric(7, 2)")
                .HasColumnName("Total_Investment");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_At");
            entity.Property(e => e.UserStockId).HasColumnName("User_Stock_Id");

            entity.HasOne(d => d.InvestmentType).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.InvestmentTypeId)
                .HasConstraintName("FK__Order_Det__Inves__2180FB33");

            entity.HasOne(d => d.UserStock).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.UserStockId)
                .HasConstraintName("FK__Order_Det__User___22751F6C");
        });

        modelBuilder.Entity<RoleTable>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role_tab__D80AB4BB3E7FDEA3");

            entity.ToTable("Role_table");

            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StateTable>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__State_Ta__AF9338F7120AAF54");

            entity.ToTable("State_Table");

            entity.Property(e => e.StateId).HasColumnName("State_Id");
            entity.Property(e => e.State)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StockDetail>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__Stock_De__EFA64E98AEF7FB08");

            entity.ToTable("Stock_Details");

            entity.Property(e => e.StockId).HasColumnName("Stock_Id");
            entity.Property(e => e.BrokerId).HasColumnName("Broker_id");
            entity.Property(e => e.ProviderId).HasColumnName("Provider_id");

            entity.HasOne(d => d.Broker).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.BrokerId)
                .HasConstraintName("FK__Stock_Det__Broke__6FE99F9F");

            entity.HasOne(d => d.Provider).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("FK__Stock_Det__Provi__6EF57B66");
        });

        modelBuilder.Entity<StockExchangeTable>(entity =>
        {
            entity.HasKey(e => e.StockExchangeId).HasName("PK__Stock_Ex__F86FADC33F8117BC");

            entity.ToTable("Stock_Exchange_table");

            entity.Property(e => e.StockExchangeId).HasColumnName("Stock_Exchange_Id");
            entity.Property(e => e.StockExchange)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Stock_Exchange");
        });

        modelBuilder.Entity<StockProvider>(entity =>
        {
            entity.HasKey(e => e.ProviderId).HasName("PK__Stock_Pr__6543BA9448765ACA");

            entity.ToTable("Stock_Provider");

            entity.Property(e => e.ProviderId).HasColumnName("Provider_Id");
            entity.Property(e => e.AvailableStocksQuantity).HasColumnName("Available_Stocks_Quantity");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_At");
            entity.Property(e => e.ExpenseRatio)
                .HasColumnType("numeric(4, 2)")
                .HasColumnName("Expense_Ratio");
            entity.Property(e => e.PerStockPrice)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("Per_Stock_Price");
            entity.Property(e => e.ProviderName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Provider_Name");
            entity.Property(e => e.StockExchangeId).HasColumnName("Stock_Exchange_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_At");

            entity.HasOne(d => d.StockExchange).WithMany(p => p.StockProviders)
                .HasForeignKey(d => d.StockExchangeId)
                .HasConstraintName("FK__Stock_Pro__Stock__68487DD7");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User_Det__206D9170726AEC1D");

            entity.ToTable("User_Details");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(125)
                .IsUnicode(false)
                .HasColumnName("Address_Line1");
            entity.Property(e => e.AnnualIncome)
                .HasColumnType("numeric(10, 2)")
                .HasColumnName("Annual_Income");
            entity.Property(e => e.BalanceAmount)
                .HasColumnType("numeric(8, 2)")
                .HasColumnName("Balance_Amount");
            entity.Property(e => e.BankDetailsId).HasColumnName("Bank_Details_Id");
            entity.Property(e => e.CityId).HasColumnName("City_Id");
            entity.Property(e => e.CountryId).HasColumnName("Country_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.DateOfBirth).HasColumnName("Date_Of_Birth");
            entity.Property(e => e.Email)
                .HasMaxLength(35)
                .IsUnicode(false);
            entity.Property(e => e.GenderId).HasColumnName("Gender_Id");
            entity.Property(e => e.MaritalStatusId).HasColumnName("Marital_Status_Id");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Mobile_Number");
            entity.Property(e => e.NationalityId).HasColumnName("Nationality_Id");
            entity.Property(e => e.Occupation)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.StateId).HasColumnName("State_Id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Updated_At");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.BankDetails).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.BankDetailsId)
                .HasConstraintName("FK__User_Deta__Bank___619B8048");

            entity.HasOne(d => d.City).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__User_Deta__City___5EBF139D");

            entity.HasOne(d => d.Country).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__User_Deta__Count__60A75C0F");

            entity.HasOne(d => d.Gender).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.GenderId)
                .HasConstraintName("FK__User_Deta__Gende__5CD6CB2B");

            entity.HasOne(d => d.MaritalStatus).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.MaritalStatusId)
                .HasConstraintName("FK__User_Deta__Marit__5DCAEF64");

            entity.HasOne(d => d.Nationality).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.NationalityId)
                .HasConstraintName("FK__User_Deta__Natio__5BE2A6F2");

            entity.HasOne(d => d.Role).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__User_Deta__Role___5AEE82B9");

            entity.HasOne(d => d.State).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__User_Deta__State__5FB337D6");
        });

        modelBuilder.Entity<UserStockDetail>(entity =>
        {
            entity.HasKey(e => e.UserStockId).HasName("PK__User_Sto__0D6D331EBFFD3702");

            entity.ToTable("User_Stock_Details");

            entity.Property(e => e.UserStockId).HasColumnName("User_Stock_Id");
            entity.Property(e => e.StockId).HasColumnName("Stock_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Stock).WithMany(p => p.UserStockDetails)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__User_Stoc__Stock__73BA3083");

            entity.HasOne(d => d.User).WithMany(p => p.UserStockDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__User_Stoc__User___72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
