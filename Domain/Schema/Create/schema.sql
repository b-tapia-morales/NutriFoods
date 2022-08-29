SET SEARCH_PATH = "nutrifoods";

DROP TABLE IF EXISTS user_allergy;
DROP TABLE IF EXISTS user_body_metrics;
DROP TABLE IF EXISTS user_profile;
DROP TABLE IF EXISTS meal_menu_recipe;
DROP TABLE IF EXISTS meal_menu;
DROP TABLE IF EXISTS meal_plan;
DROP TABLE IF EXISTS recipe_nutrient;
DROP TABLE IF EXISTS recipe_quantity;
DROP TABLE IF EXISTS recipe_measure;
DROP TABLE IF EXISTS recipe_steps;
DROP TABLE IF EXISTS recipe_diet;
DROP TABLE IF EXISTS recipe_meal_type;
DROP TABLE IF EXISTS recipe_dish_type;
DROP TABLE IF EXISTS recipe;
DROP TABLE IF EXISTS ingredient_measure;
DROP TABLE IF EXISTS ingredient_nutrient;
DROP TABLE IF EXISTS ingredient;
DROP TABLE IF EXISTS nutrient;
DROP TABLE IF EXISTS nutrient_subtype;
DROP TABLE IF EXISTS nutrient_type;
DROP TABLE IF EXISTS diet;
DROP TABLE IF EXISTS dish_type;
DROP TABLE IF EXISTS meal_type;
DROP TABLE IF EXISTS tertiary_group;
DROP TABLE IF EXISTS secondary_group;
DROP TABLE IF EXISTS primary_group;
DROP TABLE IF EXISTS meal_menu_recipe;
DROP TABLE IF EXISTS meal_menu;
DROP TABLE IF EXISTS meal_plan;

DROP EXTENSION IF EXISTS fuzzystrmatch;
DROP EXTENSION IF EXISTS pg_trgm;

DROP SCHEMA IF EXISTS nutrifoods;

CREATE SCHEMA IF NOT EXISTS nutrifoods;

SET SEARCH_PATH = "nutrifoods";

CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;
CREATE EXTENSION IF NOT EXISTS pg_trgm;

