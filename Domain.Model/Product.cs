using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Product
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int ProductId { get; set; }

        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }
        public Category Category { get; set; } //virtual?, (?)
    }
}
