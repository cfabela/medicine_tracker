using System;
using System.Threading.Tasks;

using MedicineTracker.ViewModels;
using Xamarin.Forms;

namespace MedicineTracker.Services
{
    public interface INavigationService
    {
        //Async removes the most recent Page from the navigation stack
        Task<Page> RemoveViewFromStack();

        //Navigate to a particular ViewModel within out MVVM Model
        Task NavigateTo<TVM>()
            where TVM : BaseViewModel;
    }
}
