using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("Admin",
        authBuilder =>
        {
            authBuilder.RequireRole("Admin");
        });

});
builder.Services.AddAntiforgery();
builder.Services.AddDbContext<ComparDbContext>();
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = false;
}
)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ComparDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();
builder.Services.AddScoped<ICourseDal, EFCourseDal>();
builder.Services.AddScoped<IContactFormDal, EFContactFormDal>();
builder.Services.AddScoped<ICourseTopicDal, EFCourseTopicDal>();
builder.Services.AddScoped<ITagDal, EFTagDal>();
builder.Services.AddScoped<IArticleImageDal, EFArticleImageDal>();
builder.Services.AddScoped<IArticleDal, EfArticleDal>();
builder.Services.AddScoped<IVideoDal, EFVideoDal>();
builder.Services.AddScoped<ISkillDal, EFSkillDal>();
builder.Services.AddScoped<ICourseToSkillDal, EFCourseToSkillDal>();
builder.Services.AddScoped<IArticleToTagDal, EfArticleToTagDal>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IContactFormService, ContactFormService>();
builder.Services.AddScoped<ICourseTopicService, CourseTopicService>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleImageService, ArticleImageService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<ICourseToSkillService, CourseToSkillService>();
builder.Services.AddScoped<IArticleToTagService, ArticleToTagService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
  );
app.MapControllerRoute(
          name: "areas",
          pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}"
        );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
