using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MedicineTracker.Services;

namespace MedicineTracker.ViewModels
{

    public class MedicineListItem
    {
        public int Id;
        public string BrandName { get; set; }
        public string Description { get; set; }
        public string SideEffects { get; set; }
        public string DateDoseTaken { get; set; }
    }

    public class MedicineListPageViewModel : BaseViewModel
    {

        public ObservableCollection<MedicineListItem> MedicineList;


        public MedicineListPageViewModel(INavigationService navService) : base(navService)
        {
        }

        //Retrive the Medicine items from our SQLite database
        public void GetMedicineItems()
        {
            MedicineList = new ObservableCollection<MedicineListItem>();

            //Iterate through each item stored within our SQLite database
            foreach(var item in new Database.Database().GetItems())
            {
                MedicineList.Add(new MedicineListItem
                {
                    Id = item.Id,
                    BrandName = item.BrandName,
                    Description = item.Description,
                    SideEffects = item.SideEffects,
                    DateDoseTaken = item.DateDoseTaken.ToString("dd-MM-yyyy"),
                });
            }
        }

        public override async Task Init()
        {
            await Task.Factory.StartNew(() => { });
        }
    }
}
