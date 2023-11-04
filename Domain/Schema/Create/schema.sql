SET SEARCH_PATH = "nutrifoods";

DROP EXTENSION IF EXISTS btree_gist;
DROP EXTENSION IF EXISTS "uuid-ossp";
DROP EXTENSION IF EXISTS pg_trgm;
DROP EXTENSION IF EXISTS fuzzystrmatch;

DROP SCHEMA IF EXISTS nutrifoods CASCADE;

CREATE SCHEMA IF NOT EXISTS nutrifoods;

SET SEARCH_PATH = "nutrifoods";
SET TIMEZONE = "America/Santiago";

CREATE EXTENSION IF NOT EXISTS btree_gist;
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS pg_trgm;
CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;

CREATE TABLE IF NOT EXISTS ingredient
(
    id         SERIAL,
    name       VARCHAR(64) NOT NULL,
    synonyms   VARCHAR(64) ARRAY DEFAULT ARRAY []::VARCHAR[],
    is_animal  BOOLEAN     NOT NULL,
    food_group INTEGER     NOT NULL,
    UNIQUE (name),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS ingredient_measure
(
    id            SERIAL,
    name          VARCHAR(64) NOT NULL,
    grams         FLOAT       NOT NULL,
    is_default    BOOLEAN     NOT NULL,
    ingredient_id INTEGER     NOT NULL,
    UNIQUE (name, ingredient_id),
    PRIMARY KEY (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id)
);

CREATE TABLE IF NOT EXISTS ingredient_nutrient
(
    id            SERIAL,
    nutrient      INTEGER NOT NULL,
    quantity      FLOAT   NOT NULL,
    unit          INTEGER NOT NULL,
    ingredient_id INTEGER NOT NULL,
    UNIQUE (nutrient, ingredient_id),
    PRIMARY KEY (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id)
);

CREATE TABLE IF NOT EXISTS recipe
(
    id         SERIAL,
    name       VARCHAR(64) NOT NULL,
    author     VARCHAR(64) NOT NULL,
    url        TEXT        NOT NULL,
    portions   INTEGER     NOT NULL,
    time       INTEGER,
    difficulty INTEGER,
    meal_types INTEGER ARRAY DEFAULT ARRAY []::INTEGER[],
    dish_types INTEGER ARRAY DEFAULT ARRAY []::INTEGER[],
    UNIQUE (url),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS recipe_step
(
    id          SERIAL,
    recipe_id   INTEGER NOT NULL,
    number      INTEGER NOT NULL,
    description TEXT    NOT NULL,
    UNIQUE (recipe_id, number),
    PRIMARY KEY (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id)
);

CREATE TABLE IF NOT EXISTS recipe_measure
(
    id                    SERIAL,
    recipe_id             INTEGER NOT NULL,
    ingredient_measure_id INTEGER NOT NULL,
    integer_part          INTEGER NOT NULL,
    numerator             INTEGER NOT NULL,
    denominator           INTEGER NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (ingredient_measure_id) REFERENCES ingredient_measure (id)
);

CREATE TABLE IF NOT EXISTS recipe_quantity
(
    id            SERIAL,
    recipe_id     INTEGER NOT NULL,
    ingredient_id INTEGER NOT NULL,
    grams         FLOAT   NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id)
);

CREATE TABLE IF NOT EXISTS recipe_nutrient
(
    id        SERIAL,
    nutrient  INTEGER NOT NULL,
    quantity  FLOAT   NOT NULL,
    unit      INTEGER NOT NULL,
    recipe_id INTEGER NOT NULL,
    UNIQUE (recipe_id, nutrient),
    PRIMARY KEY (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id)
);

CREATE TABLE IF NOT EXISTS meal_plan
(
    id            SERIAL,
    meals_per_day INTEGER NOT NULL,
    created_on    TIMESTAMP DEFAULT now(),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS daily_plan
(
    id                       SERIAL,
    meal_plan_id             INTEGER NOT NULL,
    day                      INTEGER NOT NULL,
    physical_activity_level  INTEGER NOT NULL,
    physical_activity_factor FLOAT   NOT NULL,
    adjustment_factor        INTEGER NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (meal_plan_id) REFERENCES meal_plan (id)
);

CREATE TABLE IF NOT EXISTS daily_plan_target
(
    id             SERIAL,
    daily_plan_id  INTEGER NOT NULL,
    nutrient       INTEGER NOT NULL,
    quantity       FLOAT   NOT NULL,
    unit           INTEGER NOT NULL,
    threshold_type INTEGER NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id)
);

CREATE TABLE IF NOT EXISTS daily_plan_nutrient
(
    id            SERIAL,
    daily_plan_id INTEGER NOT NULL,
    nutrient      INTEGER NOT NULL,
    quantity      FLOAT   NOT NULL,
    unit          INTEGER NOT NULL,
    error_margin  FLOAT,
    PRIMARY KEY (id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id)
);

CREATE TABLE IF NOT EXISTS daily_menu
(
    id                SERIAL,
    daily_plan_id     INTEGER    NOT NULL,
    intake_percentage INTEGER    NOT NULL,
    meal_type         INTEGER    NOT NULL,
    hour              VARCHAR(8) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id)
);

