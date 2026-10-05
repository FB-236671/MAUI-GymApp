namespace MAUI_GymApp;

public class Calculator
{
    public static int GeneralStrength(int age, double weight, string sex, int intialLift)
    {
        double[] values;
        double frostMcCoef = 0;

        if (string.Equals(sex, "male", StringComparison.OrdinalIgnoreCase))
        {
            values = new[]
            {
                47.46178854, 8.472061379, 0.07369410346, -0.001395833811, 0.00000707665973070743, -0.0000000120804336482315
            };
        }
        else if (string.Equals(sex, "female", StringComparison.OrdinalIgnoreCase))
        {
            values = new[]
            {
                -125.4255398, 13.71219419, -0.03307250631, -0.001050400051, 0.00000938773881462799, -0.000000023334613884954
            };
        }
        else
        {
            return 0;
        }

        double polynomial = values[0] + values[1] * weight + values[2] * Math.Pow(weight, 2) + values[3] * Math.Pow(weight, 3) + values[4] * Math.Pow(weight, 4) + values[5] * Math.Pow(weight, 5);

        if (polynomial == 0)
        {
            return 0;
        }

        double wilksCoef = 600 / polynomial;

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

    public static int OneRepMax(int weight, int reps)
    {
        double oneRM;

        if (reps <= 10)
        {
            oneRM = weight * 36.0 / (37.0 - reps);
        }
        else
        {
            oneRM = weight * (1 + reps / 30.0);
        }

        if (oneRM < weight)
        {
            return weight;
        }
        else
        {
            return Convert.ToInt32(Math.Round(oneRM, 0));
        }
    }
}