namespace NutrientRetrieval.Model;

public class FoodPortion
{
    public int Id { get; set; }
    public float GramWeight { get; set; }
    public int SequenceNumber { get; set; }
    public float Amount { get; set; }
    public string Modifier { get; set; }
}