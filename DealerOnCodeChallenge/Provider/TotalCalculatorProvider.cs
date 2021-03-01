using DealerOnCodeChallenge.Business;
using DealerOnCodeChallenge.Models;
using DealerOnCodeChallenge.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DealerOnCodeChallenge.Provider
{
    public interface ITotalCalculatorProvider
    {
        public IEnumerable<CalculatedItem> CalculateItemTotals(IEnumerable<Item> items);
    }
    public class TotalCalculatorProvider : ITotalCalculatorProvider
    {
        private ReadOnlyDictionary<ItemOrigin, double> ItemOriginTaxInfo 
            => new ReadOnlyDictionary<ItemOrigin, double>(new Dictionary<ItemOrigin, double> 
            {
                { ItemOrigin.Domestic, 0},
                { ItemOrigin.Imported, 0.05}            
            });
        private ReadOnlyDictionary<ItemClassification, double> ItemClassificationTaxInfo
            => new ReadOnlyDictionary<ItemClassification, double>(new Dictionary<ItemClassification, double>
            {
                { ItemClassification.Book, 0 },
                { ItemClassification.Food, 0 },
                { ItemClassification.Medical, 0 },
                { ItemClassification.Other, 0.10 }
            });

        public IEnumerable<CalculatedItem> CalculateItemTotals(IEnumerable<Item> items)
        {
            var result = items.Select(item =>
            {
                var classificationTaxRate = ItemClassificationTaxInfo.GetValueOrDefault(item.ItemClassification);
                var itemOriginTaxRate = ItemOriginTaxInfo.GetValueOrDefault(item.ItemOrigin);

                var classificationTaxAmount = Math.Round(decimal.ToDouble(item.Price) * classificationTaxRate, 2).GoUpToNearest5Hundredths();
                var itemOriginalTaxAmount = Math.Round(decimal.ToDouble(item.Price) * itemOriginTaxRate, 2).GoUpToNearest5Hundredths();


                var count = items.Where(x => x.ItemId == item.ItemId).Count();

                return new CalculatedItem()
                {
                    Count = count,
                    Item = item,
                    ItemClassificationTaxAmount = Convert.ToDecimal(classificationTaxAmount),
                    ItemOriginTaxAmount = Convert.ToDecimal(itemOriginalTaxAmount)
                };
            }).GroupBy(x => x.Item.ItemId).Select( i => i.FirstOrDefault());

            return result;
        }
    }
}
