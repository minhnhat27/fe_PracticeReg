using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/SignIn";
        options.AccessDeniedPath = "/Account/Forbidden/";
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
        options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
        //options.SaveTokens = true;
    });

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequiredLength = 5;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;

//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    options.User.RequireUniqueEmail = true;

//    options.SignIn.RequireConfirmedEmail = true;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//});

//builder.Services.ConfigureApplicationCookie(options => {
//    // options.Cookie.HttpOnly = true;  
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
//    options.LoginPath = $"/login/";
//    options.LogoutPath = $"/logout/";
//    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
//});

//builder.Services.AddOptions();
//builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//builder.Services.AddSingleton<IEmailSender, SendMailService>();

//builder.Services.AddAuthentication()
//    .AddGoogle(googleOptions =>
//    {
//        IConfigurationSection ggAuthSection = builder.Configuration.GetSection("Authentication:Google");
//        googleOptions.ClientId = ggAuthSection["ClientId"]!;
//        googleOptions.ClientSecret = ggAuthSection["ClientSecret"]!;
//        googleOptions.CallbackPath = "/login-with-Google";
//    });

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
