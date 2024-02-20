using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using WPC_App_BlazorServerSide.Components.Forms;
using WPC_App_BlazorServerSide.Models;
using WPC_App_BlazorServerSide.Services;

namespace WPC_App_BlazorServerSide.ViewModels
{
    public class CrimeData_ViewModel : ICrimeData_ViewModel
    {
        private readonly ICrimeDataInputModel inputModel;
        private readonly ICrimeDataService crimeDataService;
        private readonly IMapModel mapModel;
        private List<CrimeDataModel> crimeData = new();
        private List<CrimeDataModel> crimeDataDisplayed = new();
        private IReadOnlyList<CrimeCategoryModel> crimeCategories;
        private List<CrimeCategoryModel> crimeCategoriesSelected = new();
        private DateTimeOffset latestDataDate = new();

        public CrimeData_ViewModel(ICrimeDataService crimeDataService, ICrimeDataInputModel inputModel, IMapModel mapModel)
        {
            this.inputModel = inputModel;
            this.crimeDataService = crimeDataService;
            this.mapModel = mapModel;
        }

        //private List<CrimeDataResponse> crimeData = new();
        public ICrimeDataInputModel InputModel => this.inputModel;

        public IMapModel MapModel => this.mapModel;

        public DateTimeOffset LatestDataDate => latestDataDate;

        public IReadOnlyList<CrimeCategoryModel> CrimeCategories => crimeCategories;

        public ICrimeDataService CrimeDataService => crimeDataService;


        public event PropertyChangedEventHandler? PropertyChanged = null!;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void InvalidFormSubmitted(EditContext editContext)
        {
        }

        public async Task ValidFormSubmitted(ICrimeDataInputForm inputform)
        {
            inputModel.Latitude = inputform.Latitude;
            inputModel.Longitude = inputform.Longitude;
            inputModel.Date = inputform.Date ?? latestDataDate.UtcDateTime;

            crimeData = await crimeDataService.GetCrimeData(inputModel.Latitude, inputModel.Longitude, inputModel.Date);
            await OnCrimeCategorySelectionUpdated();
        }

        public List<CrimeCategoryModel> CrimeCategoriesSelected { 
            get {
                return crimeCategoriesSelected; 
            } 
            set { 
                crimeCategoriesSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CrimeCategoriesSelected)));
            } 
        }

        public List<CrimeDataModel> CrimeDataDisplayed {
            get => crimeDataDisplayed;
            set
            {
                if (crimeDataDisplayed != value)
                {
                    crimeDataDisplayed = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CrimeDataDisplayed)));
                }
            }
        }

        public async Task Initialize()
        {
            crimeCategories = await crimeDataService.GetAllCategories();
            var latestDataDateResponse = await crimeDataService.GetLastUpdatedDate();

            latestDataDate = DateTimeOffset.Parse(latestDataDateResponse.Date, new CultureInfo("en-GB"));

            foreach (var crimeCat in crimeCategories)
            {
                crimeCategoriesSelected.Add(crimeCat);
            }
        }

        public async Task OnCrimeCategorySelectionUpdated()
        {
            var datatoDisplay = new List<CrimeDataModel>();
            var selectedCategories = crimeCategoriesSelected.Where(category => category.IsSelected == true).ToList();

            foreach (var selectedCategory in selectedCategories)
            {
                var dataToAdd = crimeData.Where(dataPoint => dataPoint.Category == selectedCategory.CategoryCode).ToList();
                foreach (var dataPoint in dataToAdd)
                {
                    datatoDisplay.Add(dataPoint);
                }
            }

            CrimeDataDisplayed = datatoDisplay;
        }
    }
}
