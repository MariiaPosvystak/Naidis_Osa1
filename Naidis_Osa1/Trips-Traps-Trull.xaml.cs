using Microsoft.Maui.Graphics.Text;

namespace Naidis_Osa1;

public partial class Trips_Traps_Trull : ContentPage
{
    private Button[,] buttons = new Button[3, 3];
    private string currentPlayer = "X";
    private bool gameOver = false;
    ScrollView sv;
    Label lbl;
    HorizontalStackLayout hsl;
    VerticalStackLayout vsl;
    List<string> nupud = new List<string>() { "Tagasi", "Avaleht", "Edasi" };
    public Trips_Traps_Trull()
    {
        BackgroundColor = Color.FromHex("#D1CBC7");
        lbl = new Label
        {
            Text = "Trips Traps Trull",
            FontSize = 36,
            FontFamily = "LowerWestSide400",
            TextColor = Color.FromHex("#907564"),
            HorizontalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold
        };

        // Кнопки управления
        Button newGameBtn = new Button
        {
            Text = "Uus mäng",
            TextColor = Color.FromHex("#D1CBC7"),
            BackgroundColor = Color.FromHex("#907564")
        };
        newGameBtn.Clicked += NewGame;

        Button whoStartsBtn = new Button
        {
            Text = "Kes alustab?",
            TextColor = Color.FromHex("#D1CBC7"),
            BackgroundColor = Color.FromHex("#907564")
        };
        whoStartsBtn.Clicked += WhoStarts;

        // Игровое поле
        Grid grid = new Grid();

        for (int i = 0; i < 3; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var button = new Button
                {
                    FontSize = 40,
                    BackgroundColor = Color.FromHex("#907564")
                };

                button.Clicked += OnButtonClicked;

                buttons[row, col] = button;
                grid.Add(button, col, row);
            }
        }

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
                TextColor = Color.FromHex("#D1CBC7"),
                BackgroundColor = Color.FromHex("#907564"),
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
            HorizontalOptions = LayoutOptions.Center,
            Children = { lbl, grid, whoStartsBtn, newGameBtn, hsl }
        };
        sv = new ScrollView { Content = vsl };
        Content = sv;
    }
    private void OnButtonClicked(object sender, EventArgs e)
    {
        if (gameOver) return;

        Button button = (Button)sender;
        if (!string.IsNullOrEmpty(button.Text)) return;

        button.Text = "X";
        button.TextColor = Colors.Red;

        if (CheckWinner()) { EndGame("X"); return; }
        if (IsDraw()) { gameOver = true; DisplayAlert("Mäng läbi", "Viik!", "OK"); return; }

        currentPlayer = "O";
        SmartBotMove();
        //if (gameOver) return;

        //Button button = (Button)sender;

        //if (!string.IsNullOrEmpty(button.Text))
        //    return;

        //// Человек ходит
        //button.Text = "X";
        //button.TextColor = Colors.Red;

        //if (CheckWinner())
        //{
        //    gameOver = true;
        //    DisplayAlert("Mäng läbi", "X võitis!", "OK");
        //    return;
        //}

        //if (IsDraw())
        //{
        //    gameOver = true;
        //    DisplayAlert("Mäng läbi", "Viik!", "OK");
        //    return;
        //}

        //// Теперь ход бота
        //currentPlayer = "O";
        //BotMove();
        //if (gameOver) return;

        //Button button = (Button)sender;

        //if (!string.IsNullOrEmpty(button.Text))
        //    return;

        //button.Text = currentPlayer;

        //if (currentPlayer == "X")
        //    button.TextColor = Colors.Red;  
        //else
        //    button.TextColor = Colors.Blue;

        //if (CheckWinner())
        //{
        //    gameOver = true;
        //    DisplayAlertAsync("Mäng läbi", $"{currentPlayer} võitis!", "OK");
        //    return;
        //}

        //if (IsDraw())
        //{
        //    gameOver = true;
        //    DisplayAlertAsync("Mäng läbi", "Viik!", "OK");
        //    return;
        //}

        //if (currentPlayer == "X")
        //{
        //    currentPlayer = "O";
        //}
        //else
        //{
        //    currentPlayer = "X";
        //}
    }
    private void BotMove()
    {
        if (gameOver) return;

        // Собираем все свободные кнопки
        List<Button> freeButtons = new List<Button>();
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if (string.IsNullOrEmpty(buttons[row, col].Text))
                    freeButtons.Add(buttons[row, col]);
            }
        }

        if (freeButtons.Count == 0) return; // нет свободных клеток

        // Выбираем случайную клетку
        Random rnd = new Random();
        Button chosen = freeButtons[rnd.Next(freeButtons.Count)];

        // Ставим символ бота
        chosen.Text = "O";
        chosen.TextColor = Colors.Blue;

        // Проверяем победу
        if (CheckWinner())
        {
            gameOver = true;
            DisplayAlert("Mäng läbi", "O võitis!", "OK");
            return;
        }

        if (IsDraw())
        {
            gameOver = true;
            DisplayAlert("Mäng läbi", "Viik!", "OK");
            return;
        }

        // После хода бота ход возвращается к человеку
        currentPlayer = "X";
    }
    private void Highlight(Button b1, Button b2, Button b3)
    {
        b1.TextColor = Colors.Black;
        b2.TextColor = Colors.Black;
        b3.TextColor = Colors.Black;
    }
    private void SmartBotMove()
    {
        if (gameOver) return;

        // 1. Попробовать выиграть
        if (TryToWinOrBlock("O")) return;

        // 2. Заблокировать X
        if (TryToWinOrBlock("X")) return;

        // 3. Иначе случайный ход
        Random rnd = new Random();
        List<Button> free = new List<Button>();
        for (int r = 0; r < 3; r++)
            for (int c = 0; c < 3; c++)
                if (string.IsNullOrEmpty(buttons[r, c].Text))
                    free.Add(buttons[r, c]);

        if (free.Count == 0) return;

        Button chosen = free[rnd.Next(free.Count)];
        chosen.Text = "O";
        chosen.TextColor = Colors.Blue;

        if (CheckWinner()) EndGame("O");
        else currentPlayer = "X";
    }
    private bool TryLine(Button b1, Button b2, Button b3, string player)
    {
        Button[] line = new Button[] { b1, b2, b3 };
        int countPlayer = 0;
        Button empty = null;

        foreach (var b in line)
        {
            if (b.Text == player) countPlayer++;
            else if (string.IsNullOrEmpty(b.Text)) empty = b;
        }

        if (countPlayer == 2 && empty != null)
        {
            // Если player = O => бот выиграл
            // Если player = X => блокируем
            empty.Text = "O";
            empty.TextColor = Colors.Blue;

            if (CheckWinner()) EndGame("O");
            else currentPlayer = "X";

            return true;
        }

        return false;
    }
    private void EndGame(string winner)
    {
        gameOver = true;
        DisplayAlert("Mäng läbi", $"{winner} võitis!", "OK");
    }
    private bool TryToWinOrBlock(string player)
    {
        for (int i = 0; i < 3; i++)
        {
            // Проверка строк
            if (TryLine(buttons[i, 0], buttons[i, 1], buttons[i, 2], player)) return true;
            // Проверка колонок
            if (TryLine(buttons[0, i], buttons[1, i], buttons[2, i], player)) return true;
        }

        // Диагонали
        if (TryLine(buttons[0, 0], buttons[1, 1], buttons[2, 2], player)) return true;
        if (TryLine(buttons[0, 2], buttons[1, 1], buttons[2, 0], player)) return true;

        return false;
    }
    private bool CheckWinner()
    {
        for (int i = 0; i < 3; i++)
        {
            if (buttons[i, 0].Text == currentPlayer &&
                buttons[i, 1].Text == currentPlayer &&
                buttons[i, 2].Text == currentPlayer)
            {
                Highlight(buttons[i, 0], buttons[i, 1], buttons[i, 2]);
                return true;
            }

            if (buttons[0, i].Text == currentPlayer &&
                buttons[1, i].Text == currentPlayer &&
                buttons[2, i].Text == currentPlayer)
            {
                Highlight(buttons[0, i], buttons[1, i], buttons[2, i]);
                return true;
            }
        }
        if (buttons[0, 0].Text == currentPlayer &&
            buttons[1, 1].Text == currentPlayer &&
            buttons[2, 2].Text == currentPlayer)
        {
            Highlight(buttons[0, 0], buttons[1, 1], buttons[2, 2]);
            return true;
        }

        if (buttons[0, 2].Text == currentPlayer &&
            buttons[1, 1].Text == currentPlayer &&
            buttons[2, 0].Text == currentPlayer)
        {
            Highlight(buttons[0, 2], buttons[1, 1], buttons[2, 0]);
            return true;
        }
        return false;
    }

    private bool IsDraw()
    {
        foreach (var button in buttons)
        {
            if (string.IsNullOrEmpty(button.Text))
                return false;
        }
        return true;
    }

    private void NewGame(object sender, EventArgs e)
    {
        
        foreach (var button in buttons)
        {
            button.Text = "";
            if (button.Text == "X") button.TextColor = Colors.Red;
            else button.TextColor = Colors.Green;
        }
        currentPlayer = "X";
        gameOver = false;
    }

    private void WhoStarts(object sender, EventArgs e)
    {
        Random rnd = new Random();
        currentPlayer = rnd.Next(2) == 0 ? "X" : "O";
        DisplayAlertAsync("Info", $"{currentPlayer} alustab!", "OK");
    }
    private void Liikumine(object? sender, EventArgs e)
    {
        Button nupp = sender as Button;
        if (nupp.ZIndex == 0)
        {
            Navigation.PushAsync(new PickerImagePage());
        }
        else if (nupp.ZIndex == 1)
        {
            Navigation.PopToRootAsync();
        }
        else if (nupp.ZIndex == 2)
        {
            Navigation.PushAsync(new Trips_Traps_Trull());
        }
    }
}