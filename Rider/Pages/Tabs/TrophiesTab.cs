using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class TrophiesTab : ContentView
{
    private List<RecordPasser> recordList = Record.GetRecordsByUser(MainPage.User.UserId);

    public TrophiesTab()
    {
        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children = { BuildHeaderRow() }
        };

        foreach (RecordPasser record in recordList)
        {
            if (Trophy.GetTrophyByExercise(record.ExerciseId) == null)
            {
                continue;
            }
            TrophyPasser trophies = Trophy.GetTrophyByExercise(record.ExerciseId);
            string trophySource;
            UserPasser user = MainPage.User;
            int lift = Calculator.GeneralStrength(user.Age, user.Weight, user.Sex, record.OneRM);

            if (trophies.Platinum <= lift)
            {
                trophySource = "platinumtrophy.png";
            }
            else if (trophies.Gold <= lift)
            {
                trophySource = "goldtrophy.png";
            }
            else if (trophies.Silver <= lift)
            {
                trophySource = "silvertrophy.png";
            }
            else if (trophies.Bronze <= lift)
            {
                trophySource = "bronzetrophy.png";
            }
            else
            {
                trophySource = "notrophy.png";
            }
            
            layout.Add(BuildTrophyCard(record, trophySource));
        }

        Content = new ScrollView { Content = layout };
    }

    private static Grid MakeRowGrid() => new Grid
    {
        ColumnDefinitions =
        {
            new ColumnDefinition { Width = new GridLength(48) },
            new ColumnDefinition { Width = GridLength.Star },
            new ColumnDefinition { Width = new GridLength(70) },
            new ColumnDefinition { Width = new GridLength(70) }
        },
        ColumnSpacing = 12,
        Padding = new Thickness(16, 12)
    };

    private static View BuildHeaderRow()
    {
        Grid header = MakeRowGrid();

        header.Add(UIElements.MakeCell(string.Empty, 14, TextAlignment.Center), 0);
        header.Add(UIElements.MakeCell("Exercise", 14, TextAlignment.Start), 1);
        header.Add(UIElements.MakeCell("1RM", 14, TextAlignment.End), 2);
        header.Add(UIElements.MakeCell("Best", 14, TextAlignment.End), 3);

        return header;
    }

    private static View BuildTrophyCard(RecordPasser record, string trophySource)
    {
        Grid inner = MakeRowGrid();
        
        inner.Add(UIElements.MakeImage(trophySource, 48, 48));
        inner.Add(UIElements.MakeCell(Exercise.GetExerciseName(record.ExerciseId), 16, TextAlignment.Start), 1);
        inner.Add(UIElements.MakeCell($"{record.OneRM}kg", 18, TextAlignment.End), 2);
        inner.Add(UIElements.MakeCell($"{record.BestWeight}kg", 18, TextAlignment.End), 3);

        return UIElements.MakeCard(Colors.LightGray, inner);
    }
}
