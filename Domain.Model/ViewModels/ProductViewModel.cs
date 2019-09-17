using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class ReadProductViewModel
    {
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public float Price { get; set; }

        public string CategoryName { get; set; }

    }
}
