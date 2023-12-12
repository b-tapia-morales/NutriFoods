using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    public virtual DbSet<Address> Addresses { get; set; } = null!;

    public virtual DbSet<AdverseFoodReaction> AdverseFoodReactions { get; set; } = null!;

    public virtual DbSet<Anthropometry> Anthropometries { get; set; } = null!;

    public virtual DbSet<ClinicalAnamnesis> ClinicalAnamneses { get; set; } = null!;

    public virtual DbSet<ClinicalSign> ClinicalSigns { get; set; } = null!;

    public virtual DbSet<Consultation> Consultations { get; set; } = null!;

    public virtual DbSet<ContactInfo> ContactInfos { get; set; } = null!;

    public virtual DbSet<DailyMenu> DailyMenus { get; set; } = null!;

    public virtual DbSet<DailyPlan> DailyPlans { get; set; } = null!;

    public virtual DbSet<Disease> Diseases { get; set; } = null!;

    public virtual DbSet<EatingSymptom> EatingSymptoms { get; set; } = null!;

    public virtual DbSet<FoodConsumption> FoodConsumptions { get; set; } = null!;

    public virtual DbSet<HarmfulHabit> HarmfulHabits { get; set; } = null!;

    public virtual DbSet<Ingestible> Ingestibles { get; set; } = null!;

    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;

    public virtual DbSet<IngredientMeasure> IngredientMeasures { get; set; } = null!;

    public virtual DbSet<MenuRecipe> MenuRecipes { get; set; } = null!;

    public virtual DbSet<NutritionalAnamnesis> NutritionalAnamneses { get; set; } = null!;

    public virtual DbSet<NutritionalTarget> NutritionalTargets { get; set; } = null!;

    public virtual DbSet<NutritionalValue> NutritionalValues { get; set; } = null!;

    public virtual DbSet<Nutritionist> Nutritionists { get; set; } = null!;

    public virtual DbSet<Patient> Patients { get; set; } = null!;

    public virtual DbSet<PersonalInfo> PersonalInfos { get; set; } = null!;

    public virtual DbSet<Recipe> Recipes { get; set; } = null!;

    public virtual DbSet<RecipeMeasure> RecipeMeasures { get; set; } = null!;

    public virtual DbSet<RecipeQuantity> RecipeQuantities { get; set; } = null!;

    public virtual DbSet<RecipeStep> RecipeSteps { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql("Host=localhost;Database=nutrifoods_db;Username=nutrifoods_dev;Password=MVmYneLqe91$",
            opt =>
            {
                opt.UseNetTopologySuite();
                opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

    [DbFunction("nutrifoods.normalize_str", IsBuiltIn = true)]
    public static string NormalizeStr(string str) => throw new NotSupportedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("nutrifoods", "btree_gist")
            .HasPostgresExtension("nutrifoods", "fuzzystrmatch")
            .HasPostgresExtension("nutrifoods", "pg_trgm")
            .HasPostgresExtension("nutrifoods", "unaccent")
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
                .OnDelete(DeleteBehavior.Cascade)
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
            entity.Property(e => e.NutritionalAnamnesisId).HasColumnName("nutritional_anamnesis_id");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.NutritionalAnamnesis).WithMany(p => p.AdverseFoodReactions)
                .HasForeignKey(d => d.NutritionalAnamnesisId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("adverse_food_reaction_nutritional_anamnesis_id_fkey");
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
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("last_updated");
            entity.Property(e => e.MuscleMassPercentage).HasColumnName("muscle_mass_percentage");
            entity.Property(e => e.WaistCircumference).HasColumnName("waist_circumference");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Anthropometry)
                .HasForeignKey<Anthropometry>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade)
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
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ClinicalAnamnesis)
                .HasForeignKey<ClinicalAnamnesis>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("clinical_sign_clinical_anamnesis_id_fkey");
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("consultation_pkey");

            entity.ToTable("consultation", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Purpose).HasColumnName("purpose");
            entity.Property(e => e.RegisteredOn)
                .HasDefaultValueSql("((now())::timestamp(0) without time zone)::date")
                .HasColumnName("registered_on");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.Patient).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("consultation_patient_id_fkey");

            entity.HasMany(d => d.DailyPlans).WithMany(p => p.Consultations)
                .UsingEntity<Dictionary<string, object>>(
                    "MealPlan",
                    r => r.HasOne<DailyPlan>().WithMany()
                        .HasForeignKey("DailyPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("meal_plan_daily_plan_id_fkey"),
                    l => l.HasOne<Consultation>().WithMany()
                        .HasForeignKey("ConsultationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("meal_plan_consultation_id_fkey"),
                    j =>
                    {
                        j.HasKey("ConsultationId", "DailyPlanId").HasName("meal_plan_pkey");
                        j.ToTable("meal_plan", "nutrifoods");
                        j.IndexerProperty<Guid>("ConsultationId").HasColumnName("consultation_id");
                        j.IndexerProperty<int>("DailyPlanId").HasColumnName("daily_plan_id");
                    });
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
                .OnDelete(DeleteBehavior.Cascade)
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
            entity.Property(e => e.MealType).HasColumnName("meal_type");

            entity.HasOne(d => d.DailyPlan).WithMany(p => p.DailyMenus)
                .HasForeignKey(d => d.DailyPlanId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("daily_menu_daily_plan_id_fkey");

            entity.HasMany(d => d.NutritionalTargets).WithMany(p => p.DailyMenus)
                .UsingEntity<Dictionary<string, object>>(
                    "DailyMenuNutritionalTarget",
                    r => r.HasOne<NutritionalTarget>().WithMany()
                        .HasForeignKey("NutritionalTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_menu_nutritional_target_nutritional_target_id_fkey"),
                    l => l.HasOne<DailyMenu>().WithMany()
                        .HasForeignKey("DailyMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_menu_nutritional_target_daily_menu_id_fkey"),
                    j =>
                    {
                        j.HasKey("DailyMenuId", "NutritionalTargetId").HasName("daily_menu_nutritional_target_pkey");
                        j.ToTable("daily_menu_nutritional_target", "nutrifoods");
                        j.IndexerProperty<int>("DailyMenuId").HasColumnName("daily_menu_id");
                        j.IndexerProperty<int>("NutritionalTargetId").HasColumnName("nutritional_target_id");
                    });

            entity.HasMany(d => d.NutritionalValues).WithMany(p => p.DailyMenus)
                .UsingEntity<Dictionary<string, object>>(
                    "DailyMenuNutritionalValue",
                    r => r.HasOne<NutritionalValue>().WithMany()
                        .HasForeignKey("NutritionalValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_menu_nutritional_value_nutritional_value_id_fkey"),
                    l => l.HasOne<DailyMenu>().WithMany()
                        .HasForeignKey("DailyMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_menu_nutritional_value_daily_menu_id_fkey"),
                    j =>
                    {
                        j.HasKey("DailyMenuId", "NutritionalValueId").HasName("daily_menu_nutritional_value_pkey");
                        j.ToTable("daily_menu_nutritional_value", "nutrifoods");
                        j.IndexerProperty<int>("DailyMenuId").HasColumnName("daily_menu_id");
                        j.IndexerProperty<int>("NutritionalValueId").HasColumnName("nutritional_value_id");
                    });
        });

        modelBuilder.Entity<DailyPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("daily_plan_pkey");

            entity.ToTable("daily_plan", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdjustmentFactor).HasColumnName("adjustment_factor");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.PhysicalActivityFactor).HasColumnName("physical_activity_factor");
            entity.Property(e => e.PhysicalActivityLevel).HasColumnName("physical_activity_level");

            entity.HasMany(d => d.NutritionalTargets).WithMany(p => p.DailyPlans)
                .UsingEntity<Dictionary<string, object>>(
                    "DailyPlanNutritionalTarget",
                    r => r.HasOne<NutritionalTarget>().WithMany()
                        .HasForeignKey("NutritionalTargetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_plan_nutritional_target_nutritional_target_id_fkey"),
                    l => l.HasOne<DailyPlan>().WithMany()
                        .HasForeignKey("DailyPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_plan_nutritional_target_daily_plan_id_fkey"),
                    j =>
                    {
                        j.HasKey("DailyPlanId", "NutritionalTargetId").HasName("daily_plan_nutritional_target_pkey");
                        j.ToTable("daily_plan_nutritional_target", "nutrifoods");
                        j.IndexerProperty<int>("DailyPlanId").HasColumnName("daily_plan_id");
                        j.IndexerProperty<int>("NutritionalTargetId").HasColumnName("nutritional_target_id");
                    });

            entity.HasMany(d => d.NutritionalValues).WithMany(p => p.DailyPlans)
                .UsingEntity<Dictionary<string, object>>(
                    "DailyPlanNutritionalValue",
                    r => r.HasOne<NutritionalValue>().WithMany()
                        .HasForeignKey("NutritionalValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_plan_nutritional_value_nutritional_value_id_fkey"),
                    l => l.HasOne<DailyPlan>().WithMany()
                        .HasForeignKey("DailyPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("daily_plan_nutritional_value_daily_plan_id_fkey"),
                    j =>
                    {
                        j.HasKey("DailyPlanId", "NutritionalValueId").HasName("daily_plan_nutritional_value_pkey");
                        j.ToTable("daily_plan_nutritional_value", "nutrifoods");
                        j.IndexerProperty<int>("DailyPlanId").HasColumnName("daily_plan_id");
                        j.IndexerProperty<int>("NutritionalValueId").HasColumnName("nutritional_value_id");
                    });
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("disease_pkey");

            entity.ToTable("disease", "nutrifoods");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nutrifoods.uuid_generate_v4()")
                .HasColumnName("id");
            entity.Property(e => e.ClinicalAnamnesisId).HasColumnName("clinical_anamnesis_id");
            entity.Property(e => e.InheritanceTypes)
                .HasDefaultValueSql("ARRAY[]::integer[]")
                .HasColumnName("inheritance_types");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name");

            entity.HasOne(d => d.ClinicalAnamnesis).WithMany(p => p.Diseases)
                .HasForeignKey(d => d.ClinicalAnamnesisId)
                .OnDelete(DeleteBehavior.Cascade)
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
            entity.Property(e => e.NutritionalAnamnesisId).HasColumnName("nutritional_anamnesis_id");
            entity.Property(e => e.Observations)
                .HasDefaultValueSql("''::text")
                .HasColumnName("observations");

            entity.HasOne(d => d.NutritionalAnamnesis).WithMany(p => p.EatingSymptoms)
                .HasForeignKey(d => d.NutritionalAnamnesisId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("eating_symptom_nutritional_anamnesis_id_fkey");
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
            entity.Property(e => e.NutritionalAnamnesisId).HasColumnName("nutritional_anamnesis_id");

            entity.HasOne(d => d.NutritionalAnamnesis).WithMany(p => p.FoodConsumptions)
                .HasForeignKey(d => d.NutritionalAnamnesisId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("food_consumption_nutritional_anamnesis_id_fkey");
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
            entity.Property(e => e.NutritionalAnamnesisId).HasColumnName("nutritional_anamnesis_id");
            entity.Property(e => e.Observations)
                .HasDefaultValueSql("''::text")
                .HasColumnName("observations");

            entity.HasOne(d => d.NutritionalAnamnesis).WithMany(p => p.HarmfulHabits)
                .HasForeignKey(d => d.NutritionalAnamnesisId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("harmful_habit_nutritional_anamnesis_id_fkey");
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
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.ClinicalAnamnesis).WithMany(p => p.Ingestibles)
                .HasForeignKey(d => d.ClinicalAnamnesisId)
                .OnDelete(DeleteBehavior.Cascade)
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

            entity.HasMany(d => d.NutritionalValues).WithMany(p => p.Ingredients)
                .UsingEntity<Dictionary<string, object>>(
                    "IngredientNutrient",
                    r => r.HasOne<NutritionalValue>().WithMany()
                        .HasForeignKey("NutritionalValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("ingredient_nutrient_nutritional_value_id_fkey"),
                    l => l.HasOne<Ingredient>().WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("ingredient_nutrient_ingredient_id_fkey"),
                    j =>
                    {
                        j.HasKey("IngredientId", "NutritionalValueId").HasName("ingredient_nutrient_pkey");
                        j.ToTable("ingredient_nutrient", "nutrifoods");
                        j.IndexerProperty<int>("IngredientId").HasColumnName("ingredient_id");
                        j.IndexerProperty<int>("NutritionalValueId").HasColumnName("nutritional_value_id");
                    });
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("ingredient_measure_ingredient_id_fkey");
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("menu_recipe_daily_menu_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.MenuRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Cascade)
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
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("last_updated");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.NutritionalAnamnesis)
                .HasForeignKey<NutritionalAnamnesis>(d => d.Id)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("nutritional_anamnesis_id_fkey");
        });

        modelBuilder.Entity<NutritionalTarget>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutritional_target_pkey");

            entity.ToTable("nutritional_target", "nutrifoods");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualError).HasColumnName("actual_error");
            entity.Property(e => e.ActualQuantity).HasColumnName("actual_quantity");
            entity.Property(e => e.ExpectedError).HasColumnName("expected_error");
            entity.Property(e => e.ExpectedQuantity).HasColumnName("expected_quantity");
            entity.Property(e => e.IsPriority).HasColumnName("is_priority");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.ThresholdType).HasColumnName("threshold_type");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<NutritionalValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("nutritional_value_pkey");

            entity.ToTable("nutritional_value", "nutrifoods");

            entity.HasIndex(e => new { e.Nutrient, e.Quantity }, "nutritional_value_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DailyValue).HasColumnName("daily_value");
            entity.Property(e => e.Nutrient).HasColumnName("nutrient");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Unit).HasColumnName("unit");
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
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
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
                .HasDefaultValueSql("(now())::timestamp(0) without time zone")
                .HasColumnName("joined_on");
            entity.Property(e => e.NutritionistId).HasColumnName("nutritionist_id");

            entity.HasOne(d => d.Nutritionist).WithMany(p => p.Patients)
                .HasForeignKey(d => d.NutritionistId)
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("personal_info_id_fkey");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("recipe_pkey");

            entity.ToTable("recipe", "nutrifoods");

            entity.HasIndex(e => new { e.Name, e.Author }, "recipe_name_author_key").IsUnique();

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
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Portions).HasColumnName("portions");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.Url).HasColumnName("url");

            entity.HasMany(d => d.NutritionalValues).WithMany(p => p.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "RecipeNutrient",
                    r => r.HasOne<NutritionalValue>().WithMany()
                        .HasForeignKey("NutritionalValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("recipe_nutrient_nutritional_value_id_fkey"),
                    l => l.HasOne<Recipe>().WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("recipe_nutrient_recipe_id_fkey"),
                    j =>
                    {
                        j.HasKey("RecipeId", "NutritionalValueId").HasName("recipe_nutrient_pkey");
                        j.ToTable("recipe_nutrient", "nutrifoods");
                        j.IndexerProperty<int>("RecipeId").HasColumnName("recipe_id");
                        j.IndexerProperty<int>("NutritionalValueId").HasColumnName("nutritional_value_id");
                    });
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recipe_measure_ingredient_measure_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeMeasures)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recipe_measure_recipe_id_fkey");
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("recipe_quantity_ingredient_id_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeQuantities)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.Cascade)
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
                .OnDelete(DeleteBehavior.Cascade)
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

        // Nutritional values
        builder.Entity<NutritionalValue>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));
        builder.Entity<NutritionalValue>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));

        // Ingredient
        builder.Entity<Ingredient>().Property(e => e.FoodGroup)
            .HasConversion(e => e.Value, e => FoodGroups.FromValue(e));

        // Recipe
        builder.Entity<Recipe>().Property(e => e.Difficulty)
            .HasConversion(e => (e ?? Difficulties.None).Value, e => Difficulties.FromValue(e));
        builder.Entity<Recipe>().Property(e => e.MealTypes)
            .HasPostgresArrayConversion(e => e.Value, e => MealTypes.FromValue(e))
            .Metadata
            .SetValueComparer(new ValueComparer<List<MealTypes>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Value)),
                c => c.ToList()));
        builder.Entity<Recipe>()
            .Property(e => e.DishTypes)
            .HasPostgresArrayConversion(e => e.Value, e => DishTypes.FromValue(e))
            .Metadata
            .SetValueComparer(new ValueComparer<List<DishTypes>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Value)),
                c => c.ToList()));

        // Nutritional Target
        builder.Entity<NutritionalTarget>().Property(e => e.Unit)
            .HasConversion(e => e.Value, e => Units.FromValue(e));
        builder.Entity<NutritionalTarget>().Property(e => e.Nutrient)
            .HasConversion(e => e.Value, e => Nutrients.FromValue(e));
        builder.Entity<NutritionalTarget>().Property(e => e.ThresholdType)
            .HasConversion(e => e.Value, e => ThresholdTypes.FromValue(e));

        // Daily Plan / Menu
        builder.Entity<DailyPlan>().Property(e => e.Day)
            .HasConversion(e => e.Value, e => Days.FromValue(e));
        builder.Entity<DailyPlan>().Property(e => e.PhysicalActivityLevel)
            .HasConversion(e => e.Value, e => PhysicalActivities.FromValue(e));

        builder.Entity<DailyMenu>().Property(e => e.MealType)
            .HasConversion(e => e.Value, e => MealTypes.FromValue(e));

        // User
        builder.Entity<PersonalInfo>().Property(e => e.BiologicalSex)
            .HasConversion(e => e.Value, e => BiologicalSexes.FromValue(e));

        builder.Entity<Address>().Property(e => e.Province)
            .HasConversion(e => e.Value, e => Provinces.FromValue(e));

        builder.Entity<Consultation>().Property(e => e.Type)
            .HasConversion(e => e.Value, e => ConsultationTypes.FromValue(e));
        builder.Entity<Consultation>().Property(e => e.Purpose)
            .HasConversion(e => e.Value, e => ConsultationPurposes.FromValue(e));

        builder.Entity<Disease>()
            .Property(e => e.InheritanceTypes)
            .HasPostgresArrayConversion(e => e.Value, e => InheritanceTypes.FromValue(e))
            .Metadata
            .SetValueComparer(new ValueComparer<List<InheritanceTypes>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Value)),
                c => c.ToList()));

        builder.Entity<Ingestible>().Property(e => e.Type)
            .HasConversion(e => e.Value, e => IngestibleTypes.FromValue(e));
        builder.Entity<Ingestible>().Property(e => e.Adherence)
            .HasConversion(e => e.Value, e => Frequencies.FromValue(e));
        builder.Entity<Ingestible>().Property(e => e.Unit)
            .HasConversion(e => (e ?? Units.None).Value, e => Units.FromValue(e));

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