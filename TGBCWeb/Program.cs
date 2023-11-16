using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using TBGC.DataAccess.Data;
using TBGC.DataAccess.Repository;
using TBGC.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Utility;
using DataAccess.DBInitializer;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDBContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDBInitializer, DbInitializer>();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
string accountSid = "AC37f20763ac3b293f908e1e0290b3dcd1";
string authToken = "c4937d899e21f3e4df66b1d65c91eccb";

TwilioClient.Init(accountSid, authToken);

var message = MessageResource.Create(
    body: "This is the ship that made the Kessel Run in fourteen parsecs?",
    from: new Twilio.Types.PhoneNumber("+18445130861"),
    to: new Twilio.Types.PhoneNumber("+14848857000")
);

Console.WriteLine(message.Sid);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
SeedDatabase();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}");

app.Run();
void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDBInitializer>();
        dbInitializer.Initialize();
    }
}