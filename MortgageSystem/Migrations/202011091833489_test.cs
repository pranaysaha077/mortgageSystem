namespace MortgageSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CardDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CardNo = c.Long(nullable: false),
                        CardHolderName = c.String(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CVV = c.Int(nullable: false),
                        CardType = c.String(nullable: false),
                        Balance = c.Double(nullable: false),
                        CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerLoanMortages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        Contact = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        ProjectPlan = c.String(nullable: false),
                        PersonalCreditReport = c.String(nullable: false),
                        BussinessCreditReport = c.String(nullable: false),
                        FinancialStatement = c.String(nullable: false),
                        BankStatement = c.String(nullable: false),
                        LoanNumber = c.String(),
                        Amount = c.Double(nullable: false),
                        LoanTenure = c.Int(nullable: false),
                        InterestRate = c.Double(nullable: false),
                        EmiOption = c.String(nullable: false),
                        DayOfEmiPayment = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoanStatus = c.String(),
                        LoanApplyDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        MortgageItem = c.String(nullable: false),
                        ValueOfMortgage = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 18),
                        ConfirmPass = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Dob = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Email = c.String(nullable: false),
                        Contact = c.Long(nullable: false),
                        SecurityAnimal = c.String(nullable: false),
                        SecurityNumber = c.String(nullable: false),
                        SecurityBirthPlace = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Emis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmiNumber = c.Int(nullable: false),
                        MonthlyAmt = c.Double(nullable: false),
                        AmountToPaid = c.Double(nullable: false),
                        OutstandingBal = c.Double(nullable: false),
                        LDayOfPayment = c.DateTime(precision: 7, storeType: "datetime2"),
                        NDayOfPayment = c.DateTime(precision: 7, storeType: "datetime2"),
                        LoanNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForgetPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SecurityAnimal = c.String(nullable: false),
                        SecurityNumber = c.String(nullable: false),
                        SecurityBirthPlace = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForgetUseIds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmailId = c.String(nullable: false),
                        Contact = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Tenure = c.Int(nullable: false),
                        InterestRate = c.Double(nullable: false),
                        BankStatement = c.String(nullable: false),
                        FinancialStatus = c.String(nullable: false),
                        LoanStatus = c.String(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoanStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        UserType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Mortgages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MortgageItem = c.String(nullable: false),
                        MortgageValue = c.Double(nullable: false),
                        ValueType = c.String(),
                        MortgageInterest = c.Double(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        LoanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Officers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        UserId = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 18),
                        ConfirmPass = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Dob = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Pan = c.String(nullable: false),
                        Designation = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Contact = c.Long(nullable: false),
                        SecurityAnimal = c.String(nullable: false),
                        SecurityNumber = c.String(nullable: false),
                        SecurityBirthPlace = c.String(nullable: false),
                        UserType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaidAmount = c.Double(nullable: false),
                        DayOfPayemt = c.DateTime(precision: 7, storeType: "datetime2"),
                        EmiId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResetPasswords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 18),
                        ConfirmPass = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecurityQues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FQues = c.String(nullable: false),
                        SQues = c.String(nullable: false),
                        TQues = c.String(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Terms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Term = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Terms");
            DropTable("dbo.SecurityQues");
            DropTable("dbo.ResetPasswords");
            DropTable("dbo.Payments");
            DropTable("dbo.Officers");
            DropTable("dbo.Mortgages");
            DropTable("dbo.LoginUsers");
            DropTable("dbo.LoanStatus");
            DropTable("dbo.Loans");
            DropTable("dbo.ForgetUseIds");
            DropTable("dbo.ForgetPasswords");
            DropTable("dbo.Emis");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerLoanMortages");
            DropTable("dbo.CardDetails");
            DropTable("dbo.Admins");
        }
    }
}
