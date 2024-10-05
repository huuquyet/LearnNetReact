using Microsoft.EntityFrameworkCore;
using LearnNetReact.Data;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// var connStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("MvcMovieContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieContext' not found."));
var connStrBuilder = new SqliteConnectionStringBuilder(builder.Configuration.GetConnectionString("MvcMovieSqliteContext") ?? throw new InvalidOperationException("Connection string 'MvcMovieSqliteContext' not found."))
{
    Mode = SqliteOpenMode.ReadWriteCreate,
    Password = builder.Configuration["DbPassword"]
};
builder.Services.AddDbContext<MvcMovieContext>(options =>
    options.UseSqlite(connStrBuilder.ToString()));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    MovieSeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();