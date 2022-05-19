using AutoMapper;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Profiles
{
    public class RegisterProfile:Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterDTO, User>();
        }
    }
}
