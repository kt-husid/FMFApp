namespace BootstrapWebApplication.Migrations
{
    using BootstrapWebApplication.BLL;
    using BootstrapWebApplication.DAL;
    using BootstrapWebApplication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BootstrapContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BootstrapContext db)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            CreatePersonTitles(db);
            CreatePersonGenders(db);
            CreateBankAccount(db);

            
            base.Seed(db);
            db.SaveChanges();
        }

        private void CreateBankAccount(BootstrapContext db)
        {
            var changeEventHandler = new ChangeEventHandler("bha");
            if (db.Banks.Where(x => x.RegNumber == 0).FirstOrDefault() == null)
            {
                var changeEvent = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEvent);

                db.Banks.Add(new Bank()
                {
                    RegNumber = 0,
                    Name = "UNKNOWN",
                    ChangeEvent = changeEvent
                });
            }
        }

        private void CreatePersonGenders(BootstrapContext db)
        {
            var changeEventHandler = new ChangeEventHandler("bha");
            if (db.PersonGenders.Count() == 0)
            {
                var changeEventMale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventFemale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventUnknown = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);
                
                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "M", // Male
                    Description = "M",
                    ChangeEvent = changeEventMale
                });
                db.PersonGenders.Add(new PersonGender()
                {
                    // Female
                    Code = "F",
                    Description = "F",
                    ChangeEvent = changeEventFemale
                });
                db.PersonGenders.Add(new PersonGender()
                {
                    Code = "U", // Unknown
                    Description = "U",
                    ChangeEvent = changeEventUnknown
                });
            }
        }

        private void CreatePersonTitles(BootstrapContext db)
        {
            var changeEventHandler = new ChangeEventHandler("bha");

            if (db.PersonTitles.Count() == 0)
            {
                var changeEventMale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventFemale = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                var changeEventUnknown = changeEventHandler.Create(db);
                db.ChangeEvents.Add(changeEventMale);

                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "mr",
                    Description = "Mr.",
                    ChangeEvent = changeEventMale
                });
                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "mrs",
                    Description = "Mrs.",
                    ChangeEvent = changeEventFemale
                });
                db.PersonTitles.Add(new PersonTitle()
                {
                    Code = "U",
                    Description = "Unknown",
                    ChangeEvent = changeEventUnknown
                });
            }
        }
    }
}
