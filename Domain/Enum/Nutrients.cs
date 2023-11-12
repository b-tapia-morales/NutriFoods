using Ardalis.SmartEnum;
using static Domain.Enum.Units;

namespace Domain.Enum;

public class Nutrients : SmartEnum<Nutrients>, IHierarchicalEnum<Nutrients, NutrientToken>
{
    public static readonly Nutrients None =
        new(nameof(None), (int)NutrientToken.None, "", "", false, Units.None, null);

    public static readonly Nutrients Energy =
        new(nameof(Energy), (int)NutrientToken.Energy, "Energía, total", "", true, KiloCalories, null);

    public static readonly Nutrients Carbohydrates =
        new(nameof(Carbohydrates), (int)NutrientToken.Carbohydrates, "Carbohidratos, total", "", true, Grams, null);

    public static readonly Nutrients Fiber =
        new(nameof(Fiber), (int)NutrientToken.Fiber, "Fibra", "", false, Grams, Carbohydrates);

    public static readonly Nutrients Sugars =
        new(nameof(Sugars), (int)NutrientToken.Sugars, "Azúcares, total", "", true, Grams, Carbohydrates);

    public static readonly Nutrients Sucrose =
        new(nameof(Sucrose), (int)NutrientToken.Sucrose, "Sucrosa", "Sacarosa", false, Grams, Sugars);

    public static readonly Nutrients Glucose =
        new(nameof(Glucose), (int)NutrientToken.Glucose, "Glucosa", "", false, Grams, Sugars);

    public static readonly Nutrients Fructose =
        new(nameof(Fructose), (int)NutrientToken.Fructose, "Fructosa", "Levulosa", false, Grams, Sugars);

    public static readonly Nutrients Lactose =
        new(nameof(Lactose), (int)NutrientToken.Lactose, "Lactosa", "", false, Grams, Sugars);

    public static readonly Nutrients Maltose =
        new(nameof(Maltose), (int)NutrientToken.Maltose, "Maltosa", "", false, Grams, Sugars);

    public static readonly Nutrients Galactose =
        new(nameof(Galactose), (int)NutrientToken.Galactose, "Galactosa", "", false, Grams, Sugars);

    public static readonly Nutrients Starch =
        new(nameof(Starch), (int)NutrientToken.Starch, "Almidón", "Fécula", false, Grams, Carbohydrates);

    public static readonly Nutrients FattyAcids =
        new(nameof(FattyAcids), (int)NutrientToken.FattyAcids, "Ácidos grasos, total", "", true, Grams, null);

    public static readonly Nutrients Saturated =
        new(nameof(Saturated), (int)NutrientToken.Saturated, "Ácidos grasos saturados, total", "", true,
            Grams, FattyAcids);

    public static readonly Nutrients Butanoic =
        new(nameof(Butanoic), (int)NutrientToken.Butanoic, "Ácido butanoico", "Ácido butírico", false,
            Grams, Saturated);

    public static readonly Nutrients Hexanoic =
        new(nameof(Hexanoic), (int)NutrientToken.Hexanoic, "Ácido hexanoico", "Ácido caproico", false,
            Grams, Saturated);

    public static readonly Nutrients Octanoic =
        new(nameof(Octanoic), (int)NutrientToken.Octanoic, "Ácido octanoico", "Ácido caprílico", false,
            Grams, Saturated);

    public static readonly Nutrients Decanoic =
        new(nameof(Decanoic), (int)NutrientToken.Decanoic, "Ácido decanoico", "Ácido cáprico", false, Grams, Saturated);

    public static readonly Nutrients Dodecanoic =
        new(nameof(Dodecanoic), (int)NutrientToken.Dodecanoic, "Ácido dodecanoico", "Ácido láurico", false,
            Grams, Saturated);

    public static readonly Nutrients Tridecanoic =
        new(nameof(Tridecanoic), (int)NutrientToken.Tridecanoic, "Ácido tridecanoico", "", false, Grams, Saturated);

