namespace MAUI_GymApp;

public class Controls
{
    public static Button MakeButton(string text, Color textColor, Color backgroundColor, Color borderColor, int borderWidth, int cornerRadius, int fontSize, Thickness padding) => new()
    {
        Text = text,
        TextColor = textColor,
        BackgroundColor = backgroundColor,
        BorderColor = borderColor,
        BorderWidth = borderWidth,
        CornerRadius = cornerRadius,
        FontSize = fontSize,
        Padding = padding,
        FontFamily = "OrbitronBold"
    };
    
    public static Button MakeButton(string text, Color textColor, Color backgroundColor, Color borderColor, int borderWidth, int cornerRadius, int fontSize, int widthRequest, int heightRequest, Thickness padding) => new()
    {
        Text = text,
        TextColor = textColor,
        BackgroundColor = backgroundColor,
        BorderColor = borderColor,
        BorderWidth = borderWidth,
        CornerRadius = cornerRadius,
        FontSize = fontSize,
        WidthRequest = widthRequest,
        HeightRequest = heightRequest,
        Padding = padding,
        FontFamily = "OrbitronBold"
    };

    public static Label MakeLabel(string text, int fontSize, Color textColor, LayoutOptions layout) => new()
    {
        Text = text,
        FontSize = fontSize,
        TextColor = textColor,
        VerticalOptions = layout,
        FontFamily = "OrbitronBold"
    };
    
    public static Label MakeLabel(string text, int fontSize) => new()
    {
        Text = text,
        FontSize = fontSize,
        TextColor = Colors.Black,
        VerticalOptions = LayoutOptions.Center,
        FontFamily = "OrbitronBold"
    };

    public static Entry MakeTextBox(string placeHolder, Color placeHolderColor, Color textColor, int fontSize, int width, LayoutOptions layout) => new()
    {
        Placeholder = placeHolder,
        FontFamily = "OrbitronBold",
        BackgroundColor = Colors.Transparent,
        PlaceholderColor = placeHolderColor,
        TextColor = textColor,
        FontSize = fontSize,
        WidthRequest = width,
        VerticalOptions = layout
    };

    public static Picker MakeSelector(string title, List<string> list, int fontSize) => new()
    {
        Title = title,
        ItemsSource = list,
        TextColor = Colors.Black,
        FontFamily = "OrbitronBold",
        TitleColor = Colors.Black,
        FontSize = fontSize
    };
}