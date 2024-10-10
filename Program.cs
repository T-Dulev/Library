using Library;
using System.Data.SQLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

try
{
    using var connection = new SQLiteConnection("Data Source=Library.db");
    connection.Open();

    // Interacting with the database 
    // ...

}
catch (SQLiteException e)
{
    // Display the exception
    Console.WriteLine(e.Message);
}