    public static readonly Nutrients Tetradecanoic =
        new(nameof(Tetradecanoic), (int)NutrientToken.Tetradecanoic, "Ácido tetradecanoico", "Ácido mirístico", false,
            Grams, Saturated);

    public static readonly Nutrients Pentadecanoic =
        new(nameof(Pentadecanoic), (int)NutrientToken.Pentadecanoic, "Ácido pentadecanoico", "", false,
            Grams, Saturated);

    public static readonly Nutrients Hexadecanoic =
        new(nameof(Hexadecanoic), (int)NutrientToken.Hexadecanoic, "Ácido hexadecanoico", "Ácido palmítico", false,
            Grams, Saturated);

    public static readonly Nutrients Heptadecanoic =
        new(nameof(Heptadecanoic), (int)NutrientToken.Heptadecanoic, "Ácido heptadecanoico", "Ácido margárico", false,
            Grams, Saturated);

    public static readonly Nutrients Octadecanoic =
        new(nameof(Octadecanoic), (int)NutrientToken.Octadecanoic, "Ácido octadecanoico", "Ácido esteárico", false,
            Grams, Saturated);

    public static readonly Nutrients Eicosanoic =
        new(nameof(Eicosanoic), (int)NutrientToken.Eicosanoic, "Ácido eicosanoico", "Ácido araquídico", false, Grams,
            Saturated);

    public static readonly Nutrients Docosanoic =
        new(nameof(Docosanoic), (int)NutrientToken.Docosanoic, "Ácido docosanoico", "Ácido behénico", false,
            Grams, Saturated);

    public static readonly Nutrients Tetracosanoic =
        new(nameof(Tetracosanoic), (int)NutrientToken.Tetracosanoic, "Ácido tetracosanoico", "Ácido lignocérico", false,
            Grams, Saturated);

    public static readonly Nutrients Monounsaturated =
        new(nameof(Monounsaturated), (int)NutrientToken.Monounsaturated, "Ácidos grasos monoinsaturados, total", "",
            true, Grams, FattyAcids);

    public static readonly Nutrients Tetradecenoic =
        new(nameof(Tetradecenoic), (int)NutrientToken.Tetradecenoic, "Ácido tetradecenoico", "Ácido miristoleico",
            false, Grams, Monounsaturated);

    public static readonly Nutrients Pentadecenoic =
        new(nameof(Pentadecenoic), (int)NutrientToken.Pentadecenoic, "Ácido pentadecenoico", "", false,
            Grams, Monounsaturated);

    public static readonly Nutrients Hexadecenoic =
        new(nameof(Hexadecenoic), (int)NutrientToken.Hexadecenoic, "Ácido hexadecenoico", "Ácido palmitoleico", false,
            Grams, Monounsaturated);

    public static readonly Nutrients CisHexadecenoic =
        new(nameof(CisHexadecenoic), (int)NutrientToken.CisHexadecenoic, "Ácido cis-hexadecenoico", "", false, Grams,
            Monounsaturated);

    public static readonly Nutrients Heptadecenoic =
        new(nameof(Heptadecenoic), (int)NutrientToken.Heptadecenoic, "Ácido heptadecenoico", "", false,
            Grams, Monounsaturated);

    public static readonly Nutrients Iheptadecenoic =
        new(nameof(Iheptadecenoic), (int)NutrientToken.Iheptadecenoic, "Ácido i-heptadecenoico", "", false, Grams,
            Monounsaturated);

    public static readonly Nutrients Octadecenoic =
        new(nameof(Octadecenoic), (int)NutrientToken.Octadecenoic, "Ácido octadecenoico", "Ácido oleico", false, Grams,
            Monounsaturated);

    public static readonly Nutrients CisOctadecenoic =
        new(nameof(CisOctadecenoic), (int)NutrientToken.CisOctadecenoic, "Ácido cis-octadecenoico", "", false, Grams,
            Monounsaturated);

