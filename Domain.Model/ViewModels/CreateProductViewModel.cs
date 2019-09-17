using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class CreateProductViewModel
    {
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int CategoryId{ get; set; }
    }
}
