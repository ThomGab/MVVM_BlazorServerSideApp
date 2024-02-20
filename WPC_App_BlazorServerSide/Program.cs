using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Refit;
using WPC_App_BlazorServerSide.Components.Forms;
using WPC_App_BlazorServerSide.Models;
using WPC_App_BlazorServerSide.Services;
using WPC_App_BlazorServerSide.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ICrimeData_ViewModel, CrimeData_ViewModel>();
builder.Services.AddTransient<ICrimeDataInputForm, CrimeDataInputForm>();
builder.Services.AddTransient<ICrimeDataInputModel, CrimeDataInputModel>();
builder.Services.AddTransient<IMapModel, MapModel>();
builder.Services.AddScoped<ICrimeDataService,CrimeDataService> ();
builder.Services.AddRefitClient<ICrimeDataApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri("https://data.police.uk/api"));
builder.Services.AddBlazorise(options => { options.Immediate = true; }).AddBootstrapProviders().AddFontAwesomeIcons();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
