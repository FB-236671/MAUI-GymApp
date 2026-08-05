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
            TrophyPasser trophies = Trophy.GetTrophyByExercise(record.ExerciseId);
            string trophySource;
            UserPasser user = MainPage.User;
            int lift = Calculator.GeneralStrength(user.Age, user.Weight, user.Sex, record.ExerciseId);

            if (trophies.Platinum <= record.OneRM)
            {
                trophySource = "PlatinumTrophy.png";
            }
            else if (trophies.Gold <= record.OneRM)
            {
                trophySource = "GoldTrophy.png";
            }
            else if (trophies.Silver <= record.OneRM)
            {
                trophySource = "SilverTrophy.png";
            }
            else if (trophies.Bronze <= record.OneRM)
            {
                trophySource = "BronzeTrophy.png";
            }
            else
            {
                trophySource = "NoTrophy.png";
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

        header.Add(Controls.MakeCell(string.Empty, 14, TextAlignment.Center), 0);
        header.Add(Controls.MakeCell("Exercise", 14, TextAlignment.Start), 1);
        header.Add(Controls.MakeCell("1RM", 14, TextAlignment.End), 2);
        header.Add(Controls.MakeCell("Best", 14, TextAlignment.End), 3);

        return header;
    }

    private static View BuildTrophyCard(RecordPasser record, string trophySource)
    {
        Grid inner = MakeRowGrid();
        
        inner.Add(Controls.MakeImage(trophySource, 48, 48));
        inner.Add(Controls.MakeCell(Exercise.GetExerciseName(record.ExerciseId), 16, TextAlignment.Start), 1);
        inner.Add(Controls.MakeCell($"{record.OneRM}kg", 18, TextAlignment.End), 2);
        inner.Add(Controls.MakeCell($"{record.BestWeight}kg", 18, TextAlignment.End), 3);

        return new Border
        {
            BackgroundColor = Colors.LightGray,
            Content = inner
        };
    }
}
