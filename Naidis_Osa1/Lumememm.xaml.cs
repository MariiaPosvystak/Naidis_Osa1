using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace Naidis_Osa1;

public partial class Lumememm : ContentPage
{
    Frame body, head, bucket, button1, button2, button3, eye1, eye2;
    Polygon nose;
    Picker picker;
    Label lbl;
    Slider Slider;
    Stepper speedStepper;
    Random random = new Random();
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public Lumememm()
    {
        //head.HasShadow = false;
        //body.HasShadow = false;
        BackgroundColor = Colors.LightSkyBlue;

        // ===== Lumememm (AbsoluteLayout) =====
        AbsoluteLayout absoluteLayout = new AbsoluteLayout
        {
            HeightRequest = 350
        };
        body = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 100,
            HeightRequest = 160,
            WidthRequest = 160
        };
        head = new Frame
        {
            BackgroundColor = Colors.White,
            CornerRadius = 70,
            HeightRequest = 120,
            WidthRequest = 120
        };
        bucket = new Frame
        {
            BackgroundColor = Colors.Black,
            HeightRequest = 70,
            WidthRequest = 60
        };
        button1 = new Frame
        {
            BackgroundColor = Colors.Black,
            CornerRadius = 18,
            HeightRequest = 18,
            WidthRequest = 18
        };
        button2 = new Frame
        {
            BackgroundColor = Colors.Black,
            CornerRadius = 23,
            HeightRequest = 23,
            WidthRequest = 23
        };
        eye1 = new Frame
        {
            BackgroundColor = Colors.Black,
            CornerRadius = 20,
            HeightRequest = 3,
            WidthRequest = 3
        };
        eye2 = new Frame
        {
            BackgroundColor = Colors.Black,
            CornerRadius = 20,
            HeightRequest = 3,
            WidthRequest = 3
        };
        //nose = new Polygon
        //{
        //    Points = new PointCollection
        //    {
        //        new Point(0, 0.5),
        //        new Point(1, 0),
        //        new Point(1, 1)
        //    },
        //    Fill = new SolidColorBrush(Colors.Orange),
        //    Stroke = Colors.Orange,
        //    StrokeThickness = 5
        //};
        var nose = new Polygon
        {
            Points = new PointCollection
            {
                new Point(0, 15),   
                new Point(15, 5),    
                new Point(15, 15)    
            },
            Fill = Colors.Orange,
            WidthRequest = 25,
            HeightRequest = 15
        };
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

        AbsoluteLayout.SetLayoutBounds(nose, new Rect(0.5, 0.42, -1, -1));
        AbsoluteLayout.SetLayoutFlags(nose, AbsoluteLayoutFlags.PositionProportional);

        absoluteLayout.Children.Add(body);
        absoluteLayout.Children.Add(head);
        absoluteLayout.Children.Add(eye1);
        absoluteLayout.Children.Add(eye2);
        absoluteLayout.Children.Add(nose);
        absoluteLayout.Children.Add(bucket);
        absoluteLayout.Children.Add(button1);
        absoluteLayout.Children.Add(button2);
        


        // ===== Picker =====
        picker = new Picker { Title = "Vali tegevus" };
        picker.Items.Add("Peida lumememm");
        picker.Items.Add("Näita lumememm");
        picker.Items.Add("Muuda värvi");
        picker.Items.Add("Sulata");
        picker.Items.Add("Tantsi");

        // ===== Button =====
        Button actionButton = new Button
        {
            Text = "Käivita tegevus"
        };
        actionButton.Clicked += OnActionClicked;

        // ===== Label =====
        lbl = new Label
        {
            FontSize = 18,
            HorizontalOptions = LayoutOptions.Center
        };

        // ===== Slider =====
        Label sliderlbl = new Label { Text = "Läbipaistvus" };
        Slider = new Slider
        {
            Minimum = 0,
            Maximum = 1,
            Value = 1
        };
        Slider.ValueChanged += OnSliderChanged;

        // ===== Stepper =====
        Label stepperlbl = new Label { Text = "Kiirus (ms)" };
        speedStepper = new Stepper
        {
            Minimum = 100,
            Maximum = 3000,
            Increment = 100,
            Value = 1000
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
                TextColor = Colors.LightSkyBlue,
                BackgroundColor = Colors.DarkBlue,
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
            Children =
            {
                absoluteLayout,
                picker,
                actionButton,
                lbl,
                sliderlbl,
                Slider,
                stepperlbl,
                speedStepper,
                hsl
            }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
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
                body.IsVisible = false;
                head.IsVisible = false;
                bucket.IsVisible = false;
                break;

            case "Näita lumememm":
                body.IsVisible = true;
                head.IsVisible = true;
                bucket.IsVisible = true;

                body.Opacity = 1;
                head.Opacity = 1;
                bucket.Opacity = 1;

                body.Scale = 1;
                head.Scale = 1;
                bucket.Scale = 1;
                break;

            case "Muuda värvi":
                bool answer = await DisplayAlertAsync(
                    "Värv",
                    "Kas muuta lumememme värvi?",
                    "Jah",
                    "Ei");

                if (answer)
                {
                    Color randomColor = Color.FromRgb(
                        random.Next(256),
                        random.Next(256),
                        random.Next(256));

                    body.BackgroundColor = randomColor;
                    head.BackgroundColor = randomColor;
                }
                break;

            case "Sulata":
                await Task.WhenAll(
                    body.ScaleToAsync(0.5, speed),
                    head.ScaleToAsync(0.5, speed),
                    bucket.ScaleToAsync(0.5, speed),
                    body.FadeToAsync(0, speed),
                    head.FadeToAsync(0, speed),
                    bucket.FadeToAsync(0, speed)
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
            body.TranslateToAsync(-50, 0, half),
            head.TranslateToAsync(-50, 0, half),
            bucket.TranslateToAsync(-50, 0, half)
        );

        await Task.WhenAll(
            body.TranslateToAsync(50, 0, half),
            head.TranslateToAsync(50, 0, half),
            bucket.TranslateToAsync(50, 0, half)
        );

        await Task.WhenAll(
            body.TranslateToAsync(0, 0, half),
            head.TranslateToAsync(0, 0, half),
            bucket.TranslateToAsync(0, 0, half)
        );
    }

    private void OnSliderChanged(object sender, ValueChangedEventArgs e)
    {
        body.Opacity = e.NewValue;
        head.Opacity = e.NewValue;
        bucket.Opacity = e.NewValue;
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new RGB_Page());
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