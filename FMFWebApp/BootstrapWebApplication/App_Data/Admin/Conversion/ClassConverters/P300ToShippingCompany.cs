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
    public class P300ToShippingCompany
    {
        public P300ToShippingCompany(BootstrapContext db, P300 o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var changeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            var addressChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            var phoneChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);

            var postal = db.Postals.Where(p => p.Code == o.POSTNR).FirstOrDefault();
            var country = db.Countries.Where(c => c.Code.ToLower() == "fo").FirstOrDefault();

            var entity = new Entity();
            db.Entities.Add(entity);

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

            var phone = PhoneHandler.Instance.Parse(System.Convert.ToString(o.TLF.Value));
            if (phone != null)
            {
                phone.IsPrimary = true;
                phone.Entity = entity;
                phone.ChangeEvent = phoneChangeEvent;
                db.Phones.Add(phone);
                entity.Phones.Add(phone);
            }

            var shippingCompany = new ShippingCompany()
            {
                CountryCode = country != null ? country.Code : "",
                CountryName = country != null ? country.Name : "",
                AddressLine = address != null ? address.AddressLine1 + " " + address.AddressLine2 : "",
                PostalCode = postal != null ? new Nullable<int>(postal.Code) : null,
                CityName = postal != null ? postal.CityName : "",
                PhoneCountryCode = phone != null ? new Nullable<int>(phone.CountryCode) : null,
                PhoneNumber = phone != null ? phone.Number : "",

                ChangeEvent = changeEvent,
                Code = o.Code,
                Name = o.NAVN,
                FaxNumber = o.FAX,
                ContactCompanyName = o.KONTAKT,
                ContactPersonName = o.KONTAKT2,
                KG_IALT = o.KG_IALT,
                PREFIX = o.PREFIX,
                SKIB_IALT = o.SKIB_IALT,
                ALT_ID = o.ALT_ID,
                ETI_DATO = Helper.ParseDate(o.ETI_DATO, o.ETI_HH, o.ETI_MM, "0"),
                ETI_ID = o.ETI_ID,
                EmployerNumber = o.ARB_NR.Value != 0 ? o.ARB_NR : o.Code,
                FRT_LON = o.FRT_LON,
                FRT_LON_DATO = o.FRT_LON_DATO,
                FRT_LON_NU = o.FRT_LON_NU,
                A_REG = o.A_REG,
                A_KTO = o.A_KTO,
                Entity = entity
            };
            db.ShippingCompanies.Add(shippingCompany);

            //var address = new Address()
            //{
            //    IsPrimary = true,
            //    IsActive = true,
            //    AddressLine1 = o.ADR1,
            //    AddressLine2 = o.ADR2,
            //    Postal = postal,
            //    Country = country,
            //    Entity = entity,
            //    EntityBase = shippingCompany
            //};
            //if (address != null)
            //{
            //    db.Addresses.Add(address);
            //    shippingCompany.Addresses.Add(address);
            //    //entity.Addresses.Add(address);
            //}

            //var phone = PhoneHandler.Instance.Parse(System.Convert.ToString(o.TLF.Value));
            //if (phone != null)
            //{
            //    phone.Entity = entity;
            //    phone.EntityBase = shippingCompany;
            //    db.Phones.Add(phone);
            //    shippingCompany.Phones.Add(phone);
            //    //entity.Phones.Add(phone);
            //}

           
        }
    }
}