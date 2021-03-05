using BootstrapWebApplication.BLL;
using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using BootstrapWebApplication.Models.Old;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace BootstrapWebApplication.Admin.Conversion.ClassConverters
{
    public class P100ToMember
    {
        public P100ToMember(BootstrapContext db, P100 o, ConverterData data)
        {
            #region UpdatePersonInfo
            //var person = db.Members.Where(x => x.NR == o.NR).FirstOrDefault().Person;
            //PersonGender personGender = null;
            //PersonTitle personTitle = null;
            //var gender = o.KYN.ToLower();
            //if (gender == "m")
            //{
            //    personGender = db.PersonGenders.Where(g => g.Type == "M").FirstOrDefault();// data.genderMale;
            //    personTitle = db.PersonTitles.Where(t => t.Name == "Mr.").FirstOrDefault();// data.titleMale;
            //}
            //else if (gender == "f")
            //{
            //    personGender = db.PersonGenders.Where(g => g.Type == "F").FirstOrDefault();// data.genderFemale;
            //    personTitle = db.PersonTitles.Where(t => t.Name == "Mrs.").FirstOrDefault();// data.titleFemale;
            //}
            //person.Title = personTitle;
            //person.Gender = personGender;
            #endregion

            var personDateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var personDateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var personUpdatedById = o.RET_ID;
            var personCreatedById = o.OPR_ID;
            var personChangeEvent = data.ChangeEventHandler.Create(db, personDateCreated, personDateModified, null, personCreatedById, personUpdatedById, null);

            var bankAccountChangeEvent = data.ChangeEventHandler.Create(db, personDateCreated, personDateModified, null, personCreatedById, personUpdatedById, null);
            var phoneChangeEvent = data.ChangeEventHandler.Create(db, personDateCreated, personDateModified, null, personCreatedById, personUpdatedById, null);
            var addressCangeEvent = data.ChangeEventHandler.Create(db, personDateCreated, personDateModified, null, personCreatedById, personUpdatedById, null);

            var personGender = db.PersonGenders.Where(g => g.Code == "U").FirstOrDefault();// data.genderUnknown;
            var personTitle = db.PersonTitles.Where(t => t.Code == "U").FirstOrDefault();// data.titleUnknown;
            var gender = o.KYN.ToLower();
            if (gender == "m")
            {
                personGender = db.PersonGenders.Where(g => g.Code == "M").FirstOrDefault();// data.genderMale;
                personTitle = db.PersonTitles.Where(t => t.Code == "mr").FirstOrDefault();// data.titleMale;
            }
            else if (gender == "k")
            {
                personGender = db.PersonGenders.Where(g => g.Code == "F").FirstOrDefault();// data.genderFemale;
                personTitle = db.PersonTitles.Where(t => t.Code == "mrs").FirstOrDefault();// data.titleFemale;
            }

            var entity = new Entity();
            db.Entities.Add(entity);

            var phone = PhoneHandler.Instance.Parse(Convert.ToString(o.TLF_HEIMA));
            if (phone != null)
            {
                phone.IsPrimary = true;
                phone.Entity = entity;
                phone.ChangeEvent = phoneChangeEvent;
                //phone.Person = person;
                db.Phones.Add(phone);
                //person.Phones.Add(phone);
                entity.Phones.Add(phone);
            }
            var postal = db.Postals.Where(p => p.Code.Equals(o.POSTNR.Value)).FirstOrDefault();
            var land = o.LAND.Trim().ToLower();
            if (String.IsNullOrEmpty(land))
            {
                land = "fo";
            }
            if (land.Equals("gr"))
            {
                land = "gl";
            }
            if (land.Equals("n"))
            {
                land = "no";
            }
            if (land.Equals("ul"))
            {
                land = "gb";
            }
            if (land.Equals("nk"))
            {
                land = "dk";
            }
            var country = db.Countries.Where(c => c.Code.ToLower() == land).FirstOrDefault();
            var address = new Address()
            {
                IsPrimary = true,
                IsActive = true,
                AddressLine1 = o.ADR1,
                AddressLine2 = "",//o.ADR2,
                Postal = postal,
                Country = country,
                Entity = entity,
                ChangeEvent = addressCangeEvent
            };
            db.Addresses.Add(address);
            entity.Addresses.Add(address);

            var bank = db.Banks.Where(x => x.RegNumber == o.LAST_REG).FirstOrDefault();
            var bankAccount = new BankAccount()
            {
                Bank = bank,
                AccountNumber = o.LAST_KONTO.Value,
                IsPrimary = true,
                Entity = entity,
                ChangeEvent = bankAccountChangeEvent
            };
            db.BankAccounts.Add(bankAccount);
            entity.BankAccounts.Add(bankAccount);

            var person = new Person()
            {
                ChangeEvent = personChangeEvent,
                SSN = "" + o.CPR,
                FirstName = o.FNAVN,
                MiddleName = o.MNAVN,
                LastName = o.ENAVN,
                Birthday = o.FODT,
                Gender = personGender,
                Title = personTitle,
                Entity = entity,
                IsAlive = !("" + o.STRIKA).ToLower().Equals("d") // string.IsNullOrEmpty((""+o.STRIKA).Trim())
            };
            person.FullName = String.IsNullOrEmpty(("" + person.MiddleName).Trim()) ? (person.FirstName + " " + person.LastName) : (person.FirstName + " " + person.MiddleName + " " + person.LastName);
            db.Persons.Add(person);

            var memberType = db.MemberTypes.Where(t => t.Code == o.SLAG || t.Code == "uk").FirstOrDefault();

            var memberDateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var memberDateCreated = personDateModified;// Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var memberUpdatedById = o.RET_ID;
            var memberCreatedById = personUpdatedById;//o.OPR_ID;
            var memberChangeEvent = data.ChangeEventHandler.Create(db, personDateCreated, personDateModified, null, personCreatedById, personUpdatedById, null);

            var job = db.Jobs.Where(j => j.Code.Equals(o.STARV)).FirstOrDefault();

            var member = new Member()
            {
                MemberTypeCode = memberType != null ? memberType.Code : "",
                MemberTypeDescription = memberType != null ? memberType.Description : "",
                JobCode = job != null ? job.Code : "",
                JobDescription = job != null ? job.Description : "",
                CountryCode = country != null ? country.Code : "",
                CountryName = country != null ? country.Name : "",
                AddressLine = address != null ? address.AddressLine1 + " " + address.AddressLine2 : "",
                PostalCode = postal != null ? new Nullable<int>(postal.Code) : null,
                CityName = postal != null ? postal.CityName : "",
                PhoneCountryCode = phone != null ? new Nullable<int>(phone.CountryCode) : null,
                PhoneNumber = phone != null ? phone.Number : "",


                ChangeEvent = memberChangeEvent,
                NR = o.NR.Value,
                Person = person,
                JobTitle = o.STARV,
                BURT_DG = o.BURT_DG,
                TUR_IALT = o.TUR_IALT,
                LAST_DATO = o.LAST_DATO,
                LAST_ID = o.LAST_ID,
                //INN_IALT = o.INN_IALT,
                Job = job,
                Status = ("" + o.STRIKA).ToLower(),
                MemberType = memberType,
                PRI_OWN = o.PRI_OWN,
                BETALER = o.BETALER,
                M_STATUS = o.M_STATUS,
                //todo: verify this is calculated (see  /api/Member/GetStatistics (property PaymentsTotal and SignOnsThisYear in MemberStatisticsViewModel))
                MI_TOT = o.MI_TOT,
                LG_TOT = o.LG_TOT,
                MI_AR = o.MI_AR,
                MI_ID = o.MI_ID,
                LG_AR = o.LG_AR,
                LG_KR = o.LG_KR,
                ETI_DATO = Helper.ParseDate(o.ETI_DATO, o.ETI_HH, o.ETI_MM, "0"),
                ETI_ID = o.ETI_ID
            };
            db.Members.Add(member);
        }
    }
}