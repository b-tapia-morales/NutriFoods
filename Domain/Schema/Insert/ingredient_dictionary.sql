SET SEARCH_PATH = "nutrifoods";

INSERT INTO ingredient_synonym (name, ingredient_id)
VALUES
    -- Palta --
    ('Abacate', find_ingredient_id('Palta')),
    ('Aguacate', find_ingredient_id('Palta')),
    ('Avocado', find_ingredient_id('Palta')),
    -- Albahaca --
    ('Albaca', find_ingredient_id('Albahaca')),
    ('Alhábega', find_ingredient_id('Albahaca')),
    ('Basílico', find_ingredient_id('Albahaca')),
    -- Pimiento rojo --
    ('Pimiento', find_ingredient_id('Pimiento rojo')),
    ('Pimentón rojo', find_ingredient_id('Pimiento rojo')),
    ('Morrón rojo', find_ingredient_id('Pimiento rojo')),
    -- Zapallo italiano --
    ('Zapallito', find_ingredient_id('Zapallo italiano')),
    ('Calabacita', find_ingredient_id('Zapallo italiano')),
    ('Calabacín', find_ingredient_id('Zapallo italiano')),
    -- Cebollín --
    ('Cebolleta', find_ingredient_id('Cebollín')),
    ('Puerro', find_ingredient_id('Cebollín')),
    ('Cebolla larga', find_ingredient_id('Cebollín')),
    -- Tomate cherry --
    ('Tomate cherri', find_ingredient_id('Tomate cherry')),
    ('Tomate cereza', find_ingredient_id('Tomate cherry')),
    ('Tomate pasa', find_ingredient_id('Tomate cherry')),
    ('Tomate uva', find_ingredient_id('Tomate cherry')),
    -- Brócoli --
    ('Brécol', find_ingredient_id('Brócoli')),
    ('Bróculi', find_ingredient_id('Brócoli')),
    -- Espárrago --
    ('Esparraguera', find_ingredient_id('Espárrago')),
    -- Merkén --
    ('Merquén', find_ingredient_id('Merkén')),
    -- Aceite de palta --
    ('Aceite de aguacate', find_ingredient_id('Aceite de palta')),
    -- Repollo --
    ('Col', find_ingredient_id('Repollo')),
    -- Arvejas --
    ('Alverjas', find_ingredient_id('Arvejas')),
    ('Chícharos', find_ingredient_id('Arvejas')),
    ('Guisantes', find_ingredient_id('Arvejas')),
    -- Pepino --
    ('Pepinillo', find_ingredient_id('Pepino')),
    -- Porotos verdes --
    ('Habichuelas', find_ingredient_id('Porotos verdes')),
    ('Ejote', find_ingredient_id('Porotos verdes')),
    ('Judías verdes', find_ingredient_id('Porotos verdes')),
    -- Sésamo --
    ('Semillas de sésamo', find_ingredient_id('Sésamo')),
    -- Linaza --
    ('Semillas de lino', find_ingredient_id('Linaza')),
    -- Curry --
    ('Curri', find_ingredient_id('Curry')),
    -- Pimiento verde --
    ('Pimentón verde', find_ingredient_id('Pimiento verde')),
    ('Morrón verde', find_ingredient_id('Pimiento verde')),
    -- Ciboulette --
    ('Cebolla de verdeo', find_ingredient_id('Ciboulette')),
    ('Cebollano', find_ingredient_id('Ciboulette')),
    ('Cebollino', find_ingredient_id('Ciboulette')),
    ('Ajo cebollino', find_ingredient_id('Ciboulette')),
    -- Queso cabra --
    ('Queso de cabra', find_ingredient_id('Queso cabra')),
    ('Queso de leche de cabra', find_ingredient_id('Queso cabra')),
    -- Vinagre balsámico --
    ('Aceto', find_ingredient_id('Vinagre balsámico')),
    ('Aceto balsámico', find_ingredient_id('Vinagre balsámico')),
    -- Laurel --
    ('Lauro', find_ingredient_id('Laurel')),
    -- Tahini --
    ('Tahina', find_ingredient_id('Tahini')),
    -- Camarón --
    ('Quisquilla', find_ingredient_id('Camarón')),
    ('Esquila', find_ingredient_id('Camarón')),
    -- Tocino --
    ('Tocineta', find_ingredient_id('Tocino')),
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
    ('Ahuyama', find_ingredient_id('Zapallo camote')),
    -- Mermelada de Frambuesa --
    ('Mermelada de Parrón', find_ingredient_id('Mermelada de frambuesa')),
    -- Tortilla --
    ('Memela', find_ingredient_id('Tortilla')),
    -- Caldo de ave --
    ('Consomé de ave', find_ingredient_id('Caldo de ave')),
    ('Caldo de pollo', find_ingredient_id('Caldo de ave')),
    ('Consomé de pollo', find_ingredient_id('Caldo de ave')),
    -- Granos de choclo --
    ('Granos de elote', find_ingredient_id('Granos de choclo')),
    ('Granos de chócolo', find_ingredient_id('Granos de choclo')),
    ('Granos de jojoto', find_ingredient_id('Granos de choclo')),
    -- Tapas para empanada --
    ('Masas de empanada', find_ingredient_id('Tapas para empanada')),
    -- Tortilla de maíz --
    ('Memela', find_ingredient_id('Tortilla de maíz')),
    -- Salsa de tomate --
    ('Salsa de jitomate', find_ingredient_id('Salsa de tomate')),
    -- Crema ácida --
    ('Crema agria', find_ingredient_id('Crema ácida')),
    -- Crema de leche --
    ('Nata', find_ingredient_id('Crema de leche')),
    -- Pure de zapallo --
    ('Puré de Ahuyama', find_ingredient_id('Pure de zapallo')),
    -- Lomo de ternera --
    ('Solomillo', find_ingredient_id('Lomo de ternera')),
    ('Solomillo de ternera', find_ingredient_id('Lomo de ternera')),
    -- Queso azul --
    ('Bleu', find_ingredient_id('Queso azul')),
    -- Anato --
    ('Annatto', find_ingredient_id('Anato')),
    -- Pasta de chipotle --
    ('Pasta de kaponchili', find_ingredient_id('Pasta de chipotle')),
    -- Gelatina en polvo --
    ('Jalea en polvo', find_ingredient_id('Gelatina en polvo')),
    -- Caldo de carne --
    ('Consomé de carne', find_ingredient_id('Caldo de carne')),
    -- Aceituna verde --
    ('Oliva verde', find_ingredient_id('Aceituna verde')),
    -- Almeja --
    ('Taca', find_ingredient_id('Almeja')),
    -- Semillas de zapallo --
    ('Semillas de calabaza', find_ingredient_id('Semillas de zapallo')),
    ('Semillas de calabacera', find_ingredient_id('Semillas de zapallo')),
    -- Mandarina --
    ('Naranja mandarina', find_ingredient_id('Mandarina')),
    ('Naranja mandarina', find_ingredient_id('Tangerina')),
    ('Naranja mandarina', find_ingredient_id('Tanjorona')),
    -- Bacalao --
    ('Abadejo', find_ingredient_id('Bacalao')),
    -- Chuleta de cerdo --
    ('Chuleta de chancho', find_ingredient_id('Chuleta de cerdo')),
    -- Carne de cordero --
    ('Carne de lechazo', find_ingredient_id('Carne de cordero')),
    ('Carne de ternasco', find_ingredient_id('Carne de cordero')),
    -- Rabo de ternera --
    ('Rabo de toro', find_ingredient_id('Rabo de ternera')),
    ('Cola de ternera', find_ingredient_id('Rabo de ternera')),
    ('Cola de toro', find_ingredient_id('Rabo de ternera')),
    -- Mostaza --
    ('Savora', find_ingredient_id('Mostaza')),
    -- Solomillo de cerdo --
    ('Lomo de cerdo', find_ingredient_id('Solomillo de cerdo')),
    ('Solomillo de chancho', find_ingredient_id('Solomillo de cerdo')),
    -- Manjar --
    ('Arequipe', find_ingredient_id('Manjar')),
    ('Cajeta', find_ingredient_id('Manjar')),
    ('Manjar blanco', find_ingredient_id('Manjar')),
    ('Dulce de leche', find_ingredient_id('Manjar')),
    -- Filete --
    ('Filete miñon', find_ingredient_id('Filete')),
    ('Filet mignon', find_ingredient_id('Filete')),
    -- Berries --
    ('Frutos rojos', find_ingredient_id('Berries')),
    ('Frutos del bosque', find_ingredient_id('Berries')),
    -- Ralladura de Limón --
    ('Ralladura de lima', find_ingredient_id('Ralladura de limón')),
    -- Marraqueta --
    ('Pan francés', find_ingredient_id('Marraqueta')),
    ('Pan batido', find_ingredient_id('Marraqueta')),

    -- Plateada --
    ('Tapa de asado', find_ingredient_id('Plateada')),
    ('Tapa de lomo alto', find_ingredient_id('Plateada')),
    -- Salsa blanca --
    ('Bechamel', find_ingredient_id('Salsa blanca')),
    ('Besamela', find_ingredient_id('Salsa blanca')),
    -- Crispy --
    ('Crocante', find_ingredient_id('Crispy')),
    -- Panqueque --
    ('Tortita', find_ingredient_id('Panqueque')),
    ('Panqueca', find_ingredient_id('Panqueque')),
    ('Hotcake', find_ingredient_id('Panqueque')),
    ('Celestino', find_ingredient_id('Panqueque')),
    ('Panqueque celestino', find_ingredient_id('Panqueque')),
    -- Chancaca --
    ('Panela', find_ingredient_id('Chancaca')),
    ('Papelón', find_ingredient_id('Chancaca')),
    ('Piloncillo', find_ingredient_id('Chancaca')),
    ('Tapadulce', find_ingredient_id('Chancaca')),

    -- Durazno --
    ('Melocotón', find_ingredient_id('Durazno')),
    -- Moras --
    ('Zarzamoras', find_ingredient_id('Moras')),
    ('Murras', find_ingredient_id('Moras')),

    -- Lomo liso --
    ('Nueva york', find_ingredient_id('Lomo liso')),
    ('Bife angosto', find_ingredient_id('Lomo liso')),
    ('Lomo', find_ingredient_id('Lomo liso')),
    -- Posta --

    -- Manteca --
    ('Mantequilla', find_ingredient_id('Manteca')),
    -- Pollo ganso --
    ('Redondo redondel', find_ingredient_id('Pollo ganso')),
    ('Asado pejerrey', find_ingredient_id('Pollo ganso')),
    ('Pulpa choriso', find_ingredient_id('Pollo ganso')),

    -- Lomo vetado --
    ('Bife ancho', find_ingredient_id('Lomo vetado')),
    ('Lomo alto', find_ingredient_id('Lomo vetado')),

    -- Trutros cortos --
    ('Tuto cortos', find_ingredient_id('Trutros cortos')),
    -- Cerveza --
    ('Birra', find_ingredient_id('Cerveza')),
    ('Cerbatana', find_ingredient_id('Cerveza')),
    ('Chela', find_ingredient_id('Cerveza')),
    ('Pilsen', find_ingredient_id('Cerveza')),
    -- Champiñon ostra --
    ('Seta ostra', find_ingredient_id('Champiñon ostra')),
    -- Pisco --
    ('Agua ardiente', find_ingredient_id('Pisco')),
    -- Pan de hamburguesa --
    ('Bollos de hamburguesa', find_ingredient_id('Pan de hamburguesa')),

    -- Bizcocho --
    ('Ponqué', find_ingredient_id('Bizcocho')),
    ('Queque', find_ingredient_id('Bizcocho')),
    ('Torta', find_ingredient_id('Bizcocho')),
    --Salsa barbecue--
    ('Salsa barbacoa', find_ingredient_id('Salsa barbecue')),
    -- ASiento --
    -- Dientes de dragón --
    ('Brotes de soya', find_ingredient_id('Dientes de dragón')),
    ('Brotes de soja', find_ingredient_id('Dientes de dragón')),
    -- Nutella --
    ('Crema de avellanas', find_ingredient_id('Nutella')),
    -- Frutos rojos --
    -- Huesillo --
    ('Durazno seco', find_ingredient_id('Huesillo')),
    -- Pistacho --
    ('Alfóncigo', find_ingredient_id('Pistacho')),
    ('Alhócigo', find_ingredient_id('Pistacho'))
  
    
;