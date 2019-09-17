using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Category
    {
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int CategoryId { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9""'\s-]*$", ErrorMessage = "Name must be string")]
        [StringLength(50)]
        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
