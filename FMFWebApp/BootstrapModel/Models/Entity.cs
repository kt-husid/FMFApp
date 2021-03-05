using BootstrapWebApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BootstrapWebApplication.Models
{
    //public abstract class Entity : IHasChangeEvent
    public class Entity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public virtual ICollection<BankAccount> BankAccounts { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<EmailAddress> EmailAddresses { get; set; }
        public virtual ICollection<Website> Websites { get; set; }
        public virtual ICollection<SocialNetwork> SocialNetworks { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Entity()
        {
            BankAccounts = new HashSet<BankAccount>();
            Phones = new HashSet<Phone>();
            EmailAddresses = new HashSet<EmailAddress>();
            Websites = new HashSet<Website>();
            SocialNetworks = new HashSet<SocialNetwork>();
            Addresses = new HashSet<Address>();
            Comments = new HashSet<Comment>();
        }

        [NotMapped]
        //[DataMember]
        [Display(Name = "EmailAddress", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual EmailAddress PrimaryEmailAddress
        {
            get
            {
                if (EmailAddresses.Count > 0)
                {
                    var ae = EmailAddresses.GetEnumerator();
                    while (ae.MoveNext())
                    {
                        var changeEvent = ae.Current as IHasChangeEvent;
                        if (changeEvent != null && changeEvent.ChangeEventDeletedOn == null && ae.Current.IsPrimary)
                        {
                            return ae.Current;
                        }
                    }
                }
                return null;
            }
        }

        [NotMapped]
        //[DataMember]
        [Display(Name = "Website", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Website PrimaryWebsite
        {
            get
            {
                if (Websites.Count > 0)
                {
                    var ae = Websites.GetEnumerator();
                    while (ae.MoveNext())
                    {
                        var changeEvent = ae.Current as IHasChangeEvent;
                        if (changeEvent != null && changeEvent.ChangeEventDeletedOn == null && ae.Current.IsPrimary)
                        {
                            return ae.Current;
                        }
                    }
                }
                return null;
            }
        }

        [NotMapped]
        //[DataMember]
        [Display(Name = "SocialNetwork", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual SocialNetwork PrimarySocialNetwork
        {
            get
            {
                if (SocialNetworks.Count > 0)
                {
                    var ae = SocialNetworks.GetEnumerator();
                    while (ae.MoveNext())
                    {
                        var changeEvent = ae.Current as IHasChangeEvent;
                        if (changeEvent != null && changeEvent.ChangeEventDeletedOn == null && ae.Current.IsPrimary)
                        {
                            return ae.Current;
                        }
                    }
                }
                return null;
            }
        }

        [NotMapped]
        //[DataMember]
        [Display(Name = "BankAccount", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual BankAccount PrimaryBankAccount
        {
            get
            {
                if (BankAccounts.Count > 0)
                {
                    var ae = BankAccounts.GetEnumerator();
                    while (ae.MoveNext())
                    {
                        var changeEvent = ae.Current as IHasChangeEvent;
                        if (changeEvent != null && changeEvent.ChangeEventDeletedOn == null && ae.Current.IsPrimary)
                        {
                            return ae.Current;
                        }
                    }
                }
                return null;
            }
        }

        // ADR1, POSTNR
        //[ForeignKey("PrimaryAddress")]
        //public int? PrimaryAddressId { get; set; }
        [NotMapped]
        //[DataMember]
        [Display(Name = "Address", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Address PrimaryAddress
        {
            get
            {
                if (Addresses.Count > 0)
                {
                    var ae = Addresses.GetEnumerator();
                    while (ae.MoveNext())
                    {
                        var changeEvent = ae.Current as IHasChangeEvent;
                        if (changeEvent != null && changeEvent.ChangeEventDeletedOn == null && ae.Current.IsPrimary)
                        {
                            return ae.Current;
                        }
                    }
                }
                return null;
            }
        }

        // TLF_HEIMA
        //[ForeignKey("PrimaryPhone")]
        //public int? PrimaryPhoneId { get; set; }
        [NotMapped]
        //[DataMember]
        [Display(Name = "PhoneNumber", ResourceType = typeof(BootstrapResources.Resources))]
        public virtual Phone PrimaryPhone
        {
            get
            {
                if (Phones.Count > 0)
                {
                    var ae = Phones.GetEnumerator();
                    while (ae.MoveNext())
                    {
                        var changeEvent = ae.Current as IHasChangeEvent;
                        if (changeEvent != null && changeEvent.ChangeEventDeletedOn == null && ae.Current.IsPrimary)
                        {
                            return ae.Current;
                        }
                    }
                }
                return null;
            }
        }
    }
}