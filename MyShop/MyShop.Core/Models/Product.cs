﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyShop.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        [StringLength(30)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }


        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
