using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class UIControls
{
    public static View BuildWorkoutCard(RoutineExercisesParser routineExercisesObject, List<List<SetParser>> setListList, EventHandler<CheckedChangedEventArgs> onCheckChanged)
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

        List<SetParser> setList = new();
        
        for (int i = 1; i <= routineExercisesObject.Sets; i++)
        {
            Entry reps = UIElements.MakeIntBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            Entry weight = UIElements.MakeIntBox("_", Colors.Gray, Colors.Black, 12, 50, LayoutOptions.End);
            CheckBox checkBox = new CheckBox();

            checkBox.BindingContext = routineExercisesObject;
            checkBox.CheckedChanged += onCheckChanged;
            
            workoutLayout.Children.Add(new HorizontalStackLayout()
            {
                Spacing = 8,
                Children =
                {
                    weight, reps, checkBox
                }
            });

            SetParser set = new SetParser();
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

    public static View BuildRoutineCard(string name, List<RoutineDataParser> routineExerciseList)
    {
        Entry setEntry = UIElements.MakeIntBox("Sets", Colors.White, Colors.Black, 12, 35, LayoutOptions.End);
        Picker restPicker = UIElements.MakeSelector("Rest", GeneralControls.GetRestList(), 12);
        Picker equipmentPicker = UIElements.MakeSelector("Equipment", Equipment.EquipmentNameList(), 12);

        VerticalStackLayout workoutLayout = new VerticalStackLayout()
        {
            Padding = new Thickness(20, 14),
            Spacing = 12,
            Children =
            {
                UIElements.MakeLabel(name, 22, Colors.Black, LayoutOptions.Center),
                new HorizontalStackLayout()
                {
                    Spacing = 8,
                    Children =
                    {
                        setEntry,
                        restPicker,
                        equipmentPicker
                    }
                }
            }
        };

        RoutineDataParser routineData = new RoutineDataParser();
        routineData.Name = name;
        routineData.SetEntry = setEntry;
        routineData.RestPicker = restPicker;
        routineData.EquipmentPicker = equipmentPicker;
        routineExerciseList.Add(routineData);

        return UIElements.MakeCard(Colors.LightGray, workoutLayout);
    }
}