    public static readonly Nutrients Eicosenoic =
        new(nameof(Eicosenoic), (int)NutrientToken.Eicosenoic, "Ácido eicosenoico", "Ácido gadoleico", false, Grams,
            Monounsaturated);

    public static readonly Nutrients Docosenoic =
        new(nameof(Docosenoic), (int)NutrientToken.Docosenoic, "Ácido docosenoico", "", false, Grams, Monounsaturated);

    public static readonly Nutrients CisDocosenoic =
        new(nameof(CisDocosenoic), (int)NutrientToken.CisDocosenoic, "Ácido cis-docosenoico", "Ácido erúcico", false,
            Grams, Monounsaturated);

    public static readonly Nutrients CisTetracosenoic =
        new(nameof(CisTetracosenoic), (int)NutrientToken.CisTetracosenoic, "Ácido cis-tetracosenoico",
            "Ácido nervónico", false, Grams, Monounsaturated);

    public static readonly Nutrients Polyunsaturated =
        new(nameof(Polyunsaturated), (int)NutrientToken.Polyunsaturated, "Ácidos grasos poliinsaturados, total", "",
            true, Grams, FattyAcids);

    public static readonly Nutrients Octadecadienoic =
        new(nameof(Octadecadienoic), (int)NutrientToken.Octadecadienoic, "Ácido octadecadienoico", "Ácido linoleico",
            false, Grams, Polyunsaturated);

    public static readonly Nutrients CisOctadecadienoic =
        new(nameof(CisOctadecadienoic), (int)NutrientToken.CisOctadecadienoic, "Ácido cis-octadecadienoico", "", false,
            Grams, Polyunsaturated);

    public static readonly Nutrients Ioctadecadienoic =
        new(nameof(Ioctadecadienoic), (int)NutrientToken.Ioctadecadienoic, "Ácido i-octadecadienoico", "", false, Grams,
            Polyunsaturated);

    public static readonly Nutrients ConOctadecadienoic =
        new(nameof(ConOctadecadienoic), (int)NutrientToken.ConOctadecadienoic, "Ácido octadecadienoico conjugado", "",
            false, Grams, Polyunsaturated);

    public static readonly Nutrients Octadecatrienoic =
        new(nameof(Octadecatrienoic), (int)NutrientToken.Octadecatrienoic, "Ácido octadecatrienoico",
            "Ácido linolénico", false, Grams, Polyunsaturated);

    public static readonly Nutrients Cis3Octadecatrienoic =
        new(nameof(Cis3Octadecatrienoic), (int)NutrientToken.Cis3Octadecatrienoic, "Ácido cis3-octadecatrienoico",
            "Ácido α-linolénico", false, Grams, Polyunsaturated);

    public static readonly Nutrients Cis6Octadecatrienoic =
        new(nameof(Cis6Octadecatrienoic), (int)NutrientToken.Cis6Octadecatrienoic, "Ácido cis6-octadecatrienoico",
            "Ácido γ-linolénico", false, Grams, Polyunsaturated);

    public static readonly Nutrients Ioctadecatrienoic =
        new(nameof(Ioctadecatrienoic), (int)NutrientToken.Ioctadecatrienoic, "Ácido i-octadecatrienoico", "", false,
            Grams, Polyunsaturated);

    public static readonly Nutrients Octadecatetraenoic =
        new(nameof(Octadecatetraenoic), (int)NutrientToken.Octadecatetraenoic, "Ácido octadecatetraenoico", "", false,
            Grams, Polyunsaturated);

    public static readonly Nutrients Eicosadienoic =
        new(nameof(Eicosadienoic), (int)NutrientToken.Eicosadienoic, "Ácido eicosadienoico", "", false,
            Grams, Polyunsaturated);

    public static readonly Nutrients Eicosatrienoic =
        new(nameof(Eicosatrienoic), (int)NutrientToken.Eicosatrienoic, "Ácido eicosatrienoico", "", false, Grams,
            Polyunsaturated);

