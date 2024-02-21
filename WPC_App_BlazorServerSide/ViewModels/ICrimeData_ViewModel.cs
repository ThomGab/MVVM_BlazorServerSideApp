using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel;
using WPC_App_BlazorServerSide.Components.Forms;
using WPC_App_BlazorServerSide.Models;
using WPC_App_BlazorServerSide.Services;

namespace WPC_App_BlazorServerSide.ViewModels
{
    public interface ICrimeData_ViewModel : INotifyPropertyChanged
    { 
        public ICrimeDataInputModel InputModel { get; }

        public ICrimeDataService CrimeDataService { get; }

        public DateTimeOffset LatestDataDate { get; }

        public List<CrimeDataModel> CrimeDataDisplayed { get; }

        public List<CrimeCategoryModel> CrimeCategoriesSelected { get; }

        public Task OnCrimeCategorySelectionUpdated();

        public Task ValidFormSubmitted(ICrimeDataInputForm crimeDataInputForm);

        public Task Initialize();
    }
}