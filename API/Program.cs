using System.Text.Json;
using System.Text.Json.Serialization;
using API.Ingredients;
using API.Recipes;
using Domain.Models;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

/*
DatabaseInitialization.Initialize();
AbridgedRetrieval.RetrieveFromApi();
Recipes.RecipeInsert();
Recipes.IngredientSynonyms();
Recipes.RecipeMeasures();
Recipes.InsertionOfRecipeData();
NutrientCalculation.Calculate();
*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NutrifoodsDbContext>(optionsBuilder =>
{
    if (!optionsBuilder.IsConfigured)
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection"),
            opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
}, ServiceLifetime.Singleton);

builder.Services
    .AddScoped<IIngredientRepository, IngredientRepository>()
    .AddScoped<IRecipeRepository, RecipeRepository>();

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews();

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
    options.AddFluentValidationRulesScoped();
});

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