CREATE TABLE IF NOT EXISTS primary_group
(
    id   SERIAL,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS secondary_group
(
    id               SERIAL,
    name             VARCHAR(64) NOT NULL,
    primary_group_id INTEGER     NOT NULL,
    FOREIGN KEY (primary_group_id) REFERENCES primary_group (id),
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS tertiary_group
(
    id                 SERIAL      NOT NULL,
    name               VARCHAR(64) NOT NULL,
    secondary_group_id INTEGER     NOT NULL,
    FOREIGN KEY (secondary_group_id) REFERENCES secondary_group (id),
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS dish_type
(
    id   SERIAL,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS meal_type
(
    id   SERIAL,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS diet
(
    id   SERIAL,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS nutrient_type
(
    id   SERIAL,
    name VARCHAR(64) NOT NULL,
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS nutrient_subtype
(
    id              SERIAL,
    name            VARCHAR(64) NOT NULL,
    provides_energy BOOLEAN     NOT NULL,
    type_id         INTEGER     NOT NULL,
    FOREIGN KEY (type_id) REFERENCES nutrient_type (id),
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS nutrient
(
    id            SERIAL,
    name          VARCHAR(64) NOT NULL,
    also_called   VARCHAR(64),
    is_calculated BOOLEAN     NOT NULL,
    essentiality  INTEGER     NOT NULL,
    subtype_id    INTEGER     NOT NULL,
    FOREIGN KEY (subtype_id) REFERENCES nutrient_subtype (id),
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS ingredient
(
    id                SERIAL,
    name              VARCHAR(64) NOT NULL,
    is_animal         BOOLEAN     NOT NULL,
    contains_gluten   BOOLEAN     NOT NULL,
    tertiary_group_id INTEGER     NOT NULL,
    UNIQUE (name),
    FOREIGN KEY (tertiary_group_id) REFERENCES tertiary_group (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS ingredient_nutrient
(
    id            SERIAL,
    ingredient_id INTEGER          NOT NULL,
    nutrient_id   INTEGER          NOT NULL,
    quantity      DOUBLE PRECISION NOT NULL,
    unit          INTEGER          NOT NULL,
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
    FOREIGN KEY (nutrient_id) REFERENCES nutrient (id),
    PRIMARY KEY (id)
);

CREATE INDEX ingredient_nutrient_idx ON ingredient_nutrient USING btree (quantity);

CREATE TABLE IF NOT EXISTS ingredient_measure
(
    id            SERIAL,
    ingredient_id INTEGER               NOT NULL,
    name          VARCHAR(64)           NOT NULL,
    grams         DOUBLE PRECISION      NOT NULL,
    is_default    BOOLEAN DEFAULT FALSE NOT NULL,
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe
(
    id               SERIAL,
    name             TEXT        NOT NULL,
    author           VARCHAR(64) NOT NULL,
    url              TEXT,
    portions         INTEGER,
    preparation_time INTEGER,
    UNIQUE (name, author),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_dish_type
(
    id           SERIAL,
    recipe_id    INTEGER NOT NULL,
    dish_type_id INTEGER NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (dish_type_id) REFERENCES dish_type (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_meal_type
(
    id           SERIAL,
    recipe_id    INTEGER NOT NULL,
    meal_type_id INTEGER NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (meal_type_id) REFERENCES meal_type (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_diet
(
    id        SERIAL,
    recipe_id INTEGER NOT NULL,
    diet_id   INTEGER NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (diet_id) REFERENCES diet (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_steps
(
    id          SERIAL,
    recipe      INTEGER NOT NULL,
    step        INTEGER NOT NULL,
    description TEXT DEFAULT '',
    FOREIGN KEY (recipe) REFERENCES recipe (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_measure
(
    id                    SERIAL,
    recipe_id             INTEGER           NOT NULL,
    ingredient_measure_id INTEGER           NOT NULL,
    integer_part          INTEGER DEFAULT 0 NOT NULL,
    numerator             INTEGER           NOT NULL,
    denominator           INTEGER           NOT NULL,
    description           TEXT    DEFAULT '',
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (ingredient_measure_id) REFERENCES ingredient_measure (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_quantity
(
    id            SERIAL,
    recipe_id     INTEGER          NOT NULL,
    ingredient_id INTEGER          NOT NULL,
    grams         DOUBLE PRECISION NOT NULL,
    description   TEXT DEFAULT '',
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_nutrient
(
    id          SERIAL,
    recipe_id   INTEGER          NOT NULL,
    nutrient_id INTEGER          NOT NULL,
    quantity    DOUBLE PRECISION NOT NULL,
    unit        INTEGER          NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (nutrient_id) REFERENCES nutrient (id),
    PRIMARY KEY (id)
);

CREATE INDEX recipe_nutrient_idx ON recipe_nutrient USING btree (quantity);

CREATE TABLE IF NOT EXISTS meal_plan
(
    id                   SERIAL,
    meals_per_day        INTEGER          NOT NULL,
    energy_target        DOUBLE PRECISION NOT NULL,
    carbohydrates_target DOUBLE PRECISION NOT NULL,
    lipids_target        DOUBLE PRECISION NOT NULL,
    proteins_target      DOUBLE PRECISION NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS meal_menu
(
    id                  SERIAL,
    meal_plan_id        INTEGER          NOT NULL,
    meal_type_id        INTEGER          NOT NULL,
    satiety             INTEGER          NOT NULL,
    energy_total        DOUBLE PRECISION NOT NULL,
    carbohydrates_total DOUBLE PRECISION NOT NULL,
    lipids_total        DOUBLE PRECISION NOT NULL,
    proteins_total      DOUBLE PRECISION NOT NULL,
    FOREIGN KEY (meal_plan_id) REFERENCES meal_plan (id),
    FOREIGN KEY (meal_type_id) REFERENCES meal_type (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS meal_menu_recipe
(
    id           SERIAL,
    meal_menu_id INTEGER NOT NULL,
    recipe_id    INTEGER NOT NULL,
    quantity     INTEGER NOT NULL,
    FOREIGN KEY (meal_menu_id) REFERENCES meal_menu (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_profile
(
    id               SERIAL,
    username         VARCHAR(64) NOT NULL,
    email            TEXT        NOT NULL,
    password         TEXT        NOT NULL,
    api_key          TEXT        NOT NULL,
    name             VARCHAR(64),
    last_name        VARCHAR(64),
    birthdate        DATE        NOT NULL,
    gender           INTEGER     NOT NULL,
    joined_on        TIMESTAMP   NOT NULL,
    diet_id          INTEGER,
    update_frequency INTEGER,
    meal_plan_id     INTEGER,
    UNIQUE (username),
    UNIQUE (email),
    FOREIGN KEY (diet_id) REFERENCES diet (id),
    FOREIGN KEY (meal_plan_id) REFERENCES meal_plan (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_body_metrics
(
    id                      SERIAL,
    user_id                 INTEGER          NOT NULL,
    height                  INTEGER          NOT NULL,
    weight                  DOUBLE PRECISION NOT NULL,
    body_mass_index         DOUBLE PRECISION NOT NULL,
    muscle_mass_percentage  DOUBLE PRECISION,
    physical_activity_level INTEGER          NOT NULL,
    added_on                TIMESTAMP        NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user_profile (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_allergy
(
    id            SERIAL,
    user_id       INTEGER NOT NULL,
    ingredient_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user_profile (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
    PRIMARY KEY (id)
);
