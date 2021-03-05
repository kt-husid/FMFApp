using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    [DataContract]
    public class ETransSaveViewModel
    {
        [DataMember]
        public ETrans ItemFromDb { get; set; }

        [DataMember]
        public ETrans ItemFromETrans { get; set; }
    }

    [DataContract]
    public class ETransUploadResultViewModel
    {
        [DataMember]
        public ICollection<ETrans> ItemsToValidate { get; set; }

        [DataMember]
        public ICollection<ETrans> ItemsSavedToDb { get; set; }

        [DataMember]
        public ICollection<ETrans> ItemsAlreadyAddedToDb { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public int ItemCount { get; set; }
    }


    [DataContract]
    public class ETransViewModel
    {
        [DataMember]
        public ETrans CurrentItem { get; set; }

        [DataMember]
        public IEnumerable<ETrans> Alternatives { get; set; }

        [DataMember]
        public IEnumerable<Postal> PostalCodes { get; set; }
    }

    [DataContract]
    public class ETrans : ViewModelBaseWithChangeEvent
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime? Birthday { get; set; }

        [DataMember]
        public DateTime? ETransDate { get; set; } // KOYR_DATO

        [DataMember]
        public int EmployerNumber { get; set; } // EmployerNumber

        [DataMember]
        public int BankRegNumber { get; set; } // REG_NR

        [DataMember]
        public int BankAccount { get; set; } // KONTO

        [DataMember]
        public int BankRegNumberFF { get; set; } // REG_NR_FF

        [DataMember]
        public int BankAccountFF { get; set; } // KONTO_FF

        [DataMember]
        public DateTime? TransferDate { get; set; } // TransferDate

        [DataMember]
        public decimal HolidayPayAmount { get; set; } // Amount

        [DataMember]
        public string ART { get; set; }

        [DataMember]
        public bool IsReverse { get; set; } // if R then yes. if 1 then no // old name: ART

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public int PostalCode { get; set; }

        [DataMember]
        public string CityName { get; set; }

        [DataMember]
        public string AddressLine1 { get; set; }

        [DataMember]
        public string Gender { get; set; } // M or F

        [DataMember]
        public string SSN { get; set; }
    }
}