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
}