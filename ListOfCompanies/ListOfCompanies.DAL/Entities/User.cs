using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.DAL.Entities
{
    public class User
    {
        public Guid ID { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Position { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
