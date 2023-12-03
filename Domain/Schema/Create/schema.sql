SET SEARCH_PATH = "nutrifoods";

DROP SCHEMA IF EXISTS nutrifoods CASCADE;

DROP EXTENSION IF EXISTS unaccent;
DROP EXTENSION IF EXISTS btree_gist;
DROP EXTENSION IF EXISTS "uuid-ossp";
DROP EXTENSION IF EXISTS pg_trgm;
DROP EXTENSION IF EXISTS fuzzystrmatch;

CREATE SCHEMA IF NOT EXISTS nutrifoods;

SET SEARCH_PATH = "nutrifoods";
SET TIMEZONE = "America/Santiago";

CREATE EXTENSION IF NOT EXISTS unaccent;
CREATE EXTENSION IF NOT EXISTS btree_gist;
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
CREATE EXTENSION IF NOT EXISTS pg_trgm;
CREATE EXTENSION IF NOT EXISTS fuzzystrmatch;

CREATE TABLE IF NOT EXISTS nutritional_value
(
    id          SERIAL,
    nutrient    INTEGER NOT NULL,
    quantity    FLOAT   NOT NULL,
    unit        INTEGER NOT NULL,
    daily_value FLOAT,
    PRIMARY KEY (id)
);

CREATE INDEX IF NOT EXISTS nutritional_value_idx ON nutritional_value USING btree (nutrient, quantity);

CREATE TABLE IF NOT EXISTS ingredient
(
    id         SERIAL,
    name       VARCHAR(64)       NOT NULL,
    synonyms   VARCHAR(64) ARRAY NOT NULL DEFAULT ARRAY []::VARCHAR[],
    is_animal  BOOLEAN           NOT NULL,
    food_group INTEGER           NOT NULL,
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
    PRIMARY KEY (id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id)
);

CREATE TABLE IF NOT EXISTS ingredient_nutrient
(
    ingredient_id        INTEGER NOT NULL,
    nutritional_value_id INTEGER NOT NULL,
    PRIMARY KEY (ingredient_id, nutritional_value_id),
    FOREIGN KEY (ingredient_id) REFERENCES ingredient (id),
    FOREIGN KEY (nutritional_value_id) REFERENCES nutritional_value (id)
);

CREATE TABLE IF NOT EXISTS recipe
(
    id         SERIAL,
    name       TEXT          NOT NULL,
    author     VARCHAR(64)   NOT NULL,
    url        TEXT          NOT NULL,
    portions   INTEGER,
    time       INTEGER,
    difficulty INTEGER,
    meal_types INTEGER ARRAY NOT NULL DEFAULT ARRAY []::INTEGER[],
    dish_types INTEGER ARRAY NOT NULL DEFAULT ARRAY []::INTEGER[],
    UNIQUE (url),
    UNIQUE (name, author),
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
    recipe_id            INTEGER NOT NULL,
    nutritional_value_id INTEGER NOT NULL,
    PRIMARY KEY (recipe_id, nutritional_value_id),
    FOREIGN KEY (recipe_id) REFERENCES recipe (id),
    FOREIGN KEY (nutritional_value_id) REFERENCES nutritional_value (id)
);

