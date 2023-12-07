using API.ApplicationData;
using API.DailyMenus;
using API.DailyPlans;
using API.Dto;
using API.Ingredients;
using API.Recipes;
using Domain.DatabaseInitialization;
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
using NutrientRetrieval.Averages;
using NutrientRetrieval.NutrientCalculation;
using NutrientRetrieval.Retrieval.Abridged;
using RecipeInsertion;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

#if DEBUG
DatabaseInitialization.Initialize();
await AbridgedRetrieval.RetrieveFromApi();
Ingredients.BatchInsert();
Recipes.BatchInsert();
NutrientCalculation.Calculate();
NutrientAverages.WriteStatistics();
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
}, ServiceLifetime.Singleton);

builder.Services
    .AddScoped<IValidator<DailyMenuQuery>, DailyMenuQueryValidator>()
    .AddScoped<IValidator<DailyMenuDto>, DailyMenuValidator>()
    .AddScoped<IValidator<DailyPlanDto>, DailyPlanValidator>()
    .AddScoped<IIngredientRepository, IngredientRepository>()
    .AddScoped<IRecipeRepository, RecipeRepository>()
    .AddScoped<IDailyMenuRepository, DailyMenuRepository>()
    .AddSingleton<IApplicationData, ApplicationData>();

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

builder.Services.AddFluentValidationRulesToSwagger();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config => config.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
    {
        ["activated"] = false
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true) // allow any origin
    .AllowCredentials()); // allow credentials
app.MapControllers();

app.Run();