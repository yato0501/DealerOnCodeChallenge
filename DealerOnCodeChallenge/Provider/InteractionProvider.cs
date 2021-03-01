using DealerOnCodeChallenge.Business;
using DealerOnCodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerOnCodeChallenge.Provider
{
    public interface IInteractionProvider
    {
        string GetUserInputItemOrInstruction();
        void NotifyInvalidInput();
        string CheckoutKey { get; }
        string AddCustomItemKey { get; }
        void PrintItemCatalog();
        void PrintItemsFromCart(List<Item> items);
        void AddItem();
        void ShowTotal(IEnumerable<CalculatedItem> calucatedItemTotal);
    }
    public class InteractionProvider : IInteractionProvider
    {
        private readonly IItemStoreProvider _itemStoreProvider;
        public InteractionProvider(IItemStoreProvider itemStoreProvider)
        {
            _itemStoreProvider = itemStoreProvider ?? throw new ArgumentNullException(nameof(itemStoreProvider));
        }
        public string CheckoutKey { get { return "t"; } }
        public string AddCustomItemKey { get { return "c"; } }

        public string GetUserInputItemOrInstruction()
        {
            Console.WriteLine("c - add custom item to available items.");
            Console.WriteLine("t - checkout and get total.");
            Console.WriteLine("Enter an Item# or instruction keys 't' or 'c': ");
            var keyCharInfo = Console.ReadLine();

            return keyCharInfo;
        }

        public void PrintItemCatalog()
        {
            Console.WriteLine("Available Items");
            PrintItemList(_itemStoreProvider.GetReadonlyAvailableItems().ToList());
        }

        public void PrintItemsFromCart(List<Item> items)
        {
            if (items != null && items.Any())
            {
                Console.WriteLine("\nHere's what's in your basket!");
                PrintItemList(items);
            }
            else
            {
                Console.WriteLine("Your basket is current empty.");
            }
            AddSpacer();
        }

        public void PrintItemList(List<Item> items)
        {
            items.ForEach((item) => Console.WriteLine($"Item#{item.ItemId}:{item.ItemOrigin.ToString()} {item.ItemClassification.ToString()} - {item.ItemName} - ${item.Price}"));
            AddSpacer();
        }

        public void NotifyInvalidInput()
        {
            Console.WriteLine("Invalid Input! Please try again.\n");
        }

        public void AddItem()
        {
            Console.Write("Enter New Item Name: ");
            var itemName = Console.ReadLine();

            var validPrice = false;
            decimal itemPrice = 0.00m;
            do
            {

                //////////////////////
                Console.Write("\nEnter New Item Price: ");
                validPrice = decimal.TryParse(Console.ReadLine(), out itemPrice);

                if (!validPrice)
                {
                    Console.WriteLine("\nInvalid price!!");
                }

                /////////////////////
                else
                {
                    itemPrice = decimal.Parse(itemPrice.ToString("0.00"));
                }

            } while (!validPrice);

            var validOrigin = false;
            var origin = ItemOrigin.Domestic;
            do
            {
                Console.Write("Is the new item (d)omestic or (i)mported? ");
                var value = Console.ReadKey();
                validOrigin = value.KeyChar.Equals('d') || value.KeyChar.Equals('i');
                if (!validOrigin)
                {
                    Console.WriteLine("\nInvalid item origin, please try again!");
                }
                else
                {
                    switch (value.KeyChar)
                    {
                        case 'i':
                            origin = ItemOrigin.Imported;
                            break;
                        default:
                            break;
                    }
                }
            } while (!validOrigin);

            var validClassification = false;
            ItemClassification classification = ItemClassification.Other;
            do
            {
                Console.Write("\nIs the new item (f)ood, (b)ook, (m)edical, or (o)ther? ");
                var value = Console.ReadKey();
                validClassification = value.KeyChar.Equals('f') || value.KeyChar.Equals('b') || value.KeyChar.Equals('m') || value.KeyChar.Equals('o');
                if (!validClassification)
                {
                    Console.WriteLine("\nInvalid item classification, please try again!");
                }
                else
                {
                    switch (value.KeyChar)
                    {
                        case 'f':
                            classification = ItemClassification.Food;
                            break;
                        case 'b':
                            classification = ItemClassification.Book;
                            break;
                        case 'm':
                            classification = ItemClassification.Medical;
                            break;
                        default:
                            break;
                    }

                }
            } while (!validClassification);

            _itemStoreProvider.AddToInventory(new Item 
            { 
                ItemId = _itemStoreProvider.GetReadonlyAvailableItems().Count, 
                ItemName = itemName, 
                ItemClassification = classification, 
                ItemOrigin = origin, 
                Price = itemPrice 
            });
        }

        public void ShowTotal(IEnumerable<CalculatedItem> calculatedItemTotal)
        {
            var totalTax = 0.00m;
            var total = 0.00m;
            foreach (var calculatedItem in calculatedItemTotal)
            {
                totalTax += (calculatedItem.ItemClassificationTaxAmount + calculatedItem.ItemOriginTaxAmount) * calculatedItem.Count;
                total += calculatedItem.PerItemTotal * calculatedItem.Count;

                Console.Write($"{calculatedItem.Item.ItemName}: {calculatedItem.PerItemTotal * calculatedItem.Count} ");
                if (calculatedItem.Count > 1)
                {
                    Console.Write($"({calculatedItem.Count} x {calculatedItem.PerItemTotal})");
                }
                Console.Write("\n");
                
            }
            Console.WriteLine($"Sales Tax: {totalTax}");
            Console.WriteLine($"Total: {total}");
        }



        private void AddSpacer()
        {
            Console.WriteLine("-------------------");
        }
    }
}
