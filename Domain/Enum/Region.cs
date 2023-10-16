namespace Domain.Enum;

using Ardalis.SmartEnum;

public class Region : SmartEnum<Region>, IEnum<Region, RegionToken>
{
    public static readonly Region None = new(nameof(None), 0, string.Empty, string.Empty);

    public static readonly Region AricaParinacota =
        new(nameof(AricaParinacota), (int)RegionToken.AricaParinacota, "Arica y Parinacota", "AP");

    public static readonly Region Tarapaca =
        new(nameof(Tarapaca), (int)RegionToken.Tarapaca, "Tarapacá", "TA");

    public static readonly Region Antofagasta =
        new(nameof(Antofagasta), (int)RegionToken.Antofagasta, "Antofagasta", "AN");

    public static readonly Region Atacama =
        new(nameof(Atacama), (int)RegionToken.Atacama, "Atacama", "AT");

    public static readonly Region Coquimbo =
        new(nameof(Coquimbo), (int)RegionToken.Coquimbo, "Coquimbo", "CO");

    public static readonly Region Valparaiso =
        new(nameof(Valparaiso), (int)RegionToken.Valparaiso, "Valparaíso", "VA");

    public static readonly Region Santiago =
        new(nameof(Santiago), (int)RegionToken.Santiago, "Metropolitana de Santiago", "RM");

    public static readonly Region OHiggins =
        new(nameof(OHiggins), (int)RegionToken.OHiggins, "Libertador General Bernardo O'Higgins", "LI");

    public static readonly Region Maule =
        new(nameof(Maule), (int)RegionToken.Maule, "Maule", "ML");

    public static readonly Region Nuble =
        new(nameof(Nuble), (int)RegionToken.Nuble, "Ñuble", "NB");

    public static readonly Region Biobio =
        new(nameof(Biobio), (int)RegionToken.Biobio, "Biobío", "BI");

    public static readonly Region Araucania =
        new(nameof(Araucania), (int)RegionToken.Araucania, "La Araucanía", "AR");

    public static readonly Region LosRios =
        new(nameof(LosRios), (int)RegionToken.LosRios, "Los Ríos", "LR");

    public static readonly Region LosLagos =
        new(nameof(LosLagos), (int)RegionToken.LosLagos, "Los Lagos", "LL");

    public static readonly Region Aysen =
        new(nameof(Aysen), (int)RegionToken.Aysen, "Aysén del General Carlos Ibáñez del Campo", "AI");

    public static readonly Region Magallanes =
        new(nameof(Magallanes), (int)RegionToken.Magallanes, "Magallanes y de la Antártica Chilena", "MA");

    private Region(string name, int value, string readableName, string abbreviation) : base(name, value)
    {
        ReadableName = readableName;
        Abbreviation = abbreviation;
    }

    public string Abbreviation { get; }
    public string ReadableName { get; }
    public static IReadOnlyCollection<Region> Values() => List;
}

public enum RegionToken
{
    AricaParinacota,
    Tarapaca,
    Antofagasta,
    Atacama,
    Coquimbo,
    Valparaiso,
    Santiago,
    OHiggins,
    Maule,
    Nuble,
    Biobio,
    Araucania,
    LosRios,
    LosLagos,
    Aysen,
    Magallanes
}