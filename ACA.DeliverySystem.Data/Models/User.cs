using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Data.Models
{
    public class User
    {

        public int Id { get; set; }


        public string Name { get; set; }

        public string SureName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
