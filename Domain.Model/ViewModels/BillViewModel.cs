using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Model
{
    public class BillViewModel
    {
        [DataType(DataType.Currency)]
        public float TotalAmount { get; set; }
    }
}
