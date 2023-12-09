using API.ApplicationData;
using API.DailyMenus;
using API.DailyPlans;
using API.Dto;
using API.Ingredients;
using API.Nutritionists;
using API.Recipes;
using Domain.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;
#if DEBUG
DatabaseInitialization.Initialize();
await AbridgedRetrieval.RetrieveFromApi();
Ingredients.BatchInsert();
Recipes.BatchInsert();
NutrientCalculation.Calculate();
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<NutrifoodsDbContext>(optionsBuilder =>
{
    if (!optionsBuilder.IsConfigured)
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"),
            opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

builder.Services
    .AddScoped<IApplicationData, ApplicationData>()
    .AddScoped<IValidator<DailyMenuQuery>, DailyMenuQueryValidator>()
    .AddScoped<IValidator<DailyMenuDto>, DailyMenuValidator>()
    .AddScoped<IValidator<DailyPlanDto>, DailyPlanValidator>()
    .AddScoped<IValidator<NutritionistDto>, AccountValidator>()
    .AddScoped<IIngredientRepository, IngredientRepository>()
    .AddScoped<IRecipeRepository, RecipeRepository>()
    .AddScoped<IDailyMenuRepository, DailyMenuRepository>()
    .AddScoped<INutritionistRepository, NutritionistRepository>();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "NutriFoods",
        Description = "The official API for the NutriFoods project"
    });
});

builder.Services.ConfigureSwaggerGen(options => options.AddEnumsWithValuesFixFilters());

builder.Services.AddFluentValidationRulesToSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => { config.ConfigObject.AdditionalItems.Add("syntaxHighlight", false); });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();