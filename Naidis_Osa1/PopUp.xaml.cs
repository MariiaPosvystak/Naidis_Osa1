namespace Naidis_Osa1;

public partial class PopUp : ContentPage
{
    int min = 1;
    int max = 100;
    int secret;
    Random rnd = new Random();
    Label lbl;
    ScrollView sv;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public PopUp()
    {
        BackgroundColor = Color.FromHex("#EDE0D4");
        Title = "PopUp";
        lbl = new Label
        {
            Text = "Arva number",
            FontSize = 36,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#B08968"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };
        var startBtn = new Button
        {
            Text = "Alusta mängu",
            BackgroundColor = Color.FromHex("#E6CCB2"),
            TextColor = Color.FromHex("#7F5539"),
            FontFamily = "LowerWestSide400",
            BorderWidth = 2,
            CornerRadius = 10
        };
        startBtn.Clicked += StartGame;
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
            Children = { lbl, startBtn, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new Lumememm());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new PickerImagePage());
        }
    }
    private async void StartGame(object sender, EventArgs e)
    {
        string mode = await DisplayActionSheet(
            "Kes hakkab arvama?",
            "Loobu", null,
            "Mina arvan",
            "Bot arvab");

        if (mode == "Loobu" || mode == null) return;

        string minStr = await DisplayPromptAsync("Vahemik", "Sisesta minimaalne number:", initialValue: "1");
        string maxStr = await DisplayPromptAsync("Vahemik", "Sisesta maksimaalne number:", initialValue: "100");

        if (!int.TryParse(minStr, out min) || !int.TryParse(maxStr, out max) || min >= max)
        {
            await DisplayAlert("Viga", "Vale vahemik!", "OK");
            return;
        }

        if (mode == "Mina arvan")
            await UserGuesses();
        else
            await BotGuesses();
    }
    private async Task UserGuesses()
    {
        secret = rnd.Next(min, max + 1);

        while (true)
        {
            string guessStr = await DisplayPromptAsync("Sinu kord", $"Sisesta number vahemikus {min}–{max}:");
            if (!int.TryParse(guessStr, out int guess))
            {
                await DisplayAlert("Viga", "Palun sisesta number!", "OK");
                continue;
            }

            if (guess < secret)
                await DisplayAlert("Vihje", "Õige number on SUUREM", "OK");
            else if (guess > secret)
                await DisplayAlert("Vihje", "Õige number on VÄIKSEM", "OK");
            else
            {
                await DisplayAlert("Võit!", "Sa arvasid numbri ära!", "Hurraa!");
                break;
            }
        }
    }
    private async Task BotGuesses()
    {
        int low = min;
        int high = max;

        await DisplayAlert("Alustame!", $"Mõtle välja number vahemikus {min}–{max} ja vajuta OK", "OK");

        while (low <= high)
        {
            int botGuess = (low + high) / 2;

            string answer = await DisplayActionSheet(
                $"Kas sinu number on {botGuess}?",
                "Loobu", null,
                "Suurem",
                "Väiksem",
                "Jah");

            if (answer == "Loobu" || answer == null) return;

            if (answer == "Jah")
            {
                await DisplayAlert("Võit!", "Bot arvas numbri ära!", "OK");
                return;
            }
            else if (answer == "Suurem")
                low = botGuess + 1;
            else if (answer == "Väiksem")
                high = botGuess - 1;
        }

        await DisplayAlert("Viga", "Midagi läks valesti. Võib?olla vastasid valesti.", "OK");
    }
}
