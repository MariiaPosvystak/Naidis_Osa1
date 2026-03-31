using Microsoft.Maui.Layouts;

namespace Naidis_Osa1;

public partial class Lumememm : ContentPage
{
    Frame body, head, bucket, button1, button2, eye1, eye2;
    Picker picker;
    Label lbl;
    Slider Slider;
    Stepper speedStepper;
    Button actionButton;
    Random random = new Random();
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };

    public Lumememm()
    {
        BackgroundImageSource = "lumememm.jpg";
        Title = "Lumememm";
        AbsoluteLayout absoluteLayout = new AbsoluteLayout
        {
            HeightRequest = 350
        };

        body = new Frame { BackgroundColor = Colors.White, CornerRadius = 100, HeightRequest = 160, WidthRequest = 160 };
        head = new Frame { BackgroundColor = Colors.White, CornerRadius = 70, HeightRequest = 120, WidthRequest = 120 };
        bucket = new Frame { BackgroundColor = Colors.Black, HeightRequest = 70, WidthRequest = 60 };
        button1 = new Frame { BackgroundColor = Colors.Black, CornerRadius = 18, HeightRequest = 18, WidthRequest = 18 };
        button2 = new Frame { BackgroundColor = Colors.Black, CornerRadius = 23, HeightRequest = 23, WidthRequest = 23 };
        eye1 = new Frame { BackgroundColor = Colors.Black, CornerRadius = 20, HeightRequest = 3, WidthRequest = 3 };
        eye2 = new Frame { BackgroundColor = Colors.Black, CornerRadius = 20, HeightRequest = 3, WidthRequest = 3 };

        AbsoluteLayout.SetLayoutBounds(body, new Rect(0.5, 1, 160, 160));
        AbsoluteLayout.SetLayoutFlags(body, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(head, new Rect(0.5, 0.43, 120, 120));
        AbsoluteLayout.SetLayoutFlags(head, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(bucket, new Rect(0.5, 0.2, 130, 40));
        AbsoluteLayout.SetLayoutFlags(bucket, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(button1, new Rect(0.5, 0.65, 18, 18));
        AbsoluteLayout.SetLayoutFlags(button1, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(button2, new Rect(0.5, 0.8, 23, 23));
        AbsoluteLayout.SetLayoutFlags(button2, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(eye1, new Rect(0.435, 0.42, 3, 3));
        AbsoluteLayout.SetLayoutFlags(eye1, AbsoluteLayoutFlags.PositionProportional);

        AbsoluteLayout.SetLayoutBounds(eye2, new Rect(0.568, 0.42, 3, 3));
        AbsoluteLayout.SetLayoutFlags(eye2, AbsoluteLayoutFlags.PositionProportional);

        absoluteLayout.Children.Add(body);
        absoluteLayout.Children.Add(head);
        absoluteLayout.Children.Add(eye1);
        absoluteLayout.Children.Add(eye2);
        absoluteLayout.Children.Add(bucket);
        absoluteLayout.Children.Add(button1);
        absoluteLayout.Children.Add(button2);

        picker = new Picker
        {
            Title = "Vali tegevus",
            TextColor = Color.FromHex("#7F5539"),
            BackgroundColor = Color.FromHex("#E6CCB2")
        };

        picker.Items.Add("Peida lumememm");
        picker.Items.Add("Näita lumememm");
        picker.Items.Add("Muuda värvi");
        picker.Items.Add("Sulata");
        picker.Items.Add("Tantsi");

        actionButton = new Button
        {
            Text = "Käivita tegevus",
            FontSize = 17,
            TextColor = Color.FromHex("#7F5539"),
            BackgroundColor = Color.FromHex("#E6CCB2"),
            BorderColor = Color.FromHex("#7F5539"),
            BorderWidth = 2,
            CornerRadius = 10,
            HeightRequest = 30
        };
        actionButton.Clicked += OnActionClicked;

        lbl = new Label
        {
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center
        };

        Label sliderlbl = new Label
        {
            Text = "Läbipaistvus",
            TextColor = Color.FromHex("#7F5539"),
            BackgroundColor = Color.FromHex("#E6CCB2"),
            FontSize = 20
        };

        Slider = new Slider
        {
            Minimum = 0,
            Maximum = 1,
            Value = 1,
            MinimumTrackColor = Color.FromHex("#7F5539"),
            MaximumTrackColor = Color.FromHex("#7F5539"),
            ThumbColor = Colors.Gray
        };
        Slider.ValueChanged += OnSliderChanged;

        speedStepper = new Stepper
        {
            Minimum = 100,
            Maximum = 2000,
            Increment = 100,
            Value = 500
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
                TextColor = Color.FromHex("#7F5539"),
                BackgroundColor = Color.FromHex("#E6CCB2"),
                BorderColor = Color.FromHex("#7F5539"),
                BorderWidth = 2,
                CornerRadius = 10,
                HeightRequest = 30,
                ZIndex = j
            };
            nupp.Clicked += Liikumine;
            hsl.Add(nupp);
        }

        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children =
            {
                absoluteLayout,
                picker,
                actionButton,
                lbl,
                sliderlbl,
                Slider,
                speedStepper,
                hsl
            }
        };

        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private void ShowSnowman()
    {
        body.IsVisible = true;
        head.IsVisible = true;
        bucket.IsVisible = true;
        eye1.IsVisible = true;
        eye2.IsVisible = true;
        button1.IsVisible = true;
        button2.IsVisible = true;

        body.Opacity = 1;
        head.Opacity = 1;
        bucket.Opacity = 1;
        eye1.Opacity = 1;
        eye2.Opacity = 1;
        button1.Opacity = 1;
        button2.Opacity = 1;

        body.Scale = 1;
        head.Scale = 1;
        bucket.Scale = 1;
        eye1.Scale = 1;
        eye2.Scale = 1;
        button1.Scale = 1;
        button2.Scale = 1;

        body.TranslationX = 0;
        head.TranslationX = 0;
        bucket.TranslationX = 0;
        eye1.TranslationX = 0;
        eye2.TranslationX = 0;
        button1.TranslationX = 0;
        button2.TranslationX = 0;

        body.TranslationY = 0;
        head.TranslationY = 0;
        bucket.TranslationY = 0;
        eye1.TranslationY = 0;
        eye2.TranslationY = 0;
        button1.TranslationY = 0;
        button2.TranslationY = 0;
    }
    private void HideSnowman()
    {
        body.IsVisible = false;
        head.IsVisible = false;
        bucket.IsVisible = false;
        eye1.IsVisible = false;
        eye2.IsVisible = false;
        button1.IsVisible = false;
        button2.IsVisible = false;
    }
    private void ResetSnowman()
    {
        body.Opacity = 1;
        head.Opacity = 1;
        bucket.Opacity = 1;
        eye1.Opacity = 1;
        eye2.Opacity = 1;
        button1.Opacity = 1;
        button2.Opacity = 1;

        body.Scale = 1;
        head.Scale = 1;
        bucket.Scale = 1;
        eye1.Scale = 1;
        eye2.Scale = 1;
        button1.Scale = 1;
        button2.Scale = 1;

        body.TranslationX = 0;
        head.TranslationX = 0;
        bucket.TranslationX = 0;
        eye1.TranslationX = 0;
        eye2.TranslationX = 0;
        button1.TranslationX = 0;
        button2.TranslationX = 0;

        body.TranslationY = 0;
        head.TranslationY = 0;
        bucket.TranslationY = 0;
        eye1.TranslationY = 0;
        eye2.TranslationY = 0;
        button1.TranslationY = 0;
        button2.TranslationY = 0;
    }
    private async void OnActionClicked(object sender, EventArgs e)
    {
        if (picker.SelectedItem == null)
            return;

        string action = picker.SelectedItem.ToString();
        lbl.Text = "Valitud: " + action;

        uint speed = (uint)speedStepper.Value;

        switch (action)
        {
            case "Peida lumememm":
                HideSnowman();
                break;

            case "Näita lumememm":
                ShowSnowman();
                break;

            case "Muuda värvi":
                bool answer = await DisplayAlert("Värv", "Kas muuta lumememme värvi?", "Jah", "Ei");
                if (answer)
                {
                    Color randomColor = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256));
                    body.BackgroundColor = randomColor;
                    head.BackgroundColor = randomColor;
                }
                break;

            case "Sulata":
                await Task.WhenAll(
                    body.ScaleTo(0.5, speed),
                    head.ScaleTo(0.5, speed),
                    bucket.ScaleTo(0.5, speed),
                    eye1.ScaleTo(0.5, speed),
                    eye2.ScaleTo(0.5, speed),
                    button1.ScaleTo(0.5, speed),
                    button2.ScaleTo(0.5, speed),

                    body.FadeTo(0, speed),
                    head.FadeTo(0, speed),
                    bucket.FadeTo(0, speed),
                    eye1.FadeTo(0, speed),
                    eye2.FadeTo(0, speed),
                    button1.FadeTo(0, speed),
                    button2.FadeTo(0, speed)
                );
                break;

            case "Tantsi":
                await Dance(speed);
                await TextToSpeech.Default.SpeakAsync("Jõulud tulevad!");
                break;
        }
    }

    private async Task Dance(uint speed)
    {
        uint half = speed / 2;

        await Task.WhenAll(
            body.TranslateTo(-50, 0, half),
            head.TranslateTo(-50, 0, half),
            bucket.TranslateTo(-50, 0, half),
            eye1.TranslateTo(-50, 0, half),
            eye2.TranslateTo(-50, 0, half),
            button1.TranslateTo(-50, 0, half),
            button2.TranslateTo(-50, 0, half)
        );

        await Task.WhenAll(
            body.TranslateTo(50, 0, half),
            head.TranslateTo(50, 0, half),
            bucket.TranslateTo(50, 0, half),
            eye1.TranslateTo(50, 0, half),
            eye2.TranslateTo(50, 0, half),
            button1.TranslateTo(50, 0, half),
            button2.TranslateTo(50, 0, half)
        );

        await Task.WhenAll(
            body.TranslateTo(0, 0, half),
            head.TranslateTo(0, 0, half),
            bucket.TranslateTo(0, 0, half),
            eye1.TranslateTo(0, 0, half),
            eye2.TranslateTo(0, 0, half),
            button1.TranslateTo(0, 0, half),
            button2.TranslateTo(0, 0, half)
        );
    }

    private void OnSliderChanged(object sender, ValueChangedEventArgs e)
    {
        body.Opacity = e.NewValue;
        head.Opacity = e.NewValue;
        bucket.Opacity = e.NewValue;
        eye1.Opacity = e.NewValue;
        eye2.Opacity = e.NewValue;
        button1.Opacity = e.NewValue;
        button2.Opacity = e.NewValue;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;

        if (nupp.ZIndex == 0)
            Navigation.PushAsync(new RGB_Page());
        else if (nupp.ZIndex == 1)
            Navigation.PopToRootAsync();
        else if (nupp.ZIndex == 2)
            Navigation.PushAsync(new PopUp());
    }
}