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
    public class UpdateShip
    {
        public UpdateShip(BootstrapContext db, P200 o, ConverterData data)
        {
            var ship = db.Ships.Where(x=>x.Code == o.ShipCode).FirstOrDefault();
            var shipType = db.ShipTypes.Where(t => t.Code == o.TYPA).FirstOrDefault();
            if(ship != null && shipType != null)
            {
                ship.ShipType = shipType;
                ship.ShipTypeId = shipType.Id;
            }
        }
    }

    public class P200ToShip
    {
        public P200ToShip(BootstrapContext db, P200 o, ConverterData data)
        {
            var dateModified = Helper.ParseDate(o.RET_DATO, o.RET_HH, o.RET_MM);
            var dateCreated = Helper.ParseDate(o.OPR_DATO, o.OPR_HH, o.OPR_MM);
            var updatedById = o.RET_ID;
            var createdById = o.OPR_ID;
            var shipChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            var addressChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            var phoneChangeEvent = data.ChangeEventHandler.Create(db, dateCreated, dateModified, null, createdById, updatedById, null);
            
            var shippingCompany = db.ShippingCompanies.Where(sc => sc.Code == o.REDERI.Value).FirstOrDefault();
            var shipType = db.ShipTypes.Where(t => t.Code == o.TYPA).FirstOrDefault();
            var postal = db.Postals.Where(p => p.Code == o.POSTNR.Value).FirstOrDefault();
            var country = db.Countries.Where(c => c.Code.ToLower() == "fo").FirstOrDefault();

            var entity = new Entity();
            db.Entities.Add(entity);

            var address = new Address()
            {
                IsPrimary = true,
                IsActive = true,
                AddressLine1 = o.ADR1,
                AddressLine2 = "",
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

            var ship = new Ship()
            {
                CountryCode = country != null ? country.Code : "",
                CountryName = country != null ? country.Name : "",
                AddressLine = address != null ? address.AddressLine1 + " " + address.AddressLine2 : "",
                PostalCode = postal != null ? new Nullable<int>(postal.Code) : null,
                CityName = postal != null ? postal.CityName : "",
                PhoneCountryCode = phone != null ? new Nullable<int>(phone.CountryCode) : null,
                PhoneNumber = phone != null ? phone.Number : "",

                ChangeEvent = shipChangeEvent,
                Code = o.ShipCode,
                Name = o.NAVN,
                ContactCompanyName = o.KONTAKT,
                ContactPersonName = o.KONTAKT2,
                IND_REG = o.IND_REG,
                UD_REG = o.UD_REG,
                Status = o.STAT,
                //LN_IALT = o.LN_IALT,
                SK_TYPA = o.SK_TYPA,
                ShippingCompany = shippingCompany,
                Tonnage = o.TONNAGE,
                HK = o.HK,
                //TXT_LN = o.TXT_LN,
                //BURT_DG = o.BURT_DG,
                //TUR_IALT = o.TUR_IALT,
                //LAST_DATO = o.LAST_DATO,
                ShipType = shipType,
                KG_IALT = o.KG_IALT,
                //AV_IALT = o.AV_IALT,
                //DG_IALT = o.DG_IALT,
                ALT_ID = o.ALT_ID,
                Group = o.GRUPPE,
                ETI_DATO = Helper.ParseDate(o.ETI_DATO, o.ETI_HH, o.ETI_MM, "0"),
                ETI_ID = o.ETI_ID,
                EmployerNumber = o.ARB_NR,
                Entity = entity
            };
            db.Ships.Add(ship);
            shippingCompany.Ships.Add(ship);

        }
    }
}