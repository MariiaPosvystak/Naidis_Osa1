namespace Naidis_Osa1;

public partial class RGB_Page : ContentPage
{
    Label lbl, redlbl, greenlbl, bluelbl;
    BoxView bv, redbv, greenbv, bluebv;
    Slider redSlider, greenSlider, blueSlider;
    ScrollView sv;
    HorizontalStackLayout hsl, hsl1, hsl2;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public RGB_Page()
	{
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "RGB Mudel";
        lbl = new Label
        {
            Text = "RGB mudel",
            FontSize = 36,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#B08968"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        redlbl = new Label
        {
            Text = "Red",
            FontSize = 15,
            FontFamily = "OpenSansRegular",
            TextColor = Color.FromHex("#B08968"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        greenlbl = new Label
        {
            Text = "Green",
            FontSize = 15,
            FontFamily = "OpenSansRegular",
            TextColor = Color.FromHex("#B08968"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        bluelbl = new Label
        {
            Text = "Blue",
            FontSize = 15,
            FontFamily = "OpenSansRegular",
            TextColor = Color.FromHex("#B08968"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        bv = new BoxView
        {
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 30,
        };
        redbv = new BoxView
        {
            WidthRequest = 75,
            HeightRequest = 75,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 15,
        };
        greenbv = new BoxView
        {
            WidthRequest = 75,
            HeightRequest = 75,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 15,
        };
        bluebv = new BoxView
        {
            WidthRequest = 75,
            HeightRequest = 75,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 15,
        };
        redSlider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Color.FromRgb(255, 0, 0),
            MaximumTrackColor = Color.FromRgb(0,0,0),
            ThumbColor = Colors.Gray,
            WidthRequest = 300
        };
        redSlider.ValueChanged += OnSliderValueChanger;
        greenSlider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Color.FromRgb(0, 255, 0),
            MaximumTrackColor = Color.FromRgb(0, 0, 0),
            ThumbColor = Colors.Gray,
            WidthRequest = 300
        };
        greenSlider.ValueChanged += OnSliderValueChanger;
        blueSlider = new Slider
        {
            Minimum = 0,
            Maximum = 255,
            Value = 0,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Color.FromRgb(0, 0, 255),
            MaximumTrackColor = Color.FromRgb(0, 0, 0),
            ThumbColor = Colors.Gray,
            WidthRequest = 300
        };
        blueSlider.ValueChanged += OnSliderValueChanger;
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
        hsl1 = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            Children = { redbv, greenbv, bluebv }
        };
        hsl2 = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            Children = { redlbl, greenlbl, bluelbl }
        };
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { lbl, bv, redSlider, greenSlider, blueSlider, hsl1, hsl2, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private void OnSliderValueChanger(object sender, ValueChangedEventArgs args)
    {
        if (sender == redSlider)
        {
            redlbl.Text = string.Format("Red = {0:X2}", (int)args.NewValue);
            redbv.Color = Color.FromRgb((int)args.NewValue, 0, 0);
        }
        else if (sender == greenSlider)
        {
            greenlbl.Text = string.Format("Green = {0:X2}", (int)args.NewValue);
            greenbv.Color = Color.FromRgb(0, (int)args.NewValue, 0);
        }
        else if (sender == blueSlider)
        {
            bluelbl.Text = string.Format("Blue = {0:X2}", (int)args.NewValue);
            bluebv.Color = Color.FromRgb(0, 0, (int)args.NewValue);
        }
        bv.Color = Color.FromRgb((int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value);
        lbl.TextColor = Color.FromRgb((int)redSlider.Value, (int)greenSlider.Value, (int)blueSlider.Value);
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new StepperSliderPage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new Lumememm());
        }
    }
}