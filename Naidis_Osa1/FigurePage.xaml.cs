using Microsoft.Maui.Controls.Shapes;

namespace Naidis_Osa1;

public partial class FigurePage : ContentPage
{
    BoxView bv;
    Ellipse pall;
    Polygon kolmnurk;
    Random rnd =new Random();
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public FigurePage()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Kujund";
        int r = rnd.Next(256);
        int g = rnd.Next(256);
        int b = rnd.Next(256);
        bv = new BoxView
        {
            Color = Color.FromRgb(r, g, b),
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            CornerRadius = 30,
        };
        pall = new Ellipse
        {
            WidthRequest = 200,
            HeightRequest = 200,
            Fill = new SolidColorBrush(Color.FromRgb(b, r, g)),
            HorizontalOptions = LayoutOptions.Center
        };
        kolmnurk = new Polygon()
        {
            Points=new PointCollection
            {
                new Point(0,200), //vasak all
                new Point(100,0), //keskel
                new Point(200,200), //parem all
            },
            Fill = new SolidColorBrush(Color.FromRgb(g, b, r)),
            HorizontalOptions = LayoutOptions.Center
        };
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += (sender, e) =>
        {
            int r = rnd.Next(256);
            int g = rnd.Next(256);
            int b = rnd.Next(256);
            bv.Color = Color.FromRgb(r, g, b);
            pall.Fill = Color.FromRgb(b, r, g);
            bv.WidthRequest = bv.Width + 20;
            bv.HeightRequest = bv.Height + 30;
            if (bv.WidthRequest > (int)DeviceDisplay.MainDisplayInfo.Width)
            {
                bv.WidthRequest = 200;
                bv.HeightRequest = 200;
            }
        };
        TapGestureRecognizer tap_kolmnurk = new TapGestureRecognizer();
        tap_kolmnurk.Tapped += (sender, e) =>
        {
            int r = rnd.Next(256);
            int g = rnd.Next(256);
            int b = rnd.Next(256);
            kolmnurk.Fill = Color.FromRgb(b, r, g);
            kolmnurk.TranslateTo(0, 0);
            if (kolmnurk.WidthRequest > (int)DeviceDisplay.MainDisplayInfo.Width)
            {
                kolmnurk.WidthRequest = 200;
                kolmnurk.HeightRequest = 200;
            }
        };
        bv.GestureRecognizers.Add(tap);
        pall.GestureRecognizers.Add(tap);
        kolmnurk.GestureRecognizers.Add(tap_kolmnurk);


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
                FontSize = 20,
                FontFamily = "LowerWestSide400",
                TextColor = Color.FromHex("#7F5539"),
                BackgroundColor = Color.FromHex("#E6CCB2"),
                BorderColor = Color.FromHex("#7F5539"),
                BorderWidth = 2,
                CornerRadius = 10,
                HeightRequest = 40,
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
            Children = { bv, pall, kolmnurk, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new TextPage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new TimerPage());
        }
    }
}