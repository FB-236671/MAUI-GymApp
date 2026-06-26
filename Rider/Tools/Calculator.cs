namespace MAUI_GymApp;

public class Calculator
{
    public int GeneralStrength(int age, double weight, string sex, int intialLift)
    {
        double[] values = new double[6];
        double frostMcCoef = 0;

        if (sex == "male")
        {
            values = new[]
            {
                47.46178854, 8.472061379, 0.07369410346, -0.001395833811, 0.00000707665973070743, -0.0000000120804336482315
            };
        }
        else if (sex == "female")
        {
            values = new[]
            {
                -125.4255398, 13.71219419, -0.03307250631, -0.001050400051, 0.00000938773881462799, -0.000000023334613884954
            };
        }

        double wilksCoef = 600 / (values[1] + values[2] * weight + values[3] * Math.Pow(weight, 2) + values[4] * Math.Pow(weight, 3) + values[5] * Math.Pow(weight, 4) + values[6] * Math.Pow(weight, 5));

        if (age < 23)
        {
            frostMcCoef = -0.0003185703 * Math.Pow(age, 3) + 0.02101398 * Math.Pow(age, 2) - 0.4698796 * age + 4.5683030;
        }
        else if (age < 41)
        {
            frostMcCoef = 1.0;
        }
        else
        {
            frostMcCoef = 0.0000021160122 * Math.Pow(age, 3) + 0.000087877648 * Math.Pow(age, 2) - 0.0078450059  * age + 1.0376323;
        }

        double rawResult = intialLift * frostMcCoef * wilksCoef;
        int resultLift = Convert.ToInt32(Math.Round(rawResult, 0));
        
        return resultLift;
    }
}