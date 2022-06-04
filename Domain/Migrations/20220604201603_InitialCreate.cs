using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "nutrifoods");

            migrationBuilder.CreateTable(
                name: "diet",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dish_type",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dish_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meal_type",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nutrient_type",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutrient_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "primary_group",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_primary_group", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "recipe",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    author = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    portions = table.Column<int>(type: "integer", nullable: true),
                    preparation_time = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_profile",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    api_key = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    last_name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    joined_on = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "nutrient_subtype",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    provides_energy = table.Column<bool>(type: "boolean", nullable: false),
                    type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutrient_subtype", x => x.id);
                    table.ForeignKey(
                        name: "nutrient_subtype_type_id_fkey",
                        column: x => x.type_id,
                        principalSchema: "nutrifoods",
                        principalTable: "nutrient_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "secondary_group",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    primary_group_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secondary_group", x => x.id);
                    table.ForeignKey(
                        name: "secondary_group_primary_group_id_fkey",
                        column: x => x.primary_group_id,
                        principalSchema: "nutrifoods",
                        principalTable: "primary_group",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_diet",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_id = table.Column<int>(type: "integer", nullable: false),
                    diet_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_diet", x => x.id);
                    table.ForeignKey(
                        name: "recipe_diet_diet_id_fkey",
                        column: x => x.diet_id,
                        principalSchema: "nutrifoods",
                        principalTable: "diet",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "recipe_diet_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_dish_type",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_id = table.Column<int>(type: "integer", nullable: false),
                    dish_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_dish_type", x => x.id);
                    table.ForeignKey(
                        name: "recipe_dish_type_dish_type_id_fkey",
                        column: x => x.dish_type_id,
                        principalSchema: "nutrifoods",
                        principalTable: "dish_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "recipe_dish_type_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_meal_type",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_id = table.Column<int>(type: "integer", nullable: false),
                    meal_type_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_meal_type", x => x.id);
                    table.ForeignKey(
                        name: "recipe_meal_type_meal_type_id_fkey",
                        column: x => x.meal_type_id,
                        principalSchema: "nutrifoods",
                        principalTable: "meal_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "recipe_meal_type_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_section",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, defaultValueSql: "''::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_section", x => x.id);
                    table.ForeignKey(
                        name: "recipe_section_id_fkey",
                        column: x => x.id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "meal_plan",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    meals_per_day = table.Column<int>(type: "integer", nullable: false),
                    energy_target = table.Column<double>(type: "double precision", nullable: false),
                    carbohydrates_target = table.Column<double>(type: "double precision", nullable: false),
                    lipids_target = table.Column<double>(type: "double precision", nullable: false),
                    proteins_target = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_plan", x => x.id);
                    table.ForeignKey(
                        name: "meal_plan_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "nutrifoods",
                        principalTable: "user_profile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_body_metrics",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<double>(type: "double precision", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    body_mass_index = table.Column<double>(type: "double precision", nullable: false),
                    muscle_mass_percentage = table.Column<double>(type: "double precision", nullable: true),
                    physical_activity_level = table.Column<int>(type: "integer", nullable: false),
                    diet_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_body_metrics", x => x.id);
                    table.ForeignKey(
                        name: "user_body_metrics_diet_id_fkey",
                        column: x => x.diet_id,
                        principalSchema: "nutrifoods",
                        principalTable: "diet",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_body_metrics_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "nutrifoods",
                        principalTable: "user_profile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "nutrient",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    also_called = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    is_calculated = table.Column<bool>(type: "boolean", nullable: false),
                    essentiality = table.Column<int>(type: "integer", nullable: false),
                    subtype_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nutrient", x => x.id);
                    table.ForeignKey(
                        name: "nutrient_subtype_id_fkey",
                        column: x => x.subtype_id,
                        principalSchema: "nutrifoods",
                        principalTable: "nutrient_subtype",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tertiary_group",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    secondary_group_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tertiary_group", x => x.id);
                    table.ForeignKey(
                        name: "tertiary_group_secondary_group_id_fkey",
                        column: x => x.secondary_group_id,
                        principalSchema: "nutrifoods",
                        principalTable: "secondary_group",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "recipe_steps",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_section_id = table.Column<int>(type: "integer", nullable: false),
                    step = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true, defaultValueSql: "''::text")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recipe_steps", x => x.id);
                    table.ForeignKey(
                        name: "recipe_steps_recipe_section_id_fkey",
                        column: x => x.recipe_section_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe_section",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "meal_menu",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    meal_plan_id = table.Column<int>(type: "integer", nullable: false),
                    meal_type_id = table.Column<int>(type: "integer", nullable: false),
                    satiety = table.Column<int>(type: "integer", nullable: false),
                    energy_total = table.Column<double>(type: "double precision", nullable: false),
                    carbohydrates_total = table.Column<double>(type: "double precision", nullable: false),
                    lipids_total = table.Column<double>(type: "double precision", nullable: false),
                    proteins_total = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_menu", x => x.id);
                    table.ForeignKey(
                        name: "meal_menu_meal_plan_id_fkey",
                        column: x => x.meal_plan_id,
                        principalSchema: "nutrifoods",
                        principalTable: "meal_plan",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "meal_menu_meal_type_id_fkey",
                        column: x => x.meal_type_id,
                        principalSchema: "nutrifoods",
                        principalTable: "meal_type",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ingredient",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    is_animal = table.Column<bool>(type: "boolean", nullable: false),
                    contains_gluten = table.Column<bool>(type: "boolean", nullable: false),
                    tertiary_group_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.id);
                    table.ForeignKey(
                        name: "ingredient_tertiary_group_id_fkey",
                        column: x => x.tertiary_group_id,
                        principalSchema: "nutrifoods",
                        principalTable: "tertiary_group",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "meal_menu_recipe",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    meal_menu_id = table.Column<int>(type: "integer", nullable: false),
                    recipe_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meal_menu_recipe", x => x.id);
                    table.ForeignKey(
                        name: "meal_menu_recipe_meal_menu_id_fkey",
                        column: x => x.meal_menu_id,
                        principalSchema: "nutrifoods",
                        principalTable: "meal_menu",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "meal_menu_recipe_recipe_id_fkey",
                        column: x => x.recipe_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ingredient_quantity",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_section_id = table.Column<int>(type: "integer", nullable: false),
                    ingredient_id = table.Column<int>(type: "integer", nullable: false),
                    grams = table.Column<double>(type: "double precision", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient_quantity", x => x.id);
                    table.ForeignKey(
                        name: "ingredient_quantity_ingredient_id_fkey",
                        column: x => x.ingredient_id,
                        principalSchema: "nutrifoods",
                        principalTable: "ingredient",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ingredient_quantity_recipe_section_id_fkey",
                        column: x => x.recipe_section_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe_section",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "measure",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ingredient_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    grams = table.Column<double>(type: "double precision", nullable: false),
                    is_default = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_measure", x => x.id);
                    table.ForeignKey(
                        name: "measure_ingredient_id_fkey",
                        column: x => x.ingredient_id,
                        principalSchema: "nutrifoods",
                        principalTable: "ingredient",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_allergy",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    ingredient_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_allergy", x => x.id);
                    table.ForeignKey(
                        name: "user_allergy_ingredient_id_fkey",
                        column: x => x.ingredient_id,
                        principalSchema: "nutrifoods",
                        principalTable: "ingredient",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_allergy_user_id_fkey",
                        column: x => x.user_id,
                        principalSchema: "nutrifoods",
                        principalTable: "user_profile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ingredient_measure",
                schema: "nutrifoods",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    recipe_section_id = table.Column<int>(type: "integer", nullable: false),
                    measure_id = table.Column<int>(type: "integer", nullable: false),
                    integer_part = table.Column<int>(type: "integer", nullable: false),
                    numerator = table.Column<int>(type: "integer", nullable: false),
                    denominator = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient_measure", x => x.id);
                    table.ForeignKey(
                        name: "ingredient_measure_measure_id_fkey",
                        column: x => x.measure_id,
                        principalSchema: "nutrifoods",
                        principalTable: "measure",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "ingredient_measure_recipe_section_id_fkey",
                        column: x => x.recipe_section_id,
                        principalSchema: "nutrifoods",
                        principalTable: "recipe_section",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "diet_name_key",
                schema: "nutrifoods",
                table: "diet",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "dish_type_name_key",
                schema: "nutrifoods",
                table: "dish_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ingredient_name_key",
                schema: "nutrifoods",
                table: "ingredient",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_tertiary_group_id",
                schema: "nutrifoods",
                table: "ingredient",
                column: "tertiary_group_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_measure_measure_id",
                schema: "nutrifoods",
                table: "ingredient_measure",
                column: "measure_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_measure_recipe_section_id",
                schema: "nutrifoods",
                table: "ingredient_measure",
                column: "recipe_section_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_quantity_ingredient_id",
                schema: "nutrifoods",
                table: "ingredient_quantity",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_ingredient_quantity_recipe_section_id",
                schema: "nutrifoods",
                table: "ingredient_quantity",
                column: "recipe_section_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_menu_meal_plan_id",
                schema: "nutrifoods",
                table: "meal_menu",
                column: "meal_plan_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_menu_meal_type_id",
                schema: "nutrifoods",
                table: "meal_menu",
                column: "meal_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_menu_recipe_meal_menu_id",
                schema: "nutrifoods",
                table: "meal_menu_recipe",
                column: "meal_menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_menu_recipe_recipe_id",
                schema: "nutrifoods",
                table: "meal_menu_recipe",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_meal_plan_user_id",
                schema: "nutrifoods",
                table: "meal_plan",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "meal_type_name_key",
                schema: "nutrifoods",
                table: "meal_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "measure_ingredient_id_name_key",
                schema: "nutrifoods",
                table: "measure",
                columns: new[] { "ingredient_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nutrient_subtype_id",
                schema: "nutrifoods",
                table: "nutrient",
                column: "subtype_id");

            migrationBuilder.CreateIndex(
                name: "nutrient_also_called_key",
                schema: "nutrifoods",
                table: "nutrient",
                column: "also_called",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "nutrient_name_key",
                schema: "nutrifoods",
                table: "nutrient",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nutrient_subtype_type_id",
                schema: "nutrifoods",
                table: "nutrient_subtype",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "nutrient_subtype_name_key",
                schema: "nutrifoods",
                table: "nutrient_subtype",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "nutrient_type_name_key",
                schema: "nutrifoods",
                table: "nutrient_type",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "primary_group_name_key",
                schema: "nutrifoods",
                table: "primary_group",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "recipe_name_author_key",
                schema: "nutrifoods",
                table: "recipe",
                columns: new[] { "name", "author" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recipe_diet_diet_id",
                schema: "nutrifoods",
                table: "recipe_diet",
                column: "diet_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_diet_recipe_id",
                schema: "nutrifoods",
                table: "recipe_diet",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_dish_type_dish_type_id",
                schema: "nutrifoods",
                table: "recipe_dish_type",
                column: "dish_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_dish_type_recipe_id",
                schema: "nutrifoods",
                table: "recipe_dish_type",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_meal_type_meal_type_id",
                schema: "nutrifoods",
                table: "recipe_meal_type",
                column: "meal_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_recipe_meal_type_recipe_id",
                schema: "nutrifoods",
                table: "recipe_meal_type",
                column: "recipe_id");

            migrationBuilder.CreateIndex(
                name: "recipe_section_name_key",
                schema: "nutrifoods",
                table: "recipe_section",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_recipe_steps_recipe_section_id",
                schema: "nutrifoods",
                table: "recipe_steps",
                column: "recipe_section_id");

            migrationBuilder.CreateIndex(
                name: "IX_secondary_group_primary_group_id",
                schema: "nutrifoods",
                table: "secondary_group",
                column: "primary_group_id");

            migrationBuilder.CreateIndex(
                name: "secondary_group_name_key",
                schema: "nutrifoods",
                table: "secondary_group",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tertiary_group_secondary_group_id",
                schema: "nutrifoods",
                table: "tertiary_group",
                column: "secondary_group_id");

            migrationBuilder.CreateIndex(
                name: "tertiary_group_name_key",
                schema: "nutrifoods",
                table: "tertiary_group",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_allergy_ingredient_id",
                schema: "nutrifoods",
                table: "user_allergy",
                column: "ingredient_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_allergy_user_id",
                schema: "nutrifoods",
                table: "user_allergy",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_body_metrics_diet_id",
                schema: "nutrifoods",
                table: "user_body_metrics",
                column: "diet_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_body_metrics_user_id",
                schema: "nutrifoods",
                table: "user_body_metrics",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "user_profile_email_key",
                schema: "nutrifoods",
                table: "user_profile",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "user_profile_username_key",
                schema: "nutrifoods",
                table: "user_profile",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ingredient_measure",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "ingredient_quantity",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "meal_menu_recipe",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "nutrient",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "recipe_diet",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "recipe_dish_type",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "recipe_meal_type",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "recipe_steps",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "user_allergy",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "user_body_metrics",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "measure",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "meal_menu",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "nutrient_subtype",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "dish_type",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "recipe_section",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "diet",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "ingredient",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "meal_plan",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "meal_type",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "nutrient_type",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "recipe",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "tertiary_group",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "user_profile",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "secondary_group",
                schema: "nutrifoods");

            migrationBuilder.DropTable(
                name: "primary_group",
                schema: "nutrifoods");
        }
    }
}
