using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace MedicineTracker.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        string entryCellPlatformClassName;

        public Tests(Platform platform)
        {
            this.platform = platform;
            entryCellPlatformClassName = platform == Platform.iOS ? "UITextField" : "EntryCellEditText";
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        //Test - Creating a new medication entry to the database
        public void CreateNewMedicationEntry()
        {
            var navigationBarTitle = "Medicine Items Listing";
            var mainSreen = app.WaitForElement(x => x.Marked(navigationBarTitle));

            //Assert main screen is shown after the Medications Items Listing is displayed.
            Assert.IsTrue(mainSreen.Any(), navigationBarTitle + " screen wasnt shown after launch.");

            app.Tap(x => x.Marked("Add"));

            //Wait for the Adding Medicine Details screen to appear
            var newMedicineBarTitle = "Adding Medicine Details";
            var newMedicineScreen = app.WaitForElement(x => x.Marked(newMedicineBarTitle));

            Assert.IsTrue(newMedicineScreen.Any(), newMedicineBarTitle + " screen was not shown after tapping the Add button.");

            PopulateMedicineDetails();
            PopulateDosageDetails();

            app.Tap(x => x.Marked("Save"));

            var saveDialog = app.WaitForElement(x => x.Marked("Save Medicine Item"));

            Assert.IsTrue(saveDialog.Any(), "Save Medicine Item screeen was not shown after clicking on Save");

            app.Tap(x => x.Marked("OK"));

            var medicineItemsListing = app.WaitForElement(x => x.Marked("Medicine Items Listing"));

            Assert.IsTrue(medicineItemsListing.Any(), "Main screen was not shown after saving a new Medicine Item");
        }

        private void PopulateMedicineDetails()
        {
            // Enter in some text for our Brand Name EntryCell
            app.ClearText(x => x.Class(entryCellPlatformClassName).Index(0));
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(0), "Panadol");
            app.DismissKeyboard();

            //Enter in some text for our Description EntryCell
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(1),
                "Taken to help with headache");
            app.DismissKeyboard();


            app.ClearText(x => x.Class(entryCellPlatformClassName).Index(2));
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(2), "None specified");
            app.DismissKeyboard();
        }


        //Populate entry cell fields for our Dosage Details Section
        private void PopulateDosageDetails()
        {

            app.ClearText(x => x.Class(entryCellPlatformClassName).Index(3));
            app.EnterText(x => x.Class(entryCellPlatformClassName).Index(3), "2 tablets, 8 max per day");
            app.DismissKeyboard();

            if(platform == Platform.iOS)
            {
                app.Tap(x => x.Class(entryCellPlatformClassName).Index(4));
                app.Tap(x => x.Marked(platform == Platform.iOS ? "Done" : "OK"));
            }

            if(platform == Platform.iOS)
            {
                app.Tap(x => x.Class(entryCellPlatformClassName).Index(5));
                app.Tap(x => x.Marked(platform == Platform.iOS ? "Done" : "OK"));
            }
        }
    }
}
