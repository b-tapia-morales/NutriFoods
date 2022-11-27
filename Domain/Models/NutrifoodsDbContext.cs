using Microsoft.EntityFrameworkCore;
using Utils.Enum;

namespace Domain.Models;

public class NutrifoodsDbContext : DbContext
{
    public NutrifoodsDbContext()
    {
    }

    public NutrifoodsDbContext(DbContextOptions<NutrifoodsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DailyMealPlan> DailyMealPlans { get; set; } = null!;

    public virtual DbSet<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;

    public virtual DbSet<IngredientMeasure> IngredientMeasures { get; set; } = null!;

    public virtual DbSet<IngredientNutrient> IngredientNutrients { get; set; } = null!;

    public virtual DbSet<IngredientSynonym> IngredientSynonyms { get; set; } = null!;

    public virtual DbSet<MealPlan> MealPlans { get; set; } = null!;

    public virtual DbSet<MenuRecipe> MenuRecipes { get; set; } = null!;

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

    public virtual DbSet<UserData> UserData { get; set; } = null!;

    public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$",
            opt =>
            {
                opt.UseNetTopologySuite();
                opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("nutrifoods", "btree_gist")
            .HasPostgresExtension("nutrifoods", "fuzzystrmatch")
            .HasPostgresExtension("nutrifoods", "pg_trgm");

        modelBuilder.Entity<DailyMealPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_meal_plan_pkey");

            entity.ToTable("daily_meal_plan", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarbohydratesTotal).HasColumnName("carbohydrates_total");
            entity.Property(e => e.DayOfTheWeek).HasColumnName("day_of_the_week");
            entity.Property(e => e.EnergyTotal).HasColumnName("energy_total");
            entity.Property(e => e.LipidsTotal).HasColumnName("lipids_total");
            entity.Property(e => e.MealPlanId).HasColumnName("meal_plan_id");
            entity.Property(e => e.ProteinsTotal).HasColumnName("proteins_total");

            entity.HasOne(d => d.MealPlan).WithMany(p => p.DailyMealPlans)
                .HasForeignKey(d => d.MealPlanId)
                .HasConstraintName("daily_meal_plan_meal_plan_id_fkey");
        });

        modelBuilder.Entity<DailyMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_menu_pkey");

            entity.ToTable("daily_menu", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarbohydratesTotal).HasColumnName("carbohydrates_total");
            entity.Property(e => e.DailyMealPlan).HasColumnName("daily_meal_plan");
            entity.Property(e => e.EnergyTotal).HasColumnName("energy_total");
            entity.Property(e => e.LipidsTotal).HasColumnName("lipids_total");
            entity.Property(e => e.MealType).HasColumnName("meal_type");
            entity.Property(e => e.ProteinsTotal).HasColumnName("proteins_total");
            entity.Property(e => e.Satiety).HasColumnName("satiety");

            entity.HasOne(d => d.DailyMealPlanNavigation).WithMany(p => p.DailyMenus)
                .HasForeignKey(d => d.DailyMealPlan)
                .HasConstraintName("daily_menu_daily_meal_plan_fkey");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_pkey");

            entity.ToTable("ingredient", "nutrifoods");

            entity.HasIndex(e => e.Name, "ingredient_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContainsGluten).HasColumnName("contains_gluten");
            entity.Property(e => e.IsAnimal).HasColumnName("is_animal");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.TertiaryGroupId).HasColumnName("tertiary_group_id");

