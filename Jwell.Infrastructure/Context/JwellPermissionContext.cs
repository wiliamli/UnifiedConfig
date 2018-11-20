﻿using Jwell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Repository.Context
{
    public class JwellPermissionContext: DbContext
    {
        public JwellPermissionContext()
            : base("Default")
        {
            Configuration.UseDatabaseNullSemantics = true;
        }

        public JwellPermissionContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected JwellPermissionContext(DbCompiledModel model) : base(model)
        {
        }

        public IDbSet<Member> Members { get; set; }

        public IDbSet<Service> Services { get; set; }

        public IDbSet<Permission> ServiceMembers { get; set; }

        public IDbSet<Team> Teams { get; set; }

        //public IDbSet<AdminUser> AdminUser { get; set; }

        #region 配置信息
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();
            modelBuilder.Conventions.Add(new DecimalPropertyConvention(18, 4));
            modelBuilder.HasDefaultSchema("jwell");
            base.OnModelCreating(modelBuilder);
            Dasebase databaseType = GetDatabaseType();
            SetDefaultSchema(databaseType, modelBuilder);

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                //var sb = new StringBuilder();
                //foreach (var error in ex.EntityValidationErrors)
                //{
                //    foreach (var item in error.ValidationErrors)
                //    {
                //        sb.AppendLine(item.PropertyName + ": " + item.ErrorMessage);
                //    }
                //}
                throw ex;
            }
        }


        private void InitializeContext()
        {
            base.Configuration.UseDatabaseNullSemantics = true;
            base.Configuration.ValidateOnSaveEnabled = false;
        }

        protected virtual void SetDefaultSchema(Dasebase databaseType, DbModelBuilder modelBuilder)
        {
            switch (databaseType)
            {
                case Dasebase.SqlServer:
                    modelBuilder.HasDefaultSchema("dbo");
                    break;
                case Dasebase.Oracle:
                    {
                        string text = base.Database.Connection.ConnectionString.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                            .FirstOrDefault((string p) => p.Trim().StartsWith("User Id", StringComparison.CurrentCultureIgnoreCase));
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            string schema = text.ToUpper().Replace("USER ID", string.Empty).Replace("=", string.Empty)
                                .Trim();
                            modelBuilder.HasDefaultSchema(schema);
                        }
                        break;
                    }
            }
        }

        protected virtual Dasebase GetDatabaseType()
        {
            string name = base.Database.Connection.GetType().Name;
            if (!(name == "SqlConnection"))
            {
                if (name == "OracleConnection")
                {
                    return Dasebase.Oracle;
                }
            }
            return Dasebase.SqlServer;
        }
        #endregion
    }
}
