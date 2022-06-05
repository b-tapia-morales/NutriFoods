INSERT INTO nutrifoods.ingredient_measure (ingredient_id, name, grams)
VALUES
    -- Sal --
    (1, 'Cucharadita', 6.0),
    (1, 'Cucharada', 18.0),
    (1, 'Taza', 292.0),
    (1, 'Pizca', 0.4),
    -- Pimienta negra --
    (2, 'Cucharadita, Molida', 2.3),
    (2, 'Cucharada, Molida', 6.9),
    (2, 'Cucharadita, Entera', 2.9),
    (2, 'Pizca', 0.1),
    -- Aceite de oliva --
    (3, 'Taza', 224),
    (3, 'Cucharada', 14),
    (3, 'ml', 0.92),
    -- Cebolla --
    (4, 'Pequeña', 70),
    (4, 'Mediana', 110),
    (4, 'Grande', 150),
    (4, 'Rodaja, delgada', 9),
    (4, 'Rodaja, mediana', 14),
    (4, 'Rodaja, gruesa', 38),
    (4, 'Cucharadita, picada', 10),
    (4, 'Taza, picada', 160),
    (4, 'Taza, en rodajas', 115),
    -- Ajo --
    (5, 'Taza', 136),
    (5, 'Cucharada', 2.8),
    (5, 'Diente', 3),
    -- Huevo --
    (6, 'Grande', 50),
    (6, 'Extra grande', 56),
    (6, 'Gigante', 63),
    (6, 'Taza', 243),
    (6, 'Medio', 44),
    (6, 'Pequeño', 38),
    -- Tomate --
    (7, 'Grande', 182),
    (7, 'Medio', 123),
    (7, 'Pequeño', 91),
    -- Mantequilla --
    (8, 'Cucharada', 14.2),
    (8, 'Taza', 127),
    (8, 'Barra', 113),
    -- Limón --
    (9, 'Taza', 212),
    (9, 'Mediano', 58),
    (9, 'Grande', 84),
    -- Zanahoria --
    (10, 'Taza', 128),
    (10, 'Grande', 72),
    (10, 'Mediana', 61),
    (10, 'Pequeña', 50),
    -- Cilantro --
    (11, 'Taza', 16),
    (11, 'Rama', 2.2),
    -- Aceite --
    (12, 'Cucharada', 13.6),
    (12, 'Taza', 218),
    (12, 'Cucharadita', 4.5),
    -- Sal de mar --
    -- Mayonesa --
    (14, 'Cucharada', 13.8),
    (14, 'Taza', 220),
    -- Palta --
    (15, 'Taza', 230),
    (15, 'Grande', 304),
    -- Albahaca --
    (16, 'Hoja', 1),
    (16, 'Cucharada', 2.65),
    (16, 'Taza', 24),
    -- Perejil --
    (17, 'Taza', 60),
    (17, 'Cucharada', 3.8),
    (17, 'Ramita', 1),
    -- Comino --
    (18, 'Cucharadita', 2.1),
    (18, 'Cucharada', 6),
    -- Pimiento rojo --
    (19, 'Taza, en trozos', 149),
    (19, 'Taza, juliana', 92),
    (19, 'Cucharada', 9.3),
    (19, 'Grande', 164),
    (19, 'Mediano', 119),
    (19, 'Pequeño', 74),
    -- Crema --
    (20, 'Taza', 238),
    (20, 'Cucharada', 14),
    (20, 'Litro', 1007),
    (20, 'ml', 1.007),
    -- Pure de papas --
    (21, 'Taza', 210),
    -- Jugo de limón --
    (22, 'Taza', 244),
    (22, 'ml', 1),
    -- Oregano --
    (23, 'Cucharadita', 1.8),
    -- Queso parmesano--
    (24, 'Taza', 100),
    (24, 'Cucharadita', 5),
    -- Lechuga (169249) --
    (25, 'Taza', 36),
    (25, 'Entera', 360),
    (25, 'Hoja', 4.8),
    -- Queso crema --
    (26, 'Cucharada', 14.5),
    (26, 'Taza', 232),
    (26, 'Cucharadita', 10),
    -- Pimienta blanca --
    (27, 'Cucharadita', 2.4),
    (27, 'Cucharada', 7.1),
    -- Leche semidescremada (167697)-- 
    (28, 'Taza', 245),
    -- Zapallo italiano --
    (29, 'Taza', 124),
    (29, 'Grande', 323),
    (29, 'Mediano', 196),
    (29, 'Pequeño', 118),
    -- Espinaca --
    (30, 'Taza', 30),
    (30, 'Racimo', 340),
    (30, 'Hoja', 10),
    (30, 'Paquete', 284),
    -- Champiñon --
    (31, 'Taza', 70),
    (31, 'Grande', 23),
    (31, 'Mediano', 18),
    (31, 'Pequeño', 10),
    -- Azúcar --
    (32, 'Cucharadita', 4.2),
    (32, 'Taza', 200),
    -- Mostaza --
    (33, 'Taza', 249),
    (33, 'Cucharadita', 5),
    -- Ají color --
    (34, 'Cucharadita', 5),
    (34, 'Taza', 249)
    -- Queso --
    -- (35, '', ),

    -- Berenjena --

ON CONFLICT (ingredient_id, name) DO UPDATE SET ingredient_id = excluded.ingredient_id,
                                                grams         = excluded.grams;