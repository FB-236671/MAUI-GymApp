using System.Diagnostics;

namespace MAUI_GymApp;

public class GeneralControls
{
    public static List<string> GetRestList()
    {
        List<string> restList = new List<string>();
        Stream stream = FileSystem.OpenAppPackageFileAsync("RestList.txt").GetAwaiter().GetResult();
        StreamReader reader = new StreamReader(stream);
        string line = reader.ReadLine();
        while (line != null)
        {
            restList.Add(line);
            line = reader.ReadLine();
        }
        return restList;
    }

    public static IDispatcherTimer StartTimer(Stopwatch stopwatch, Label timerLabel, IDispatcher dispatcher)
    {
        stopwatch.Start();
        IDispatcherTimer timer = dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (sender, args) => timerLabel.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        timer.Start();
        return timer;
    }

    public static void GoHome()
    {
        Application.Current!.Windows[0].Page = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };
    }

    public static bool ConfirmQuitWorkout(Page page, IDispatcherTimer timer, Stopwatch stopwatch)
    {
        page.Dispatcher.Dispatch(async () =>
        {
            bool confirmed = await page.DisplayAlert("Quit Workout", "Are you sure you want to quit? All your progress will be lost.", "Confirm", "Cancel");
            if (!confirmed) return;

            timer.Stop();
            stopwatch.Stop();
            await page.Navigation.PopAsync();
        });
        return true;
    }

    public static void GoToLogin()
    {
        MainPage.User = null!;
        Application.Current!.Windows[0].Page = new NavigationPage(new LoginPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };
    }
}