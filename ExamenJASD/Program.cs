var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
//memoria para session
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options =>
{
    //agrega tiempo de expiraciionn
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapGet("/", () => Results.Redirect("/Index"));

app.Run();
