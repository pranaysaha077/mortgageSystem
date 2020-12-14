using MortgageSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace MortgageSystem.Models
{
    public class MortgageDBContext : DbContext
    {
        public MortgageDBContext() : base("DBCon")
        { 
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Officer> Officers { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Emi> Emis { get; set; }

        public DbSet<Mortgage> Mortgages { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<CustomerLoanMortage> CustomerLoanMortages { get; set; }

        public DbSet<LoginUser> loginUsers { get; set; }

        public DbSet<ForgetUseId> ForgetUseIds { get; set; }

        public DbSet<ForgetPassword> ForgetPasswords { get; set; }

        public DbSet<ResetPassword> ResetPasswords { get; set; }
        public DbSet<LoanStatus> LoanStatuses { get; set; }
        public DbSet<CardDetails> CardDetails { get; set; }

        public DbSet<Terms> Terms { get; set; }

        public System.Data.Entity.DbSet<MortgageSystem.Models.SecurityQues> SecurityQues { get; set; }
    }
}