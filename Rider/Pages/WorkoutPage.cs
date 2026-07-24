using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class WorkoutPage : ContentPage
{
    private readonly VerticalStackLayout _layout;
    private RoutinePasser _routineObject;
    
    public WorkoutPage(RoutinePasser routineObject)
    {
        Background = Colors.White;
        _routineObject = routineObject;
        List<RoutineExercisesPasser> exercises = RoutineExercises.GetRoutineExercisesByRoutineId(routineObject.RoutineId);

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
                                Controls.MakeLabel(_routineObject.Name, 30),
                            }
                        }
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black }
            }
        };

        foreach (RoutineExercisesPasser exercise in exercises)
        {
            _layout.Children.Add(BuildWorkoutCard(exercise));
        }

        Content = new ScrollView { Content = _layout };
    }

    private View BuildWorkoutCard(RoutineExercisesPasser routineExercisesObject)
    {
        VerticalStackLayout workoutLayout = new VerticalStackLayout()
        {
            Padding = new Thickness(20, 14),
            Spacing = 12,
            Children =
            {
                new HorizontalStackLayout()
                {
                    Spacing = 20,
                    Children =
                    {
                        Controls.MakeLabel(Exercise.GetExerciseById(routineExercisesObject.ExerciseId).Name, 22, Colors.Black, LayoutOptions.Center),
                        Controls.MakeButton(GetEquipmentName(routineExercisesObject.EquipmentId).ToString(), Colors.White, Colors.Black, Colors.Black, 5, 2, 22, new Thickness())
                    }
                },
                new BoxView { HeightRequest = 2, Color = Colors.Black },
            }
        };

        for (int i = 0; i < routineExercisesObject.Sets; i++)
        {
            workoutLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {
                    Controls.MakeTextBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End),
                    Controls.MakeTextBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End),
                    new CheckBox()
                }
            });
        }

        return new Border()
        {
            BackgroundColor = Colors.LightGray,
            Content = workoutLayout
        };
    }

    private char GetEquipmentName(int equipmentId)
    {
        EquipmentPasser equipmentObject = Equipment.GetEquipmentById(equipmentId);
        string name = equipmentObject.Name;
        return name[0];
    }
}