    public static readonly Nutrients N3Eicosatrienoic =
        new(nameof(N3Eicosatrienoic), (int)NutrientToken.N3Eicosatrienoic, "Ácido 3-eicosatrienoico", "", false, Grams,
            Polyunsaturated);

    public static readonly Nutrients N6Eicosatrienoic =
        new(nameof(N6Eicosatrienoic), (int)NutrientToken.N6Eicosatrienoic, "Ácido 6-eicosatrienoico", "", false, Grams,
            Polyunsaturated);

    public static readonly Nutrients Eicosatetraenoic =
        new(nameof(Eicosatetraenoic), (int)NutrientToken.Eicosatetraenoic, "Ácido eicosatetraenoico", "", false, Grams,
            Polyunsaturated);

    public static readonly Nutrients N6Eicosatetraenoic =
        new(nameof(N6Eicosatetraenoic), (int)NutrientToken.N6Eicosatetraenoic, "Ácido 6-eicosatetraenoico",
            "Ácido araquidónico", false, Grams, Polyunsaturated);

    public static readonly Nutrients Eicosapentaenoic =
        new(nameof(Eicosapentaenoic), (int)NutrientToken.Eicosapentaenoic, "Ácido eicosapentaenoico",
            "Ácido timnodónico", false, Grams, Polyunsaturated);

    public static readonly Nutrients N3Docosapentaenoic =
        new(nameof(N3Docosapentaenoic), (int)NutrientToken.N3Docosapentaenoic, "Ácido 3-docosapentaenoico", "", false,
            Grams, Polyunsaturated);

    public static readonly Nutrients N3Docosahexaenoic =
        new(nameof(N3Docosahexaenoic), (int)NutrientToken.N3Docosahexaenoic, "Ácido 3-docosahexaenoico", "", false,
            Grams, Polyunsaturated);

    public static readonly Nutrients Trans =
        new(nameof(Trans), (int)NutrientToken.Trans, "Ácidos grasos trans, total", "", true, Grams, null);

    public static readonly Nutrients TransMonoenoic =
        new(nameof(TransMonoenoic), (int)NutrientToken.TransMonoenoic, "Ácido trans-monoenoico", "", false,
            Grams, Trans);

    public static readonly Nutrients TransPolinoic =
        new(nameof(TransPolinoic), (int)NutrientToken.TransPolinoic, "Ácido trans-polinoico", "", false, Grams, Trans);

    public static readonly Nutrients Proteins =
        new(nameof(Proteins), (int)NutrientToken.Proteins, "Proteína, total", "", true, Grams, null);

    public static readonly Nutrients Tryptophan =
        new(nameof(Tryptophan), (int)NutrientToken.Tryptophan, "Triptófano", "", false, Grams, Proteins);

    public static readonly Nutrients Threonine =
        new(nameof(Threonine), (int)NutrientToken.Threonine, "Treonina", "", false, Grams, Proteins);

    public static readonly Nutrients Isoleucine =
        new(nameof(Isoleucine), (int)NutrientToken.Isoleucine, "Isoleucina", "", false, Grams, Proteins);

    public static readonly Nutrients Leucine =
        new(nameof(Leucine), (int)NutrientToken.Leucine, "Leucina", "", false, Grams, Proteins);

    public static readonly Nutrients Lysine =
        new(nameof(Lysine), (int)NutrientToken.Lysine, "Lisina", "", false, Grams, Proteins);

    public static readonly Nutrients Methionine =
        new(nameof(Methionine), (int)NutrientToken.Methionine, "Metionina", "", false, Grams, Proteins);

    public static readonly Nutrients Cystine =
        new(nameof(Cystine), (int)NutrientToken.Cystine, "Cistina", "", false, Grams, Proteins);

    public static readonly Nutrients Phenylalanine =
        new(nameof(Phenylalanine), (int)NutrientToken.Phenylalanine, "Fenilalanina", "", false, Grams, Proteins);

