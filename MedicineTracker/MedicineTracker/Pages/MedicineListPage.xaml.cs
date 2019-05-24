using System;
using System.Collections.Generic;
using MedicineTracker.Services;
using MedicineTracker.ViewModels;
using Xamarin.Forms;

namespace MedicineTracker.Pages
{
    public partial class MedicineListPage : ContentPage
    {

        //Return the binding context for our ViewModel
        MedicineListPageViewModel _viewModel
        {
            get{ return BindingContext as MedicineListPageViewModel; }
        }

        public MedicineListPage()
        {
            InitializeComponent();
            this.Title = "Medicine Items Listing";

            //Declare and initialise our Model Binding Context
            this.BindingContext = new MedicineListPageViewModel(DependencyService.Get<INavigationService>());
            MedicineListView.ItemSelected += medicineListView_ItemSelected;
        }

        private async void medicineListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.SelectedItem = new Database.Database().GetItem((e.SelectedItem as MedicineListItem).Id);
            await _viewModel.Navigation.NavigateTo<EditMedicineItemPageViewModel>();
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            //Get the selected item to be delted from our ListView
            var selectedItem = (MedicineListItem)((MenuItem)sender).CommandParameter;

            var alertResult = await DisplayAlert("Delete Medicine Item", "Proceed and delete medicine item?", "OK", "Cancel");
            if(alertResult == true)
            {
                var itemDeleted = new Database.Database().DeleteItem(selectedItem.Id);
                _viewModel.MedicineList.Remove(selectedItem);
            }
            else
            {
                return;
            }
        }

        ToolbarItem toolBarItem;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Initialize our ViewModel
            if(_viewModel != null)
            {
                await _viewModel.Init();
            }

            //Call our GetMedicineitems method to populate our Collection
            _viewModel.GetMedicineItems();
            MedicineListView.SetBinding(ItemsView<Cell>.ItemsSourceProperty, new Binding("."));
            MedicineListView.BindingContext = _viewModel.MedicineList;
            ToolbarItems.Add(toolBarItem = new ToolbarItem("Add", null, Add(), 0, 0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(toolBarItem);
        }

        private Action Add()
        {
            return async () =>
            {
                App.SelectedItem = null;
                await _viewModel.Navigation.NavigateTo<EditMedicineItemPageViewModel>();
            };
        }
    }
}
