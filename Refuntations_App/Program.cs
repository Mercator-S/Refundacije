using AutoMapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Refundation_App_Services.Repositories;
using Refundation_App_Services.Services;
using Refuntations_App.Areas.Identity;
using Refuntations_App.Data;
using Refuntations_App_Data.AutoMapperProfile;
using Refuntations_App_Data.Model;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<OnlineUser>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;


    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?-._@+";
    options.User.RequireUniqueEmail = true;

} )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<OnlineUser>>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddMudServices();
builder.Services.AddScoped<ICodeBookService, CodeBookService>();
builder.Services.AddScoped<IFileLoader, FileLoader>();
builder.Services.AddScoped<ICodeBookRepository, CodeBookRepository>();
builder.Services.AddScoped<IProcedureExecutor, ProcedureExecutor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var mapperConfiguration = new MapperConfiguration(configuration =>
{
    configuration.AddProfile(new FinalSettlementsProfile());
});

var mapper = mapperConfiguration.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<OnlineUser>>();
builder.Services.AddTransient<IProcedureExecutor, ProcedureExecutor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
