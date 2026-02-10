namespace Naidis_Osa1;

public partial class TextPage : ContentPage
{
    Label lbl;
    Editor editor;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi"};
    public TextPage(int i)
    {
        lbl = new Label
        {
            Text = "Pealkiri",
            FontSize = 36,
            FontFamily = "LowerWestSide400",
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        editor = new Editor
        {
            Placeholder = "Sisesta tekst ...",
            PlaceholderColor = Colors.Red,
            FontSize = 17,
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Italic
        };
        editor.TextChanged += (sender, e) =>
        {
            lbl.Text = editor.Text;
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
                FontSize = 18,
                FontFamily = "LowerWestSide400",
                TextColor = Colors.White,
                BackgroundColor = Colors.DarkBlue,
                CornerRadius = 10,
                HeightRequest = 40,
                ZIndex = j
            };
            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }
        Button volume = new Button
        {
            Text = "Volume",
            FontSize = 18,
            FontFamily = "LowerWestSide400",
            TextColor = Colors.White,
            BackgroundColor = Colors.DarkBlue,
            CornerRadius = 10,
            HeightRequest = 40,
        };
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = {lbl, editor, hsl}
        };
        vsl.Add(volume);
        volume.Clicked += Nupp_Clicked;
        Content = vsl;
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if(nupp.ZIndex == 0)
        {
            Navigation.PopAsync();
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new FigurePage());
        }
    }

    private async void Nupp_Clicked(object? sender, EventArgs e)
    {
        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

        SpeechOptions options = new SpeechOptions()
        {
            Pitch = 1.5f,    // 0.0 – 2.0
            Volume = 0.75f,  // 0.0 – 1.0
            Locale = locales.FirstOrDefault()
        };

        var text = editor.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            await DisplayAlert("Viga", "Palun sisesta tekst", "OK");
            return;
        }

        try
        {
            await TextToSpeech.SpeakAsync(text, options);
        }
        catch (Exception ex)
        {
            await DisplayAlert("TTS viga", ex.Message, "OK");
        }
    }
}