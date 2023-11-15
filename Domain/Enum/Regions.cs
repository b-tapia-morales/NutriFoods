namespace Domain.Enum;

using Ardalis.SmartEnum;

public class Regions : SmartEnum<Regions>, IEnum<Regions, RegionToken>
{
    public static readonly Regions None = new(nameof(None), (int)RegionToken.None, string.Empty, string.Empty);

    public static readonly Regions AricaParinacota =
        new(nameof(AricaParinacota), (int)RegionToken.AricaParinacota, "Arica y Parinacota", "AP");

    public static readonly Regions Tarapaca =
        new(nameof(Tarapaca), (int)RegionToken.Tarapaca, "Tarapacá", "TA");

    public static readonly Regions Antofagasta =
        new(nameof(Antofagasta), (int)RegionToken.Antofagasta, "Antofagasta", "AN");

    public static readonly Regions Atacama =
        new(nameof(Atacama), (int)RegionToken.Atacama, "Atacama", "AT");

    public static readonly Regions Coquimbo =
        new(nameof(Coquimbo), (int)RegionToken.Coquimbo, "Coquimbo", "CO");

    public static readonly Regions Valparaiso =
        new(nameof(Valparaiso), (int)RegionToken.Valparaiso, "Valparaíso", "VA");

    public static readonly Regions Santiago =
        new(nameof(Santiago), (int)RegionToken.Santiago, "Metropolitana de Santiago", "RM");

    public static readonly Regions OHiggins =
        new(nameof(OHiggins), (int)RegionToken.OHiggins, "Libertador General Bernardo O'Higgins", "LI");

    public static readonly Regions Maule =
        new(nameof(Maule), (int)RegionToken.Maule, "Maule", "ML");

    public static readonly Regions Nuble =
        new(nameof(Nuble), (int)RegionToken.Nuble, "Ñuble", "NB");

    public static readonly Regions Biobio =
        new(nameof(Biobio), (int)RegionToken.Biobio, "Biobío", "BI");

    public static readonly Regions Araucania =
        new(nameof(Araucania), (int)RegionToken.Araucania, "La Araucanía", "AR");

    public static readonly Regions LosRios =
        new(nameof(LosRios), (int)RegionToken.LosRios, "Los Ríos", "LR");

    public static readonly Regions LosLagos =
        new(nameof(LosLagos), (int)RegionToken.LosLagos, "Los Lagos", "LL");

    public static readonly Regions Aysen =
        new(nameof(Aysen), (int)RegionToken.Aysen, "Aysén del General Carlos Ibáñez del Campo", "AI");

    public static readonly Regions Magallanes =
        new(nameof(Magallanes), (int)RegionToken.Magallanes, "Magallanes y de la Antártica Chilena", "MA");

    private Regions(string name, int value, string readableName, string abbreviation) : base(name, value)
    {
        ReadableName = readableName;
        Abbreviation = abbreviation;
    }

    public string Abbreviation { get; }
    public string ReadableName { get; }
}

public enum RegionToken
{
    None,
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