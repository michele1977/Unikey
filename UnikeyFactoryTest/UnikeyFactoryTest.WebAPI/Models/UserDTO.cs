using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[*@$!#%&()^_~{}+=|.]).{6,50}$", ErrorMessage = "Formato password non valido")]
        public string Password { get; set; }
    }
}