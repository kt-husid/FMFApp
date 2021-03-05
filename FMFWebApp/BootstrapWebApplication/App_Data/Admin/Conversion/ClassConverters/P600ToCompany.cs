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
    public class P600ToCompany
    {
        public P600ToCompany(BootstrapContext db, P600 o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            var addressChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            var phoneChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var entity = new Entity();
            db.Entities.Add(entity);

            var postal = db.Postals.Where(p => p.Code == o.POSTNR.Value).FirstOrDefault();
            var country = db.Countries.Where(c => c.Code.ToLower() == "fo").FirstOrDefault();

            var address = new Address()
            {
                IsPrimary = true,
                IsActive = true,
                AddressLine1 = o.ADR1,
                AddressLine2 = o.ADR2,
                Postal = postal,
                Country = country,
                Entity = entity,
                ChangeEvent = addressChangeEvent
            };
            if (address != null)
            {
                db.Addresses.Add(address);
                entity.Addresses.Add(address);
            }
            var phone = PhoneHandler.Instance.Parse(Convert.ToString(o.TLF.Value));
            if (phone != null)
            {
                phone.IsPrimary = true;
                phone.Entity = entity;
                phone.ChangeEvent = phoneChangeEvent;
                db.Phones.Add(phone);
                entity.Phones.Add(phone);
            }

            var company = new Company()
            {
                CountryCode = country != null ? country.Code : "",
                CountryName = country != null ? country.Name : "",
                AddressLine = address != null ? address.AddressLine1 + " " + address.AddressLine2 : "",
                PostalCode = postal != null ? new Nullable<int>(postal.Code) : null,
                CityName = postal != null ? postal.CityName : "",
                PhoneCountryCode = phone != null ? new Nullable<int>(phone.CountryCode) : null,
                PhoneNumber = phone != null ? phone.Number : "",

                ChangeEvent = changeEvent,
                Code = o.CompanyCode,
                Name = o.NAVN,
                Description = "",
                ContactCompanyName = o.KONTAKT,
                ContactPersonName = o.KONTAKT2,
                AVR_IALT = o.AVR_IALT,
                KG_IALT = o.KG_IALT,
                LAST_DATO = o.LAST_DATO,
                LAST_ID = o.LAST_ID,
                Entity = entity
            };
            db.Companies.Add(company);
        }
    }
}