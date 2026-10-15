using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class EditWorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private List<RoutineDataPasser> _routineExerciseList = new();
    private Entry nameBox;

    public EditWorkoutPage(RoutinePasser routine)
    {
        Background = Colors.White;
        Picker picker = UIElements.MakeSelector("Add an Exercise", Exercise.ExerciseNameList(), 18);
        Button saveButton = UIElements.MakeButton("Save", Colors.Black, Colors.White, Colors.Black, 2, 6, 20, new Thickness());
        nameBox = UIElements.MakeTextBox("Name", Colors.Gray, Colors.Black, 30, 250, LayoutOptions.Center);

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 8,
                    Children =
                    {
                        new VerticalStackLayout()
                        {
                            Spacing = 0,
                            Children =
                            {
                                nameBox,
                                UIElements.MakeLine(2, Colors.Black)
                            }
                        }
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
                saveButton,
                picker
            }
        };
    }
}