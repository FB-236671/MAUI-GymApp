using MAUI_GymApp.Rider.Database;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_GymApp.Rider.Pages.Tabs.Stats;
public class WorkoutChart : ContentView
{
    private Label _label = UIElements.MakeLabel("Category Chart", 20);
    private VerticalStackLayout _layout;
    private VerticalStackLayout _card;

    public WorkoutChart()
    {
        _card = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 12),
            Spacing = 16,
            Children =
            {

            }
        };

        _layout = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 16, 16, 0),
            Spacing = 16,
            Children =
            {
                UIElements.MakeCard(Colors.LightGray, _card)
            }
        };
    }

    private ChartEntry[] BuildEntries(int numOfWeeks, List<WorkoutPasser> workoutList)
    {
        ChartEntry[] entries = new ChartEntry[numOfWeeks];

        for (int i = 0; i < numOfWeeks; i++)
        {

        }


        foreach (WorkoutPasser workout in workoutList)
        {

        }


        return entries;
    }
}