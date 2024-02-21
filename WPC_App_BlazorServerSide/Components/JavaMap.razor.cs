using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using WPC_App_BlazorServerSide.Helpers;
using WPC_App_BlazorServerSide.Models;
using WPC_App_BlazorServerSide.ViewModels;

namespace WPC_App_BlazorServerSide.Components
{
    public class JavaMapComponentBase : ComponentBase, IDisposable
    {
        /// <summary>
        /// Needs to handle overlapping pins! write test for this.
        /// </summary>

        [Inject]
        public ICrimeData_ViewModel ViewModel { get; set; } = null!;

        [Inject]
        public IJSRuntime jsInteropRuntime { get; set; } = null!;

        private PropertyChangedEventHandler? handler;

        protected override async Task OnInitializedAsync()
        {
            handler = async delegate (object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(ViewModel.CrimeDataDisplayed))
                {
                    await RenderMap(ViewModel.CrimeDataDisplayed);
                }

                if (e.PropertyName == nameof(ViewModel.CrimeCategoriesSelected))
                {
                    await RenderMap(ViewModel.CrimeDataDisplayed);
                }
            };

            ViewModel.PropertyChanged += handler;
        }

        private async Task RenderMap(List<CrimeDataModel> dataToDisplay)
        {
            if(
                !string.IsNullOrWhiteSpace(ViewModel.InputModel.Latitude) || 
                !string.IsNullOrWhiteSpace(ViewModel.InputModel.Longitude)
                )
            {
                await ClearMarkersOnMap();

                await CentreMapTo();
                await ZoomMapToFit();
                await AddMarkersToMap();
                await DisplayMarkersOnMap();

                StateHasChanged();
            }
        }

        private async Task ClearMarkersOnMap()
        {
            await jsInteropRuntime.InvokeVoidAsync("clearAllMarkers");
        }

        private async Task DisplayMarkersOnMap()
        {
            await jsInteropRuntime.InvokeVoidAsync("displayAllMarkers");
        }

        private async Task CentreMapTo()
        {
            await jsInteropRuntime.InvokeVoidAsync("centreMapTo", ViewModel.InputModel.Latitude, ViewModel.InputModel.Longitude);
        }

        private async Task ZoomMapToFit()
        {
            await jsInteropRuntime.InvokeVoidAsync("zoomMapToFit");
        }

        private async Task AddMarkersToMap()
        {
            foreach (var point in ViewModel.CrimeDataDisplayed)
            {
                var colour = CrimeCategoryHelpers.GetColour(point);
                await jsInteropRuntime.InvokeVoidAsync("addMarker", point.Location.Latitude, point.Location.Longitude, point.Category, colour);
            }

            Console.WriteLine("WroteMarkersToMap!");
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await jsInteropRuntime.InvokeVoidAsync("initialize");
            }
        }

        public void Dispose() => ViewModel.PropertyChanged -= handler;
    }
}
