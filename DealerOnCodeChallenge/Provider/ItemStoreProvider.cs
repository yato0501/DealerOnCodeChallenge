using DealerOnCodeChallenge.Business;
using DealerOnCodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DealerOnCodeChallenge.Provider
{
    public interface IItemStoreProvider
    {
        void AddToInventory(Item item);
        ReadOnlyCollection<Item> GetReadonlyAvailableItems();
    }

    public class ItemStoreProvider : IItemStoreProvider
    {

        private List<Item> _availiableItems = new List<Item>
        {
            new Item(){ ItemId = 0, ItemName = "Pineapple", ItemClassification = ItemClassification.Food, ItemOrigin = ItemOrigin.Domestic, Price = 4.99m },
            new Item(){ ItemId = 1, ItemName = "Imported Pineapple", ItemClassification = ItemClassification.Food, ItemOrigin = ItemOrigin.Imported, Price = 4.99m },
            new Item(){ ItemId = 2, ItemName = "Book", ItemClassification = ItemClassification.Book, ItemOrigin = ItemOrigin.Domestic, Price = 12.49m },
            new Item(){ ItemId = 3, ItemName = "Music CD", ItemClassification = ItemClassification.Other, ItemOrigin = ItemOrigin.Domestic, Price = 14.99m },
            new Item(){ ItemId = 4, ItemName = "Chocolate bar", ItemClassification = ItemClassification.Food, ItemOrigin = ItemOrigin.Domestic, Price = 0.85m },
            new Item(){ ItemId = 5, ItemName = "Imported Box of Chocolate", ItemClassification = ItemClassification.Food, ItemOrigin = ItemOrigin.Imported, Price = 10.00m },
            new Item(){ ItemId = 6, ItemName = "Imported Bottle of Perfume", ItemClassification = ItemClassification.Other, ItemOrigin = ItemOrigin.Imported, Price = 47.50m },
            new Item(){ ItemId = 7, ItemName = "Bottle of Perfume", ItemClassification = ItemClassification.Other, ItemOrigin = ItemOrigin.Domestic, Price = 18.99m },
            new Item(){ ItemId = 8, ItemName = "Packet of Headache Pills", ItemClassification = ItemClassification.Medical, ItemOrigin = ItemOrigin.Domestic, Price = 9.75m }
        };

        public void AddToInventory(Item item)
        {
            _availiableItems.Add(item);
        }
        public ReadOnlyCollection<Item> GetReadonlyAvailableItems()
        {
            return _availiableItems.AsReadOnly();
        }
    }
}
