using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BootstrapWebApplication.Admin.Conversion.ClassConverters
{
    public class ChangeEventItemHelper
    {
        /*trans_tr (p222) => trip
        trans_tr (p242) => tripline
        trans_tr (p243) => ?
        Id col: TR_NR => TripNumber
        Fields: */
        public static void GetTRANS_TRTableColumnNames(int? input, out string tableName, out string columnName)
        {
            switch (input)
            {
                case 2:
                    tableName = "";
                    columnName = "";
                    break;
                case 3:
                    tableName = "";
                    columnName = "";
                    break;
                case 4:
                    tableName = "";
                    columnName = "";
                    break;
                default:
                    tableName = "";
                    columnName = "";
                    break;
            }
        }

        /*trans_r (p300) => ShippingCompany 
        Id col: R_NR => Code
        Fields: (1=Code, 2=TLF => Phone.RawNumber, 3=Name, 4=ContactCompanyName, 
        5=ContactPersonName, 6=ADR1 => Address.AddressLine1, 7=PostalCode, 
        8=FAX (not used), 9=(no change), 10=PREFIX)*/
        public static void GetShippingCompanyTableColumnNames(int? input, out string tableName, out string columnName)
        {
            switch (input)
            {
                case 1:
                    tableName = "ShippingCompany";
                    columnName = "Code";
                    break;
                case 2:
                    tableName = "Phone";
                    columnName = "RawNumber";
                    break;
                case 3:
                    tableName = "ShippingCompany";
                    columnName = "Name";
                    break;
                case 4:
                    tableName = "ShippingCompany";
                    columnName = "ContactCompanyName";
                    break;
                case 5:
                    tableName = "ShippingCompany";
                    columnName = "ContactPersonName";
                    break;
                case 6:
                    tableName = "Address";
                    columnName = "AddressLine1";
                    break;
                case 7:
                    tableName = "Address";
                    columnName = "PostalCode";
                    break;
                case 8: // FAX (not used anymore - excluded)
                    tableName = "";
                    columnName = "";
                    break;
                case 9: // 0 -> 0 (1 item) no change
                    tableName = "";
                    columnName = "";
                    break;
                case 10:
                    tableName = "ShippingCompany";
                    columnName = "PREFIX";
                    break;
                default:
                    tableName = "";
                    columnName = "";
                    break;
            }
        }

        /*trans_s (P200) => ship
        Id col: ID => Code
        Fields: 1=Code, 2= ?, 3=Name, 4=ContactPersonName, 5=?, 6=Address.AddressLine1, 7=PostalCode, 8=Status, 9=ShipType.Code, 10=(date?), 14=EmployerNumber */
        public static void GetShipTableColumnNames(int? input, out string tableName, out string columnName)
        {
            switch (input)
            {
                case 1:
                    tableName = "Ship";
                    columnName = "Code";
                    break;
                case 2:
                    tableName = "Phone";
                    columnName = "RawNumber";
                    break;
                case 3:
                    tableName = "Ship";
                    columnName = "Name";
                    break;
                case 4:
                    tableName = "Ship";
                    columnName = "ContactPersonName";
                    break;
                case 5: // same as #14
                    tableName = "Ship";
                    columnName = "EmployerNumber";
                    break;
                case 6:
                    tableName = "Address";
                    columnName = "AddressLine1";
                    break;
                case 7:
                    tableName = "Address";
                    columnName = "PostalCode";
                    break;
                case 8:
                    tableName = "Ship";
                    columnName = "Status";
                    break;
                case 9:
                    tableName = "Ship";
                    columnName = "ShipTypeId";
                    break;
                case 10: // 1 date change from 01-01-1991 to 01-01-1992 ship code FD000 (non-existing)
                    tableName = "";
                    columnName = "";
                    break;
                case 14: // same as #5
                    tableName = "Ship";
                    columnName = "EmployerNumber";
                    break;
                default:
                    tableName = "";
                    columnName = "";
                    break;
            }
        }

        public static void GetCompanyTableColumnNames(int? input, out string tableName, out string columnName)
        {
            switch (input)
            {
                case 1:
                    tableName = "Company";
                    columnName = "Code";
                    break;
                case 2:
                    tableName = "Phone";
                    columnName = "RawNumber";
                    break;
                case 3:
                    tableName = "Company";
                    columnName = "Name";
                    break;
                case 7:
                    tableName = ""; // 100 -> 0 (1 item)
                    columnName = "";
                    break;
                case 8:// FAX (not used anymore - excluded)
                    tableName = "";
                    columnName = "";
                    break;
                default:
                    tableName = "";
                    columnName = "";
                    break;
            }
        }

        /*trans_p (FL00) => person (when updating using e-trans)
        trans_p (AJF0) => ?person?
        trans_p (AJFS) => ?person?
        trans_p (P100) => person
        trans_p (P245) => ship>trip>signon*/
        public static void GetMemberTableColumnNames(int? input, out string tableName, out string columnName)
        {
            switch (input)
            {
                case 1:
                    tableName = "Person";
                    columnName = "Birthday";
                    break;
                case 3:
                    tableName = "Phone";
                    columnName = "RawNumber";
                    break;
                case 4:
                    tableName = "Person";
                    columnName = "FirstName";
                    break;
                case 5:
                    tableName = "Person";
                    columnName = "MiddleName";
                    break;
                case 6:
                    tableName = "Person";
                    columnName = "LastName";
                    break;
                case 7:
                    tableName = "Address";
                    columnName = "AddressLine1";
                    break;
                case 8:
                    tableName = "Address";
                    columnName = "PostalCode";
                    break;
                case 9:
                    tableName = "Member";
                    columnName = "MemberType";
                    break;
                case 10:
                    tableName = "Member";
                    columnName = "JobCode";
                    break;
                case 11:
                    tableName = "Person";
                    columnName = "Gender";
                    break;
                default:
                    tableName = "";
                    columnName = "";
                    break;
            }
        }
    }
}
