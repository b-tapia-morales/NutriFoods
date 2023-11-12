using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class NutrifoodsDbContext : DbContext
{
    public NutrifoodsDbContext()
    {
    }

    public NutrifoodsDbContext(DbContextOptions<NutrifoodsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AdverseFoodReaction> AdverseFoodReactions { get; set; }

    public virtual DbSet<Anthropometry> Anthropometries { get; set; }

    public virtual DbSet<ClinicalAnamnesis> ClinicalAnamneses { get; set; }

    public virtual DbSet<ClinicalSign> ClinicalSigns { get; set; }

    public virtual DbSet<Consultation> Consultations { get; set; }

    public virtual DbSet<ContactInfo> ContactInfos { get; set; }

    public virtual DbSet<DailyMenu> DailyMenus { get; set; }

    public virtual DbSet<DailyMenuNutrient> DailyMenuNutrients { get; set; }

    public virtual DbSet<DailyPlan> DailyPlans { get; set; }

    public virtual DbSet<DailyPlanNutrient> DailyPlanNutrients { get; set; }

    public virtual DbSet<DailyPlanTarget> DailyPlanTargets { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<EatingSymptom> EatingSymptoms { get; set; }

    public virtual DbSet<FoodConsumption> FoodConsumptions { get; set; }

    public virtual DbSet<HarmfulHabit> HarmfulHabits { get; set; }

    public virtual DbSet<Ingestible> Ingestibles { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<IngredientMeasure> IngredientMeasures { get; set; }

    public virtual DbSet<IngredientNutrient> IngredientNutrients { get; set; }

    public virtual DbSet<MealPlan> MealPlans { get; set; }

    public virtual DbSet<MenuRecipe> MenuRecipes { get; set; }

    public virtual DbSet<NutritionalAnamnesis> NutritionalAnamneses { get; set; }

    public virtual DbSet<Nutritionist> Nutritionists { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeMeasure> RecipeMeasures { get; set; }

    public virtual DbSet<RecipeNutrient> RecipeNutrients { get; set; }

    public virtual DbSet<RecipeQuantity> RecipeQuantities { get; set; }

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; }

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
            .HasPostgresExtension("nutrifoods", "pg_trgm")
            .HasPostgresExtension("nutrifoods", "uuid-ossp");

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("address_pkey");

            entity.ToTable("address", "nutrifoods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.PostalCode).HasColumnName("postal_code");
            entity.Property(e => e.Province).HasColumnName("province");
            entity.Property(e => e.Street).HasColumnName("street");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Address)
                .HasForeignKey<Address>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("address_id_fkey");
        });

        modelBuilder.Entity<AdverseFoodReaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("adverse_food_reaction_pkey");

            entity.ToTable("adverse_food_reaction", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.FoodGroup).HasColumnName("food_group");
            entity.Property(e => e.NutritionalAnamnesis).HasColumnName("nutritional_anamnesis");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.NutritionalAnamnesesNavigation).WithMany(p => p.AdverseFoodReactions)
                .HasForeignKey(d => d.NutritionalAnamnesis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("adverse_food_reaction_nutritional_anamnesis_fkey");
        });

        modelBuilder.Entity<Anthropometry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("anthropometry_pkey");

            entity.ToTable("anthropometry", "nutrifoods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bmi).HasColumnName("bmi");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(now())::date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(now())::date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.MuscleMassPercentage).HasColumnName("muscle_mass_percentage");
            entity.Property(e => e.WaistCircumference).HasColumnName("waist_circumference");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Anthropometry)
                .HasForeignKey<Anthropometry>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("anthropometry_id_fkey");
        });

        modelBuilder.Entity<ClinicalAnamnesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clinical_anamnesis_pkey");

            entity.ToTable("clinical_anamnesis", "nutrifoods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ClinicalAnamnesis)
                .HasForeignKey<ClinicalAnamnesis>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clinical_anamnesis_id_fkey");
        });

        modelBuilder.Entity<ClinicalSign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clinical_sign_pkey");

            entity.ToTable("clinical_sign", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ClinicalAnamnesisId).HasColumnName("clinical_anamnesis_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Observations)
                .HasDefaultValueSql("''::text")
                .HasColumnName("observations");

            entity.HasOne(d => d.ClinicalAnamnesis).WithMany(p => p.ClinicalSigns)
                .HasForeignKey(d => d.ClinicalAnamnesisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clinical_sign_clinical_anamnesis_id_fkey");
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("consultation_pkey");

            entity.ToTable("consultation", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.MealPlanId).HasColumnName("meal_plan_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.RegisteredOn)
                .HasDefaultValueSql("(now())::date")
                .HasColumnName("registered_on");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.MealPlan).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.MealPlanId)
                .HasConstraintName("consultation_meal_plan_id_fkey");

            entity.HasOne(d => d.Patient).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("consultation_patient_id_fkey");
        });

        modelBuilder.Entity<ContactInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contact_info_pkey");

            entity.ToTable("contact_info", "nutrifoods");

            entity.HasIndex(e => e.Email, "contact_info_email_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FixedPhone)
                .HasMaxLength(16)
                .HasColumnName("fixed_phone");
            entity.Property(e => e.MobilePhone)
                .HasMaxLength(16)
                .HasColumnName("mobile_phone");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ContactInfo)
                .HasForeignKey<ContactInfo>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_info_id_fkey");
        });

        modelBuilder.Entity<DailyMenu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_menu_pkey");

            entity.ToTable("daily_menu", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DailyPlanId).HasColumnName("daily_plan_id");
            entity.Property(e => e.Hour)
                .HasMaxLength(8)
                .HasColumnName("hour");
            entity.Property(e => e.IntakePercentage).HasColumnName("intake_percentage");
            entity.Property(e => e.MealTypes).HasColumnName("meal_type");

            entity.HasOne(d => d.DailyPlan).WithMany(p => p.DailyMenus)
                .HasForeignKey(d => d.DailyPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daily_menu_daily_plan_id_fkey");
        });

        modelBuilder.Entity<DailyMenuNutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_menu_nutrient_pkey");

            entity.ToTable("daily_menu_nutrient", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DailyMenuId).HasColumnName("daily_menu_id");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.DailyMenu).WithMany(p => p.DailyMenuNutrients)
                .HasForeignKey(d => d.DailyMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daily_menu_nutrient_daily_menu_id_fkey");
        });

        modelBuilder.Entity<DailyPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_plan_pkey");

            entity.ToTable("daily_plan", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdjustmentFactor).HasColumnName("adjustment_factor");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.MealPlanId).HasColumnName("meal_plan_id");
            entity.Property(e => e.PhysicalActivityFactor).HasColumnName("physical_activity_factor");
            entity.Property(e => e.PhysicalActivityLevel).HasColumnName("physical_activity_level");

            entity.HasOne(d => d.MealPlan).WithMany(p => p.DailyPlans)
                .HasForeignKey(d => d.MealPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daily_plan_meal_plan_id_fkey");
        });

        modelBuilder.Entity<DailyPlanNutrient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_plan_nutrient_pkey");

            entity.ToTable("daily_plan_nutrient", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DailyPlanId).HasColumnName("daily_plan_id");
            entity.Property(e => e.ErrorMargin).HasColumnName("error_margin");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.DailyPlan).WithMany(p => p.DailyPlanNutrients)
                .HasForeignKey(d => d.DailyPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daily_plan_nutrient_daily_plan_id_fkey");
        });

        modelBuilder.Entity<DailyPlanTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_plan_target_pkey");

            entity.ToTable("daily_plan_target", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DailyPlanId).HasColumnName("daily_plan_id");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ThresholdType).HasColumnName("threshold_type");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.DailyPlan).WithMany(p => p.DailyPlanTargets)
                .HasForeignKey(d => d.DailyPlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("daily_plan_target_daily_plan_id_fkey");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("disease_pkey");

            entity.ToTable("disease", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ClinicalAnamnesisId).HasColumnName("clinical_anamnesis_id");
            entity.Property(e => e.InheritanceType).HasColumnName("inheritance_type");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.HasOne(d => d.ClinicalAnamnesis).WithMany(p => p.Diseases)
                .HasForeignKey(d => d.ClinicalAnamnesisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("disease_clinical_anamnesis_id_fkey");
        });

        modelBuilder.Entity<EatingSymptom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eating_symptom_pkey");

            entity.ToTable("eating_symptom", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.NutritionalAnamnesis).HasColumnName("nutritional_anamnesis");
            entity.Property(e => e.Observations)
                .HasDefaultValueSql("''::text")
                .HasColumnName("observations");

            entity.HasOne(d => d.NutritionalAnamnesesNavigation).WithMany(p => p.EatingSymptoms)
                .HasForeignKey(d => d.NutritionalAnamnesis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eating_symptom_nutritional_anamnesis_fkey");
        });

        modelBuilder.Entity<FoodConsumption>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("food_consumption_pkey");

            entity.ToTable("food_consumption", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.FoodGroup).HasColumnName("food_group");
            entity.Property(e => e.Frequency).HasColumnName("frequency");
            entity.Property(e => e.NutritionalAnamnesis).HasColumnName("nutritional_anamnesis");

            entity.HasOne(d => d.NutritionalAnamnesesNavigation).WithMany(p => p.FoodConsumptions)
                .HasForeignKey(d => d.NutritionalAnamnesis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("food_consumption_nutritional_anamnesis_fkey");
        });

        modelBuilder.Entity<HarmfulHabit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("harmful_habit_pkey");

            entity.ToTable("harmful_habit", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.NutritionalAnamnesis).HasColumnName("nutritional_anamnesis");
            entity.Property(e => e.Observations)
                .HasDefaultValueSql("''::text")
                .HasColumnName("observations");

            entity.HasOne(d => d.NutritionalAnamnesesNavigation).WithMany(p => p.HarmfulHabits)
                .HasForeignKey(d => d.NutritionalAnamnesis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("harmful_habit_nutritional_anamnesis_fkey");
        });

        modelBuilder.Entity<Ingestible>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingestible_pkey");

            entity.ToTable("ingestible", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Adherence).HasColumnName("adherence");
            entity.Property(e => e.AdministrationTimes)
                .HasDefaultValueSql("ARRAY[]::character varying[]")
                .HasColumnType("character varying(8)[]")
                .HasColumnName("administration_times");
            entity.Property(e => e.ClinicalAnamnesisId).HasColumnName("clinical_anamnesis_id");
            entity.Property(e => e.Dosage).HasColumnName("dosage");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Observations)
                .HasDefaultValueSql("''::text")
                .HasColumnName("observations");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.ClinicalAnamnesis).WithMany(p => p.Ingestibles)
                .HasForeignKey(d => d.ClinicalAnamnesisId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingestible_clinical_anamnesis_id_fkey");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_pkey");

            entity.ToTable("ingredient", "nutrifoods");

            entity.HasIndex(e => e.Name, "ingredient_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FoodGroup).HasColumnName("food_group");
            entity.Property(e => e.IsAnimal).HasColumnName("is_animal");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Synonyms)
                .HasDefaultValueSql("ARRAY[]::character varying[]")
                .HasColumnType("character varying(64)[]")
                .HasColumnName("synonyms");
        });

        modelBuilder.Entity<IngredientMeasure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingredient_measure_pkey");

            entity.ToTable("ingredient_measure", "nutrifoods");

            entity.HasIndex(e => new { e.Name, e.IngredientId }, "ingredient_measure_name_ingredient_id_key")
                .IsUnique();

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

            entity.HasIndex(e => new { e.Nutrient, e.IngredientId }, "ingredient_nutrient_nutrient_ingredient_id_key")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.IngredientNutrients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingredient_nutrient_ingredient_id_fkey");
        });

        modelBuilder.Entity<MealPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("meal_plan_pkey");

            entity.ToTable("meal_plan", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.MealsPerDay).HasColumnName("meals_per_day");
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

        modelBuilder.Entity<NutritionalAnamnesis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutritional_anamnesis_pkey");

            entity.ToTable("nutritional_anamnesis", "nutrifoods");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(now())::date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.NutritionalAnamnesis)
                .HasForeignKey<NutritionalAnamnesis>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("nutritional_anamnesis_id_fkey");
        });

        modelBuilder.Entity<Nutritionist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutritionist_pkey");

            entity.ToTable("nutritionist", "nutrifoods");

            entity.HasIndex(e => e.Email, "nutritionist_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "nutritionist_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.JoinedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_on");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_pkey");

            entity.ToTable("patient", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.JoinedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("joined_on");
            entity.Property(e => e.NutritionistId).HasColumnName("nutritionist_id");

            entity.HasOne(d => d.Nutritionist).WithMany(p => p.Patients)
                .HasForeignKey(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_nutritionist_id_fkey");
        });

        modelBuilder.Entity<PersonalInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("personal_info_pkey");

            entity.ToTable("personal_info", "nutrifoods");

            entity.HasIndex(e => e.Rut, "personal_info_rut_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BiologicalSex).HasColumnName("biological_sex");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.LastNames)
                .HasMaxLength(50)
                .HasColumnName("last_names");
            entity.Property(e => e.Names)
                .HasMaxLength(50)
                .HasColumnName("names");
            entity.Property(e => e.Rut)
                .HasMaxLength(16)
                .HasColumnName("rut");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.PersonalInfo)
                .HasForeignKey<PersonalInfo>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("personal_info_id_fkey");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_pkey");

            entity.ToTable("recipe", "nutrifoods");

            entity.HasIndex(e => e.Url, "recipe_url_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(64)
                .HasColumnName("author");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.DishTypes)
                .HasDefaultValueSql("ARRAY[]::integer[]")
                .HasColumnName("dish_types");
            entity.Property(e => e.MealTypes)
                .HasDefaultValueSql("ARRAY[]::integer[]")
                .HasColumnName("meal_types");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");
            entity.Property(e => e.Portions).HasColumnName("portions");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Url).HasColumnName("url");
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

            entity.HasIndex(e => new { e.RecipeId, e.Nutrient }, "recipe_nutrient_recipe_id_nutrient_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.Unit).HasColumnName("unit");

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
            entity.HasKey(e => e.Id).HasName("recipe_step_pkey");

            entity.ToTable("recipe_step", "nutrifoods");

            entity.HasIndex(e => new { e.RecipeId, e.Number }, "recipe_step_recipe_id_number_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeSteps)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recipe_step_recipe_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static void OnModelCreatingPartial(ModelBuilder builder)
    {
        // Ignores
        builder.Entity<Ingredient>().Ignore(e => e.RecipeQuantities);
        builder.Entity<IngredientMeasure>().Ignore(e => e.RecipeMeasures);
        builder.Entity<Recipe>().Ignore(e => e.MenuRecipes);
        builder.Entity<MealPlan>().Ignore(e => e.Consultations);

        // Ingredient
        builder.Entity<Ingredient>().Property(e => e.FoodGroup)
            .HasConversion(e => e.Value, e => FoodGroups.FromValue(e));
        builder.Entity<IngredientNutrient>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));
        builder.Entity<IngredientNutrient>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));

        // Recipe
        builder.Entity<Recipe>().Property(e => e.Difficulty)
            .HasConversion(e => (e ?? Difficulties.None).Value, e => Difficulties.FromValue(e));
        builder.Entity<Recipe>().Property(e => e.MealTypes)
            .HasConversion(e => e.Select(x => x.Value), e => e.Select(MealTypes.FromValue).ToArray());
        builder.Entity<Recipe>().Property(e => e.DishTypes)
            .HasConversion(e => e.Select(x => x.Value), e => e.Select(DishTypes.FromValue).ToArray());
        builder.Entity<RecipeNutrient>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));
        builder.Entity<RecipeNutrient>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));

        // Meal Plan
        builder.Entity<DailyPlan>().Property(e => e.Day)
            .HasConversion(e => e.Value, e => Days.FromValue(e));
        builder.Entity<DailyPlan>().Property(e => e.PhysicalActivityLevel)
            .HasConversion(e => e.Value, e => PhysicalActivities.FromValue(e));
        builder.Entity<DailyPlanTarget>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));
        builder.Entity<DailyPlanTarget>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));
        builder.Entity<DailyPlanTarget>().Property(e => e.ThresholdType)
            .HasConversion(e => e.Value, e => ThresholdTypes.FromValue(e));
        builder.Entity<DailyPlanNutrient>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));
        builder.Entity<DailyPlanNutrient>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));
        builder.Entity<DailyMenu>().Property(e => e.MealTypes)
            .HasConversion(e => e.Value, e => MealTypes.FromValue(e));
        builder.Entity<DailyMenuNutrient>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));
        builder.Entity<DailyMenuNutrient>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));

        // User
        builder.Entity<PersonalInfo>().Property(e => e.BiologicalSex)
            .HasConversion(e => e.Value, e => BiologicalSexes.FromValue(e));
        builder.Entity<Address>().Property(e => e.Province)
            .HasConversion(e => e.Value, e => Provinces.FromValue(e));
        builder.Entity<Consultation>().Property(e => e.Type)
            .HasConversion(e => e.Value, e => ConsultationTypes.FromValue(e));
        builder.Entity<Consultation>().Property(e => e.Purpose)
            .HasConversion(e => e.Value, e => ConsultationPurposes.FromValue(e));
        builder.Entity<Disease>().Property(e => e.InheritanceType)
            .HasConversion(e => e.Value, e => InheritanceTypes.FromValue(e));
        builder.Entity<Ingestible>().Property(e => e.Type)
            .HasConversion(e => e.Value, e => IngestibleTypes.FromValue(e));
        builder.Entity<Ingestible>().Property(e => e.Adherence)
            .HasConversion(e => e.Value, e => Frequencies.FromValue(e));
        builder.Entity<AdverseFoodReaction>().Property(e => e.FoodGroup)
            .HasConversion(e => e.Value, e => FoodGroups.FromValue(e));
        builder.Entity<AdverseFoodReaction>().Property(e => e.Type)
            .HasConversion(e => e.Value, e => FoodReactions.FromValue(e));
        builder.Entity<FoodConsumption>().Property(e => e.FoodGroup)
            .HasConversion(e => e.Value, e => FoodGroups.FromValue(e));
        builder.Entity<FoodConsumption>().Property(e => e.Frequency)
            .HasConversion(e => e.Value, e => Frequencies.FromValue(e));
    }
}