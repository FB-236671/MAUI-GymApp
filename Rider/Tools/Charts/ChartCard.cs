namespace MAUI_GymApp;

public class ChartCard : ContentView
{
    private readonly ChartDefinition _main;
    private readonly ChartDefinition? _alternate;
    private readonly Switch _switch = new Switch() { OnColor = Colors.White, ThumbColor = Colors.Black };
    private readonly Label _label;
    private readonly VerticalStackLayout _card;

    public ChartCard(ChartDefinition main, ChartDefinition? alternate = null)
    {
        _main = main;
        _alternate = alternate;
        _label = UIElements.MakeLabel(main.Title, 20);

        _card = new VerticalStackLayout()
        {
            Padding = new Thickness(16, 12),
            Spacing = 16,
            Children =
            {
                BuildHeader(),
                main.Build()
            }
        };

        _switch.Toggled += OnToggled;
        Content = UIElements.MakeCard(Colors.LightGray, _card);
    }
    
    private Grid BuildHeader()
    {
        _label.HorizontalOptions = LayoutOptions.Start;
        _switch.HorizontalOptions = LayoutOptions.End;
        _switch.VerticalOptions = LayoutOptions.Center;

        Grid header = new Grid()
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            }
        };

        header.Add(_label, 0);

        if (_alternate != null)
        {
            header.Add(_switch, 1);
        }

        return header;
    }
    
    private void OnToggled(object? sender, ToggledEventArgs e)
    {
        ChartDefinition chart = e.Value ? _alternate! : _main;

        _card.Children.RemoveAt(1);
        _card.Children.Add(chart.Build());
        _label.Text = chart.Title;
    }
}