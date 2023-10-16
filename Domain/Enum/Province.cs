using Ardalis.SmartEnum;

namespace Domain.Enum;

public class Province : SmartEnum<Province>, IEnum<Province, ProvinceToken>, IComposableEnum<Province, Region>
{
    public static readonly Province None =
        new(nameof(None), (int)ProvinceToken.None, "", "", null!);

    public static readonly Province Arica =
        new(nameof(Arica), (int)ProvinceToken.Arica, "Arica", "Arica", Region.AricaParinacota);

    public static readonly Province Parinacota =
        new(nameof(Parinacota), (int)ProvinceToken.Parinacota, "Parinacota", "Putre", Region.AricaParinacota);

    public static readonly Province Iquique =
        new(nameof(Iquique), (int)ProvinceToken.Iquique, "Iquique", "Iquique", Region.Tarapaca);

    public static readonly Province Tamarugal =
        new(nameof(Tamarugal), (int)ProvinceToken.Tamarugal, "Tamarugal", "Pozo Almonte", Region.Tarapaca);

    public static readonly Province Tocopilla =
        new(nameof(Tocopilla), (int)ProvinceToken.Tocopilla, "Tocopilla", "Tocopilla", Region.Tarapaca);

    public static readonly Province ElLoa =
        new(nameof(ElLoa), (int)ProvinceToken.ElLoa, "El Loa", "Calama", Region.Tarapaca);

    public static readonly Province Antofagasta =
        new(nameof(Antofagasta), (int)ProvinceToken.Antofagasta, "Antofagasta", "Antofagasta", Region.Antofagasta);

    public static readonly Province Chanaral =
        new(nameof(Chanaral), (int)ProvinceToken.Chanaral, "Chañaral", "Chañaral", Region.Atacama);

    public static readonly Province Copiapo =
        new(nameof(Copiapo), (int)ProvinceToken.Copiapo, "Copiapó", "Copiapó", Region.Atacama);

    public static readonly Province Huasco =
        new(nameof(Huasco), (int)ProvinceToken.Huasco, "Huasco", "Vallenar", Region.Atacama);

    public static readonly Province Elqui =
        new(nameof(Elqui), (int)ProvinceToken.Elqui, "Elqui", "La Serena", Region.Coquimbo);

    public static readonly Province Limari =
        new(nameof(Limari), (int)ProvinceToken.Limari, "Limarí", "Ovalle", Region.Coquimbo);

    public static readonly Province Choapa =
        new(nameof(Choapa), (int)ProvinceToken.Choapa, "Choapa", "Illapel", Region.Coquimbo);

    public static readonly Province Petorca =
        new(nameof(Petorca), (int)ProvinceToken.Petorca, "Petorca", "La Ligua", Region.Valparaiso);

    public static readonly Province LosAndes =
        new(nameof(LosAndes), (int)ProvinceToken.LosAndes, "Los Andes", "Los Andes", Region.Valparaiso);

    public static readonly Province SanFelipe =
        new(nameof(SanFelipe), (int)ProvinceToken.SanFelipe, "San Felipe de Aconcagua", "San Felipe",
            Region.Valparaiso);

    public static readonly Province Quillota =
        new(nameof(Quillota), (int)ProvinceToken.Quillota, "Quillota", "Quillota", Region.Valparaiso);

    public static readonly Province Valparaiso =
        new(nameof(Valparaiso), (int)ProvinceToken.Valparaiso, "Valparaíso", "Valparaíso", Region.Valparaiso);

    public static readonly Province SanAntonio =
        new(nameof(SanAntonio), (int)ProvinceToken.SanAntonio, "San Antonio", "San Antonio", Region.Valparaiso);

    public static readonly Province IslaDePascua =
        new(nameof(IslaDePascua), (int)ProvinceToken.IslaDePascua, "Isla de Pascua", "Hanga Roa", Region.Valparaiso);

    public static readonly Province MargaMarga =
        new(nameof(MargaMarga), (int)ProvinceToken.MargaMarga, "Marga Marga", "Quilpué", Region.Valparaiso);

    public static readonly Province Santiago =
        new(nameof(Santiago), (int)ProvinceToken.Santiago, "Santiago", "Santiago", Region.Santiago);

    public static readonly Province Cordillera =
        new(nameof(Cordillera), (int)ProvinceToken.Cordillera, "Cordillera", "Puente Alto", Region.Santiago);

    public static readonly Province Maipo =
        new(nameof(Maipo), (int)ProvinceToken.Maipo, "Maipo", "San Bernardo", Region.Santiago);

    public static readonly Province Melipilla =
        new(nameof(Melipilla), (int)ProvinceToken.Melipilla, "Melipilla", "Melipilla", Region.Santiago);

    public static readonly Province Talagante =
        new(nameof(Talagante), (int)ProvinceToken.Talagante, "Talagante", "Talagante", Region.Santiago);

    public static readonly Province Cachapoal =
        new(nameof(Cachapoal), (int)ProvinceToken.Cachapoal, "Cachapoal", "Rancagua", Region.OHiggins);

