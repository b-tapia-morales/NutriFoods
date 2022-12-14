select (energy.id) idRecipe,energy.quantityE,Carbo.quantityC,protein.quantityp,lipid.quantityL
from (SELECT (E.recipe_id) id, (E.quantity) quantityE
      from recipe_nutrient E
      where E.nutrient_id = 1) energy, 
    (SELECT (E.recipe_id) id, (E.quantity) quantityC
     from recipe_nutrient E
     where E.nutrient_id = 2) Carbo ,
     (SELECT (E.recipe_id) id, (E.quantity) quantityp
      from recipe_nutrient E
      where E.nutrient_id = 63) protein,
     (SELECT (E.recipe_id) id, (E.quantity) quantityL
      from recipe_nutrient E
      where E.nutrient_id = 12) lipid,
    nutrifoods.recipe_meal_type rm
where energy.id = Carbo.id and energy.id = protein.id and energy.id = lipid.id and energy.id = rm.recipe_id and rm.meal_type_id = 2;

select (energy.id) idRecipe,energy.quantityE,Carbo.quantityC,protein.quantityp,lipid.quantityL
from (SELECT (E.recipe_id) id, (E.quantity) quantityE
      from recipe_nutrient E
      where E.nutrient_id = 1) energy, 
    (SELECT (E.recipe_id) id, (E.quantity) quantityC
     from recipe_nutrient E
     where E.nutrient_id = 2) Carbo ,
     (SELECT (E.recipe_id) id, (E.quantity) quantityp
      from recipe_nutrient E
      where E.nutrient_id = 63) protein,
     (SELECT (E.recipe_id) id, (E.quantity) quantityL
      from recipe_nutrient E
      where E.nutrient_id = 12) lipid,
    nutrifoods.recipe_meal_type rm
where energy.id = Carbo.id and energy.id = protein.id and energy.id = lipid.id and energy.id = rm.recipe_id and quantityE < 250.0 and rm.meal_type_id = 1;