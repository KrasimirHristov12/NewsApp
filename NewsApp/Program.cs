using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Data.Models;
using NewsApp.Data.Seeders;
using NewsApp.Filters;
using NewsApp.Models;
using NewsApp.Models.Articles;
using NewsApp.Models.Comments;
using NewsApp.Services.Articles;
using NewsApp.Services.Categories;
using NewsApp.Services.Comments;
using NewsApp.Services.Emails;
using NewsApp.Services.Files;
using NewsApp.Services.Football;
using NewsApp.Services.GeoInfoProvider;
using NewsApp.Services.Likes;
using NewsApp.Services.Mapping;
using NewsApp.Services.Weather;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = true;

    options.User.RequireUniqueEmail = true;
}).AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews(configure =>
{
    configure.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    configure.Filters.Add<BadRequestFilterAttribute>();
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<ILikesService, LikesService>();
builder.Services.AddScoped<IGeoInfoProviderService, GeoInfoProviderService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IFootballService, FootballService>();
builder.Services.AddScoped<IEmailSenderService, EmailSenderService>();
builder.Services.AddScoped<IFilesService, FilesService>();
builder.Services.AddAutoMapper(typeof(Program));

var mapperConfiguration = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<Article, DisplayArticleViewModel>();
    cfg.CreateMap<CommentsInputModel, Comment>()
                    .ForMember(c => c.OuterCommentId, opt =>
                    {
                        opt.MapFrom(vm => vm.OuterCommentId == null ? null : vm.OuterCommentId);
                    });
    cfg.CreateMap<Article, UpdateArticleInputModel>();
});
IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);




builder.Services.ConfigureApplicationCookie(configure =>
{
    configure.LoginPath = "/Users/Login";
});

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.SchemaName = "dbo";
    options.TableName = "DistributedCache";
    
});

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

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await new CategoriesSeeder().SeedAsync(dbContext);
    await new RolesSeeder(roleManager).SeedAsync(dbContext);
}
AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
        name: "areaPath",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
