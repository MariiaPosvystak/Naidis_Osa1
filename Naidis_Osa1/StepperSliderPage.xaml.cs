using Microsoft.Maui.Layouts;

namespace Naidis_Osa1;

public partial class StepperSliderPage : ContentPage
{
    Label lbl;
    Stepper stepper;
    Slider slider;
    AbsoluteLayout al;
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public StepperSliderPage()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "StepperSlider";
        lbl = new Label
        {
            Text = "...",
            BackgroundColor = Colors.LightGray
        };
        stepper = new Stepper
        {
            Minimum = 0,
            Maximum = 360,
            Increment = 5,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center
        };
        stepper.ValueChanged += Stepper_Slider_ValueChanged;
        slider = new Slider
        {
            Minimum = 0,
            Maximum = 360,
            Value = 50,
            HorizontalOptions = LayoutOptions.Center,
            MinimumTrackColor = Colors.LightGray,
            MaximumTrackColor = Colors.DarkGray,
            ThumbColor = Colors.Gray,
            WidthRequest = 300
        };
        slider.ValueChanged += Stepper_Slider_ValueChanged;
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
            Children = { hsl }
        };
        sv = new ScrollView { Content = vsl };
        al = new AbsoluteLayout { Children = { lbl, stepper, slider, sv } };
        List<View> controls = new List<View> { lbl, stepper, slider, sv };
        for (int i = 0; i < controls.Count; i++)
        {
            double yKoht = 0.2 + i * 0.2;
            AbsoluteLayout.SetLayoutBounds(controls[i], new Rect(0.5, yKoht, 300, 70));
            AbsoluteLayout.SetLayoutFlags(controls[i], AbsoluteLayoutFlags.PositionProportional);
        }
        Content = al;
    }
    private void Stepper_Slider_ValueChanged(object? sender, ValueChangedEventArgs e)
    {
        lbl.Text = $"Stepperi/Slideri väärtus: {e.NewValue:F0}";
        lbl.FontSize = 24 + e.NewValue / 4;
        lbl.BackgroundColor = Color.FromRgb((int)(e.NewValue * 2.55), (int)(255 - e.NewValue * 2.55), 128);
        lbl.TextColor = Color.FromRgb((int)(255 - e.NewValue * 2.55), (int)(e.NewValue * 2.55), 128);
        lbl.Rotation = e.NewValue;
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new DateTimePage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new RGB_Page());
        }
    }
}