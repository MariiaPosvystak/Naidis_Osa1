using System.Threading.Tasks;

namespace Naidis_Osa1;

public partial class StartPage : ContentPage
{
	VerticalStackLayout vst;
	ScrollView sv;
	public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(), new FigurePage(), new TimerPage(), new Valgusfoor(), 
		new DateTimePage(), new StepperSliderPage(), new RGB_Page(), new Lumememm(), new PopUp(), 
		new PickerImagePage(), new Trips_Traps_Trull(), new TableView_Page(), new Sobrade_kont(), new List_Page(),
		new Euroopa_riik(), new CarouselView() };
	public List<string> LeheNimed = new List<string>() { "Tekst", "Kujund", "Timer", "Valgusfoor", "DateTime", "StepperSlider", "RGB", "Lumememm", 
		"PopUp", "Grid", "Trips Traps Trull", "TableView", 
		"Kontaktandmed", "List", "Euroopa riikide", "Carousel" };
	public StartPage()
	{
		BackgroundColor = Color.FromHex("#EDE0D4");
		Title = "Avaleht";
        vst = new VerticalStackLayout { Padding=20, Spacing=15 };
		for (int i=0; i < Lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = LeheNimed[i],
				FontSize = 25,
				BackgroundColor = Color.FromHex("#7F5539"),
				FontFamily = "LowerWestSide400",
				TextColor = Color.FromHex("#EDE0D4"),
				CornerRadius = 10,
				HeightRequest = 50,
				ZIndex = i
			};
			vst.Add(nupp);
			nupp.Clicked += (sender, e) =>
			{
				var valik = Lehed[nupp.ZIndex];
				Navigation.PushAsync(valik);
			};
		}
		sv = new ScrollView { Content = vst };
		Content = sv;
	}
}