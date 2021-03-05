using BootstrapWebApplication.BLL;
using BootstrapWebApplication.Admin.Conversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BootstrapWebApplication.Models;

namespace BootstrapWebApplication
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //#region ShipComment
            //Mapper.CreateMap<ShipCommentViewModel, ShipComment>()
            //    //.ForMember(m => m.ChangeEventId, o => o.Ignore())
            //    .ForMember(m => m.ChangeEvent, o => o.Ignore())
            //    .ForMember(m => m.Ship, o => o.Ignore());

            //Mapper.CreateMap<ShipComment, ShipCommentViewModel>()
            //    .ForMember(m => m.ChangeEvent, o => o.Ignore());

            ////Mapper.CreateMap<ShipComment, ShipCommentViewModel>()
            ////    .ForMember(d => d.ChangeEventUpdatedByUserIdCode, o => o.MapFrom(src => src.ChangeEvent.UpdatedByUserIdCode))
            ////    .ForMember(d => d.ChangeEventUpdatedOn, o => o.MapFrom(src => src.ChangeEvent.UpdatedOn));
            //#endregion

            //Mapper.CreateMap<MemberCommentViewModel, MemberComment>()
            //    //.ForMember(m => m.ChangeEventId, o => o.Ignore())
            //    .ForMember(m => m.ChangeEvent, o => o.Ignore())
            //    .ForMember(m => m.Member, o => o.Ignore());
            //Mapper.CreateMap<MemberComment, MemberCommentViewModel>();

            //Mapper.CreateMap<BankAccount, MemberBankAccountViewModel>()
            //    .ForMember(m => m.RegNumber, o => o.MapFrom(n=>n.Bank.RegNumber))
            //    .ForMember(m => m.Entity, o => o.Ignore())
            //    .ForMember(m => m.MemberId, o => o.Ignore())
            //    .ForMember(m => m.ChangeEventId, o => o.Ignore())
            //    .ForMember(m => m.ChangeEvent, o => o.Ignore());
            //    //.ForMember(m => m.RegNumber, o => o.Ignore());

            //Mapper.CreateMap<MemberBankAccountViewModel, BankAccount>()
            //    .ForMember(m => m.Bank, o => o.Ignore())
            //    .ForMember(m => m.Entity, o => o.Ignore());

            //Mapper.CreateMap<AddressViewModel, Address>()
            //    .ForMember(m => m.Postal, o => o.Ignore())
            //    .ForMember(m => m.Country, o => o.Ignore())
            //    .ForMember(m => m.Entity, o => o.Ignore());


            #region ShipTrip
            //Mapper.CreateMap<Trip, ShipTripViewModel>();

            //Mapper.CreateMap<Trip, ShipTripViewModel>()
            //    .ForMember(d => d.ChangeEventUpdatedByUserIdCode, o => o.MapFrom(src => src.ChangeEvent.UpdatedByUserIdCode))
            //    .ForMember(d => d.ChangeEventUpdatedOn, o => o.MapFrom(src => src.ChangeEvent.UpdatedOn));
            #endregion

        }

    }
}