using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Recidol.Models
{
    public class lineItems
    {
        [Key]
        public int id { get; set; }
        public string receiptId { get; set; }
        public string description { get; set; }
        public double? quantity { get; set; }
        public double? totalAmount { get; set; }
        public double? price { get; set; }
    }
}