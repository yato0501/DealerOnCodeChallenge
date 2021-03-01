using System;
using System.Collections.Generic;
using System.Text;

namespace DealerOnCodeChallenge.Models
{
    public class CalculatedItem
    {
        public int Count { get; set; }
        public Item Item { get; set; }
        public decimal ItemOriginTaxAmount { get; set; }
        public decimal ItemClassificationTaxAmount { get; set; }

        public decimal PerItemTotal => (Item.Price + ItemClassificationTaxAmount + ItemOriginTaxAmount);
        public decimal TotalPreTax => Item.Price * Count; 
    }
}