    public static readonly Nutrients Tyrosine =
        new(nameof(Tyrosine), (int)NutrientToken.Tyrosine, "Tirosina", "", false, Grams, Proteins);

    public static readonly Nutrients Valine =
        new(nameof(Valine), (int)NutrientToken.Valine, "Valina", "", false, Grams, Proteins);

    public static readonly Nutrients Arginine =
        new(nameof(Arginine), (int)NutrientToken.Arginine, "Arginina", "", false, Grams, Proteins);

    public static readonly Nutrients Histidine =
        new(nameof(Histidine), (int)NutrientToken.Histidine, "Histidina", "", false, Grams, Proteins);

    public static readonly Nutrients Alanine =
        new(nameof(Alanine), (int)NutrientToken.Alanine, "Alanina", "", false, Grams, Proteins);

    public static readonly Nutrients AsparticAcid =
        new(nameof(AsparticAcid), (int)NutrientToken.AsparticAcid, "Ácido aspártico", "", false, Grams, Proteins);

    public static readonly Nutrients GlutamicAcid =
        new(nameof(GlutamicAcid), (int)NutrientToken.GlutamicAcid, "Ácido glutámico", "", false, Grams, Proteins);

    public static readonly Nutrients Glycine =
        new(nameof(Glycine), (int)NutrientToken.Glycine, "Glicina", "", false, Grams, Proteins);

    public static readonly Nutrients Proline =
        new(nameof(Proline), (int)NutrientToken.Proline, "Prolina", "", false, Grams, Proteins);

    public static readonly Nutrients Serine =
        new(nameof(Serine), (int)NutrientToken.Serine, "Serina", "", false, Grams, Proteins);

    public static readonly Nutrients Vitamins =
        new(nameof(Vitamins), (int)NutrientToken.Vitamins, "Vitamina", "", false, Units.None, null);

    public static readonly Nutrients VitaminA =
        new(nameof(VitaminA), (int)NutrientToken.VitaminA, "Vitamina A", "", true, Micrograms, Vitamins);

    public static readonly Nutrients AlphaCarotene =
        new(nameof(AlphaCarotene), (int)NutrientToken.AlphaCarotene, "Alfa-caroteno", "α-caroteno", false,
            Micrograms, VitaminA);

    public static readonly Nutrients BetaCarotene =
        new(nameof(BetaCarotene), (int)NutrientToken.BetaCarotene, "Beta-caroteno", "β-caroteno", false,
            Units.None, VitaminA);

    public static readonly Nutrients BetaCryptoxanthin =
        new(nameof(BetaCryptoxanthin), (int)NutrientToken.BetaCryptoxanthin, "Beta-criptoxantina", "β-criptoxantina",
            false, Micrograms, VitaminA);

    public static readonly Nutrients LuteinZeaxanthin =
        new(nameof(LuteinZeaxanthin), (int)NutrientToken.LuteinZeaxanthin, "Luteína + Zeaxantina", "", false,
            Micrograms, VitaminA);

    public static readonly Nutrients Lycopene =
        new(nameof(Lycopene), (int)NutrientToken.Lycopene, "Licopeno", "", false, Micrograms, VitaminA);

    public static readonly Nutrients Retinol =
        new(nameof(Retinol), (int)NutrientToken.Retinol, "Retinol", "", false, Micrograms, VitaminA);

    public static readonly Nutrients VitaminB1 =
        new(nameof(VitaminB1), (int)NutrientToken.VitaminB1, "Vitamina B1", "Tiamina", false, Milligrams, Vitamins);

    public static readonly Nutrients VitaminB2 =
        new(nameof(VitaminB2), (int)NutrientToken.VitaminB2, "Vitamina B2", "Riboflavina", false, Milligrams, Vitamins);

    public static readonly Nutrients VitaminB3 =
        new(nameof(VitaminB3), (int)NutrientToken.VitaminB3, "Vitamina B3", "Niacina", false, Milligrams, Vitamins);

