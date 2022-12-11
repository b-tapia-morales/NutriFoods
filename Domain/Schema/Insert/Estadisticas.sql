SET search_path = "nutrifoods";

select (energy.id) idRecipe, energy.quantityE, Carbo.quantityC, protein.quantityp, lipid.quantityL
from (SELECT (E.recipe_id) id, (E.quantity) quantityE
      from recipe_nutrient E
      where E.nutrient_id = 1) energy,
     (SELECT (E.recipe_id) id, (E.quantity) quantityC
      from recipe_nutrient E
      where E.nutrient_id = 2) Carbo,
     (SELECT (E.recipe_id) id, (E.quantity) quantityp
      from recipe_nutrient E
      where E.nutrient_id = 63) protein,
     (SELECT (E.recipe_id) id, (E.quantity) quantityL
      from recipe_nutrient E
      where E.nutrient_id = 12) lipid,
     nutrifoods.recipe_meal_type rm
where energy.id = Carbo.id
  and energy.id = protein.id
  and energy.id = lipid.id
  and energy.id = rm.recipe_id
  and rm.meal_type = 3;

WITH joined_table AS (SELECT DISTINCT ON (r.id) *
                      FROM recipe r
                               INNER JOIN recipe_nutrient rn ON r.id = rn.recipe_id
                               INNER JOIN recipe_meal_type rm ON r.id = rm.recipe_id
                      WHERE rn.nutrient_id = 1)
SELECT *
FROM joined_table j
WHERE j.quantity <= 400
ORDER BY j.quantity;
