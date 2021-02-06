using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostsBoard.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; } //email for login 
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
