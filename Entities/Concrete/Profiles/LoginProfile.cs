using AutoMapper;
using Entities.Concrete.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.Profiles
{
    public class LoginProfile:Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginDTO, User>();
        }
    }
}
