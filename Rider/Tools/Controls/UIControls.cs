using MAUI_GymApp.Rider.Database;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class UIControls
{
        public static View BuildWorkoutCard(RoutineExercisesPasser routineExercisesObject, List<List<SetPasser>> setListList)
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
                        UIElements.MakeLabel(Exercise.GetExerciseById(routineExercisesObject.ExerciseId).Name, 22, Colors.Black, LayoutOptions.Center),
                        UIElements.MakeButton(Equipment.GetEquipmentById(routineExercisesObject.EquipmentId).Name[0].ToString(), Colors.White, Colors.Black, Colors.Black, 5, 2, 22, new Thickness())
                    }
                },
                UIElements.MakeLine(2, Colors.Black),
            }
        };

        List<SetPasser> setList = new();
        
        for (int i = 1; i <= routineExercisesObject.Sets; i++)
        {
            Entry reps = UIElements.MakeTextBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            Entry weight = UIElements.MakeTextBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            CheckBox checkBox = new CheckBox();
            
            workoutLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {
                    weight, reps, checkBox
                }
            });

            SetPasser set = new SetPasser();
            set.SetNumber = i;
            set.Weight = weight;
            set.Reps = reps;
            set.CheckBox = checkBox;
            set.EquipmentMultiplier = Equipment.GetEquipmentById(routineExercisesObject.EquipmentId).Multiplier;
            setList.Add(set);
        }

        setListList.Add(setList);
        
        return UIElements.MakeCard(Colors.LightGray, workoutLayout);
    }
}