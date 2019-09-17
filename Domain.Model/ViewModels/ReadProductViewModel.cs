using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class ProductViewModel
    {
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Name must be string")]
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}
