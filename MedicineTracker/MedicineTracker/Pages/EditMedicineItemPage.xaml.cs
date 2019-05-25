using System;
using MedicineTracker.Services;
using MedicineTracker.ViewModels;
using Xamarin.Forms;

namespace MedicineTracker.Pages
{
    public partial class EditMedicineItemPage : ContentPage
    {
        //Return the binding context for our ViewModel
        EditMedicineItemPageViewModel _viewModel
        {
            get { return BindingContext as EditMedicineItemPageViewModel; }
        }

        public EditMedicineItemPage()
        {
            InitializeComponent();

            //Declare and initialise our model Binding Context
            this.BindingContext = new EditMedicineItemPageViewModel(DependencyService.Get<INavigationService>());
            SetBinding(Page.TitleProperty, new Binding(EditMedicineItemPageViewModel.TitlePropertyName));
        }


        ToolbarItem toolbarItem;
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Initialize our ViewModel
            if(_viewModel != null)
            {
                await _viewModel.Init();
            }

            ToolbarItems.Add(toolbarItem = new ToolbarItem("Save", null, Save(), 0, 0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(toolbarItem);
        }

        private Action Save()
        {
            return async () =>
            {
                var alertResult = await DisplayAlert("Save Medicine Item", "Proceed and save changes?", "OK", "Cancel");
                if (alertResult == true)
                {
                    var saveResult = _viewModel.Save();
                    if (!saveResult)
                        await DisplayAlert("Error", "Brand Name and Description are required", "OK ");
                    else
                        await _viewModel.Navigation.RemoveViewFromStack();
                }
                else
                {
                    await _viewModel.Navigation.RemoveViewFromStack();
                }
            };
        }
    }
}
