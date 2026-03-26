namespace Naidis_Osa1;

public partial class TextPage : ContentPage
{
    Label lbl;
    Editor editor;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi"};
    public TextPage()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "Tekst";
        lbl = new Label
        {
            Text = "Pealkiri",
            FontSize = 36,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#B08968"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        editor = new Editor
        {
            Placeholder = "Sisesta tekst ...",
            PlaceholderColor = Color.FromHex("#751628"),
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
        Button volume = new Button
        {
            Text = "Volume",
            FontSize = 16,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#7F5539"),
            BackgroundColor = Color.FromHex("#E6CCB2"),
            BorderColor = Color.FromHex("#7F5539"),
            BorderWidth = 2,
            CornerRadius = 10,
            HeightRequest = 30,
        };
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = {lbl, editor, volume, hsl }
        };
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