using Microsoft.Maui.Controls.Shapes;

namespace Naidis_Osa1;

public partial class Valgusfoor : ContentPage
{
    Button btn_sisse, btn_valja, btn_oo, btn_auto;
    Label lbl;
    Grid bg;
    VerticalStackLayout vsl;
    HorizontalStackLayout hsl, hsl1, hsl2;
    Ellipse red, yellow, green;
    ScrollView sv;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public Valgusfoor()
    {
        bg = new Grid
        {
            WidthRequest = 180,
            HeightRequest = 470,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Black
        };
        red = new Ellipse
        {
            WidthRequest = 165,
            HeightRequest = 150,
            Fill = new SolidColorBrush(Colors.Gray),
            Stroke = Colors.Black,
            StrokeThickness = 5,
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
        };
        yellow = new Ellipse
        {
            WidthRequest = 165,
            HeightRequest = 150,
            Fill = new SolidColorBrush(Colors.Gray),
            Stroke = Colors.Black,
            StrokeThickness = 5,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        green = new Ellipse
        {
            WidthRequest = 165,
            HeightRequest = 150,
            Fill = new SolidColorBrush(Colors.Gray),
            Stroke = Colors.Black,
            StrokeThickness = 5,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tap_red = new TapGestureRecognizer();
        tap_red.Tapped += (sender, e) =>
        {
            red.Fill = new SolidColorBrush(Colors.Red);
        };
        TapGestureRecognizer tap_yellow = new TapGestureRecognizer();
        tap_yellow.Tapped += (sender, e) =>
        {
            yellow.Fill = new SolidColorBrush(Colors.Yellow);
        };
        TapGestureRecognizer tap_green = new TapGestureRecognizer();
        tap_green.Tapped += (sender, e) =>
        {
            green.Fill = new SolidColorBrush(Colors.Green);
        };
        red.GestureRecognizers.Add(tap_red);
        yellow.GestureRecognizers.Add(tap_yellow);
        green.GestureRecognizers.Add(tap_green);
        lbl = new Label
        {
            Text = "Valgusfoor",
            FontSize = 36,
            FontFamily = "OpenSansSemibold",
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        Button btn_sisse = new Button
        {
            Text = "Sisse",
            FontSize = 28,
            FontFamily = "OpenSansSemibold",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
        };
        Button btn_valja = new Button
        {
            Text = "Välja",
            FontSize = 28,
            FontFamily = "OpenSansSemibold",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
        };
        Button btn_oo = new Button
        {
            Text = "Öö",
            FontSize = 28,
            FontFamily = "OpenSansSemibold",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
        };
        Button btn_auto = new Button
        {
            Text = "Automaatreþiim",
            FontSize = 28,
            FontFamily = "OpenSansSemibold",
            TextColor = Colors.Chocolate,
            BackgroundColor = Colors.Beige,
            CornerRadius = 10,
            HeightRequest = 50,
        };
        btn_sisse.Clicked += Btn_sisse_Click;
        btn_valja.Clicked += Btn_valja_Click;
        btn_oo.Clicked += Btn_oo_Click;
        btn_auto.Clicked += Btn_auto_Click;
        bg.Children.Add(red);
        bg.Children.Add(yellow);
        bg.Children.Add(green);
        hsl1 = new HorizontalStackLayout
        {
            btn_sisse, btn_valja,btn_oo, btn_auto
        };
        hsl = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[j],
                FontSize = 28,
                FontFamily = "LowerWestSide400",
                TextColor = Colors.Chocolate,
                BackgroundColor = Colors.Beige,
                CornerRadius = 10,
                HeightRequest = 50,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { lbl, bg, hsl1, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private void Btn_valja_Click(object? sender, EventArgs e)
    {
        red.Fill = Colors.Gray;
        yellow.Fill = Colors.Gray;
        green.Fill = Colors.Gray;
        _cts?.Cancel();
        isActive = false;
    }
    private void Btn_sisse_Click(object? sender, EventArgs e)
    {
        red.Fill = Colors.Red;
        yellow.Fill = Colors.Yellow;
        green.Fill = Colors.Green;
    }
    private bool isActive = false;
    private CancellationTokenSource _cts;

    private async void Btn_oo_Click(object sender, EventArgs e)
    {
        lbl.Text = "Ööreþiim";
        if (!isActive)
        {
            isActive = true;
            _cts = new CancellationTokenSource();

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    await yellow.FadeTo(0.3, 500);
                    yellow.Fill = new SolidColorBrush(Colors.Yellow);

                    await yellow.FadeTo(1.0, 500);
                    yellow.Fill = new SolidColorBrush(Colors.Gray);
                }
            }
            catch (TaskCanceledException) { }
        }
        else
        {
            _cts?.Cancel();
            isActive = false;
        }
    }
    private async void Btn_auto_Click(object? sender, EventArgs e)
    {
        if (!isActive)
        {
            isActive = true;
            _cts = new CancellationTokenSource();

            try
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    lbl.Text = "Seisa";
                    red.Fill = new SolidColorBrush(Colors.Red);
                    await Task.Delay(1500);
                    red.Fill = new SolidColorBrush(Colors.Gray);
                    await Task.Delay(200);
                    lbl.Text = "Valmista";
                    yellow.Fill = new SolidColorBrush(Colors.Yellow);
                    await Task.Delay(1000);
                    yellow.Fill = new SolidColorBrush(Colors.Gray);
                    await Task.Delay(200);
                    lbl.Text = "Sõida";
                    green.Fill = new SolidColorBrush(Colors.Green);
                    await Task.Delay(1500);
                    green.Fill = new SolidColorBrush(Colors.Gray);
                    await Task.Delay(200);
                    lbl.Text = "Valmista";
                    yellow.Fill = new SolidColorBrush(Colors.Yellow);
                    await Task.Delay(1000);
                    yellow.Fill = new SolidColorBrush(Colors.Gray);
                    await Task.Delay(200);
                }
            }
            catch (TaskCanceledException) { }
        }
        else
        {
            _cts?.Cancel();
            isActive = false;
        }
    }
    
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new TimerPage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new DateTimePage());
        }
    }
}