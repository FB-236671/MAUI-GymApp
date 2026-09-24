using System.Diagnostics;

namespace MAUI_GymApp;

public class GeneralControls
{
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
        Application.Current!.MainPage = new NavigationPage(new MainPage())
        {
            BarBackgroundColor = Colors.White,
            BarTextColor = Colors.Black,
        };
    }
}