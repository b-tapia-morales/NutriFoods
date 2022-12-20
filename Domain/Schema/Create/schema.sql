SET SEARCH_PATH = "nutrifoods";

DROP SCHEMA IF EXISTS nutrifoods CASCADE;

CREATE SCHEMA IF NOT EXISTS nutrifoods;

SET SEARCH_PATH = "nutrifoods";

CREATE EXTENSION btree_gist;
CREATE EXTENSION IF NOT EXISTS pg_trgm;
CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;

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
    contains_gluten   BOOLEAN     NOT NULL DEFAULT FALSE,
    tertiary_group_id INTEGER     NOT NULL,
    UNIQUE (name),
    FOREIGN KEY (tertiary_group_id) REFERENCES tertiary_group (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS ingredient_synonym
(
    id            SERIAL,
    name          VARCHAR(64) NOT NULL,
    ingredient_id INTEGER     NOT NULL,
    UNIQUE (name),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
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
    ingredient_id INTEGER          NOT NULL,
    name          VARCHAR(64)      NOT NULL,
    grams         DOUBLE PRECISION NOT NULL,
    is_default    BOOLEAN          NOT NULL DEFAULT FALSE,
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
    id        SERIAL,
    recipe_id INTEGER NOT NULL,
    dish_type INTEGER NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_meal_type
(
    id        SERIAL,
    recipe_id INTEGER NOT NULL,
    meal_type INTEGER NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_diet
(
    id        SERIAL,
    recipe_id INTEGER NOT NULL,
    diet      INTEGER NOT NULL,
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
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
    energy_target        DOUBLE PRECISION NOT NULL,
    carbohydrates_target DOUBLE PRECISION NOT NULL,
    lipids_target        DOUBLE PRECISION NOT NULL,
    proteins_target      DOUBLE PRECISION NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS daily_meal_plan
(
    id                  SERIAL,
    meal_plan_id        INTEGER,
    day_of_the_week     INTEGER          NOT NULL,
    energy_total        DOUBLE PRECISION NOT NULL,
    carbohydrates_total DOUBLE PRECISION NOT NULL,
    lipids_total        DOUBLE PRECISION NOT NULL,
    proteins_total      DOUBLE PRECISION NOT NULL,
    FOREIGN KEY (meal_plan_id) REFERENCES meal_plan (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS daily_meal_plan_nutrient
(
    id                 SERIAL,
    daily_meal_plan_id INTEGER          NOT NULL,
    nutrient_id        INTEGER          NOT NULL,
    quantity           DOUBLE PRECISION NOT NULL,
    unit               INTEGER          NOT NULL,
    dri_percentage     DOUBLE PRECISION,
    FOREIGN KEY (daily_meal_plan_id) REFERENCES daily_meal_plan (id),
    FOREIGN KEY (nutrient_id) REFERENCES nutrient (id),
    PRIMARY KEY (id)
);

CREATE INDEX daily_meal_plan_nutrient_idx ON daily_meal_plan_nutrient USING btree (quantity);

CREATE TABLE IF NOT EXISTS daily_menu
(
    id                  SERIAL,
    daily_meal_plan_id  INTEGER,
    meal_type           INTEGER          NOT NULL,
    satiety             INTEGER          NOT NULL,
    energy_total        DOUBLE PRECISION NOT NULL,
    carbohydrates_total DOUBLE PRECISION NOT NULL,
    lipids_total        DOUBLE PRECISION NOT NULL,
    proteins_total      DOUBLE PRECISION NOT NULL,
    FOREIGN KEY (daily_meal_plan_id) REFERENCES daily_meal_plan (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS daily_menu_nutrient
(
    id            SERIAL,
    daily_menu_id INTEGER          NOT NULL,
    nutrient_id   INTEGER          NOT NULL,
    quantity      DOUBLE PRECISION NOT NULL,
    unit          INTEGER          NOT NULL,
    FOREIGN KEY (daily_menu_id) REFERENCES daily_menu (id),
    FOREIGN KEY (nutrient_id) REFERENCES nutrient (id),
    PRIMARY KEY (id)
);

CREATE INDEX daily_menu_nutrient_idx ON daily_menu_nutrient USING btree (quantity);

CREATE TABLE IF NOT EXISTS menu_recipe
(
    id            SERIAL,
    recipe_id     INTEGER NOT NULL,
    portions      INTEGER NOT NULL,
    daily_menu_id INTEGER NOT NULL,
    FOREIGN KEY (daily_menu_id) REFERENCES daily_menu (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    PRIMARY KEY (id, recipe_id)
);

CREATE TABLE IF NOT EXISTS user_profile
(
    id           UUID DEFAULT gen_random_uuid(),
    username     VARCHAR(64) NOT NULL,
    email        TEXT        NOT NULL,
    api_key      TEXT        NOT NULL,
    joined_on    TIMESTAMP   NOT NULL,
    meal_plan_id INTEGER,
    UNIQUE (username),
    UNIQUE (email),
    FOREIGN KEY (meal_plan_id) REFERENCES meal_plan (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_data
(
    id               UUID    NOT NULL,
    name             VARCHAR(64),
    last_name        VARCHAR(64),
    birthdate        DATE    NOT NULL,
    gender           INTEGER NOT NULL,
    diet             INTEGER,
    intended_use     INTEGER,
    update_frequency INTEGER,
    FOREIGN KEY (id) REFERENCES user_profile (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_body_metrics
(
    id                UUID DEFAULT gen_random_uuid(),
    user_id           UUID             NOT NULL,
    height            INTEGER          NOT NULL,
    weight            DOUBLE PRECISION NOT NULL,
    body_mass_index   DOUBLE PRECISION NOT NULL,
    physical_activity INTEGER          NOT NULL,
    added_on          TIMESTAMP        NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user_profile (id),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS user_allergy
(
    id            UUID DEFAULT gen_random_uuid(),
    user_id       UUID    NOT NULL,
    ingredient_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user_profile (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
    PRIMARY KEY (id)
);
