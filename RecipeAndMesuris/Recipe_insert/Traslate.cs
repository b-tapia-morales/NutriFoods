namespace RecipeAndMesuris.Recipe_insert;

public class Traslate
{
    private String _nameNormal;
    private String _traslateName;

    public Traslate(String nameNormal,String traslateName)
    {
        _nameNormal = nameNormal;
        _traslateName = traslateName;
    }

    public String GetNameNormal()
    {
        return _nameNormal;
    }

    public String GetTraslateName()
    {
        return _traslateName;
    }
}