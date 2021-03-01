using DealerOnCodeChallenge.Business;
using DealerOnCodeChallenge.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DealerOnCodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection sc = new ServiceCollection();
            IServiceProvider sp = sc.AddScoped<IRegisterProvider, RegisterProvider>()
            .AddScoped<IItemStoreProvider, ItemStoreProvider>()
            .AddScoped<IInteractionProvider, InteractionProvider>()
            .AddScoped<ITotalCalculatorProvider, TotalCalculatorProvider>()
            .BuildServiceProvider();

            var register = sp.GetRequiredService<IRegisterProvider>();

            register.RegisterProcess();
        }
    }
}
