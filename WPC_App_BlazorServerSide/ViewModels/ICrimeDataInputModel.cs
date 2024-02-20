namespace WPC_App_BlazorServerSide.ViewModels
{
    public interface ICrimeDataInputModel
    {
        string Latitude { get; set; }
        string Longitude { get; set; }

        DateTime Date { get; set; }
    }
}