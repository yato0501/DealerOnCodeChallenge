using DealerOnCodeChallenge.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerOnCodeChallenge.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public ItemClassification ItemClassification { get; set; }
        public ItemOrigin ItemOrigin { get; set; }
        public decimal Price { get; set; }
    }
}
