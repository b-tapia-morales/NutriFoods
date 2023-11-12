using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Provinces : SmartEnum<Provinces>, IEnum<Provinces, ProvinceToken>, IComposableEnum<Provinces, Regions>
{
    public static readonly Provinces None =
        new(nameof(None), (int)ProvinceToken.None, "", "", null!);

    public static readonly Provinces Arica =
        new(nameof(Arica), (int)ProvinceToken.Arica, "Arica", "Arica", Regions.AricaParinacota);

    public static readonly Provinces Parinacota =
        new(nameof(Parinacota), (int)ProvinceToken.Parinacota, "Parinacota", "Putre", Regions.AricaParinacota);

    public static readonly Provinces Iquique =
        new(nameof(Iquique), (int)ProvinceToken.Iquique, "Iquique", "Iquique", Regions.Tarapaca);

    public static readonly Provinces Tamarugal =
        new(nameof(Tamarugal), (int)ProvinceToken.Tamarugal, "Tamarugal", "Pozo Almonte", Regions.Tarapaca);

    public static readonly Provinces Tocopilla =
        new(nameof(Tocopilla), (int)ProvinceToken.Tocopilla, "Tocopilla", "Tocopilla", Regions.Tarapaca);

    public static readonly Provinces ElLoa =
        new(nameof(ElLoa), (int)ProvinceToken.ElLoa, "El Loa", "Calama", Regions.Tarapaca);

    public static readonly Provinces Antofagasta =
        new(nameof(Antofagasta), (int)ProvinceToken.Antofagasta, "Antofagasta", "Antofagasta", Regions.Antofagasta);

    public static readonly Provinces Chanaral =
        new(nameof(Chanaral), (int)ProvinceToken.Chanaral, "Chañaral", "Chañaral", Regions.Atacama);

    public static readonly Provinces Copiapo =
        new(nameof(Copiapo), (int)ProvinceToken.Copiapo, "Copiapó", "Copiapó", Regions.Atacama);

    public static readonly Provinces Huasco =
        new(nameof(Huasco), (int)ProvinceToken.Huasco, "Huasco", "Vallenar", Regions.Atacama);

    public static readonly Provinces Elqui =
        new(nameof(Elqui), (int)ProvinceToken.Elqui, "Elqui", "La Serena", Regions.Coquimbo);

    public static readonly Provinces Limari =
        new(nameof(Limari), (int)ProvinceToken.Limari, "Limarí", "Ovalle", Regions.Coquimbo);

    public static readonly Provinces Choapa =
        new(nameof(Choapa), (int)ProvinceToken.Choapa, "Choapa", "Illapel", Regions.Coquimbo);

    public static readonly Provinces Petorca =
        new(nameof(Petorca), (int)ProvinceToken.Petorca, "Petorca", "La Ligua", Regions.Valparaiso);

    public static readonly Provinces LosAndes =
        new(nameof(LosAndes), (int)ProvinceToken.LosAndes, "Los Andes", "Los Andes", Regions.Valparaiso);

    public static readonly Provinces SanFelipe =
        new(nameof(SanFelipe), (int)ProvinceToken.SanFelipe, "San Felipe de Aconcagua", "San Felipe",
            Regions.Valparaiso);

    public static readonly Provinces Quillota =
        new(nameof(Quillota), (int)ProvinceToken.Quillota, "Quillota", "Quillota", Regions.Valparaiso);

    public static readonly Provinces Valparaiso =
        new(nameof(Valparaiso), (int)ProvinceToken.Valparaiso, "Valparaíso", "Valparaíso", Regions.Valparaiso);

    public static readonly Provinces SanAntonio =
        new(nameof(SanAntonio), (int)ProvinceToken.SanAntonio, "San Antonio", "San Antonio", Regions.Valparaiso);

    public static readonly Provinces IslaDePascua =
        new(nameof(IslaDePascua), (int)ProvinceToken.IslaDePascua, "Isla de Pascua", "Hanga Roa", Regions.Valparaiso);

    public static readonly Provinces MargaMarga =
        new(nameof(MargaMarga), (int)ProvinceToken.MargaMarga, "Marga Marga", "Quilpué", Regions.Valparaiso);

    public static readonly Provinces Santiago =
        new(nameof(Santiago), (int)ProvinceToken.Santiago, "Santiago", "Santiago", Regions.Santiago);

    public static readonly Provinces Cordillera =
        new(nameof(Cordillera), (int)ProvinceToken.Cordillera, "Cordillera", "Puente Alto", Regions.Santiago);

    public static readonly Provinces Maipo =
        new(nameof(Maipo), (int)ProvinceToken.Maipo, "Maipo", "San Bernardo", Regions.Santiago);

    public static readonly Provinces Melipilla =
        new(nameof(Melipilla), (int)ProvinceToken.Melipilla, "Melipilla", "Melipilla", Regions.Santiago);

    public static readonly Provinces Talagante =
        new(nameof(Talagante), (int)ProvinceToken.Talagante, "Talagante", "Talagante", Regions.Santiago);

    public static readonly Provinces Cachapoal =
        new(nameof(Cachapoal), (int)ProvinceToken.Cachapoal, "Cachapoal", "Rancagua", Regions.OHiggins);

    public static readonly Provinces Colchagua =
        new(nameof(Colchagua), (int)ProvinceToken.Colchagua, "Colchagua", "San Fernando", Regions.OHiggins);

    public static readonly Provinces CardenalCaro =
        new(nameof(CardenalCaro), (int)ProvinceToken.CardenalCaro, "Cardenal Caro", "Pichilemu", Regions.OHiggins);

    public static readonly Provinces Curico =
        new(nameof(Curico), (int)ProvinceToken.Curico, "Curicó", "Curicó", Regions.Maule);

    public static readonly Provinces
        Talca = new(nameof(Talca), (int)ProvinceToken.Talca, "Talca", "Talca", Regions.Maule);

    public static readonly Provinces Linares =
        new(nameof(Linares), (int)ProvinceToken.Linares, "Linares", "Linares", Regions.Maule);

    public static readonly Provinces Cauquenes =
        new(nameof(Cauquenes), (int)ProvinceToken.Cauquenes, "Cauquenes", "Cauquenes", Regions.Maule);

    public static readonly Provinces Diguillin =
        new(nameof(Diguillin), (int)ProvinceToken.Diguillin, "Diguillín", "Chillán", Regions.Nuble);

    public static readonly Provinces Itata =
        new(nameof(Itata), (int)ProvinceToken.Itata, "Itata", "Quirihue", Regions.Nuble);

    public static readonly Provinces Punilla =
        new(nameof(Punilla), (int)ProvinceToken.Punilla, "Punilla", "San Carlos", Regions.Nuble);

    public static readonly Provinces Biobio =
        new(nameof(Biobio), (int)ProvinceToken.Biobio, "Biobío", "Los Ángeles", Regions.Biobio);

    public static readonly Provinces Concepcion =
        new(nameof(Concepcion), (int)ProvinceToken.Concepcion, "Concepción", "Concepción", Regions.Biobio);

    public static readonly Provinces Arauco =
        new(nameof(Arauco), (int)ProvinceToken.Arauco, "Arauco", "Lebu", Regions.Biobio);

    public static readonly Provinces Malleco =
        new(nameof(Malleco), (int)ProvinceToken.Malleco, "Malleco", "Angol", Regions.Araucania);

    public static readonly Provinces Cautin =
        new(nameof(Cautin), (int)ProvinceToken.Cautin, "Cautín", "Temuco", Regions.Araucania);

    public static readonly Provinces Valdivia =
        new(nameof(Valdivia), (int)ProvinceToken.Valdivia, "Valdivia", "Valdivia", Regions.LosRios);

    public static readonly Provinces Ranco =
        new(nameof(Ranco), (int)ProvinceToken.Ranco, "Ranco", "La Unión", Regions.LosRios);

    public static readonly Provinces Osorno =
        new(nameof(Osorno), (int)ProvinceToken.Osorno, "Osorno", "Osorno", Regions.LosLagos);

    public static readonly Provinces Llanquihue =
        new(nameof(Llanquihue), (int)ProvinceToken.Llanquihue, "Llanquihue", "Puerto Montt", Regions.LosLagos);

    public static readonly Provinces Chiloe =
        new(nameof(Chiloe), (int)ProvinceToken.Chiloe, "Chiloé", "Castro", Regions.LosLagos);

    public static readonly Provinces Palena =
        new(nameof(Palena), (int)ProvinceToken.Palena, "Palena", "Chaitén", Regions.LosLagos);

    public static readonly Provinces Coyhaique =
        new(nameof(Coyhaique), (int)ProvinceToken.Coyhaique, "Coyhaique", "Coyhaique", Regions.Aysen);

    public static readonly Provinces Aysen =
        new(nameof(Aysen), (int)ProvinceToken.Aysen, "Aysén", "Puerto Aysén", Regions.Aysen);

    public static readonly Provinces GeneralCarrera =
        new(nameof(GeneralCarrera), (int)ProvinceToken.GeneralCarrera, "General Carrera", "Chile Chico", Regions.Aysen);

    public static readonly Provinces CapitanPrat =
        new(nameof(CapitanPrat), (int)ProvinceToken.CapitanPrat, "Capitán Prat", "Cochrane", Regions.Aysen);

    public static readonly Provinces UltimaEsperanza =
        new(nameof(UltimaEsperanza), (int)ProvinceToken.UltimaEsperanza, "Última Esperanza", "Puerto Natales",
            Regions.Magallanes);

    public static readonly Provinces Magallanes =
        new(nameof(Magallanes), (int)ProvinceToken.Magallanes, "Magallanes", "Punta Arenas",
            Regions.Magallanes);

    public static readonly Provinces TierraDelFuego =
        new(nameof(TierraDelFuego), (int)ProvinceToken.TierraDelFuego, "Tierra del Fuego", "Porvenir",
            Regions.Magallanes);

    public static readonly Provinces Antartica =
        new(nameof(Antartica), (int)ProvinceToken.Antartica, "Antártica Chilena",
            "Puerto Williams", Regions.Magallanes);

    private Provinces(string name, int value, string readableName, string capital, Regions region) : base(name, value)
    {
        ReadableName = readableName;
        Capital = capital;
        Region = region;
    }

    public string ReadableName { get; }
    public string Capital { get; }
    public Regions Region { get; }
    Regions IComposableEnum<Provinces, Regions>.Category => Region;
}

public enum ProvinceToken
{
    None,
    Arica,
    Parinacota,
    Iquique,
    Tamarugal,
    Tocopilla,
    ElLoa,
    Antofagasta,
    Chanaral,
    Copiapo,
    Huasco,
    Elqui,
    Limari,
    Choapa,
    Petorca,
    LosAndes,
    SanFelipe,
    Quillota,
    Valparaiso,
    SanAntonio,
    IslaDePascua,
    MargaMarga,
    Santiago,
    Cordillera,
    Maipo,
    Melipilla,
    Talagante,
    Cachapoal,
    Colchagua,
    CardenalCaro,
    Curico,
    Talca,
    Linares,
    Cauquenes,
    Diguillin,
    Itata,
    Punilla,
    Biobio,
    Concepcion,
    Arauco,
    Malleco,
    Cautin,
    Valdivia,
    Ranco,
    Osorno,
    Llanquihue,
    Chiloe,
    Palena,
    Coyhaique,
    Aysen,
    GeneralCarrera,
    CapitanPrat,
    UltimaEsperanza,
    Magallanes,
    TierraDelFuego,
    Antartica
}