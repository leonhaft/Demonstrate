using Elsa.Persistence.EntityFramework.Sqlite;
using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var elsaSection = builder.Configuration.GetSection("Elsa");

builder.Services.AddElsa(config => config
.UseEntityFrameworkPersistence(ef => ef.UseSqlite())
.AddConsoleActivities()
    .AddHttpActivities(elsaSection.GetSection("Server").Bind)
    .AddQuartzTemporalActivities()
    .AddWorkflowsFrom<Program>());

builder.Services.AddElsaApiEndpoints();

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

app.UseHttpActivities();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapFallbackToPage("/Workflows");
});
app.Run();
