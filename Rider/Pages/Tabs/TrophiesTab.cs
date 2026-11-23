using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp.Views;

public class TrophiesTab : ContentView
{
    private List<RecordRepository> recordList = Record.GetRecordsByUser(MainPage.User.UserId);
    private UserRepository user;
    private Entry _testEntry;
    private Label _testLabel;

    public TrophiesTab(UserRepository user)
    {
        user = user;

        _testEntry = UIElements.MakeTextBox("Inital Weight", Colors.Gray, Colors.Black, 14, 20, LayoutOptions.Center);
        _testLabel = UIElements.MakeLabel("_", 20);
        Button testButton = UIElements.MakeButton("Test Caculator", Colors.Black, Colors.White, Colors.Black, 2, 6, 12, 4);

        VerticalStackLayout layout = new VerticalStackLayout
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 8,
            Children = { BuildHeaderRow(), UIElements.MakeLine() }
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

        layout.Add(UIElements.MakeLine());
        layout.Add(new VerticalStackLayout
        {
            Children =
                {
                    _testEntry, testButton, _testLabel
                }
        });

        testButton.Clicked += OnTestButtonClick;

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

    private static View BuildTrophyCard(RecordRepository record, string trophySource)
    {
        Grid inner = MakeRowGrid();
        
        inner.Add(UIElements.MakeImage(trophySource, 40, 40));
        inner.Add(UIElements.MakeCell(Exercise.GetExerciseName(record.ExerciseId), 10, TextAlignment.Start), 1);
        inner.Add(UIElements.MakeCell($"{record.OneRM}kg", 12, TextAlignment.End), 2);
        inner.Add(UIElements.MakeCell($"{record.BestWeight}kg", 12, TextAlignment.End), 3);

        return UIElements.MakeCard(Colors.LightGray, inner);
    }

    private async void OnTestButtonClick(object? sender, EventArgs e)
    {
        _testLabel.Text = Calculator.GeneralStrength(user, Convert.ToInt32(_testEntry.Text)).ToString();
    }
}