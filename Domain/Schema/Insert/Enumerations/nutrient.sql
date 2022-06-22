INSERT INTO nutrifoods.nutrient_type (name)
VALUES ('Carbohidratos'),
       ('Grasas y Ácidos grasos'),
       ('Proteínas y Aminoácidos'),
       ('Vitaminas'),
       ('Minerales'),
       ('Esteroles'),
       ('Otros'),
       ('Energía')
ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.nutrient_subtype (name, provides_energy, type_id)
VALUES ('Carbohidratos, total', true, 1),
       ('Fibra', true, 1),
       ('Almidón', true, 1),
       ('Azúcar', true, 1),
       ('Grasa, total', true, 2),
       ('Ácidos grasos saturados', true, 2),
       ('Ácidos grasos monoinsaturados', true, 2),
       ('Ácidos grasos poliinsaturados', true, 2),
       ('Ácidos grasos trans', true, 2),
       ('Proteínas', true, 3),
       ('Vitaminas', false, 4),
       ('Minerales', false, 5),
       ('Esteroles', false, 6),
       ('Alcohol', true, 7),
       ('Otros', false, 7),
       ('Energía', true, 8)

ON CONFLICT DO NOTHING;

INSERT INTO nutrifoods.nutrient (name, also_called, is_calculated, essentiality, subtype_id)
VALUES
    -- Carbohidratos --
    ('Carbohidratos, total', '', true, 0, 1),                                -- 
    ('Fibra', '', false, 2, 2),                                              -- 
    ('Almidón', 'Fécula', false, 2, 3),
    -- Azúcares --
    ('Azúcares, total', '', true, 0, 4),
    ('Fructosa', 'Levulosa', false, 2, 4),
    ('Galactosa', '', false, 2, 4),
    ('Glucosa', '', false, 2, 4),
    ('Lactosa', '', false, 2, 4),
    ('Maltosa', '', false, 2, 4),
    ('Sucrosa', 'Sacarosa', false, 2, 4),                                    -- 
    --Ácidos grasos--
    ('Ácidos grasos, total', '', true, 2, 5),                                -- 
    -- Ácidos grasos saturados --
    ('Ácidos grasos saturados, total', '', true, 2, 6),
    ('Ácido butanoico', 'Ácido butírico', false, 2, 6),                      -- SFA 4:0  
    ('Ácido hexanoico', 'Ácido caproico', false, 2, 6),                      -- SFA 6:0
    ('Ácido octanoico', 'Ácido caprílico', false, 2, 6),                     -- SFA 8:0
    ('Ácido decanoico', 'Ácido cáprico', false, 2, 6),                       -- SFA 10:0
    ('Ácido dodecanoico', 'Ácido láurico', false, 2, 6),                     -- SFA 12:0
    ('Ácido tridecanoico', '', false, 2, 6),                                 -- SFA 13:0
    ('Ácido tetradecanoico', 'Ácido mirístico', false, 2, 6),                -- SFA 14:0
    ('Ácido pentadecanoico', '', false, 2, 6),                               -- SFA 15:0
    ('Ácido hexadecanoico', 'Ácido palmítico', false, 2, 6),                 -- SFA 16:0  
    ('Ácido heptadecanoico', 'Ácido margárico', false, 2, 6),                -- SFA 17:0
    ('Ácido octadecanoico', 'Ácido esteárico', false, 2, 6),                 -- SFA 18:0
    ('Ácido eicosanoico', 'Ácido araquídico', false, 2, 6),                  -- SFA 20:0
    ('Ácido docosanoico', 'Ácido behénico', false, 2, 6),                    -- SFA 22:0
    ('Ácido tetracosanoico', 'Ácido lignocérico', false, 2, 6),              -- SFA 24:0 
    -- Ácidos grasos monoinsaturados --
    ('Ácidos grasos monoinsaturados, total', '', true, 2, 7),
    ('Ácido tetradecenoico', 'Ácido miristoleico', false, 2, 7),             -- MUFA 14:1 
    ('Ácido pentadecenoico', '', false, 2, 7),                               -- MUFA 15:1
    ('Ácido hexadecenoico', 'Ácido miristoleico', false, 2, 7),              -- MUFA 16:1
    ('Ácido cis-hexadecenoico', '', false, 2, 7),                            -- MUFA 16:1 c
    ('Ácido heptadecenoico', '', false, 2, 7),                               -- MUFA 17:1
    ('Ácido octadecenoico', 'Ácido oleico', false, 2, 7),                    -- MUFA 18:1
    ('Ácido cis-octadecenoico', '', false, 2, 7),                            -- MUFA 18:1 c
    ('Ácido eicosenoico', 'Ácido gadoleico', false, 2, 7),                   -- MUFA 20:1
    ('Ácido docosenoico', '', false, 2, 7),                                  -- MUFA 22:1
    ('Ácido cis-docosenoico', 'Ácido erúcico', false, 2, 7),                 -- MUFA 22:1 c
    ('Ácido cis-tetracosenoico', 'Ácido nervónico', false, 2, 7),            -- MUFA 24:1 c
    -- Ácidos grasos poliinsaturados --
    ('Ácidos grasos poliinsaturados, total', '', true, 2, 8),
    ('Ácido octadecadienoico', 'Ácido linoleico', false, 2, 8),              -- PUFA 18:2 
    ('Ácido cis-octadecadienoico', '', false, 2, 8),                         -- PUFA 18:2 c n-6 
    ('Ácido i-octadecadienoico', '', false, 2, 8),                           -- PUFA 18:2 i 
    ('Ácido conjugated-octadecadienoico', '', false, 2, 8),                  -- PUFA 18:2 con 
    ('Ácido octadecatrienoico', 'Ácido linolenico', false, 2, 8),            -- PUFA 18:3 
    ('Ácido cis3-octadecatrienoico', 'Ácido alpha-linolenico', false, 2, 8), -- PUFA 18:3 c n-3 
    ('Ácido cis6-octadecatrienoico', 'Ácido gamma-linolenico', false, 2, 8), -- PUFA 18:3 c n-6 
    ('Ácido i-octadecatrienoico', '', false, 2, 8),                          -- PUFA 18:3 i 
    ('Ácido octadecatetraenoico', 'Ácido parinarico', false, 2, 8),          -- PUFA 18:4 
    ('Ácido eicosatrienoico', '', false, 2, 8),                              -- PUFA 20:3 
    ('Ácido 3-eicosatrienoico', '', false, 2, 8),                            -- PUFA 20:3 n-3 
    ('Ácido 6-eicosatrienoico', '', false, 2, 8),                            -- PUFA 20:3 n-6 
    ('Ácido eicosatetraenoico', '', false, 2, 8),                            -- PUFA 20:4 
    ('Ácido 6-eicosatetraenoico', 'Ácido araquidónico', false, 2, 8),        -- PUFA 20:4 n-6 
    ('Ácido eicosapentaenoico', 'Ácido timnodónico', false, 2, 8),           -- PUFA 20:5 n-3 
    ('Ácido docosapentaenoico', '', false, 2, 8),                            -- PUFA 22:5 n-3 
    ('Ácido docosahexaenoico', '', false, 2, 8),                             -- PUFA 22:6 
    -- Ácidos grasos trans--
    ('Ácidos grasos trans, total', '', true, 2, 9),
    ('Ácido trans-monoenoico, total', '', true, 2, 9),
    ('Ácido trans-polinoico, total', '', true, 2, 9),
    -- Proteínas -
    ('Ácido aspártico', '', false, 2, 10),
    ('Ácido glutámico', '', false, 2, 10),
    ('Alanina', '', false, 2, 10),
    ('Arginina', '', false, 2, 10),
    ('Cistina', '', false, 2, 10),
    ('Fenilalanina', '', false, 2, 10),
    ('Glicina', '', false, 2, 10),
    ('Histidina', '', false, 2, 10),
    ('Isoleucina', '', false, 2, 10),
    ('Leucina', '', false, 2, 10),
    ('Lisina', '', false, 2, 10),
    ('Metionina', '', false, 2, 10),
    ('Prolina', '', false, 2, 10),
    ('Serina', '', false, 2, 10),
    ('Tirosina', '', false, 2, 10),
    ('Treonina', '', false, 2, 10),
    ('Triptófano', '', false, 2, 10),
    ('Valina', '', false, 2, 10),
    -- Vitaminas --
    ('Ácido ascórbico', 'Vitamina C', false, 0, 11),
    ('Tiamina', 'Vitamina B1', false, 0, 11),
    ('Riboflavina', 'Vitamina B2', false, 0, 11),
    ('Niacina', 'Vitamina B3', false, 0, 11),
    ('Ácido pantoténico', 'Vitamina B5', false, 0, 11),
    ('Vitamina B6', '', false, 0, 11),
    ('Vitamina B12', '', false, 0, 11),
    ('Ácido fólico', 'Vitamina B9', false, 0, 11),
    ('Colina', '', false, 1, 11),
    ('Vitamina A', '', false, 2, 11),
    ('Vitamina D', '', false, 2, 11),
    ('Vitamina E', '', false, 2, 11),
    ('Vitamina K', '', false, 2, 11),
    -- Minerales --
    ('Calcio', 'Ca', false, 2, 12),
    ('Hierro', 'Fe', false, 2, 12),
    ('Magnesio', 'Mg', false, 2, 12),
    ('Fósforo', 'P', false, 2, 12),
    ('Potasio', 'K', false, 2, 12),
    ('Sodio', 'Na', false, 2, 12),
    ('Zinc', 'Zn', false, 2, 12),
    ('Cobre', 'Cu', false, 2, 12),
    ('Manganeso', 'Mn', false, 2, 12),
    ('Selenio', 'Se', false, 2, 12),
    ('Fluoruro', 'F', false, 2, 12),
    -- Esteroles --
    ('Colesterol', '', false, 2, 13),
    ('Estigmasterol', 'Stigmasterin', false, 2, 13),
    ('Campesterol', 'Campestanol', false, 2, 13),
    ('Beta-sitosterol', '', false, 2, 13),
    -- Alcohol --
    ('Alcohol', '', false, 2, 14),
    ('Ácido eicosadienoico', '', false, 2, 8),                               -- PUFA 20:2 n-6 cis
    ('Energía', '', true, 0, 16)


ON CONFLICT DO NOTHING;