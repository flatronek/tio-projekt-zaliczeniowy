using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    public class DbRate
    {
        [Key]
        public int RestaurantId { get; set; }

        [Key]
        public int UserId { get; set; }

        public int Rate { get; set; }
    }
}