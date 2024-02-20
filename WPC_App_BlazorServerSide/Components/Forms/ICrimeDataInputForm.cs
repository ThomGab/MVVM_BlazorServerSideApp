namespace WPC_App_BlazorServerSide.Components.Forms
{
    public interface ICrimeDataInputForm
    {
        string Latitude { get; set; }
        string Longitude { get; set; }

        DateTime? Date { get; set; }
    }
}