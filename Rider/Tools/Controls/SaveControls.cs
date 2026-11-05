using System.Diagnostics;
using System.Threading.Tasks;
using MAUI_GymApp.Rider.Database.Objects;

namespace MAUI_GymApp;

public class SaveControls
{
    public static void SaveWholeWorkout(List<RoutineExercisesParser> routineExerciseList, List<List<SetParser>> setListList, Stopwatch stopwatch)
    {
        WorkoutParser workoutObject = new WorkoutParser();
        List<WorkoutExercisesParser> workoutExercisesList = new List<WorkoutExercisesParser>();
        List<List<WorkoutSetsParser>> workoutSetsListList = new List<List<WorkoutSetsParser>>();

        foreach (RoutineExercisesParser exercise in routineExerciseList)
        {
            WorkoutExercisesParser workoutExercisesObject = new WorkoutExercisesParser();
            workoutExercisesObject.ExerciseId = exercise.ExerciseId;
            workoutExercisesObject.Sets = exercise.Sets;
            workoutExercisesList.Add(workoutExercisesObject);
        }

        int totalVolume = 0;
        List<(double bestWeight, int oneRM)> records = new();

        foreach (List<SetParser> setList in setListList)
        {
            List<WorkoutSetsParser> workoutSetsList = new List<WorkoutSetsParser>();
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
                    double weight = Convert.ToDouble(set.Weight.Text);
                    int reps = Convert.ToInt32(set.Reps.Text);

                    totalVolume += Calculator.GeneralStrength(Convert.ToInt32(Math.Round(weight * reps)));

                    WorkoutSetsParser workoutSetsObject = new WorkoutSetsParser();
                    workoutSetsObject.SetNumber = set.SetNumber + drawback;
                    workoutSetsObject.Weight = Math.Round(weight * set.EquipmentMultiplier, 1);
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
            RecordParser bestRecordObject = Record.GetRecordById(workoutObject.UserId, routineExerciseList[i].ExerciseId);

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
                RecordParser recordObject = new RecordParser();
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
            await Application.Current!.MainPage!.DisplayAlert("Input Error", string.Join("\n", errors), "Close");
            return false;
        }

        return true;
    }
}