using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MedicineTracker.Services
{
    public interface INavigationService
    {
        //Async removes the most recent Page from the navigation stack
        Task<Page> RemoveViewFromStack();

        //Navigate to a particular ViewModel within out MVVM Model
        Task NaviateTo<TVM>()
            where TVM : BaseViewModel;
    }
}
