SET SEARCH_PATH = "nutrifoods";

INSERT INTO nutrient_type (name)
VALUES ('Energía'),
       ('Carbohidratos'),
       ('Lípidos'),
       ('Proteínas'),
       ('Vitaminas'),
       ('Minerales'),
       ('Esteroles'),
       ('Otros')

ON CONFLICT DO NOTHING;

INSERT INTO nutrient_subtype (name, provides_energy, type_id)
VALUES ('Energía', true, 1),
       ('Carbohidratos, total', true, 2),
       ('Fibra', true, 2),
       ('Azúcar', true, 2),
       ('Almidón', true, 2),
       ('Grasa, total', true, 3),
       ('Ácidos grasos saturados', true, 3),
       ('Ácidos grasos monoinsaturados', true, 3),
       ('Ácidos grasos poliinsaturados', true, 3),
       ('Ácidos grasos trans', true, 3),
       ('Proteínas', true, 4),
       ('Vitaminas', false, 5),
       ('Minerales', false, 6),
       ('Esteroles', false, 7),
       ('Alcohol', true, 8),
       ('Otros', false, 8)

ON CONFLICT DO NOTHING;

INSERT INTO nutrient (name, also_called, is_calculated, essentiality, subtype_id)
VALUES 
       ('Energía', '', true, 1, 1),
       ('Carbohidratos, total', '', true, 3, 2),
       ('Fibra', '', false, 2, 3),
       -- Azúcar --
       ('Azúcares, total', '', true, 3, 4),
       ('Sucrosa', 'Sacarosa', false, 2, 4),
       ('Glucosa', '', false, 3, 4),
       ('Fructosa', 'Levulosa', false, 3, 4),
       ('Lactosa', '', false, 3, 4),
       ('Maltosa', '', false, 3, 4),
       ('Galactosa', '', false, 3, 4),
       -- Almidón --
       ('Almidón', 'Fécula', false, 3, 5),
       
       ('Ácidos grasos, total', '', true, 3, 6),
       -- Ácidos grasos saturados --
       ('Ácidos grasos saturados, total', '', true, 3, 7),
       ('Ácido butanoico', 'Ácido butírico', false, 3, 7),                      -- SFA 4:0  
       ('Ácido hexanoico', 'Ácido caproico', false, 3, 7),                      -- SFA 6:0
       ('Ácido octanoico', 'Ácido caprílico', false, 3, 7),                     -- SFA 8:0
       ('Ácido decanoico', 'Ácido cáprico', false, 3, 7),                       -- SFA 10:0
       ('Ácido dodecanoico', 'Ácido láurico', false, 3, 7),                     -- SFA 12:0
       ('Ácido tridecanoico', '', false, 3, 7),                                 -- SFA 13:0
       ('Ácido tetradecanoico', 'Ácido mirístico', false, 3, 7),                -- SFA 14:0
       ('Ácido pentadecanoico', '', false, 3, 7),                               -- SFA 15:0
       ('Ácido hexadecanoico', 'Ácido palmítico', false, 3, 7),                 -- SFA 16:0  
       ('Ácido heptadecanoico', 'Ácido margárico', false, 3, 7),                -- SFA 17:0
       ('Ácido octadecanoico', 'Ácido esteárico', false, 3, 7),                 -- SFA 18:0
       ('Ácido eicosanoico', 'Ácido araquídico', false, 3, 7),                  -- SFA 20:0
       ('Ácido docosanoico', 'Ácido behénico', false, 3, 7),                    -- SFA 22:0
       ('Ácido tetracosanoico', 'Ácido lignocérico', false, 3, 7),              -- SFA 24:0 
       -- Ácidos grasos monoinsaturados --
       ('Ácidos grasos monoinsaturados, total', '', true, 3, 8),
       ('Ácido tetradecenoico', 'Ácido miristoleico', false, 2, 8),             -- MUFA 14:1 
       ('Ácido pentadecenoico', '', false, 2, 8),                               -- MUFA 15:1
       ('Ácido hexadecenoico', 'Ácido palmitoleico', false, 2, 8),              -- MUFA 16:1
       ('Ácido cis-hexadecenoico', '', false, 2, 8),                            -- MUFA 16:1 c
       ('Ácido heptadecenoico', '', false, 2, 8),                               -- MUFA 17:1
       ('Ácido i-heptadecenoico', '', false, 2, 8),                             -- MUFA 17:1 i
       ('Ácido octadecenoico', 'Ácido oleico', false, 2, 8),                    -- MUFA 18:1
       ('Ácido cis-octadecenoico', '', false, 2, 8),                            -- MUFA 18:1 c
       ('Ácido eicosenoico', 'Ácido gadoleico', false, 2, 8),                   -- MUFA 20:1
       ('Ácido docosenoico', '', false, 2, 8),                                  -- MUFA 22:1
       ('Ácido cis-docosenoico', 'Ácido erúcico', false, 2, 8),                 -- MUFA 22:1 c
       ('Ácido cis-tetracosenoico', 'Ácido nervónico', false, 2, 8),            -- MUFA 24:1 c
       -- Ácidos grasos poliinsaturados  --
       ('Ácidos grasos poliinsaturados, total', '', true, 2, 9),
       ('Ácido octadecadienoico', 'Ácido linoleico', false, 1, 9),              -- PUFA 18:2 
       ('Ácido cis-octadecadienoico', '', false, 2, 9),                         -- PUFA 18:2 c n-6 
       ('Ácido i-octadecadienoico', '', false, 2, 9),                           -- PUFA 18:2 i 
       ('Ácido conjugated-octadecadienoico', '', false, 2, 9),                  -- PUFA 18:2 con 
       ('Ácido octadecatrienoico', 'Ácido linolenico', false, 1, 9),            -- PUFA 18:3 
       ('Ácido cis3-octadecatrienoico', 'Ácido alpha-linolenico', false, 2, 9), -- PUFA 18:3 c n-3 
       ('Ácido cis6-octadecatrienoico', 'Ácido gamma-linolenico', false, 2, 9), -- PUFA 18:3 c n-6 
       ('Ácido i-octadecatrienoico', '', false, 2, 9),                          -- PUFA 18:3 i 
       ('Ácido octadecatetraenoico', 'Ácido parinarico', false, 2, 9),          -- PUFA 18:4 
       ('Ácido eicosadienoico', '', false, 2, 9),                               -- PUFA 20:2 n-6 cis
       ('Ácido eicosatrienoico', '', false, 2, 9),                              -- PUFA 20:3 
       ('Ácido 3-eicosatrienoico', '', false, 2, 9),                            -- PUFA 20:3 n-3 
       ('Ácido 6-eicosatrienoico', '', false, 2, 9),                            -- PUFA 20:3 n-6 
       ('Ácido eicosatetraenoico', '', false, 2, 9),                            -- PUFA 20:4 
       ('Ácido 6-eicosatetraenoico', 'Ácido araquidónico', false, 2, 9),        -- PUFA 20:4 n-6 
       ('Ácido eicosapentaenoico', 'Ácido timnodónico', false, 2, 9),           -- PUFA 20:5 n-3 
       ('Ácido 3-docosapentaenoico', '', false, 2, 9),                          -- PUFA 22:5 n-3 
       ('Ácido 3-docosahexaenoico', '', false, 2, 9),                           -- PUFA 22:6 n-3
       
       -- Ácidos grasos trans--
       ('Ácidos grasos trans, total', '', true, 2, 10),
       ('Ácido trans-monoenoico, total', '', true, 2, 10),
       ('Ácido trans-polinoico, total', '', true, 2, 10),

       -- Proteínas -
       ('Proteína, total', '', true, 3, 11),
       ('Triptófano', '', false, 1, 11),
       ('Treonina', '', false, 1, 11),
       ('Isoleucina', '', false, 1, 11),
       ('Leucina', '', false, 1, 11),
       ('Lisina', '', false, 1, 11),
       ('Metionina', '', false, 1, 11),
       ('Cistina', '', false, 3, 11),
       ('Fenilalanina', '', false, 1, 11),
       ('Tirosina', '', false, 2, 11),
       ('Valina', '', false, 1, 11),
       ('Arginina', '', false, 3, 11),
       ('Histidina', '', false, 1, 11),
       ('Alanina', '', false, 3, 11),
       ('Ácido aspártico', '', false, 3, 11),
       ('Ácido glutámico', '', false, 3, 11),
       ('Glicina', '', false, 3, 11),
       ('Prolina', '', false, 2, 11),
       ('Serina', '', false, 2, 11),
       -- Vitaminas --
       ('Ácido ascórbico', 'Vitamina C', false, 1, 12),
       ('Tiamina', 'Vitamina B1', false, 1, 12),
       ('Riboflavina', 'Vitamina B2', false, 1, 12),
       ('Niacina', 'Vitamina B3', false, 1, 12),
       ('Ácido pantoténico', 'Vitamina B5', false, 1, 12),
       ('Vitamina B6', '', false, 1, 12),
       ('Folato, total', '', true, 3, 12),
       ('Ácido fólico', 'Vitamina B9', false, 1, 12),
       ('Folato, alimento', '', false, 2, 12),
       ('Folato, DFE', '', true, 2, 12),
       ('Colina', '', false, 2, 12),
       ('Betaína', '', false, 2, 12),
       ('Vitamina B12', '', false, 1, 12),
       ('Vitamina B12, añadida', '', false, 2, 12),
       ('Vitamina A', '', false, 2, 12),
       ('Retinol', '', false, 2, 12),
       ('Vitamina D', '', false, 2, 12),
       ('Vitamina E', '', false, 2, 12),
       ('Vitamina K', '', false, 2, 12),
       
       -- Minerales --
       ('Calcio', 'Ca', false, 1, 13),
       ('Hierro', 'Fe', false, 1, 13),
       ('Magnesio', 'Mg', false, 1, 13),
       ('Fósforo', 'P', false, 1, 13),
       ('Potasio', 'K', false, 1, 13),
       ('Sodio', 'Na', false, 1, 13),
       ('Zinc', 'Zn', false, 1, 13),
       ('Cobre', 'Cu', false, 1, 13),
       ('Manganeso', 'Mn', false, 1, 13),
       ('Selenio', 'Se', false, 1, 13),
       ('Fluoruro', 'F', false, 1, 13),
       
       -- Esteroles --
       ('Colesterol', '', false, 3, 14),
       ('Estigmasterol', 'Stigmasterin', false, 2, 14),
       ('Campesterol', 'Campestanol', false, 2, 14),
       ('Beta-sitosterol', '', false, 2, 14),
       
       -- Alcohol --
       ('Alcohol etílico', '', false, 3, 15),
       -- Otros --
       ('Agua', '', false, 1, 16),
       ('Ceniza', '', false, 2, 16),
       ('Cafeína', '', false, 3, 16),
       ('Teobromina', '', false, 3, 16)
;


SELECT id, name
from nutrifoods.nutrient_subtype;