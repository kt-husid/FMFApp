using BootstrapWebApplication.DAL;
using BootstrapWebApplication.Models;
using Kthusid;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace BootstrapWebApplication.BLL
{
    public class MemberHandler
    {
        public Member Member { get; set; }

        public MemberHandler(Member member)
        {
            this.Member = member;
        }

        public MemberHandler() : this(null) { }

        public static decimal GetHolidayPaySum(IList<HolidayPay_HOVD> list)
        {
            var sum = 0m;
            var lastAmount = 0m;
            foreach (var x in list)
            {
                var subtractAmount = 0m;
                if (x.Amount.HasValue)
                {
                    lastAmount = x.Amount.Value;
                    //todo: why is KONTO = 0, REG_NR = 0 and PLUS = - (when ART == 'R') ???
                    if (x.ART == "R")
                    {
                        subtractAmount = x.Amount.Value + lastAmount;
                    }
                    sum += x.Amount.Value - subtractAmount;
                }
            }
            return sum;
        }

        public MemberHolidayPayYearTotal GetHolidayPaySumForYear(int year)
        {
            MemberHolidayPayYearTotal result = null;
            using (var db = new BootstrapContext())
            {
                // HolidayPay Period: 1 april - 31 march
                DateTime from = new DateTime(year, 4, 1);
                DateTime to = new DateTime(year + 1, 3, 31);
                var holidayPays = db.HolidayPay_HOVDs.Where(x => x.ChangeEvent != null && !x.ChangeEvent.DeletedOn.HasValue && x.MemberId == Member.Id);
                holidayPays = holidayPays.Where(x => x.TransferDate >= from && x.TransferDate <= to);
                var sum = GetHolidayPaySum(holidayPays.ToList());
                //var sum = 0m;
                //var lastAmount = 0m;
                //holidayPays.ToList().ForEach(x =>
                //{
                //    var subtractAmount = 0m;
                //    if (x.Amount.HasValue)
                //    {
                //        lastAmount = x.Amount.Value;
                //        if (x.ART == "R")
                //        {
                //            subtractAmount = x.Amount.Value + lastAmount;
                //        }
                //        sum += x.Amount.Value - subtractAmount;
                //    }
                //});
                var count = holidayPays.Count();
                var date = holidayPays.Max(x => x.TransferDate);
                result = new MemberHolidayPayYearTotal()
                {
                    Id = year,
                    Year = year,
                    LastDate = date,
                    Sum = sum,
                    Count = count
                };
                db.Dispose();
            }
            return result;
        }

        public MemberTripInfoForYearTotal GetTripInfoForYear(int year)
        {
            MemberTripInfoForYearTotal tripInfo = null;
            using (var db = new BootstrapContext())
            {
                tripInfo = new MemberTripInfoForYearTotal()
                {
                    Id = year,
                    Year = year,
                    LastDate = null,
                    ShipCode = "",
                    Trips = 0,
                    Days = 0m
                };
                DateTime from = new DateTime(year - 1, 12, 26);
                DateTime to = new DateTime(year, 12, 31);
                var member = db.Members.Where(x => x.Id == Member.Id).FirstOrDefault();
                var signOns = member.SignOns.FilterDeleted().Where(x => x.From >= from && x.To <= to);
                SignOn lastSignOn = null;
                signOns.ToList().ForEach(x =>
                {
                    if (lastSignOn == null || x.From.Value >= lastSignOn.From.Value)
                    {
                        lastSignOn = x;
                        tripInfo.Trips++;
                    }
                    var signOnDayCount = (x.To.Value - x.From.Value).TotalDays + 1;
                    if (x.To.Value.Year == year)
                    {
                        tripInfo.Days += (decimal)(signOnDayCount);
                    }
                    //tripInfo.Days += (x.To.Value - x.From.Value).Days + 1;
                    //if (lastSignOn == null || x.From.Value >= lastSignOn.From.Value)
                    //{
                    //    lastSignOn = x;
                    //}
                });
                tripInfo.LastDate = lastSignOn != null ? lastSignOn.From : null;
                tripInfo.ShipCode = lastSignOn != null ? lastSignOn.ShipCode : null;
                //tripInfo.Trips = signOns.Count();
                db.Dispose();
            }

            //Member.SignOns.FilterDeleted().ToList().ForEach(x =>
            //{
            //    if (lastSignOn.SignOn == null || x.From.Value >= lastSignOn.SignOn.From.Value)
            //    {
            //        //double tripDayCount = 0;
            //        if (x.Trip != null && x.To.Value.Year == year)
            //        {
            //            lastSignOn.TripsThisYear++;
            //            //tripDayCount = (x.Trip.To.Value - x.Trip.From.Value).TotalDays + 1;
            //        }
            //        var signOnDayCount = (x.To.Value - x.From.Value).TotalDays + 1;
            //        if (x.To.Value.Year == year)
            //            lastSignOn.DaysThisYear += (decimal)(signOnDayCount);// + signOnDayCount > 0 ? 1 : 0);
            //        //if (x.To.Value.Year == year && tripDayCount > 0)
            //        //    lastSignOn.TripsThisYear += (decimal)signOnDayCount / (decimal)(tripDayCount);// + 1);
            //        lastSignOn.SignOn = x;
            //    }
            //});

            return tripInfo;
        }

        public string PrintSignOns(string _fontFamily, int _fontSize, string _fontStyle)
        {
            return null;
        }

        public string PrintEnvelope(PrintPageSettings pageSettings, ApplicationUser user)
        {
            var result = "-1";
            try
            {
                using (var db = new BootstrapContext())
                {
                    pageSettings.Data = GetEnvelopeText();
                    var client = new RestClient(user != null ? user.AppSettings.PrintServerUrl : null);
                    var request = new RestRequest("api/print/Print/{pageSettings}", Method.POST);
                    //request.AddParameter("text", GetEnvelopeText());
                    request.AddObject(pageSettings);//, ParameterType.UrlSegment);
                    var response = client.Execute(request);
                    result = response.Content;
                    db.Dispose();
                }
            }
            catch (Exception) { }
            return result;
        }

        private string GetEnvelopeText()
        {
            var sb = new StringBuilder();
            if (Member.Person != null)
            {
                sb.AppendLine(Member.Person.FullName);
                var address = Member.PrimaryAddress; //Member.Person.Entity.PrimaryAddress;
                if (address != null)
                {
                    sb.AppendLine(address.AddressLine1);
                    if (address.Postal != null)
                    {
                        sb.AppendLine(address.Postal.Code + " " + address.Postal.CityName);
                    }
                }
            }
            return sb.ToString();
        }

        public MemberLastPaymentViewModel GetLastPayment()
        {
            var lastPayment = Member.Payments.LastOrDefault();
            return new MemberLastPaymentViewModel()
            {
                BankAccount = lastPayment != null ? lastPayment.BankAccount : null,
                HolidayPay = lastPayment != null ? lastPayment.HolidayPay : null,
                MembershipPayment = lastPayment != null ? lastPayment.MembershipPayment : null,
                Price = lastPayment != null ? lastPayment.Price : null,
            };
        }

        public MemberStatisticsViewModel GetStatistics(int year)
        {
            var paymentsFiltered = Member.Payments.FilterDeleted().Where(x => x.PaidOn.HasValue && x.PaidOn.Value.Year == year);
            var signOnsFiltered = Member.SignOns.FilterDeleted().Where(x => x.From.Value.Year == year && x.To.Value.Year == year);
            var lastSignOn = new MemberStatisticsViewModel()
            {
                DaysThisYear = 0,
                TripsThisYear = 0,
                PaymentsYear = paymentsFiltered.Select(x => x.PaidOn).FirstOrDefault(),
                PaymentsSum = paymentsFiltered.Select(x => x.Price).Sum().Value,
                PaymentsTotal = Member.Payments.Count,
                SignOnsThisYear = signOnsFiltered.Count()
            };

            Member.SignOns.FilterDeleted().ToList().ForEach(x =>
            {
                if (lastSignOn.SignOn == null || x.From.Value >= lastSignOn.SignOn.From.Value)
                {
                    //double tripDayCount = 0;
                    if (x.Trip != null && x.To.Value.Year == year)
                    {
                        lastSignOn.TripsThisYear++;
                        //tripDayCount = (x.Trip.To.Value - x.Trip.From.Value).TotalDays + 1;
                    }
                    var signOnDayCount = (x.To.Value - x.From.Value).TotalDays + 1;
                    if (x.To.Value.Year == year)
                        lastSignOn.DaysThisYear += (decimal)(signOnDayCount);// + signOnDayCount > 0 ? 1 : 0);
                    //if (x.To.Value.Year == year && tripDayCount > 0)
                    //    lastSignOn.TripsThisYear += (decimal)signOnDayCount / (decimal)(tripDayCount);// + 1);
                    lastSignOn.SignOn = x;
                }
            });
            return lastSignOn;
        }

        public IQueryable<Member> GetMembersFiltered(IQueryable<Member> list, string filter, int minItems)
        {
            var membersFiltered = list as IQueryable<Member>;
            if (filter.Length >= minItems)
            {
                DateTime dt = DateTime.Now;
                if (!DateTime.TryParseExact(filter, "dMyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    if (!DateTime.TryParseExact(filter, "dMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    {
                        DateTime.TryParseExact(filter, "d-M-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                    }
                }
                membersFiltered = membersFiltered.Where(s =>
                                        s.ChangeEvent != null && 
                                        s.ChangeEvent.DeletedOn == null &&
                                        s.Person != null &&
                                        (s.Person.Birthday.Value.Equals(dt)
                                        || ("" + s.NR).ToLower().Contains(filter.ToLower())
                                       || s.Person.SSN.ToLower().Contains(filter.ToLower())
                                       || s.Person.FirstName.ToLower().Contains(filter.ToLower())
                                       || s.Person.MiddleName.ToLower().Contains(filter.ToLower())
                                       || s.Person.LastName.ToLower().Contains(filter.ToLower())
                                       || (s.Person.FirstName + " " + s.Person.LastName).ToLower().Contains(filter.ToLower())
                                       || (s.Person.FirstName + (s.Person.MiddleName.Length > 0 ? " " + s.Person.MiddleName : "") + " " + s.Person.LastName).ToLower().Contains(filter.ToLower()))
                                       || s.PhoneNumber.ToLower().Equals(filter.ToLower())
                );
            }
            return membersFiltered;
        }

        public Member CreateFromViewModel(MemberCreateViewModel o, BootstrapContext db, ChangeEvent[] changeEvents)
        {
            //Convert the ViewModel to DB Object (Model)

            var entity = new Entity();
            db.Entities.Add(entity);

            var phone = PhoneHandler.Instance.Parse(Convert.ToString(o.PhoneNumber));
            if (phone != null)
            {
                phone.IsPrimary = true;
                phone.Entity = entity;
                phone.ChangeEvent = changeEvents[0];
                //phone.Person = person;
                db.Phones.Add(phone);
                //person.Phones.Add(phone);
                entity.Phones.Add(phone);
            }
            //var postal = db.Postals.Where(p => p.Id == (o.Postal != null ? o.Postal.Id : o.PostalId)).FirstOrDefault();
            Postal postal = null;
            if (o.Postal != null)
            {
                postal = db.Postals.FilterDeleted().Where(x => x.Id == o.Postal.Id).FirstOrDefault();
            }
            else
            {
                postal = db.Postals.FilterDeleted().Where(x => x.Id == o.PostalId).FirstOrDefault();
            }
            if (postal == null)
            {
                postal = db.Postals.FilterDeleted().Where(x => x.Code == 100).FirstOrDefault();
            }
            Country country = null;
            if (o.Country != null)
            {
                country = db.Countries.FilterDeleted().Where(x => x.Id == o.Country.Id).FirstOrDefault();
            }
            else
            {
                country = db.Countries.FilterDeleted().Where(x => x.Id == o.CountryId).FirstOrDefault();
            }
            if (country == null)
            {
                country = db.Countries.FilterDeleted().Where(x => ("" + x.Code).ToLower().Equals("fo")).FirstOrDefault();
            }
            var address = new Address()
            {
                IsPrimary = true,
                IsActive = true,
                AddressLine1 = o.AddressLine1,
                AddressLine2 = o.AddressLine2,
                Postal = postal,
                //PostalId = o.Postal != null ? o.Postal.Id : db.Postals.Where(x => x.Code == 100).FirstOrDefault().Id,
                Country = country,
                //CountryId = o.Country != null ? o.Country.Id : db.Countries.Where(x => x.Code.ToLower().Equals("fo")).FirstOrDefault().Id,
                Entity = entity
            };
            db.Addresses.Add(address);
            entity.Addresses.Add(address);

            var bankAccount = new BankAccount()
            {
                AccountNumber = o.AccountNumber.HasValue ? o.AccountNumber.Value : -1,
                BankId = o.BankId,
                IsPrimary = true
            };
            db.BankAccounts.Add(bankAccount);
            entity.BankAccounts.Add(bankAccount);

            var gender = o.Gender != null ? o.Gender : db.PersonGenders.FilterDeleted().Where(x => x.Id == o.GenderId).FirstOrDefault();
            gender = gender != null ? gender : db.PersonGenders.FilterDeleted().FirstOrDefault();

            var personTitle = db.PersonTitles.FilterDeleted().Where(t => t.Code.ToLower() == "u").FirstOrDefault();
            if (gender.Code.ToLower() == "m")
            {
                personTitle = db.PersonTitles.FilterDeleted().Where(t => t.Code.ToLower() == "mr").FirstOrDefault();
            }
            else if (gender.Code.ToLower() == "f")
            {
                personTitle = db.PersonTitles.FilterDeleted().Where(t => t.Code.ToLower() == "mrs").FirstOrDefault();
            }

            DateTime birthday = DateTime.Now;
            DateTime.TryParseExact(o.Birthday.HasValue ? o.Birthday.Value.ToString("d") : "01-01-0001", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out birthday);
            if (birthday.Year < 1850)
            {
                birthday = new DateTime(1900, birthday.Month, birthday.Day);
            }

            var person = new Person()
            {
                IsAlive = true,
                ChangeEvent = changeEvents[1],
                SSN = o.SSN,
                FirstName = o.FirstName,
                MiddleName = o.MiddleName,
                LastName = o.LastName,
                Birthday = new Nullable<DateTime>(birthday),
                Gender = gender,
                Title = personTitle,
                Entity = entity
            };
            person.FullName = String.IsNullOrEmpty(("" + person.MiddleName).Trim()) ? (person.FirstName + " " + person.LastName) : (person.FirstName + " " + person.MiddleName + " " + person.LastName);
            db.Persons.Add(person);

            var memberType = o.MemberType != null ? o.MemberType : db.MemberTypes.Where(t => t.Id == o.MemberTypeId).FirstOrDefault();
            memberType = memberType != null ? memberType : db.MemberTypes.Where(t => t.Code.ToLower().Equals("ff")).FirstOrDefault();

            var job = o.Job != null ? o.Job : db.Jobs.Where(j => j.Id == o.JobId).FirstOrDefault();
            job = job != null ? job : db.Jobs.Where(t => String.IsNullOrEmpty(t.Code)).FirstOrDefault();

            var lastMember = db.Members.OrderByDescending(x => x.Id).Take(1).FirstOrDefault();

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
                GenderCode = gender != null ? gender.Code : null,

                ChangeEvent = changeEvents[2],
                NR = lastMember != null ? lastMember.NR + 1 : 1000,
                Person = person,
                JobTitle = job.Code,
                //INN_IALT = o.INN_IALT,
                Job = job,
                MemberType = memberType,
                //PRI_OWN = o.PRI_OWN,
                //BETALER = o.BETALER,
                //M_STATUS = o.M_STATUS,
                ETI_DATO = null,
                ETI_ID = String.Empty
            };
            db.Members.Add(member);
            return member;
        }

        internal static Member Update(Member member, MemberEditViewModel o, BootstrapContext db)
        {
            return member;
        }
    }
}