    public static readonly Nutrients VitaminB5 =
        new(nameof(VitaminB5), (int)NutrientToken.VitaminB5, "Vitamina B5", "Ácido pantoténico", false,
            Milligrams, Vitamins);

    public static readonly Nutrients VitaminB6 =
        new(nameof(VitaminB6), (int)NutrientToken.VitaminB6, "Vitamina B6", "", false, Milligrams, Vitamins);

    public static readonly Nutrients VitaminB9 =
        new(nameof(VitaminB9), (int)NutrientToken.VitaminB9, "Vitamina B9", "Folato", true, Micrograms, Vitamins);

    public static readonly Nutrients FolateFood =
        new(nameof(FolateFood), (int)NutrientToken.FolateFood, "Folato, alimento", "", false, Micrograms, VitaminB9);

    public static readonly Nutrients FolicAcid =
        new(nameof(FolicAcid), (int)NutrientToken.FolicAcid, "Ácido fólico", "", false, Micrograms, VitaminB9);

    public static readonly Nutrients VitaminB12 =
        new(nameof(VitaminB12), (int)NutrientToken.VitaminB12, "Vitamina B12", "Cobalamina", true,
            Micrograms, Vitamins);

    public static readonly Nutrients VitaminB12Added =
        new(nameof(VitaminB12Added), (int)NutrientToken.VitaminB12Added, "Vitamina B12, añadida", "", false,
            Micrograms, VitaminB12);

    public static readonly Nutrients VitaminC =
        new(nameof(VitaminC), (int)NutrientToken.VitaminC, "Vitamina C", "Ácido ascórbico", false,
            Milligrams, Vitamins);

    public static readonly Nutrients VitaminD =
        new(nameof(VitaminD), (int)NutrientToken.VitaminD, "Vitamina D", "", true, Micrograms, Vitamins);

    public static readonly Nutrients VitaminD2 =
        new(nameof(VitaminD2), (int)NutrientToken.VitaminD2, "Vitamina D2", "Ergocalciferol", false,
            Micrograms, VitaminD);

    public static readonly Nutrients VitaminD3 =
        new(nameof(VitaminD3), (int)NutrientToken.VitaminD3, "Vitamina D3", "Cholecalciferol", false,
            Micrograms, VitaminD);

    public static readonly Nutrients VitaminE =
        new(nameof(VitaminE), (int)NutrientToken.VitaminE, "Vitamina E", "α-tocoferol", false, Milligrams, Vitamins);

    public static readonly Nutrients VitaminEAdded =
        new(nameof(VitaminEAdded), (int)NutrientToken.VitaminEAdded, "Vitamina E, añadida", "", false,
            Milligrams, VitaminE);

    public static readonly Nutrients VitaminK1 =
        new(nameof(VitaminK1), (int)NutrientToken.VitaminK1, "Vitamina K1", "Filoquinona", false, Micrograms, Vitamins);

    public static readonly Nutrients VitaminK2 =
        new(nameof(VitaminK2), (int)NutrientToken.VitaminK2, "Vitamina K2", "Menaquinona-4", false,
            Micrograms, Vitamins);

    public static readonly Nutrients VitaminDk =
        new(nameof(VitaminDk), (int)NutrientToken.VitaminDk, "Vitamina dK", "Dihidrofiloquinona", true,
            Micrograms, Vitamins);

    public static readonly Nutrients Betaine =
        new(nameof(Betaine), (int)NutrientToken.Betaine, "Betaína", "", false, Milligrams, Vitamins);

    public static readonly Nutrients Choline =
        new(nameof(Choline), (int)NutrientToken.Choline, "Colina", "", false, Milligrams, Vitamins);

    public static readonly Nutrients Minerals =
        new(nameof(Minerals), (int)NutrientToken.Minerals, "Minerales", "", false, Units.None, null);

    public static readonly Nutrients Calcium =
        new(nameof(Calcium), (int)NutrientToken.Calcium, "Calcio", "Ca", false, Milligrams, Minerals);

