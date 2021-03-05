using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BootstrapWebApplication.Models
{
    public interface IHasId
    {
        int Id { get; set; }
    }

    public interface IHasEntity : IHasId
    {
        int? EntityId { get; set; }
        Entity Entity { get; set; }
    }

    public interface IHasStuff : IHasId
    {
        Phone PrimaryPhone { get; }
        Address PrimaryAddress {get; }
        ICollection<Phone> Phones { get; }
        ICollection<Address> Addresses { get; }

        string CountryCode { get; set; }
        string CountryName { get; set; }
        string AddressLine { get; set; }
        int? PostalCode { get; set; }
        string CityName { get; set; }
        int? PhoneCountryCode { get; set; }
        string PhoneNumber { get; set; }
    }

    //public abstract class IHasEntityWithMore : BootstrapWebApplication.Interfaces.IHasChangeEvent, IHasId, IHasEntity
    //{
    //    [Key]
    //    [DataMember]
    //    public int Id { get; set; }

    //    [ForeignKey("Entity")]
    //    [DataMember]
    //    public int? EntityId { get; set; }
    //    [DataMember]
    //    public virtual Entity Entity { get; set; }

    //    //public ICollection<Address> Addresses { get; set; }
    //    //public ICollection<Phone> Phones { get; set; }
    //    [NotMapped]
    //    [DataMember]
    //    public virtual ICollection<Address> Addresses
    //    {
    //        get
    //        {
    //            if (Entity != null)
    //            {
    //                return Entity.Addresses;
    //            }
    //            return null;
    //        }
    //    }

    //    [NotMapped]
    //    [DataMember]
    //    public virtual ICollection<Phone> Phones
    //    {
    //        get
    //        {
    //            if (Entity != null)
    //            {
    //                return Entity.Phones;
    //            }
    //            return null;
    //        }
    //    }
    //}

    //public interface IHasEntityWithMore : IHasId //: IHasEntity
    //{
    //    Address PrimaryAddress { get; }
    //    Phone PrimaryPhone { get; }

    //    ICollection<Address> Addresses { get; }
    //    ICollection<Phone> Phones { get; }
    //}

    public interface IHasPerson
    {
        int? PersonId { get; set; }
        Person Person { get; set; }
    }

    public interface IPerson //: IHasEntity
    {
        //int? EntityId { get; set; }
        //Entity Entity { get; set; }

        string SSN { get; set; }

        string FirstName { get; set; }

        string MiddleName { get; set; }

        string LastName { get; set; }

        string FullName { get; }

        DateTime? Birthday { get; set; }

        int? GenderId { get; set; }

        int? TitleId { get; set; }

        PersonTitle Title { get; set; }

        PersonGender Gender { get; set; }

        //ICollection<Phone> Phones { get; set; }

        //ICollection<EmailAddress> EmailAddresses { get; set; }

        //ICollection<Website> Websites { get; set; }

        //ICollection<SocialNetwork> SocialNetworks { get; set; }

        //ICollection<Address> Addresses { get; set; }

        //ICollection<Company> Companies { get; set; }
    }
}