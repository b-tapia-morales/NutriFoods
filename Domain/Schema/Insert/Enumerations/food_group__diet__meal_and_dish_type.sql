INSERT INTO nutrifoods.primary_group (name)
VALUES ('Frutas'),
       ('Verduras'),
       ('Granos'),
       ('Alimentos proteicos'),
       ('Lácteos'),
       ('Otros')
ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.secondary_group (name, primary_group_id)
VALUES ('Fruta entera', 1),
       ('Jugo de fruta', 1),
       ('Vegetales verdes', 2),
       ('Vegetales rojos', 2),
       ('Legumbres', 2),
       ('Vegetales con almidón', 2),
       ('Otros vegetales', 2),
       ('Granos enteros', 3),
       ('Granos refinados', 3),
       ('Pescados y mariscos', 4),
       ('Carne, Aves y Huevos', 4),
       ('Frutos secos, semilla y soya', 4),
       ('Leche y yogurt', 5),
       ('Queso', 5),
       ('Aceite', 6),
       ('Azúcar', 6),
       ('Condimento', 6),
       ('Otros', 6),
       ('Alcohol', 6),
       ('Algas', 6)
ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.tertiary_group (name, secondary_group_id)
VALUES ('Fruta entera', 1),
       ('Jugo de fruta', 2),
       ('Vegetales verdes', 3),
       ('Vegetales rojos', 4),
       ('Legumbres', 5),
       ('Vegetales con almidón', 6),
       ('Otros vegetales', 7),
       ('Granos enteros', 8),
       ('Granos refinados', 9),
       ('Pescados', 10),
       ('Mariscos', 10),
       ('Carne', 11),
       ('Aves', 11),
       ('Huevos', 11),
       ('Frutos secos', 12),
       ('Semilla', 12),
       ('Soya', 12),
       ('Leche', 13),
       ('Yogurt', 13),
       ('Queso', 14),
       ('Aceite', 15),
       ('Azúcar', 16),
       ('Condimento', 17),
       ('Otros', 18),
       ('Alcohol', 6),
       ('Algas', 6)
ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.meal_type (name)
VALUES
    -- Hora del día
    ('Desayuno'),    --Gourmet
    ('Almuerzo'),    --Gourmet
    ('Cena'),        --Gourmet
    ('Hora del té'), --Gourmet
    ('Refrigerio')
ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.dish_type (name)
VALUES ('Aperitivo'),  --Comidas chilenas
       ('Bebida'),     --Comidas chilenas
       ('Ensalada'),   --Comidas chilenas
       ('Pan'),        --Comidas chilenas
       ('Principal'),  --Comidas chilenas
       ('Postre'),     --Comidas chilenas
       ('Repostería'), --Gourmet
       ('Salsa'),      --Comidas chilenas
       ('Sándwich'),   --Comidas chilenas
       ('Sopa')        --Comidas chilenas
ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.diet (name)
VALUES ('Lacto-Vegetariana'),
       ('Ovo-Vegetariana'),
       ('Pollotariana'),
       ('Pescetariana'),
       ('Pollo-Pescetariana'),
       ('Keto'),
       ('Sin Gluten')
ON CONFLICT DO NOTHING;