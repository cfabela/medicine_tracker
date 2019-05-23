using System;
using MedicineTracker.Models;
using MedicineTracker.Services;
using MedicineTracker.ViewModels;
using Xamarin.Forms;

namespace MedicineTracker
{
    public partial class App : Application
    {
        public App()
        {
            //The root page of our application
            var mainPage = new NavigationPage(new MedicinelistPage());

            //Create an instance of our NavigationService service
            var navService = DependencyService.Get<INavigationService>() as NavigationService;

            //Assign the main page to our navigation service
            navService.XFNavigation = mainPage.Navigation;

            //Register each of our View Models on our Navigation Stack
            navService.RegisterViewMapping(typeof(MedicineListPageViewModel), typeof(MedicineListPage));
            navService.RegisterViewMapping(typeof(EditMedicineItemPageViewModel), typeof(EditMedicineItemPage));

            MainPage = mainPage;
        }

        public static MedicineItem SelectedItem { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
