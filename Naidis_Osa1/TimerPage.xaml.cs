namespace Naidis_Osa1;

public partial class TimerPage : ContentPage
{
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public TimerPage()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Timer";
        Label lbl = new Label
        {
            Text = "Valjuta nuppule ja siia",
            FontSize = 18,
            TextColor = Color.FromHex("#B08968"),
            FontFamily = "LowerWestSide400"
        };
        Button timer_btn = new Button
        {
            Text = "Naita info",
            FontSize = 18,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#7F5539"),
            BackgroundColor = Color.FromHex("#E6CCB2"),
            BorderColor = Color.FromHex("#7F5539"),
            BorderWidth = 2,
            CornerRadius = 10,
            HeightRequest = 40
        };
        timer_btn.Clicked += timer_btn_Clicked;
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += (sender, e) =>
        {
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
                FontSize = 16,
                FontFamily = "LowerWestSide400",
                TextColor = Color.FromHex("#7F5539"),
                BackgroundColor = Color.FromHex("#E6CCB2"),
                BorderColor = Color.FromHex("#7F5539"),
                BorderWidth = 2,
                CornerRadius = 10,
                HeightRequest = 30,
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
            Children = { lbl, timer_btn, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }

    bool on_off = true;

    private async void ShowTime()
    {
        while (on_off)
        {
            Button timer_btn = new Button();
            timer_btn.Text = DateTime.Now.ToString("T");
            await Task.Delay(1000);
        }
    }

    private void timer_btn_Clicked(object sender, EventArgs e)
    {
        if (on_off)
        {
            on_off = false;
        }
        else
        {
            on_off = true;
            ShowTime();
        }
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new FigurePage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new Valgusfoor());
        }
    }
}