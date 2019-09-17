using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class Bill
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int BillId { get; set; }

        [DataType(DataType.Currency)]
        public float TotalAmount { get; set; }

    }
}
