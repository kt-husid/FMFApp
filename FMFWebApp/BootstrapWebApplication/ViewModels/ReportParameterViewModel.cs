using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class MemberListReportStatusCode
    {
        public enum Code { OK, FAILED }

        [DataMember]
        public Code StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class MemberListReportStatus
    {
        [DataMember]
        public int ItemsDone { get; set; }

        [DataMember]
        public int ItemsTotal { get; set; }

        [DataMember]
        public int TimeSpent { get; set; }

        [DataMember]
        public bool CanOpenReport { get; set; }
    }
    [DataContract]
    public class MemberListReportParameters
    {
        [DataMember]
        public int PostalFrom { get; set; }
        [DataMember]
        public int PostalTo { get; set; }
        [DataMember]
        public string MemberTypeCode { get; set; }
        [DataMember]
        public string MemberJobCodeFrom { get; set; }
        [DataMember]
        public string MemberJobCodeTo { get; set; }
        [DataMember]
        public string SignOnFrom { get; set; }
        [DataMember]
        public string SignOnTo { get; set; }

    }

    
    
    
    [DataContract]
    public class LabelReportStatusCode
    {
        public enum Code { OK, FAILED }

        [DataMember]
        public Code StatusCode { get; set; }

        [DataMember]
        public string Message { get; set; }
    }

    [DataContract]
    public class LabelReportStatus
    {
        [DataMember]
        public int ItemsDone { get; set; }

        [DataMember]
        public int ItemsTotal { get; set; }

        [DataMember]
        public int TimeSpent { get; set; }

        [DataMember]
        public bool CanOpenReport { get; set; }
    }



    [DataContract]
    public class MemberLabelReportParameters
    {
        [DataMember]
        public int PostalFrom { get; set; }

        [DataMember]
        public int PostalTo { get; set; }

        [DataMember]
        public string MemberTypeCode { get; set; }

        [DataMember]
        public DateTime? SignOnFrom { get; set; }

        [DataMember]
        public DateTime? SignOnTo { get; set; }

        //[DataMember]
        //public string FromJobCode { get; set; }

        //[DataMember]
        //public string ToJobCode { get; set; }

        [DataMember]
        public string MemberJobCodeFrom { get; set; }

        [DataMember]
        public string MemberJobCodeTo { get; set; }
    }

    [DataContract]
    public class ShipLabelReportParameters
    {
        [DataMember]
        public string ShipCodeFrom { get; set; }

        [DataMember]
        public string ShipCodeTo { get; set; }
    }
}