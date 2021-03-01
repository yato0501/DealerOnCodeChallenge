using DealerOnCodeChallenge.Models;
using DealerOnCodeChallenge.Provider;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DealerOnCodeChallenge.Business
{
    public interface IRegisterProvider
    {
        public void RegisterProcess();
    }

    public class RegisterProvider : IRegisterProvider
    {
        private readonly IInteractionProvider _interactionProvider;
        private readonly IItemStoreProvider _itemStoreProvider;
        private readonly ITotalCalculatorProvider _totalCalculatorProvider;

        public RegisterProvider(IInteractionProvider interactionProvider, IItemStoreProvider itemStoreProvider, ITotalCalculatorProvider totalCalculatorProvider)
        {
            _interactionProvider = interactionProvider ?? throw new ArgumentNullException(nameof(interactionProvider));
            _itemStoreProvider = itemStoreProvider ?? throw new ArgumentNullException(nameof(itemStoreProvider));
            _totalCalculatorProvider = totalCalculatorProvider ?? throw new ArgumentNullException(nameof(totalCalculatorProvider));
        }

        public void RegisterProcess()
        {
            List<Item> itemsInCart = new List<Item>();
            string keyCharInfo = string.Empty;
            do
            {
                _interactionProvider.PrintItemsFromCart(itemsInCart);
                _interactionProvider.PrintItemCatalog();

                keyCharInfo = _interactionProvider.GetUserInputItemOrInstruction();
                var availableItems = _itemStoreProvider.GetReadonlyAvailableItems();

                if (availableItems.Any(item => item.ItemId.ToString().Equals(keyCharInfo)))
                {
                    itemsInCart.Add(availableItems.First(x => x.ItemId.ToString().Equals(keyCharInfo)));
                }
                else if (keyCharInfo.Equals(_interactionProvider.AddCustomItemKey))
                {
                    _interactionProvider.AddItem();
                }
                else if (!keyCharInfo.Equals(_interactionProvider.CheckoutKey))
                {
                    _interactionProvider.NotifyInvalidInput();
                }
                else
                {
                    var calucatedItemTotal = _totalCalculatorProvider.CalculateItemTotals(itemsInCart);
                    _interactionProvider.ShowTotal(calucatedItemTotal);
                }

            } while (!keyCharInfo.Equals(_interactionProvider.CheckoutKey));
        }

    }
}
