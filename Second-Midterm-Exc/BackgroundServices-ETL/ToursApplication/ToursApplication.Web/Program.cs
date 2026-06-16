using EvolveDb;
using ExamsApplication.Repository;
using ExamsApplication.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ToursApplication.Repository.Implementation;
using ToursApplication.Service;
using ToursApplication.Service.Interface;
using ToursApplication.Service.Jobs;
using ToursApplication.Web.Mapper;
using Quartz;
using ToursApplication.Repository;
using ToursApplication.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
           .UseLazyLoadingProxies());
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddTransient<ITourService, TourService>();
builder.Services.AddTransient<ITravelAgencyService, TravelAgencyService>();
builder.Services.AddTransient<IGuidesService, GuidesService>();
builder.Services.AddTransient<IOffersService, OffersService>();
builder.Services.AddTransient<IBookingService, BookingService>();
builder.Services.AddScoped<ILegacyTourRepository, LegacyTourRepository>();
builder.Services.AddScoped<ITourRepository, TourRepository>();

builder.Services.AddTransient<TourMapper>();
builder.Services.AddTransient<TravelAgencyMapper>();
builder.Services.AddTransient<GuidesMapper>();
builder.Services.AddTransient<OffersMapper>();
builder.Services.AddTransient<BookingMapper>();

builder.Services.AddHostedService<BookCleanUpBackgroundService>();

builder.Services.AddQuartz(options =>
{
    var jobKey = new JobKey("tour-etl", "integration");

    options.AddJob<QuartzTourEtlJob>(job =>
        job.WithIdentity(jobKey));

    options.AddTrigger(trigger =>
    {
        trigger.ForJob(jobKey)
            .WithIdentity("tour-etl-trigger")
            .WithCronSchedule("0 0/1 * * * ?")
            .WithDescription("Synchronizes tours and offers every five minutes");
    });
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

var legacyConnectionString =
    builder.Configuration.GetConnectionString("LegacyConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'LegacyConnection' not found.");

builder.Services.AddDbContext<LegacyTourDbContext>(options =>
    options.UseSqlServer(legacyConnectionString));

using var loggerFactory = LoggerFactory.Create(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

var logger = loggerFactory.CreateLogger("Evolve");

try
{
    using var cnx = new SqliteConnection(connectionString);

    var evolve = new Evolve(cnx, msg => logger.LogInformation(msg))
    {
        Locations = new[] { "Database/Migrations" },
        IsEraseDisabled = true
    };

    evolve.Migrate();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Database migration failed.");
    throw;
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();