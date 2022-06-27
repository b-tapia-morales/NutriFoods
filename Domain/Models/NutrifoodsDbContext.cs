using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public class NutrifoodsDbContext : DbContext
{
    private readonly IConfiguration _configuration = null!;

    public NutrifoodsDbContext()
    {
    }

    public NutrifoodsDbContext(DbContextOptions<NutrifoodsDbContext> options)
        : base(options)
    {
    }

    public NutrifoodsDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public NutrifoodsDbContext(DbContextOptions<NutrifoodsDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Diet> Diets { get; set; } = null!;
    public virtual DbSet<DishType> DishTypes { get; set; } = null!;
    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
    public virtual DbSet<IngredientMeasure> IngredientMeasures { get; set; } = null!;
    public virtual DbSet<IngredientNutrient> IngredientNutrients { get; set; } = null!;
    public virtual DbSet<MealMenu> MealMenus { get; set; } = null!;
    public virtual DbSet<MealMenuRecipe> MealMenuRecipes { get; set; } = null!;
    public virtual DbSet<MealPlan> MealPlans { get; set; } = null!;
    public virtual DbSet<MealType> MealTypes { get; set; } = null!;
    public virtual DbSet<Nutrient> Nutrients { get; set; } = null!;
    public virtual DbSet<NutrientSubtype> NutrientSubtypes { get; set; } = null!;
    public virtual DbSet<NutrientType> NutrientTypes { get; set; } = null!;
    public virtual DbSet<PrimaryGroup> PrimaryGroups { get; set; } = null!;
    public virtual DbSet<Recipe> Recipes { get; set; } = null!;
    public virtual DbSet<RecipeDiet> RecipeDiets { get; set; } = null!;
    public virtual DbSet<RecipeDishType> RecipeDishTypes { get; set; } = null!;
    public virtual DbSet<RecipeMealType> RecipeMealTypes { get; set; } = null!;
    public virtual DbSet<RecipeMeasure> RecipeMeasures { get; set; } = null!;
    public virtual DbSet<RecipeNutrient> RecipeNutrients { get; set; } = null!;
    public virtual DbSet<RecipeQuantity> RecipeQuantities { get; set; } = null!;
    public virtual DbSet<RecipeStep> RecipeSteps { get; set; } = null!;
    public virtual DbSet<SecondaryGroup> SecondaryGroups { get; set; } = null!;
    public virtual DbSet<TertiaryGroup> TertiaryGroups { get; set; } = null!;
    public virtual DbSet<UserAllergy> UserAllergies { get; set; } = null!;
    public virtual DbSet<UserBodyMetric> UserBodyMetrics { get; set; } = null!;
    public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(
                _configuration.GetConnectionString("DatabaseConnection"),
                opt => opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Diet>(entity =>
        {
            entity.ToTable("diet", "nutrifoods");

            entity.HasIndex(e => e.Name, "diet_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<DishType>(entity =>
        {
            entity.ToTable("dish_type", "nutrifoods");

            entity.HasIndex(e => e.Name, "dish_type_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.ToTable("ingredient", "nutrifoods");

            entity.HasIndex(e => e.Name, "ingredient_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.ContainsGluten).HasColumnName("contains_gluten");

            entity.Property(e => e.IsAnimal).HasColumnName("is_animal");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.TertiaryGroupId).HasColumnName("tertiary_group_id");

            entity.HasOne(d => d.TertiaryGroup)
                .WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.TertiaryGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_tertiary_group_id_fkey");
        });

        modelBuilder.Entity<IngredientMeasure>(entity =>
        {
            entity.ToTable("ingredient_measure", "nutrifoods");

            entity.HasIndex(e => new {e.IngredientId, e.Name}, "ingredient_measure_ingredient_id_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Grams).HasColumnName("grams");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

            entity.Property(e => e.IsDefault).HasColumnName("is_default");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.HasOne(d => d.Ingredient)
                .WithMany(p => p.IngredientMeasures)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_measure_ingredient_id_fkey");
        });

        modelBuilder.Entity<IngredientNutrient>(entity =>
        {
            entity.ToTable("ingredient_nutrient", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

            entity.Property(e => e.NutrientId).HasColumnName("nutrient_id");

            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Ingredient)
                .WithMany(p => p.IngredientNutrients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_nutrient_ingredient_id_fkey");

            entity.HasOne(d => d.Nutrient)
                .WithMany(p => p.IngredientNutrients)
                .HasForeignKey(d => d.NutrientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_nutrient_nutrient_id_fkey");
        });

        modelBuilder.Entity<MealMenu>(entity =>
        {
            entity.ToTable("meal_menu", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CarbohydratesTotal).HasColumnName("carbohydrates_total");

            entity.Property(e => e.EnergyTotal).HasColumnName("energy_total");

            entity.Property(e => e.LipidsTotal).HasColumnName("lipids_total");

            entity.Property(e => e.MealPlanId).HasColumnName("meal_plan_id");

            entity.Property(e => e.MealTypeId).HasColumnName("meal_type_id");

            entity.Property(e => e.ProteinsTotal).HasColumnName("proteins_total");

            entity.Property(e => e.Satiety).HasColumnName("satiety");

            entity.HasOne(d => d.MealPlan)
                .WithMany(p => p.MealMenus)
                .HasForeignKey(d => d.MealPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("meal_menu_meal_plan_id_fkey");

            entity.HasOne(d => d.MealType)
                .WithMany(p => p.MealMenus)
                .HasForeignKey(d => d.MealTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("meal_menu_meal_type_id_fkey");
        });

        modelBuilder.Entity<MealMenuRecipe>(entity =>
        {
            entity.ToTable("meal_menu_recipe", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.MealMenuId).HasColumnName("meal_menu_id");

            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.MealMenu)
                .WithMany(p => p.MealMenuRecipes)
                .HasForeignKey(d => d.MealMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("meal_menu_recipe_meal_menu_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.MealMenuRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("meal_menu_recipe_recipe_id_fkey");
        });

        modelBuilder.Entity<MealPlan>(entity =>
        {
            entity.ToTable("meal_plan", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CarbohydratesTarget).HasColumnName("carbohydrates_target");

            entity.Property(e => e.EnergyTarget).HasColumnName("energy_target");

            entity.Property(e => e.LipidsTarget).HasColumnName("lipids_target");

            entity.Property(e => e.MealsPerDay).HasColumnName("meals_per_day");

            entity.Property(e => e.ProteinsTarget).HasColumnName("proteins_target");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.MealPlans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("meal_plan_user_id_fkey");
        });

        modelBuilder.Entity<MealType>(entity =>
        {
            entity.ToTable("meal_type", "nutrifoods");

            entity.HasIndex(e => e.Name, "meal_type_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Nutrient>(entity =>
        {
            entity.ToTable("nutrient", "nutrifoods");

            entity.HasIndex(e => e.Name, "nutrient_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.AlsoCalled)
                .HasMaxLength(64)
                .HasColumnName("also_called");

            entity.Property(e => e.Essentiality).HasColumnName("essentiality");

            entity.Property(e => e.IsCalculated).HasColumnName("is_calculated");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.SubtypeId).HasColumnName("subtype_id");

            entity.HasOne(d => d.Subtype)
                .WithMany(p => p.Nutrients)
                .HasForeignKey(d => d.SubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutrient_subtype_id_fkey");
        });

        modelBuilder.Entity<NutrientSubtype>(entity =>
        {
            entity.ToTable("nutrient_subtype", "nutrifoods");

            entity.HasIndex(e => e.Name, "nutrient_subtype_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.ProvidesEnergy).HasColumnName("provides_energy");

            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type)
                .WithMany(p => p.NutrientSubtypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutrient_subtype_type_id_fkey");
        });

        modelBuilder.Entity<NutrientType>(entity =>
        {
            entity.ToTable("nutrient_type", "nutrifoods");

            entity.HasIndex(e => e.Name, "nutrient_type_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PrimaryGroup>(entity =>
        {
            entity.ToTable("primary_group", "nutrifoods");

            entity.HasIndex(e => e.Name, "primary_group_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.ToTable("recipe", "nutrifoods");

            entity.HasIndex(e => new {e.Name, e.Author}, "recipe_name_author_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Author)
                .HasMaxLength(64)
                .HasColumnName("author");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.Portions).HasColumnName("portions");

            entity.Property(e => e.PreparationTime).HasColumnName("preparation_time");

            entity.Property(e => e.Url).HasColumnName("url");
        });

        modelBuilder.Entity<RecipeDiet>(entity =>
        {
            entity.ToTable("recipe_diet", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.DietId).HasColumnName("diet_id");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Diet)
                .WithMany(p => p.RecipeDiets)
                .HasForeignKey(d => d.DietId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_diet_diet_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeDiets)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_diet_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeDishType>(entity =>
        {
            entity.ToTable("recipe_dish_type", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.DishTypeId).HasColumnName("dish_type_id");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.DishType)
                .WithMany(p => p.RecipeDishTypes)
                .HasForeignKey(d => d.DishTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_dish_type_dish_type_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeDishTypes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_dish_type_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeMealType>(entity =>
        {
            entity.ToTable("recipe_meal_type", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.MealTypeId).HasColumnName("meal_type_id");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.MealType)
                .WithMany(p => p.RecipeMealTypes)
                .HasForeignKey(d => d.MealTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_meal_type_meal_type_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeMealTypes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_meal_type_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeMeasure>(entity =>
        {
            entity.ToTable("recipe_measure", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Denominator).HasColumnName("denominator");

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasDefaultValueSql("''::text");

            entity.Property(e => e.IngredientMeasureId).HasColumnName("ingredient_measure_id");

            entity.Property(e => e.IntegerPart).HasColumnName("integer_part");

            entity.Property(e => e.Numerator).HasColumnName("numerator");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.IngredientMeasure)
                .WithMany(p => p.RecipeMeasures)
                .HasForeignKey(d => d.IngredientMeasureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_measure_ingredient_measure_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeMeasures)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_measure_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeNutrient>(entity =>
        {
            entity.ToTable("recipe_nutrient", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.NutrientId).HasColumnName("nutrient_id");

            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Nutrient)
                .WithMany(p => p.RecipeNutrients)
                .HasForeignKey(d => d.NutrientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_nutrient_nutrient_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeNutrients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_nutrient_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeQuantity>(entity =>
        {
            entity.ToTable("recipe_quantity", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasDefaultValueSql("''::text");

            entity.Property(e => e.Grams).HasColumnName("grams");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Ingredient)
                .WithMany(p => p.RecipeQuantities)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_quantity_ingredient_id_fkey");

            entity.HasOne(d => d.Recipe)
                .WithMany(p => p.RecipeQuantities)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_quantity_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.ToTable("recipe_steps", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Description)
                .HasColumnName("description")
                .HasDefaultValueSql("''::text");

            entity.Property(e => e.Recipe).HasColumnName("recipe");

            entity.Property(e => e.Step).HasColumnName("step");

            entity.HasOne(d => d.RecipeNavigation)
                .WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.Recipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_steps_recipe_fkey");
        });

        modelBuilder.Entity<SecondaryGroup>(entity =>
        {
            entity.ToTable("secondary_group", "nutrifoods");

            entity.HasIndex(e => e.Name, "secondary_group_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.PrimaryGroupId).HasColumnName("primary_group_id");

            entity.HasOne(d => d.PrimaryGroup)
                .WithMany(p => p.SecondaryGroups)
                .HasForeignKey(d => d.PrimaryGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("secondary_group_primary_group_id_fkey");
        });

        modelBuilder.Entity<TertiaryGroup>(entity =>
        {
            entity.ToTable("tertiary_group", "nutrifoods");

            entity.HasIndex(e => e.Name, "tertiary_group_name_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.SecondaryGroupId).HasColumnName("secondary_group_id");

            entity.HasOne(d => d.SecondaryGroup)
                .WithMany(p => p.TertiaryGroups)
                .HasForeignKey(d => d.SecondaryGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tertiary_group_secondary_group_id_fkey");
        });

        modelBuilder.Entity<UserAllergy>(entity =>
        {
            entity.ToTable("user_allergy", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient)
                .WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_allergy_ingredient_id_fkey");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_allergy_user_id_fkey");
        });

        modelBuilder.Entity<UserBodyMetric>(entity =>
        {
            entity.ToTable("user_body_metrics", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.BodyMassIndex).HasColumnName("body_mass_index");

            entity.Property(e => e.DietId).HasColumnName("diet_id");

            entity.Property(e => e.Height).HasColumnName("height");

            entity.Property(e => e.MuscleMassPercentage).HasColumnName("muscle_mass_percentage");

            entity.Property(e => e.PhysicalActivityLevel).HasColumnName("physical_activity_level");

            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Diet)
                .WithMany(p => p.UserBodyMetrics)
                .HasForeignKey(d => d.DietId)
                .HasConstraintName("user_body_metrics_diet_id_fkey");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserBodyMetrics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_body_metrics_user_id_fkey");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("user_profile", "nutrifoods");

            entity.HasIndex(e => e.Email, "user_profile_email_key")
                .IsUnique();

            entity.HasIndex(e => e.Username, "user_profile_username_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.ApiKey).HasColumnName("api_key");

            entity.Property(e => e.Birthdate).HasColumnName("birthdate");

            entity.Property(e => e.Email).HasColumnName("email");

            entity.Property(e => e.Gender).HasColumnName("gender");

            entity.Property(e => e.JoinedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_on");

            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("last_name");

            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.Property(e => e.Password).HasColumnName("password");

            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrimaryGroup>().Ignore(e => e.SecondaryGroups);
        modelBuilder.Entity<SecondaryGroup>().Ignore(e => e.TertiaryGroups);
        modelBuilder.Entity<TertiaryGroup>().Ignore(e => e.Ingredients);
        modelBuilder.Entity<Diet>().Ignore(e => e.RecipeDiets).Ignore(e => e.UserBodyMetrics);
        modelBuilder.Entity<MealType>().Ignore(e => e.RecipeMealTypes).Ignore(e => e.MealMenus);
        modelBuilder.Entity<NutrientType>().Ignore(e => e.NutrientSubtypes);
        modelBuilder.Entity<NutrientSubtype>().Ignore(e => e.Nutrients);
        modelBuilder.Entity<Nutrient>().Ignore(e => e.IngredientNutrients).Ignore(e => e.RecipeNutrients);
        modelBuilder.Entity<Ingredient>().Ignore(e => e.RecipeQuantities).Ignore(e => e.UserAllergies);
        modelBuilder.Entity<Recipe>().Ignore(e => e.MealMenuRecipes);
        modelBuilder.Entity<MealPlan>().Ignore(e => e.User);
        modelBuilder.Entity<MealMenu>().Ignore(e => e.MealPlan);

        modelBuilder.Entity<Nutrient>().Property(e => e.Essentiality)
            .HasConversion(v => v.Value, v => Essentiality.FromValue(v));
        modelBuilder.Entity<IngredientNutrient>().Property(e => e.Unit)
            .HasConversion(v => v.Value, v => Unit.FromValue(v));
        modelBuilder.Entity<RecipeNutrient>().Property(e => e.Unit)
            .HasConversion(v => v.Value, v => Unit.FromValue(v));
        modelBuilder.Entity<UserProfile>().Property(e => e.Gender)
            .HasConversion(v => v.Value, v => Gender.FromValue(v));
        modelBuilder.Entity<UserBodyMetric>().Property(e => e.PhysicalActivityLevel)
            .HasConversion(v => v.Value, v => PhysicalActivity.FromValue(v));
        modelBuilder.Entity<MealMenu>().Property(e => e.Satiety)
            .HasConversion(v => v.Value, v => Satiety.FromValue(v));
    }
}