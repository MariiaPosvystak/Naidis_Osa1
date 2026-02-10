namespace Naidis_Osa1;

public partial class TimerPage : ContentPage
{
    VerticalStackLayout vsl;
    public TimerPage()
    {
        Label lbl = new Label
        {
            Text = "Valjuta nuppule ja siia",
            FontSize = 18,
            FontFamily = "LowerWestSide400"
        };
        Button timer_btn = new Button
        {
            Text = "Naita info",
            FontSize = 18,
            FontFamily = "LowerWestSide400",
            TextColor = Colors.White,
            BackgroundColor = Colors.DarkBlue,
            CornerRadius = 10,
            HeightRequest = 40
        };
        timer_btn.Clicked += timer_btn_Clicked;
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += (sender, e) =>
        {
        };
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children = { lbl, timer_btn }
        };
        Content = vsl;
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
}