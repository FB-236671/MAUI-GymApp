using System.Diagnostics;
using System.Threading.Tasks;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class SaveControls
{
    public static void SaveWholeWorkout(List<RoutineExercisesRepository> routineExerciseList, List<List<SetParser>> setListList, Stopwatch stopwatch)
    {

<<<<<<< TODO: Unmerged change from project 'MAUI-GymApp (net9.0-windows10.0.19041.0)', Before:
        WorkoutParser workoutObject = new WorkoutParser();
        List<WorkoutExercisesRepository> workoutExercisesList = new List<WorkoutExercisesRepository>();
=======
        WorkoutRepository workoutObject = new WorkoutParser();
        List<WorkoutExercisesParser> workoutExercisesList = new List<WorkoutExercisesParser>();
>>>>>>> After
        WorkoutRepository workoutObject = new WorkoutRepository();
        List<WorkoutExercisesRepository> workoutExercisesList = new List<WorkoutExercisesRepository>();
        List<List<WorkoutSetsRepository>> workoutSetsListList = new List<List<WorkoutSetsRepository>>();

        foreach (RoutineExercisesRepository exercise in routineExerciseList)
        {

<<<<<<< TODO: Unmerged change from project 'MAUI-GymApp (net9.0-windows10.0.19041.0)', Before:
            WorkoutExercisesParser workoutExercisesObject = new WorkoutExercisesParser();
            workoutExercisesObject.ExerciseId = exercise.ExerciseId;
=======
            WorkoutExercisesRepository workoutExercisesObject = new WorkoutExercisesParser();
            workoutExercisesObject.ExerciseId = exercise.ExerciseId;
>>>>>>> After
            WorkoutExercisesRepository workoutExercisesObject = new WorkoutExercisesRepository();
            workoutExercisesObject.ExerciseId = exercise.ExerciseId;
            workoutExercisesObject.Sets = exercise.Sets;
            workoutExercisesList.Add(workoutExercisesObject);
        }

        int totalVolume = 0;
        List<(double bestWeight, int oneRM)> records = new();

        foreach (List<SetParser> setList in setListList)
        {
            List<WorkoutSetsRepository> workoutSetsList = new List<WorkoutSetsRepository>();
            int drawback = 0;
            double heaviestWeight = 0;
            int best1RM = 0;

            foreach (SetParser set in setList)
            {
                if (!set.CheckBox.IsChecked)
                {
                    drawback += -1;
                }

                else
                {
                    double weight = Convert.ToDouble(set.Weight.Text) * set.EquipmentMultiplier;
                    int reps = Convert.ToInt32(set.Reps.Text);

                    totalVolume += Calculator.GeneralStrength(Convert.ToInt32(Math.Round(weight * reps)));


<<<<<<< TODO: Unmerged change from project 'MAUI-GymApp (net9.0-windows10.0.19041.0)', Before:
                    WorkoutSetsParser workoutSetsObject = new WorkoutSetsParser();
                    workoutSetsObject.SetNumber = set.SetNumber + drawback;
=======
                    WorkoutSetsRepository workoutSetsObject = new WorkoutSetsParser();
                    workoutSetsObject.SetNumber = set.SetNumber + drawback;
>>>>>>> After
                    WorkoutSetsRepository workoutSetsObject = new WorkoutSetsRepository();
                    workoutSetsObject.SetNumber = set.SetNumber + drawback;
                    workoutSetsObject.Weight = Math.Round(weight, 1);
                    workoutSetsObject.Reps = reps;
                    workoutSetsList.Add(workoutSetsObject);

                    if (heaviestWeight < workoutSetsObject.Weight)
                    {
                        heaviestWeight = workoutSetsObject.Weight;
                    }

                    int oneRM = Calculator.OneRepMax(workoutSetsObject.Weight, workoutSetsObject.Reps);

                    if (best1RM < oneRM)
                    {
                        best1RM = oneRM;
                    }
                }
            }
            workoutSetsListList.Add(workoutSetsList);
            records.Add((heaviestWeight, best1RM));
        }

        workoutObject.UserId = MainPage.User.UserId;
        workoutObject.Duration = (int)stopwatch.Elapsed.TotalSeconds;
        workoutObject.Volume = totalVolume;
        workoutObject.Date = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        for (int i = 0; i < workoutExercisesList.Count; i++)
        {
            workoutExercisesList[i].Sets = workoutSetsListList[i].Count;
        }

        Workout.SaveWorkout(workoutObject, workoutExercisesList, workoutSetsListList);

        for (int i = 0; i < routineExerciseList.Count; i++)
        {
            RecordRepository bestRecordObject = Record.GetRecordById(workoutObject.UserId, routineExerciseList[i].ExerciseId);

            double previousBestWeight;
            int previousOneRM;

            if (bestRecordObject == null)
            {
                previousBestWeight = 0;
                previousOneRM = 0;
            }
            else
            {
                previousBestWeight = bestRecordObject.BestWeight;
                previousOneRM = bestRecordObject.OneRM;
            }

            if (previousBestWeight < records[i].bestWeight || previousOneRM < records[i].oneRM)
            {

<<<<<<< TODO: Unmerged change from project 'MAUI-GymApp (net9.0-windows10.0.19041.0)', Before:
                RecordParser recordObject = new RecordParser();
                recordObject.UserId = workoutObject.UserId;
=======
                RecordRepository recordObject = new RecordParser();
                recordObject.UserId = workoutObject.UserId;
>>>>>>> After
                RecordRepository recordObject = new RecordRepository();
                recordObject.UserId = workoutObject.UserId;
                recordObject.ExerciseId = routineExerciseList[i].ExerciseId;
                recordObject.BestWeight = Math.Max(previousBestWeight, records[i].bestWeight);
                recordObject.OneRM = Math.Max(previousOneRM, records[i].oneRM);

                Record.SaveRecord(recordObject);
            }
        }
    }

    public static async Task<bool> ValidateSets(List<List<SetParser>> setListList)
    {
        var errors = new List<string>();

        foreach (List<SetParser> setList in setListList)
        {
            foreach (SetParser set in setList)
            {
                if (!set.CheckBox.IsChecked) { continue; }

                if (TypeChecker.DecimalIndexCheck(set.Weight.Text, "Weight", set.SetNumber.ToString(), 0, 1000, out decimal weight) is string weightError) { errors.Add(weightError); }
                if (TypeChecker.IntegerIndexCheck(set.Reps.Text, "Reps", set.SetNumber.ToString(), 1, 30, out int reps) is string repsError) { errors.Add(repsError); }
            }
        }

        if (errors.Count > 0)
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert("Input Error", string.Join("\n", errors), "Close");
            return false;
        }

        return true;
    }

    public static void LoadRoutine(Entry name, RoutineRepository routine, VerticalStackLayout layout, List<RoutineDataRepository> routineExerciseList)
    {
        name.Text = routine.Name;

        List<RoutineExercisesRepository> routineExercisesList = RoutineExercises.GetRoutineExercisesByRoutineId(routine.RoutineId);

        foreach (RoutineExercisesRepository exercise in routineExercisesList)
        {
            layout.Children.Add(UIControls.BuildRoutineCard(Exercise.GetExerciseById(exercise.ExerciseId).Name, routineExerciseList));

            RoutineDataRepository card = routineExerciseList[^1];
            card.SetEntry.Text = exercise.Sets.ToString();
            card.RestPicker.SelectedItem = exercise.Rest.ToString();
            card.EquipmentPicker.SelectedItem = Equipment.GetEquipmentById(exercise.EquipmentId).Name;
        }
    }
}