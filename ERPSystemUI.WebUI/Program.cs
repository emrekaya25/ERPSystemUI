using ERPSystemUI.WebUI.ExceptionHelper;
using ERPSystemUI.WebUI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
builder.Services.AddSingleton<ResponseChecker, ResponseChecker>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/NotFound");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseApiResponseMiddleware();
app.UseSession();
app.UseSessionNullCheckMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();
app.UseCors(opt => { opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });


app.UseAuthorization();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        // 404 sayfasýný göster
        //context.Request.Path = "/Admin/ExtraPages/ErrorPages/NotFound.html"; // 404 sayfasýnýn yolunu güncelleyin
        context.Response.Redirect("/Admin/ExtraPages/ErrorPages/NotFound.html");
    }
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Kök URL'ye gelen talepleri Login'e yönlendir
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("/Login");
        return Task.CompletedTask;
    });
});

app.Run();