    public static readonly Province Colchagua =
        new(nameof(Colchagua), (int)ProvinceToken.Colchagua, "Colchagua", "San Fernando", Region.OHiggins);

    public static readonly Province CardenalCaro =
        new(nameof(CardenalCaro), (int)ProvinceToken.CardenalCaro, "Cardenal Caro", "Pichilemu", Region.OHiggins);

    public static readonly Province Curico =
        new(nameof(Curico), (int)ProvinceToken.Curico, "Curicó", "Curicó", Region.Maule);

    public static readonly Province
        Talca = new(nameof(Talca), (int)ProvinceToken.Talca, "Talca", "Talca", Region.Maule);

    public static readonly Province Linares =
        new(nameof(Linares), (int)ProvinceToken.Linares, "Linares", "Linares", Region.Maule);

    public static readonly Province Cauquenes =
        new(nameof(Cauquenes), (int)ProvinceToken.Cauquenes, "Cauquenes", "Cauquenes", Region.Maule);

    public static readonly Province Diguillin =
        new(nameof(Diguillin), (int)ProvinceToken.Diguillin, "Diguillín", "Chillán", Region.Nuble);

    public static readonly Province Itata =
        new(nameof(Itata), (int)ProvinceToken.Itata, "Itata", "Quirihue", Region.Nuble);

    public static readonly Province Punilla =
        new(nameof(Punilla), (int)ProvinceToken.Punilla, "Punilla", "San Carlos", Region.Nuble);

    public static readonly Province Biobio =
        new(nameof(Biobio), (int)ProvinceToken.Biobio, "Biobío", "Los Ángeles", Region.Biobio);

    public static readonly Province Concepcion =
        new(nameof(Concepcion), (int)ProvinceToken.Concepcion, "Concepción", "Concepción", Region.Biobio);

    public static readonly Province Arauco =
        new(nameof(Arauco), (int)ProvinceToken.Arauco, "Arauco", "Lebu", Region.Biobio);

    public static readonly Province Malleco =
        new(nameof(Malleco), (int)ProvinceToken.Malleco, "Malleco", "Angol", Region.Araucania);

    public static readonly Province Cautin =
        new(nameof(Cautin), (int)ProvinceToken.Cautin, "Cautín", "Temuco", Region.Araucania);

    public static readonly Province Valdivia =
        new(nameof(Valdivia), (int)ProvinceToken.Valdivia, "Valdivia", "Valdivia", Region.LosRios);

    public static readonly Province Ranco =
        new(nameof(Ranco), (int)ProvinceToken.Ranco, "Ranco", "La Unión", Region.LosRios);

    public static readonly Province Osorno =
        new(nameof(Osorno), (int)ProvinceToken.Osorno, "Osorno", "Osorno", Region.LosLagos);

    public static readonly Province Llanquihue =
        new(nameof(Llanquihue), (int)ProvinceToken.Llanquihue, "Llanquihue", "Puerto Montt", Region.LosLagos);

    public static readonly Province Chiloe =
        new(nameof(Chiloe), (int)ProvinceToken.Chiloe, "Chiloé", "Castro", Region.LosLagos);

    public static readonly Province Palena =
        new(nameof(Palena), (int)ProvinceToken.Palena, "Palena", "Chaitén", Region.LosLagos);

    public static readonly Province Coyhaique =
        new(nameof(Coyhaique), (int)ProvinceToken.Coyhaique, "Coyhaique", "Coyhaique", Region.Aysen);

    public static readonly Province Aysen =
        new(nameof(Aysen), (int)ProvinceToken.Aysen, "Aysén", "Puerto Aysén", Region.Aysen);

    public static readonly Province GeneralCarrera =
        new(nameof(GeneralCarrera), (int)ProvinceToken.GeneralCarrera, "General Carrera", "Chile Chico", Region.Aysen);

    public static readonly Province CapitanPrat =
        new(nameof(CapitanPrat), (int)ProvinceToken.CapitanPrat, "Capitán Prat", "Cochrane", Region.Aysen);

    public static readonly Province UltimaEsperanza =
        new(nameof(UltimaEsperanza), (int)ProvinceToken.UltimaEsperanza, "Última Esperanza", "Puerto Natales",
            Region.Magallanes);

    public static readonly Province Magallanes =
        new(nameof(Magallanes), (int)ProvinceToken.Magallanes, "Magallanes", "Punta Arenas",
            Region.Magallanes);

    public static readonly Province TierraDelFuego =
        new(nameof(TierraDelFuego), (int)ProvinceToken.TierraDelFuego, "Tierra del Fuego", "Porvenir",
            Region.Magallanes);

    public static readonly Province Antartica =
        new(nameof(Antartica), (int)ProvinceToken.Antartica, "Antártica Chilena",
            "Puerto Williams", Region.Magallanes);

    private Province(string name, int value, string readableName, string capital, Region region) : base(name, value)
    {
        ReadableName = readableName;
        Capital = capital;
        Region = region;
    }

    public string ReadableName { get; }
    public string Capital { get; }
    public Region Region { get; }
    Region IComposableEnum<Province, Region>.Category => Region;
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