    public static readonly Nutrients Iron =
        new(nameof(Iron), (int)NutrientToken.Iron, "Hierro", "Fe", false, Milligrams, Minerals);

    public static readonly Nutrients Magnesium =
        new(nameof(Magnesium), (int)NutrientToken.Magnesium, "Magnesio", "Mg", false, Milligrams, Minerals);

    public static readonly Nutrients Phosphorus =
        new(nameof(Phosphorus), (int)NutrientToken.Phosphorus, "Fósforo", "P", false, Milligrams, Minerals);

    public static readonly Nutrients Potassium =
        new(nameof(Potassium), (int)NutrientToken.Potassium, "Potasio", "K", false, Milligrams, Minerals);

    public static readonly Nutrients Sodium =
        new(nameof(Sodium), (int)NutrientToken.Sodium, "Sodio", "Na", false, Milligrams, Minerals);

    public static readonly Nutrients Zinc =
        new(nameof(Zinc), (int)NutrientToken.Zinc, "Zinc", "Zn", false, Milligrams, Minerals);

    public static readonly Nutrients Copper =
        new(nameof(Copper), (int)NutrientToken.Copper, "Cobre", "Cu", false, Milligrams, Minerals);

    public static readonly Nutrients Manganese =
        new(nameof(Manganese), (int)NutrientToken.Manganese, "Manganeso", "Mn", false, Milligrams, Minerals);

    public static readonly Nutrients Selenium =
        new(nameof(Selenium), (int)NutrientToken.Selenium, "Selenio", "Se", false, Micrograms, Minerals);

    public static readonly Nutrients Fluoride =
        new(nameof(Fluoride), (int)NutrientToken.Fluoride, "Fluoruro", "F", false, Micrograms, Minerals);

    public static readonly Nutrients Sterols =
        new(nameof(Sterols), (int)NutrientToken.Sterols, "Esteroles", "", true, Units.None, null);

    public static readonly Nutrients Cholesterol =
        new(nameof(Cholesterol), (int)NutrientToken.Cholesterol, "Colesterol", "", false, Milligrams, Sterols);

    public static readonly Nutrients Stigmasterol =
        new(nameof(Stigmasterol), (int)NutrientToken.Stigmasterol, "Estigmasterol", "", false, Milligrams, Sterols);

    public static readonly Nutrients Campesterol =
        new(nameof(Campesterol), (int)NutrientToken.Campesterol, "Campesterol", "Campestanol", false,
            Milligrams, Sterols);

    public static readonly Nutrients BetaSitosterol =
        new(nameof(BetaSitosterol), (int)NutrientToken.BetaSitosterol, "Beta-sitosterol", "β-sitosterol", false,
            Milligrams, Sterols);

    public static readonly Nutrients Alcohol =
        new(nameof(Alcohol), (int)NutrientToken.Alcohol, "Alcohol etílico", "", false, Grams, null);

    public static readonly Nutrients Other =
        new(nameof(Other), (int)NutrientToken.Other, "Otros", "", false, Units.None, null);

    public static readonly Nutrients Water =
        new(nameof(Water), (int)NutrientToken.Water, "Agua", "", false, Grams, Other);

    public static readonly Nutrients Ash =
        new(nameof(Ash), (int)NutrientToken.Ash, "Ceniza", "", false, Grams, Other);

    public static readonly Nutrients Caffeine =
        new(nameof(Caffeine), (int)NutrientToken.Caffeine, "Cafeína", "", false, Milligrams, Other);

    public static readonly Nutrients Theobromine =
        new(nameof(Theobromine), (int)NutrientToken.Theobromine, "Teobromina", "", false, Milligrams, Other);

    private Nutrients(string name, int value, string readableName, string alternativeName, bool isTotal,
        Units unit, Nutrients? category) : base(name, value)
    {
        ReadableName = readableName;
        AlternativeName = alternativeName;
        IsTotal = isTotal;
        Category = category;
        IsTopCategory = category == null;
        Unit = unit;
    }

