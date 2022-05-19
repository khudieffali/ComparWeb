using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        [RegularExpression("^[a-zA-z0-9_]*$", ErrorMessage = "Yalnız hərf, rəqəm və '_' işarəsi olmalıdır")]
        public string UserName { get; set; } = null!;
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Zəhmət olmasa parol ilə eyni qeyd edin")]
        public string ConfirmPassword { get; set; } = null!;


    }
}
