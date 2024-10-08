﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FishingStoreApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [Range(1,1000)]
        public double Price { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [DisplayName("Product Code")]
        public string ProductCode {  get; set; }
        
        [DisplayName("Stocks")]
        public int Stocks { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
