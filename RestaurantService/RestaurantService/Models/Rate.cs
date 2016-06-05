using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantService.Models
{
    public class Rate
    {
        private int token;
        private int rate;

        public Rate(int restaurantId, int token, int rate)
        {
            this.restaurantId = restaurantId;
            this.token = token;
            this.rate = rate;
        }

        public Rate()
        {
        }

        [Key]
        public int id { get; set; }
        public int restaurantId { get; set; }
        public int userId { get; set; }
        public int score { get; set; }
    }
}