using BigCatCookinAPI.Services;
using BigCatCookinAPI.Services.DataAccess;
using BigCatCookinAPI.Services.DataAccess.BigCatCookinDB;
using BigCatCookinAPI.Services.DataAccess.Interface;
using BigCatCookinAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGroceryStoreService, GroceryStoreService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IGeolocationService, GeoLocationService>();

builder.Services.AddSingleton<IRecipeService, RecipeService>(); 
builder.Services.AddSingleton<IChatGPTService, ChatGPTService>();
builder.Services.AddSingleton<IBigCatCookinConfig, BigCatCookinConfig>();

//DB Services
builder.Services.AddSingleton<IBigCatCookingDb, BigCatCookingDb>();
builder.Services.AddSingleton<ICouponDatabaseService, CouponDatabaseService>();
builder.Services.AddSingleton<IGrocerDatabaseService, GrocerDatabaseService>();
builder.Services.AddSingleton<IRecipeDatabaseService, RecipeDatabaseService>();
builder.Services.AddSingleton<IUserDatabaseService, UserDatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
