using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class MemberHolidayPayOverviewViewModel
    {
        [DataMember]
        public Member Member { get; set; }

        [DataMember]
        public ICollection<MemberHolidayPayYearTotal> MemberHolidayPayYearTotals { get; set; }

        [DataMember]
        public decimal SumTotal
        {
            get
            {
                //return MemberHolidayPayYearTotals.Sum(x => x.Sum && x.Sum != 0m).Value;
                //var totalSum = 0m;
                return MemberHolidayPayYearTotals.Sum(x =>
                {
                    if (x.Sum.HasValue && x.Sum.Value > 0m)
                        return x.Sum.Value;
                    return 0m;
                });
                //MemberHolidayPayYearTotals.ToList().ForEach(x =>
                //{
                //    if (x.Sum.HasValue && x.Sum.Value > 0m)
                //    {
                //        totalSum += x.Sum.Value;
                //    }
                //});
                //return totalSum;
            }
        }

        [DataMember]
        public decimal SumAverage
        {
            get
            {
                return MemberHolidayPayYearTotals.Average(x =>
                {
                    if (x.Sum.HasValue && x.Sum.Value > 0m)
                        return x.Sum.Value;
                    return 0m;
                });
            }
        }

        public MemberHolidayPayOverviewViewModel(Member member)
        {
            this.Member = member;
            MemberHolidayPayYearTotals = new List<MemberHolidayPayYearTotal>();
        }
    }

    public class MemberHolidayPayYearTotal
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public DateTime? LastDate { get; set; }
        public decimal? Sum { get; set; }

        public int Count { get; set; }
    }
}