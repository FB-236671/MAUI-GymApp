using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class TrophiesTab : ContentView
{
    private UserRepository _user;
    public TrophiesTab(UserRepository user)
    {
        _user = user;
        List<RecordRepository> recordList = Record.GetRecordsByUser(user.UserId);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children = { UIElements.MakeLabel("Exercise Records", 30), UIElements.MakeLine(), BuildHeaderRow() }
        };

        foreach (RecordRepository record in recordList)
        {
            if (Trophy.GetTrophyByExercise(record.ExerciseId) == null)
            {
                continue;
            }
            TrophyRepository trophies = Trophy.GetTrophyByExercise(record.ExerciseId);
            string trophySource;
            int lift = Calculator.GeneralStrength(user, record.OneRM);

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
            new ColumnDefinition { Width = new GridLength(60) },
            new ColumnDefinition { Width = new GridLength(60) }
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

    private View BuildTrophyCard(RecordRepository record, string trophySource)
    {
        Button infoButton = UIElements.MakeButton("⋮", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness(8));

        Grid inner = MakeRowGrid();
        
        inner.Add(UIElements.MakeImage(trophySource, 48, 48));
        inner.Add(UIElements.MakeCell(Exercise.GetExerciseName(record.ExerciseId), 12, TextAlignment.Start), 1);
        inner.Add(UIElements.MakeCell($"{record.OneRM}kg", 12, TextAlignment.End), 2);
        inner.Add(UIElements.MakeCell($"{record.BestWeight}kg", 12, TextAlignment.End), 3);
        inner.Add(infoButton, 4);

        infoButton.CommandParameter = record.ExerciseId;
        infoButton.Clicked += OnInfoClicked;

        return UIElements.MakeCard(Colors.LightGray, inner);
    }

    private async void OnInfoClicked(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ExerciseInfoPage(_user));
    }
}