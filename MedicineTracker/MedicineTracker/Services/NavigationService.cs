using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using MedicineTracker.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace MedicineTracker.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation XFNavigation { get; set; }
        readonly IDictionary<Type, Type> _viewMapping = new Dictionary<Type, Type>();

       //Register our ViewModel and View within our Dictionary
       public void RegisterViewMapping(Type viewModel, Type view)
        {
            _viewMapping.Add(viewModel, view);
        }

        //Removes the most recent Page from the navigation stack.
        public Task<Page> RemoveViewFromStack()
        {
            return XFNavigation.PopAsync();
        }

        //Navigates navigates to a specific ViewModel
        public async Task NavifateTo<TVM>()
            where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));

            if (XFNavigation.NavigationStack.Last().BindingContext is BaseViewModel)
                await ((BaseViewModel)(XFNavigation.NavigationStack.Last().BindingContext)).Init();
        }

        //Navigates to a specific ViewModel within our dictionary viewMapping
        public async Task NavigateToView(Type viewModelType)
        {
            Type viewType;

            if (!_viewMapping.TryGetValue(viewModelType, out viewType))
                throw new ArgumentException("No view found in View Mapping for ");


            var constructor = viewType.GetTypeInfo().DeclaredConstructors.
                FirstOrDefault(dc => !dc.GetParameters().Any());

            var view = constructor.Invoke(null) as Page;
            await XFNavigation.PushAsync(view, true);
        }








    }
}