    public string ReadableName { get; }
    public string AlternativeName { get; }
    public bool IsTopCategory { get; }
    public bool IsTotal { get; }
    public Nutrients? Category { get; }
    public Units Unit { get; }
}

public enum NutrientToken
{
    None,

    // Energy
    Energy,

    // Carbohydrates
    Carbohydrates,
    Fiber,
    Sugars,
    Sucrose,
    Glucose,
    Fructose,
    Lactose,
    Maltose,
    Galactose,
    Starch,

    // Fatty Acids
    FattyAcids,

    // Saturated Fatty Acids
    Saturated,
    Butanoic,
    Hexanoic,
    Octanoic,
    Decanoic,
    Dodecanoic,
    Tridecanoic,
    Tetradecanoic,
    Pentadecanoic,
    Hexadecanoic,
    Heptadecanoic,
    Octadecanoic,
    Eicosanoic,
    Docosanoic,
    Tetracosanoic,

    // Monounsaturated Fatty Acids
    Monounsaturated,
    Tetradecenoic,
    Pentadecenoic,
    Hexadecenoic,
    CisHexadecenoic,
    Heptadecenoic,
    Iheptadecenoic,
    Octadecenoic,
    CisOctadecenoic,
    Eicosenoic,
    Docosenoic,
    CisDocosenoic,
    CisTetracosenoic,

    // Polyunsaturated Fatty Acids
    Polyunsaturated,
    Octadecadienoic,
    CisOctadecadienoic,
    Ioctadecadienoic,
    ConOctadecadienoic,
    Octadecatrienoic,
    Cis3Octadecatrienoic,
    Cis6Octadecatrienoic,
    Ioctadecatrienoic,
    Octadecatetraenoic,
    Eicosadienoic,
    Eicosatrienoic,
    N3Eicosatrienoic,
    N6Eicosatrienoic,
    Eicosatetraenoic,
    N6Eicosatetraenoic,
    Eicosapentaenoic,
    N3Docosapentaenoic,
    N3Docosahexaenoic,

    // Trans Fatty Acids
    Trans,
    TransMonoenoic,
    TransPolinoic,

    // Proteins
    Proteins,
    Tryptophan,
    Threonine,
    Isoleucine,
    Leucine,
    Lysine,
    Methionine,
    Cystine,
    Phenylalanine,
    Tyrosine,
    Valine,
    Arginine,
    Histidine,
    Alanine,
    AsparticAcid,
    GlutamicAcid,
    Glycine,
    Proline,
    Serine,

    // Vitaminas
    Vitamins,

    // Vitamina A
    VitaminA,
    AlphaCarotene,
    BetaCarotene,
    BetaCryptoxanthin,
    LuteinZeaxanthin,
    Lycopene,
    Retinol,

    // Vitaminas B1-B6
    VitaminB1,
    VitaminB2,
    VitaminB3,
    VitaminB5,
    VitaminB6,

    // Vitamina B9
    VitaminB9,
    FolateFood,
    FolicAcid,

    // Vitamina B12
    VitaminB12,
    VitaminB12Added,

    // Vitamina C
    VitaminC,

    // Vitamina D
    VitaminD,
    VitaminD2,
    VitaminD3,

    // Vitamina E
    VitaminE,
    VitaminEAdded,

    // Vitamina K
    VitaminK1,
    VitaminK2,
    VitaminDk,

    // Otras vitaminas
    Betaine,
    Choline,

    // Minerals
    Minerals,
    Calcium,
    Iron,
    Magnesium,
    Phosphorus,
    Potassium,
    Sodium,
    Zinc,
    Copper,
    Manganese,
    Selenium,
    Fluoride,

    // Esteroles
    Sterols,
    Cholesterol,
    Stigmasterol,
    Campesterol,
    BetaSitosterol,

    // Alcohol
    Alcohol,

    // Otros
    Other,
    Water,
    Ash,
    Caffeine,
    Theobromine
}