SET SEARCH_PATH = "nutrifoods";

INSERT INTO ingredient_synonym (name, ingredient_id)
VALUES
    -- Palta --
    ('Abacate', 15),
    ('Aguacate', 15),
    ('Avocado', 15),
    -- Albahaca --
    ('Albaca', 16),
    ('Alhábega', 16),
    ('Basílico', 16),
    -- Pimiento rojo --
    ('Pimiento', 19),
    ('Pimentón rojo', 19),
    ('Morrón rojo', 19),
    -- Zapallo italiano --
    ('Zapallito', 29),
    ('Calabacita', 29),
    ('Calabacín', 29),
    -- Cebollín --
    ('Cebolleta', 37),
    ('Puerro', 37),
    ('Cebolla larga', 37),
    -- Tomate cherry --
    ('Tomate cherri', 39),
    ('Tomate cereza', 39),
    ('Tomate pasa', 39),
    ('Tomate uva', 39),
    -- Brócoli --
    ('Brécol', 43),
    ('Bróculi', 43),
    -- Espárrago --
    ('Esparraguera', 45),
    -- Merkén --
    ('Merquén', 51),
    -- Aceite de palta --
    ('Aceite de aguacate', 62),
    -- Repollo --
    ('Col', 63),
    -- Arvejas --
    ('Alverjas', 65),
    ('Chícharos', 65),
    ('Guisantes', 65),
    -- Pepino --
    ('Pepinillo', 66),
    -- Porotos verdes --
    ('Habichuelas', 67),
    ('Ejote', 67),
    ('Judías verdes', 67),
    -- Sésamo --
    ('Semillas de sésamo', 69),
    -- Linaza --
    ('Semillas de lino', 70),
    -- Curry --
    ('Curri', 74),
    -- Pimiento verde --
    ('Pimentón verde', 79),
    ('Morrón verde', 79),
    -- Ciboulette --
    ('Cebolla de verdeo', 80),
    ('Cebollano', 80),
    ('Cebollino', 80),
    ('Ajo cebollino', 80),
    -- Queso cabra --
    ('Queso de cabra', 81),
    ('Queso de leche de cabra', 81),
    -- Vinagre balsámico --
    ('Aceto', 82),
    ('Aceto balsámico', 82),
    -- Laurel --
    ('Lauro', 83),
    -- Tahini --
    ('Tahina', 85),
    -- Camarón --
    ('Quisquilla', 89),
    ('Esquila', 89),
    -- Tocino --
    ('Tocineta', 90),
    -- Betarraga --
    ('Remolacha', find_ingredient_id('Betarraga')),
    ('Betabel', find_ingredient_id('Betarraga')),
    -- Alcachofa --
    ('Alcacil', find_ingredient_id('Alcachofa')),
    ('Alcaucil', find_ingredient_id('Alcachofa')),
    ('Alcarcil', find_ingredient_id('Alcachofa')),
    -- Eneldo --
    ('Aneldo', find_ingredient_id('Eneldo')),
    ('Abesón', find_ingredient_id('Eneldo')),
    ('Hinojo hediondo', find_ingredient_id('Eneldo')),
    ('Hinojo fétido', find_ingredient_id('Eneldo')),
    -- Salsa Inglesa --
    ('Salsa Worcestershire', find_ingredient_id('Salsa Inglesa')),
    ('Salsa Worcester', find_ingredient_id('Salsa Inglesa')),
    -- Pimiento amarillo --
    ('Morrón amarillo', find_ingredient_id('Pimiento amarillo')),
    -- Cerdo --
    ('Animal de bellota', find_ingredient_id('Cerdo')),
    ('Chancho', find_ingredient_id('Cerdo')),
    ('Coche', find_ingredient_id('Cerdo')),
    ('Cochino', find_ingredient_id('Cerdo')),
    ('Cocho', find_ingredient_id('Cerdo')),
    ('Cuche', find_ingredient_id('Cerdo')),
    ('Cuchi', find_ingredient_id('Cerdo')),
    ('Cuino', find_ingredient_id('Cerdo')),
    ('Cuto', find_ingredient_id('Cerdo')),
    ('Gorrino', find_ingredient_id('Cerdo')),
    ('Guarro', find_ingredient_id('Cerdo')),
    ('Marrano', find_ingredient_id('Cerdo')),
    ('Puerco', find_ingredient_id('Cerdo')),
    ('Tunco', find_ingredient_id('Cerdo')),
    ('Lecho', find_ingredient_id('Cerdo')),
    ('Cochinillo', find_ingredient_id('Cerdo')),
    -- Ají --
    ('Chile', find_ingredient_id('Ají')),
    ('Ají en vaina', find_ingredient_id('Cerdo')),

    -- Anchoa --
    ('Anchoveta', find_ingredient_id('Anchoa')),
    ('Bocarte', find_ingredient_id('Anchoa')),
    ('Boquerón', find_ingredient_id('Anchoa')),
    -- Chalota --
    ('Escaloña', find_ingredient_id('Chalota')),
    -- Frutilla --
    ('Fresa', find_ingredient_id('Frutilla')),
    -- Porotos --
    ('Alubia', find_ingredient_id('Porotos')),
    ('Caraota', find_ingredient_id('Porotos')),
    ('Frejol', find_ingredient_id('Porotos')),
    ('Frijol', find_ingredient_id('Porotos')),
    ('Haba', find_ingredient_id('Porotos')),
    ('Habichuela', find_ingredient_id('Porotos')),
    ('Judía', find_ingredient_id('Porotos')),
    -- Pasas --
    ('Pasa de uva', find_ingredient_id('Pasas')),
    ('Uva pasa', find_ingredient_id('Pasas')),
    ('Pansa', find_ingredient_id('Pasas')),
    -- Ají verde --
    ('Chile verde', find_ingredient_id('Ají verde')),
    -- Maicena -- 
    ('Almidón de Maíz', find_ingredient_id('Maicena')),
    ('Maizena', find_ingredient_id('Maicena')),
    ('Fécula de maíz', find_ingredient_id('Maicena')),
    -- Porotos granados --
    ('Chaucha', find_ingredient_id('Porotos granados')),
    ('Ejote', find_ingredient_id('Porotos granados')),
    ('Judía verde', find_ingredient_id('Porotos granados')),
    ('Vainica', find_ingredient_id('Porotos granados')),
    ('Vainita', find_ingredient_id('Porotos granados')),
    -- Ricotta --
    ('Requesón', find_ingredient_id('Ricotta')),
    -- Jalapeño --
    ('Cuarasmeñio', find_ingredient_id('Jalapeño')),
    -- Jamón --
    ('Jamón york', find_ingredient_id('Jamón')),
    ('Jamoneta', find_ingredient_id('Jamón')),
    -- Ulte --
    ('Lembo', find_ingredient_id('Ulte')),
    ('Lunfo', find_ingredient_id('Ulte')),
    ('Raguay', find_ingredient_id('Ulte')),
    -- Maracuyá --
    ('Calala', find_ingredient_id('Maracuyá')),
    ('Chinola', find_ingredient_id('Maracuyá')),
    ('Parchío', find_ingredient_id('Maracuyá')),
    ('Parchita', find_ingredient_id('Maracuyá')),
    -- Escalopa de carne --
    ('Milanesa', find_ingredient_id('Escalopa de carne')),
    -- Papas fritas --
    ('Papas a la francesa', find_ingredient_id('Papas fritas')),
    ('Papitas', find_ingredient_id('Papas fritas')),
    ('Patatas fritas', find_ingredient_id('Papas fritas')),
    -- Ketchup --
    ('Cachú', find_ingredient_id('Ketchup')),
    -- Piña --
    ('Ananá', find_ingredient_id('Piña')),
    -- Plátano --
    ('Banana', find_ingredient_id('Plátano')),
    ('Cambur', find_ingredient_id('Plátano')),
    ('Guineo', find_ingredient_id('Plátano')),
    -- Cochayuyo --
    ('Chicoria de mar', find_ingredient_id('Cochayuyo')),
    ('Mochoco', find_ingredient_id('Cochayuyo')),
    ('Yuyo', find_ingredient_id('Cochayuyo')),
    -- Acelga --
    ('Bleda', find_ingredient_id('Acelga')),
    -- Quinoa --
    ('Quinua', find_ingredient_id('Quinoa')),
    -- Salsa de soya --
    ('Salsa de soja', find_ingredient_id('Salsa de soya')),
    -- Azúcar rubia --
    ('Azúcar amarilla', find_ingredient_id('Azúcar rubia')),
    ('Azúcar negra', find_ingredient_id('Azúcar rubia')),
    ('Azúcar morena', find_ingredient_id('Azúcar rubia')),
    ('Azúcar terciado', find_ingredient_id('Azúcar rubia')),
    -- Tomate pera --
    ('Tomate roma', find_ingredient_id('Tomate pera')),    
    -- Zapallo camote --
    ('Ahuyama', find_ingredient_id('Zapallo camote'))
    
    
    

   
;