            entity.HasOne(d => d.TertiaryGroup).WithMany(p => p.Ingredients)
                .HasForeignKey(d => d.TertiaryGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_tertiary_group_id_fkey");
        });

        modelBuilder.Entity<IngredientMeasure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_measure_pkey");

            entity.ToTable("ingredient_measure", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Grams).HasColumnName("grams");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientMeasures)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_measure_ingredient_id_fkey");
        });

        modelBuilder.Entity<IngredientNutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_nutrient_pkey");

            entity.ToTable("ingredient_nutrient", "nutrifoods");

            entity.HasIndex(e => e.Quantity, "ingredient_nutrient_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.NutrientId).HasColumnName("nutrient_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientNutrients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_nutrient_ingredient_id_fkey");

            entity.HasOne(d => d.Nutrient).WithMany(p => p.IngredientNutrients)
                .HasForeignKey(d => d.NutrientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_nutrient_nutrient_id_fkey");
        });

        modelBuilder.Entity<IngredientSynonym>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_synonym_pkey");

            entity.ToTable("ingredient_synonym", "nutrifoods");

            entity.HasIndex(e => e.Name, "ingredient_synonym_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientSynonyms)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_synonym_ingredient_id_fkey");
        });

        modelBuilder.Entity<MealPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("meal_plan_pkey");

            entity.ToTable("meal_plan", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarbohydratesTarget).HasColumnName("carbohydrates_target");
            entity.Property(e => e.EnergyTarget).HasColumnName("energy_target");
            entity.Property(e => e.LipidsTarget).HasColumnName("lipids_target");
            entity.Property(e => e.ProteinsTarget).HasColumnName("proteins_target");
        });

        modelBuilder.Entity<MenuRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menu_recipe_pkey");

            entity.ToTable("menu_recipe", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DailyMenuId).HasColumnName("daily_menu_id");
            entity.Property(e => e.Portions).HasColumnName("portions");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.DailyMenu).WithMany(p => p.MenuRecipes)
                .HasForeignKey(d => d.DailyMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_recipe_daily_menu_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.MenuRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menu_recipe_recipe_id_fkey");
        });

        modelBuilder.Entity<Nutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutrient_pkey");

            entity.ToTable("nutrient", "nutrifoods");

            entity.HasIndex(e => e.Name, "nutrient_name_key").IsUnique();

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

            entity.HasOne(d => d.Subtype).WithMany(p => p.Nutrients)
                .HasForeignKey(d => d.SubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutrient_subtype_id_fkey");
        });

        modelBuilder.Entity<NutrientSubtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutrient_subtype_pkey");

            entity.ToTable("nutrient_subtype", "nutrifoods");

            entity.HasIndex(e => e.Name, "nutrient_subtype_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.ProvidesEnergy).HasColumnName("provides_energy");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.NutrientSubtypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutrient_subtype_type_id_fkey");
        });

        modelBuilder.Entity<NutrientType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutrient_type_pkey");

            entity.ToTable("nutrient_type", "nutrifoods");

            entity.HasIndex(e => e.Name, "nutrient_type_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PrimaryGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("primary_group_pkey");

            entity.ToTable("primary_group", "nutrifoods");

            entity.HasIndex(e => e.Name, "primary_group_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_pkey");

            entity.ToTable("recipe", "nutrifoods");

            entity.HasIndex(e => new { e.Name, e.Author }, "recipe_name_author_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(64)
                .HasColumnName("author");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Portions).HasColumnName("portions");
            entity.Property(e => e.PreparationTime).HasColumnName("preparation_time");
            entity.Property(e => e.Url).HasColumnName("url");
        });

        modelBuilder.Entity<RecipeDiet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_diet_pkey");

            entity.ToTable("recipe_diet", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diet).HasColumnName("diet");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeDiets)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_diet_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeDishType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_dish_type_pkey");

            entity.ToTable("recipe_dish_type", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DishType).HasColumnName("dish_type");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeDishTypes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_dish_type_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeMealType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_meal_type_pkey");

            entity.ToTable("recipe_meal_type", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MealType).HasColumnName("meal_type");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeMealTypes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_meal_type_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeMeasure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_measure_pkey");

            entity.ToTable("recipe_measure", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Denominator).HasColumnName("denominator");
            entity.Property(e => e.IngredientMeasureId).HasColumnName("ingredient_measure_id");
            entity.Property(e => e.IntegerPart).HasColumnName("integer_part");
            entity.Property(e => e.Numerator).HasColumnName("numerator");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.IngredientMeasure).WithMany(p => p.RecipeMeasures)
                .HasForeignKey(d => d.IngredientMeasureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_measure_ingredient_measure_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeMeasures)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_measure_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeNutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_nutrient_pkey");

            entity.ToTable("recipe_nutrient", "nutrifoods");

            entity.HasIndex(e => e.Quantity, "recipe_nutrient_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NutrientId).HasColumnName("nutrient_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Nutrient).WithMany(p => p.RecipeNutrients)
                .HasForeignKey(d => d.NutrientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_nutrient_nutrient_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeNutrients)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_nutrient_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeQuantity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_quantity_pkey");

            entity.ToTable("recipe_quantity", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Grams).HasColumnName("grams");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.RecipeQuantities)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_quantity_ingredient_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeQuantities)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_quantity_recipe_id_fkey");
        });

        modelBuilder.Entity<RecipeStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_steps_pkey");

            entity.ToTable("recipe_steps", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasDefaultValueSql("''::text")
                .HasColumnName("description");
            entity.Property(e => e.Recipe).HasColumnName("recipe");
            entity.Property(e => e.Step).HasColumnName("step");

            entity.HasOne(d => d.RecipeNavigation).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.Recipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_steps_recipe_fkey");
        });

        modelBuilder.Entity<SecondaryGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("secondary_group_pkey");

            entity.ToTable("secondary_group", "nutrifoods");

            entity.HasIndex(e => e.Name, "secondary_group_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.PrimaryGroupId).HasColumnName("primary_group_id");

            entity.HasOne(d => d.PrimaryGroup).WithMany(p => p.SecondaryGroups)
                .HasForeignKey(d => d.PrimaryGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("secondary_group_primary_group_id_fkey");
        });

        modelBuilder.Entity<TertiaryGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tertiary_group_pkey");

            entity.ToTable("tertiary_group", "nutrifoods");

            entity.HasIndex(e => e.Name, "tertiary_group_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.SecondaryGroupId).HasColumnName("secondary_group_id");

            entity.HasOne(d => d.SecondaryGroup).WithMany(p => p.TertiaryGroups)
                .HasForeignKey(d => d.SecondaryGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tertiary_group_secondary_group_id_fkey");
        });

        modelBuilder.Entity<UserAllergy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_allergy_pkey");

            entity.ToTable("user_allergy", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_allergy_ingredient_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserAllergies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_allergy_user_id_fkey");
        });

        modelBuilder.Entity<UserBodyMetric>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_body_metrics_pkey");

            entity.ToTable("user_body_metrics", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_on");
            entity.Property(e => e.BodyMassIndex).HasColumnName("body_mass_index");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.PhysicalActivity).HasColumnName("physical_activity");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.UserBodyMetrics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_body_metrics_user_id_fkey");
        });

        modelBuilder.Entity<UserData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_data_pkey");

            entity.ToTable("user_data", "nutrifoods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Diet).HasColumnName("diet");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.IntendedUse).HasColumnName("intended_use");
            entity.Property(e => e.LastName)
                .HasMaxLength(64)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.UpdateFrequency).HasColumnName("update_frequency");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserData)
                .HasForeignKey<UserData>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_data_id_fkey");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_profile_pkey");

            entity.ToTable("user_profile", "nutrifoods");

            entity.HasIndex(e => e.Email, "user_profile_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "user_profile_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ApiKey).HasColumnName("api_key");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.JoinedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_on");
            entity.Property(e => e.MealPlanId).HasColumnName("meal_plan_id");
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .HasColumnName("username");

            entity.HasOne(d => d.MealPlan).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.MealPlanId)
                .HasConstraintName("user_profile_meal_plan_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrimaryGroup>().Ignore(e => e.SecondaryGroups);
        modelBuilder.Entity<SecondaryGroup>().Ignore(e => e.TertiaryGroups);
        modelBuilder.Entity<TertiaryGroup>().Ignore(e => e.Ingredients);
        modelBuilder.Entity<NutrientType>().Ignore(e => e.NutrientSubtypes);
        modelBuilder.Entity<NutrientSubtype>().Ignore(e => e.Nutrients);
        modelBuilder.Entity<Nutrient>().Ignore(e => e.IngredientNutrients).Ignore(e => e.RecipeNutrients);
        modelBuilder.Entity<Ingredient>().Ignore(e => e.RecipeQuantities).Ignore(e => e.UserAllergies);
        modelBuilder.Entity<Recipe>().Ignore(e => e.MenuRecipes);
        modelBuilder.Entity<MealPlan>().Ignore(e => e.UserProfiles);
        modelBuilder.Entity<DailyMealPlan>().Ignore(e => e.MealPlan);
        modelBuilder.Entity<DailyMenu>().Ignore(e => e.DailyMealPlan);
        modelBuilder.Entity<MenuRecipe>().Ignore(e => e.DailyMenu);

        modelBuilder.Entity<Nutrient>().Property(e => e.Essentiality)
            .HasConversion(v => v.Value, v => EssentialityEnum.FromValue(v));

        modelBuilder.Entity<IngredientNutrient>().Property(e => e.Unit)
            .HasConversion(v => v.Value, v => UnitEnum.FromValue(v));

        modelBuilder.Entity<RecipeNutrient>().Property(e => e.Unit)
            .HasConversion(v => v.Value, v => UnitEnum.FromValue(v));
        modelBuilder.Entity<RecipeDiet>().Property(e => e.Diet)
            .HasConversion(v => v.Value, v => DietEnum.FromValue(v));
        modelBuilder.Entity<RecipeMealType>().Property(e => e.MealType)
            .HasConversion(v => v.Value, v => MealTypeEnum.FromValue(v));
        modelBuilder.Entity<RecipeDishType>().Property(e => e.DishType)
            .HasConversion(v => v.Value, v => DishTypeEnum.FromValue(v));

        modelBuilder.Entity<DailyMealPlan>().Property(e => e.DayOfTheWeek)
            .HasConversion(v => v.Value, v => DayOfWeekEnum.FromValue(v));

        modelBuilder.Entity<DailyMenu>().Property(e => e.Satiety)
            .HasConversion(v => v.Value, v => SatietyEnum.FromValue(v));
        modelBuilder.Entity<DailyMenu>().Property(e => e.MealType)
            .HasConversion(v => v.Value, v => MealTypeEnum.FromValue(v));

        modelBuilder.Entity<UserData>().Property(e => e.Gender)
            .HasConversion(v => v.Value, v => GenderEnum.FromValue(v));
        modelBuilder.Entity<UserData>().Property(e => e.Diet)
            .HasConversion(v => v!.Value, v => DietEnum.FromValue(v));
        modelBuilder.Entity<UserData>().Property(e => e.IntendedUse)
            .HasConversion(v => v!.Value, v => IntendedUseEnum.FromValue(v));
        modelBuilder.Entity<UserData>().Property(e => e.UpdateFrequency)
            .HasConversion(v => v!.Value, v => UpdateFrequencyEnum.FromValue(v));
        modelBuilder.Entity<UserBodyMetric>().Property(e => e.PhysicalActivity)
            .HasConversion(v => v.Value, v => PhysicalActivityEnum.FromValue(v));
    }
}