CREATE TABLE IF NOT EXISTS nutritional_target
(
    id                SERIAL,
    nutrient          INTEGER NOT NULL,
    expected_quantity FLOAT   NOT NULL,
    actual_quantity   FLOAT,
    expected_error    FLOAT   NOT NULL,
    actual_error      FLOAT,
    unit              INTEGER NOT NULL,
    threshold_type    INTEGER NOT NULL,
    is_priority       BOOLEAN NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS daily_plan
(
    id                       SERIAL,
    day                      INTEGER NOT NULL,
    physical_activity_level  INTEGER NOT NULL,
    physical_activity_factor FLOAT   NOT NULL,
    adjustment_factor        FLOAT   NOT NULL,
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS daily_plan_nutritional_target
(
    daily_plan_id         INTEGER NOT NULL,
    nutritional_target_id INTEGER NOT NULL,
    PRIMARY KEY (daily_plan_id, nutritional_target_id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id),
    FOREIGN KEY (nutritional_target_id) REFERENCES nutritional_target (id)
);

CREATE TABLE IF NOT EXISTS daily_plan_nutritional_value
(
    daily_plan_id        INTEGER NOT NULL,
    nutritional_value_id INTEGER NOT NULL,
    PRIMARY KEY (daily_plan_id, nutritional_value_id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id),
    FOREIGN KEY (nutritional_value_id) REFERENCES nutritional_value (id)
);

CREATE TABLE IF NOT EXISTS daily_menu
(
    id                SERIAL,
    daily_plan_id     INTEGER    NOT NULL,
    intake_percentage FLOAT      NOT NULL,
    meal_type         INTEGER    NOT NULL,
    hour              VARCHAR(8) NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id)
);

CREATE TABLE IF NOT EXISTS daily_menu_nutritional_target
(
    daily_menu_id         INTEGER NOT NULL,
    nutritional_target_id INTEGER NOT NULL,
    PRIMARY KEY (daily_menu_id, nutritional_target_id),
    FOREIGN KEY (daily_menu_id) REFERENCES daily_menu (id),
    FOREIGN KEY (nutritional_target_id) REFERENCES nutritional_target (id)
);

CREATE TABLE IF NOT EXISTS daily_menu_nutritional_value
(
    daily_menu_id        INTEGER NOT NULL,
    nutritional_value_id INTEGER NOT NULL,
    PRIMARY KEY (daily_menu_id, nutritional_value_id),
    FOREIGN KEY (daily_menu_id) REFERENCES daily_menu (id),
    FOREIGN KEY (nutritional_value_id) REFERENCES nutritional_value (id)
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
    patient_id    UUID    NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (patient_id) REFERENCES patient (id)
);

CREATE TABLE IF NOT EXISTS meal_plan
(
    consultation_id UUID    NOT NULL,
    daily_plan_id   INTEGER NOT NULL,
    PRIMARY KEY (consultation_id, daily_plan_id),
    FOREIGN KEY (consultation_id) REFERENCES consultation (id),
    FOREIGN KEY (daily_plan_id) REFERENCES daily_plan (id)
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

CREATE TABLE IF NOT EXISTS ingestible
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

CREATE OR REPLACE FUNCTION immutable_unaccent(regdictionary, text)
    RETURNS text
    LANGUAGE c
    IMMUTABLE PARALLEL SAFE STRICT AS
'$libdir/unaccent',
'unaccent_dict';

CREATE OR REPLACE FUNCTION remove_diacritics(text)
    RETURNS text
    LANGUAGE sql
    IMMUTABLE PARALLEL SAFE STRICT
RETURN immutable_unaccent(regdictionary 'unaccent', $1);

CREATE OR REPLACE FUNCTION indent_punctuation(str text)
    RETURNS text
    LANGUAGE sql
    IMMUTABLE PARALLEL SAFE STRICT
RETURN regexp_replace(str, '([:;,.\-])', '\1 ', 'g');

CREATE OR REPLACE FUNCTION remove_trailing(str text)
    RETURNS text
    LANGUAGE sql
    IMMUTABLE PARALLEL SAFE STRICT
RETURN regexp_replace(TRIM(BOTH ' ' FROM str), '\s+', ' ', 'g');

CREATE OR REPLACE FUNCTION normalize_str(str text)
    RETURNS text
    LANGUAGE sql
    IMMUTABLE PARALLEL SAFE STRICT
RETURN remove_trailing(indent_punctuation(lower(remove_diacritics(str))));

CREATE INDEX IF NOT EXISTS normalize_measure_idx ON ingredient_measure (normalize_str(name));

CREATE INDEX IF NOT EXISTS normalize_ingredient_idx ON ingredient (normalize_str(name));

CREATE INDEX IF NOT EXISTS normalize_recipe_name_idx ON recipe (normalize_str(name));

CREATE INDEX IF NOT EXISTS normalize_recipe_author_idx ON recipe (normalize_str(author));