﻿using AutoMapper;
using Contacts.DTO;
using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Profiles
{
    public class ContactProfile: Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDTO>()
                .ForMember(
                    cDTO => cDTO.PhoneNumbers, 
                    c => c.MapFrom( no => no.PhoneNumbers.Distinct())
                )
                .ForMember(
                    cDTO => cDTO.PhoneNumbersString,
                    c => c.MapFrom(no => String.Join(", ", no.PhoneNumbers.Select(n => n.Number).ToList()))
                )
                .ForMember(
                    cDTO => cDTO.ProfilePhotoId, 
                    c => c.MapFrom(contact => contact.ProfilePhoto.ProfilePhotoId)
                );

            CreateMap<ICollection<Contact>, List<ContactDTO>>();

            CreateMap<PhoneNumber, PhoneNumberDTO>()
                .ForMember(
                    phonNumDTO => phonNumDTO.ContactId,
                    phonNum => phonNum.MapFrom(c => c.Contact.ContactId)
                )
                .ForMember(
                    phonNumDTO => phonNumDTO.Note,
                    phonNum => phonNum.MapFrom(c => String.IsNullOrEmpty(c.Note)? "": c.Note)
                );

            CreateMap<ICollection<PhoneNumber>, List<PhoneNumberDTO>>();

            //*********************************************************************
            CreateMap<PhoneNumberDTO, PhoneNumber>();
            CreateMap<ContactDTO, Contact>();
            CreateMap<byte[], ProfilePhoto>()
                .ForMember(
                    profilePhoto => profilePhoto.Content, 
                    bytes => bytes.MapFrom(c => c)
                );
            CreateMap<string, ApplicationUser>()
                .ForMember(
                    user => user.Id, 
                    stringId => stringId.MapFrom(c => c)
                );
        }
    }
}
