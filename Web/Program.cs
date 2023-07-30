using Application.Persistence;
using Models.Types.Components;
using Models.Types.Products;
using Models.Types.Common;
using TestPersistence;
using Web.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IReadOnlyRepository<Part>, PartsReadRepository>();
builder.Services.AddScoped<IReadOnlyRepository<AssemblySpecification>, SpecsRepository>();
builder.Services.AddScoped<IReadOnlyRepository<(Part part, DiscreteMeasure quantity)>, Inventory>();

IConfigurationSection barcodeFormatSection =
    builder.Configuration.GetSection(BarcodeFormatOptions.BarcodeFormats);
builder.Services.Configure<BarcodeFormatOptions>(barcodeFormatSection);
builder.Services.AddSingleton<BarcodeGeneratorFactory>();

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

app.UseAuthorization();

app.MapRazorPages();

app.Run();
