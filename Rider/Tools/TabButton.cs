namespace MAUI_GymApp;

public class TabButton : VerticalStackLayout
{
    private static readonly Color SelectedColor = Colors.Black;
    private static readonly Color UnselectedColor = Color.FromArgb("#9E9E9E");

    private readonly Image _icon;
    private readonly Label _text;

    public TabButton(string source, string text, Action onTapped)
    {
        Spacing = 2;
        Padding = new Thickness(0, 8);
        HorizontalOptions = LayoutOptions.Fill;
        VerticalOptions = LayoutOptions.Fill;
        BackgroundColor = Colors.White;

        _icon = UIElements.MakeImage(source, 34, 34);
        _text = UIElements.MakeLabel(text, 14, Colors.Black, LayoutOptions.Center);
        
        _icon.HorizontalOptions = LayoutOptions.Center;
        _text.HorizontalOptions = LayoutOptions.Center;
        _text.HorizontalTextAlignment = TextAlignment.Center;

        Children.Add(_icon);
        Children.Add(_text);

        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += (_, _) => onTapped();
        GestureRecognizers.Add(tap);
    }

    public void SetSelected(bool selected)
    {
        Color color = selected ? SelectedColor : UnselectedColor;
        FontAttributes attributes = selected ? FontAttributes.Bold : FontAttributes.None;
        
        _text.TextColor = color;
        _text.FontAttributes = attributes;
    }
}