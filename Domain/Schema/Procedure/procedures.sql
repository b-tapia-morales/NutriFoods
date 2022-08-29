SET SEARCH_PATH = "nutrifoods";

DROP INDEX IF EXISTS ingredient_search_idx;
DROP INDEX IF EXISTS measure_search_idx;

DROP FUNCTION IF EXISTS find_measure_id(TEXT);
DROP FUNCTION IF EXISTS find_ingredient_id(TEXT);

CREATE INDEX measure_search_idx ON ingredient_measure USING gist (name gist_trgm_ops);
CREATE INDEX ingredient_search_idx ON ingredient USING gist (name gist_trgm_ops);

SET pg_trgm.similarity_threshold = 0.6;

CREATE OR REPLACE FUNCTION find_ingredient_id(ingredient_name TEXT) RETURNS INTEGER AS
$func$
DECLARE
BEGIN
    RETURN (SELECT id
            FROM ingredient
            WHERE ingredient_name % ingredient.name
            ORDER BY ingredient_name <-> ingredient.name DESC
            LIMIT 1);
END;
$func$
    LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION find_measure_id(measure_name TEXT) RETURNS INTEGER AS
$func$
DECLARE
BEGIN
    RETURN (SELECT id
            FROM ingredient_measure
            WHERE measure_name % ingredient_measure.name
            ORDER BY measure_name <-> ingredient_measure.name DESC
            LIMIT 1);
END;
$func$
    LANGUAGE plpgsql;