CREATE TABLE IF NOT EXISTS daily_menu_nutrient
(
    id            SERIAL,
    daily_menu_id INTEGER NOT NULL,
    nutrient      INTEGER NOT NULL,
    quantity      FLOAT   NOT NULL,
    unit          INTEGER NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (daily_menu_id) REFERENCES daily_menu (id)
);

CREATE TABLE IF NOT EXISTS menu_recipe
(
    id            SERIAL,
    daily_menu_id INTEGER NOT NULL,
    recipe_id     INTEGER NOT NULL,
    portions      INTEGER NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (daily_menu_id) REFERENCES daily_menu (id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id)
);

CREATE TABLE IF NOT EXISTS nutritionist
(
    id        UUID        NOT NULL DEFAULT uuid_generate_v4(),
    username  VARCHAR(50) NOT NULL,
    email     TEXT        NOT NULL,
    password  TEXT        NOT NULL,
    joined_on TIMESTAMP   NOT NULL DEFAULT now(),
    UNIQUE (username),
    UNIQUE (email),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS patient
(
    id              UUID      NOT NULL DEFAULT uuid_generate_v4(),
    joined_on       TIMESTAMP NOT NULL DEFAULT now(),
    nutritionist_id UUID      NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (nutritionist_id) REFERENCES nutritionist (id)
);

CREATE TABLE IF NOT EXISTS personal_info
(
    id             UUID        NOT NULL,
    rut            VARCHAR(16) NOT NULL,
    names          VARCHAR(50) NOT NULL,
    last_names     VARCHAR(50) NOT NULL,
    biological_sex INTEGER     NOT NULL,
    birthdate      DATE        NOT NULL,
    UNIQUE (rut),
    PRIMARY KEY (id),
    FOREIGN KEY (id) REFERENCES patient (id)
);

CREATE TABLE IF NOT EXISTS contact_info
(
    id           UUID        NOT NULL,
    mobile_phone VARCHAR(16) NOT NULL,
    fixed_phone  VARCHAR(16),
    email        TEXT        NOT NULL,
    UNIQUE (email),
    PRIMARY KEY (id),
    FOREIGN KEY (id) REFERENCES patient (id)
);

CREATE TABLE IF NOT EXISTS address
(
    id          UUID    NOT NULL,
    street      TEXT    NOT NULL,
    number      INTEGER NOT NULL,
    postal_code INTEGER,
    province    INTEGER NOT NULL,
    UNIQUE (id),
    PRIMARY KEY (id),
    FOREIGN KEY (id) REFERENCES patient (id)
);

CREATE TABLE IF NOT EXISTS consultation
(
    id            UUID DEFAULT uuid_generate_v4(),
    type          INTEGER NOT NULL,
    purpose       INTEGER NOT NULL,
    registered_on DATE DEFAULT now()::DATE,
    meal_plan_id  INTEGER,
    patient_id    UUID    NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (meal_plan_id) REFERENCES meal_plan (id),
    FOREIGN KEY (patient_id) REFERENCES patient (id)
);

CREATE TABLE IF NOT EXISTS clinical_anamnesis
(
    id           UUID NOT NULL,
    created_on   TIMESTAMP DEFAULT now(),
    last_updated TIMESTAMP DEFAULT now(),
    PRIMARY KEY (id),
    FOREIGN KEY (id) REFERENCES consultation (id)
);

CREATE TABLE IF NOT EXISTS clinical_sign
(
    id                    UUID DEFAULT uuid_generate_v4(),
    name                  VARCHAR(64) NOT NULL,
    observations          TEXT DEFAULT '',
    clinical_anamnesis_id UUID        NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (clinical_anamnesis_id) REFERENCES clinical_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS disease
(
    id                    UUID DEFAULT uuid_generate_v4(),
    name                  VARCHAR(64) NOT NULL,
    inheritance_type      INTEGER     NOT NULL,
    clinical_anamnesis_id UUID        NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (clinical_anamnesis_id) REFERENCES clinical_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS medication
(
    id                    UUID                      DEFAULT uuid_generate_v4(),
    name                  VARCHAR(64)      NOT NULL,
    type                  INTEGER          NOT NULL,
    administration_times  VARCHAR(8) ARRAY NOT NULL DEFAULT ARRAY []::VARCHAR[],
    dosage                INTEGER,
    adherence             INTEGER          NOT NULL,
    observations          TEXT                      DEFAULT '',
    clinical_anamnesis_id UUID             NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (clinical_anamnesis_id) REFERENCES clinical_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS nutritional_anamnesis
(
    id           UUID,
    created_on   TIMESTAMP DEFAULT now(),
    last_updated TIMESTAMP DEFAULT now()::DATE,
    PRIMARY KEY (id),
    FOREIGN KEY (id) REFERENCES consultation (id)
);

CREATE TABLE IF NOT EXISTS harmful_habit
(
    id                    UUID DEFAULT uuid_generate_v4(),
    name                  VARCHAR(64) NOT NULL,
    observations          TEXT DEFAULT '',
    nutritional_anamnesis UUID        NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (nutritional_anamnesis) REFERENCES nutritional_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS eating_symptom
(
    id                    UUID DEFAULT uuid_generate_v4(),
    name                  VARCHAR(64) NOT NULL,
    observations          TEXT DEFAULT '',
    nutritional_anamnesis UUID        NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (nutritional_anamnesis) REFERENCES nutritional_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS adverse_food_reaction
(
    id                    UUID DEFAULT uuid_generate_v4(),
    food_group            INTEGER NOT NULL,
    type                  INTEGER NOT NULL,
    nutritional_anamnesis UUID    NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (nutritional_anamnesis) REFERENCES nutritional_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS food_consumption
(
    id                    UUID DEFAULT uuid_generate_v4(),
    food_group            INTEGER NOT NULL,
    frequency             INTEGER NOT NULL,
    nutritional_anamnesis UUID    NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (nutritional_anamnesis) REFERENCES nutritional_anamnesis (id)
);

CREATE TABLE IF NOT EXISTS anthropometry
(
    id                     UUID    NOT NULL,
    height                 INTEGER NOT NULL,
    weight                 FLOAT   NOT NULL,
    bmi                    FLOAT   NOT NULL,
    muscle_mass_percentage FLOAT   NOT NULL,
    waist_circumference    FLOAT   NOT NULL,
    created_on             TIMESTAMP DEFAULT now()::DATE,
    last_updated           TIMESTAMP DEFAULT now()::DATE,
    PRIMARY KEY (id),
    FOREIGN KEY (id) REFERENCES consultation (id)
);