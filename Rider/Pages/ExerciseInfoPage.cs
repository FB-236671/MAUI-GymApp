using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class ExerciseInfoPage : ContentPage
{
    VerticalStackLayout _layout;
    public ExerciseInfoPage(UserRepository user, ExerciseRepository exercise)
    {
        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children = { UIElements.MakeLabel(exercise.Name + " Statistics", 30), UIElements.MakeLine() }
        };
    }
}