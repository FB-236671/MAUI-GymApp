using Microcharts;
using Microcharts.Maui;
using SkiaSharp;

namespace MAUI_GymApp;

public class UIElements
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
    
    public static Entry MakeIntBox(string placeHolder, Color placeHolderColor, Color textColor, int fontSize, int width, LayoutOptions layout) => new()
    {
        Placeholder = placeHolder,
        FontFamily = "OrbitronBold",
        BackgroundColor = Colors.Transparent,
        PlaceholderColor = placeHolderColor,
        TextColor = textColor,
        FontSize = fontSize,
        WidthRequest = width,
        Keyboard = Keyboard.Numeric,
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
    
    public static Label MakeCell(string text, int fontSize, TextAlignment alignment)
    {
        Label label = MakeLabel(text, fontSize);
        label.HorizontalTextAlignment = alignment;
        label.LineBreakMode = LineBreakMode.TailTruncation;
        return label;
    }

    public static Image MakeImage(string source, int width, int height) => new()
    {
        Source = source,
        WidthRequest = width,
        HeightRequest = height,
    };

    public static ChartView MakeRadarChart(int height, ChartEntry[] entries, int textSize) => new()
    {
        HeightRequest = height,
        Chart = new RadarChart
        {
            Entries = entries,
            LabelTextSize = textSize,
            IsAnimated = false,
            BackgroundColor = SKColors.Transparent
        }
    };

    public static Border MakeCard(Color backgroundColor, View inner) => new()
    {
        BackgroundColor = backgroundColor,
        Content = inner
    };

    public static BoxView MakeLine(int height, Color color) => new()
    {
        HeightRequest = height,
        Color = color
    };
}