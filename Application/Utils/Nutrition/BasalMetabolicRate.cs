namespace Application.Utils.Nutrition;

public static class BasalMetabolicRate
{
    public static double HarrisBenedict(int gender, double weight, int height, int age)
    {
        return gender switch
        {
            1 => Math.Round(66.5 + 13.75 * weight + 5 * height - 6.79 * age, 2),
            2 => Math.Round(655 + 9.56 * weight + 1.85 * height - 4.68 * age, 2),
            _ => throw new ArgumentException($"Value {gender} not recognized")
        };
    }
    
    public static double RozaShizgal(int gender, double weight, int height, int age)
    {
        return gender switch
        {
            1 => Math.Round(88.362 + 13.397 * weight + 4.799 * height - 5.677 * age, 2),
            2 => Math.Round(477.593 + 9.247 * weight + 3.098 * height - 4.330 * age, 2),
            _ => throw new ArgumentException($"Value {gender} not recognized")
        };
    }

    public static double MifflinStJeor(int gender, double weight, int height, int age)
    {
        return gender switch
        {
            1 => Math.Round(10 * weight + 6.25 * height - 5 * age + 5),
            2 => Math.Round(10 * weight + 6.25 * height - 5 * age - 161),
            _ => throw new ArgumentException($"Value {gender} not recognized")
        };
    }

}