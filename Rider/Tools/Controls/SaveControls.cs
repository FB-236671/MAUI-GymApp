using System.Diagnostics;
using System.Threading.Tasks;
using MAUI_GymApp.Rider.Database;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class SaveControls
{
    public static void SaveWholeWorkout(List<RoutineExercisesPasser> routineExerciseList, List<List<SetPasser>> setListList, Stopwatch stopwatch)
    {
        WorkoutPasser workoutObject = new WorkoutPasser();
        List<WorkoutExercisesPasser> workoutExercisesList = new List<WorkoutExercisesPasser>();
        List<List<WorkoutSetsPasser>> workoutSetsListList = new List<List<WorkoutSetsPasser>>();

        foreach (RoutineExercisesPasser exercise in routineExerciseList)
        {
            WorkoutExercisesPasser workoutExercisesObject = new WorkoutExercisesPasser();
            workoutExercisesObject.ExerciseId = exercise.ExerciseId;
            workoutExercisesObject.Sets = exercise.Sets;
            workoutExercisesList.Add(workoutExercisesObject);
        }

        int totalVolume = 0;
        List<(int bestWeight, int oneRM)> records = new();

        foreach (List<SetPasser> setList in setListList)
        {
            List<WorkoutSetsPasser> workoutSetsList = new List<WorkoutSetsPasser>();
            int drawback = 0;
            int heaviestWeight = 0;
            int best1RM = 0;

            foreach (SetPasser set in setList)
            {
                if (!set.CheckBox.IsChecked)
                {
                    drawback += -1;
                }

                else
                {
                    totalVolume += Calculator.GeneralStrength(Convert.ToInt32(Math.Round(Convert.ToDouble(set.Weight.Text)) * Convert.ToInt32(set.Reps.Text)));

                    WorkoutSetsPasser workoutSetsObject = new WorkoutSetsPasser();
                    workoutSetsObject.SetNumber = set.SetNumber + drawback;
                    workoutSetsObject.Weight = Convert.ToInt32(Math.Round(Convert.ToDouble(set.Weight.Text) * set.EquipmentMultiplier, 0));
                    workoutSetsObject.Reps = Convert.ToInt32(set.Reps.Text);
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
        workoutObject.Date = DateTimeOffset.UtcNow.ToUnixTimeSeconds;

        Workout.SaveWorkout(workoutObject, workoutExercisesList, workoutSetsListList);

        for (int i = 0; i < routineExerciseList.Count; i++)
        {
            RecordPasser bestRecordObject = Record.GetRecordById(workoutObject.UserId, routineExerciseList[i].ExerciseId);

            int previousBestWeight;
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
                RecordPasser recordObject = new RecordPasser();
                recordObject.UserId = workoutObject.UserId;
                recordObject.ExerciseId = routineExerciseList[i].ExerciseId;
                recordObject.BestWeight = Math.Max(previousBestWeight, records[i].bestWeight);
                recordObject.OneRM = Math.Max(previousOneRM, records[i].oneRM);

                Record.SaveRecord(recordObject);
            }
        }
    }

    public static async Task ValidateSets(List<List<SetPasser>> setListList)
    {
        foreach (List<SetPasser> setList in setListList)
        {
            var errors = new List<string>();

            foreach (SetPasser set in setList)
            {
                if (TypeChecker.IntegerIndexCheck(set.Weight.Text, "Weight", set.SetNumber.ToString(), 0, 1000, out int weight) is string weightError) { errors.Add(weightError); }
                if (TypeChecker.IntegerIndexCheck(set.Reps.Text, "Reps", set.SetNumber.ToString(), 1, 30, out int reps) is string repsError) { errors.Add(repsError); }
            }

            if (errors.Count > 0)
            {
                await DisplayAlert("Input Error", string.Join("\n", errors), "Close");
                return;
            }
        }
    }
}