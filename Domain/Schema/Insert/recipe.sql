INSERT INTO nutrifoods.recipe (name, author, url, portions, preparation_time)
VALUES
    -- ENSALADAS GOURMET --
    ('Ensalada de Atún', 'Gourmet', 'https://www.gourmet.cl/recetas/ensalada-de-atun/', 2, 20),
    ('Ensalada de Porotos Negros','Gourmet','https://www.gourmet.cl/recetas/ensalada-porotos-negros/', 2, 30),
    ('Ensalada de Espinacas y Frutillas','Gourmet','https://www.gourmet.cl/recetas/ensalada-espinacas-frutillas/', 2, 15),
    ('Molde de Arroz', 'Gourmet', 'https://www.gourmet.cl/recetas/molde-de-arroz/', 8, 35),
    ('Ensalada de Coleslaw', 'Gourmet', 'https://www.gourmet.cl/recetas/ensalada-de-coleslaw/', 4, 20), 
    ('Salsa de Cilantro', 'Gourmet', 'https://www.gourmet.cl/recetas/salsa-de-cilantro/', 1, 10),
    ('Ensalada de lentejas', 'Gourmet', 'https://www.gourmet.cl/recetas/ensalada-de-lentejas/', 5, 20), 
    ('Ensalada Rusa', 'Gourmet', 'https://www.gourmet.cl/recetas/ensalada-rusa/', 4, 15), 
    ('Ensalada Chilena', 'Gourmet', 'https://www.gourmet.cl/recetas/ensalada-chilena/', 4, 20), 
    ('Ensalada Griega', 'Gourmet', 'https://www.gourmet.cl/recetas/ensalada-griega/', 4, 15)
;

INSERT INTO nutrifoods.recipe_dish_type (recipe_id, dish_type_id)
VALUES
    (1, 3),
    (2, 3),
    (3, 3),
    (4, 3),
    (5, 3),
    (6, 3),
    (7, 3),
    (8, 3)
;


INSERT INTO nutrifoods.recipe_meal_type (recipe_id, meal_type_id)
VALUES
    (1, 2),
    (1, 3),
    (2, 2),
    (2, 3),
    (3, 2),
    (3, 3),
    (4, 2),
    (4, 3)
;


INSERT INTO nutrifoods.recipe_diet (recipe_id, diet_id)
VALUES
    (1, 4),
    (1, 7),
    (3, 1),
    (3, 6),
    (3, 7)
    
;

INSERT INTO nutrifoods.recipe_steps (recipe, step, description)
VALUES
    -- Ensalada de atún --
    (1, 1, 'Cortar el pepino a lo largo en dos mitades. Con una cuchara, retirar las pepas. Cortarlo en cubitos de 5mm.'),
    (1, 2, 'Cortar la manzana en cubos de 5mm.'),
    (1, 3, 'Pelar el apio y cortarlo en cubos de 5mm.'),
    (1, 4, 'Cortar la palta en láminas o en cubos según prefieran.'),
    (1, 5, 'Drenar el agua de las latas de atún y romper un poco el atún con las manos o un tenedor.'),
    (1, 6, 'Mezclar todos los ingredientes en un bowl.'),
    (1, 7, 'Mezclar todos los ingredientes con un batidor pequeño.'),
    (1, 8, 'Cubrir los ingredientes de la ensalada con el aderezo y mezclar.'),

    -- Ensalada de porotos negros --
    (2, 1, 'Cocinar el tocino solo en un sartén a fuego medio o en el horno a 180°C hasta que esté crocante.'),
    (2, 2, 'Retirar y poner sobre papel de cocina para absorber el exceso de grasa. Una vez frío, picar.'),
    (2, 3, 'Pelar el pepino y cortarlo a la mitad a lo largo. Con una cuchara, retirar las pepas. Picar en cubos de 5mm'),
    (2, 4, 'Mezclar todos los ingredientes de la ensalada en un bowl.'),
    (2, 5, 'Usar una licuadora de inmersión o licuadora tradicional para mezclar los ingredientes.'),
    (2, 6, 'Servir sobre la ensalada.'),
    
    -- Ensalada de Espinaca y Frutillas --
    (3, 1, 'Mezclar la lechuga con la espinaca en un bowl.'),
    (3, 2, 'Encima, distribuir las frutillas.'),
    (3, 3, 'Con una cuchara, agregar la ricotta y luego las almendras.'),
    (3, 4, 'Con batidor de mano pequeño, mezclar el jugo de limón y mostaza.'),
    (3, 5, 'Agregar el aceite de oliva, sal y pimienta. Mezclar para incorporar.'),
    (3, 6, 'Agregar el aliño a la ensalada y servir con un chorrito de reducción de vinagre balsámico.')
;

INSERT INTO nutrifoods.recipe_measure (recipe_id, ingredient_measure_id, integer_part, numerator, denominator, description)
VALUES

    -- Ensalada de atún --
    (1, 245, 0, 1, 2, ''),
    (1, 200, 1, 0, 0, ''),
    (1, 181, 1, 0, 0, ''),
    
    (1, 216, 2, 0, 0, ''),
     -- BERROS
    (1, 106, 2, 0, 0, 'Picadas'),
    (1, 100, 1, 0, 0, ''),
    (1, 452, 1, 0, 0, ''),
    (1, 9, 1, 0, 0, ''),
    (1, 140, 0, 1, 2, ''),
    
    -- Ensalada de porotos negros --
    (2, 245, 0, 1, 2, ''),
    (2, 168, 1, 0, 0, ''),
    (2, 32, 1, 0, 0, ''),
    (2, 14, 0, 1, 2, ''),
    (2, 100, 1, 0, 0, ''),
    (2, 140, 0, 1, 2, ''),
    (2, 77, 10, 0, 0, ''),
    (2, 9, 6, 0, 0, ''),
    
    -- Ensalada de Espinacas y Frutillas
    (3, 107, 1, 0, 0, ''),
    (3, 126, 1, 0, 0, ''),
    (3, 358, 5, 0, 0, ''),
     -- CUCHARADAS, solo hay TAZA
    (3, 100, 1, 0, 0, ''),
    (3, 140, 1, 0, 0, ''),
    (3, 9, 4, 0, 0, ''),

    -- Molde de Arroz -- 
    (4, 9, 1, 0, 0, ''),
    (4, 23, 1, 0, 0, ''),
    (4, 1, 2, 0, 0, ''),
    (4, 331, 2, 0, 0, ''),
    (4, 168, 1, 0, 0, ''),
    (4, 68, 7, 0, 0, ''),
    (4, 75, 2, 0, 0, ''),
    (4, 14, 0, 1, 4, ''),
     -- CHERRY
    (4, 9, 2, 0, 0, ''),

    -- COLESLAW --
    (5, 234, 1, 0, 0, ''),
    (5, 58, 2, 0, 0, ''),
    (5, 69, 0, 1, 2, ''),
    (5, 176, 1, 0, 0, ''),
    (5, 140, 1, 0, 0, ''),
    (5, 7, 0, 1, 2, '')
    
    
;

INSERT INTO nutrifoods.recipe_quantity (recipe_id, ingredient_id, grams, description)
VALUES
    (2, 92, 45, ''),
     -- Porotos negros receta 2
    (3, 40, 50, '')
;