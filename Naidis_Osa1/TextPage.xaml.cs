namespace Naidis_Osa1;

public partial class TextPage : ContentPage
{
    Label lbl;
    Editor editor;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public TextPage(int i)
    {
        lbl = new Label
        {
            Text = "Pealkiri",
            FontSize = 36,
            FontFamily = "Impact",
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

        hsl = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center
        };
        for (int j = 0; j < nupud.Count; j++)
        {
            Button nupp = new Button
            {
                Text = nupud[i],
                FontSize = 18,
                FontFamily = "Impact",
                TextColor = Colors.Black,
                BackgroundColor = Colors.LightBlue,
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
            Children = {lbl, editor, vsl}
        };
    }

    private void Liikumine(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}