class HighScoreManager : GameObject, IDrawable
{
    public static Dictionary<string, int> LoadHighScores()
    {
        string json = File.ReadAllText(HighScoreFilePath);
        return JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new();
    }

    public static string GetHighScoresAsString()
    {
        Dictionary<string, int> highScores = LoadHighScores();

        //sorterar listan efter storlek
        List<KeyValuePair<string, int>> sortedHighScores = highScores.OrderByDescending(score => score.Value).ToList();

        string Text = "";

        for (int i = 0; i < sortedHighScores.Count; i++)
        {
            Text += $"{i + 1} Name: {sortedHighScores[i].Key}     Score: {sortedHighScores[i].Value}\n";
        }

        return Text;
    }


    const string HighScoreFilePath = "data/highscore.json";

    Text highScoreText;
    float highScore;
    bool appliedEvent = false;
    bool doneWriting = false;
    Text instructions;

    string playerName;

    public HighScoreManager(float highScore)
    {
        this.highScore = highScore;
        highScoreText = new()
        {
            Position = new(340, 50),
            CharacterSize = 30,
            FillColor = Color.Red,
            Font = new Font("fonts/saturno.ttf"),
        };

        instructions = new()
        {
            DisplayedString = "Write your name and press space to save your highscore",
            Position = new(10, 120),
            CharacterSize = 25,
            FillColor = Color.Red,
            Font = new Font("fonts/future.ttf"),
        };
    }

    void SaveHighScore()
    {
        Dictionary<string, int> highScores = LoadHighScores();
        highScores[playerName] = (int)MathF.Round(highScore);

        File.WriteAllText(HighScoreFilePath, JsonSerializer.Serialize(highScores, new JsonSerializerOptions { WriteIndented = true }));
        System.Console.WriteLine(GetHighScoresAsString());
    }

    public void Draw(RenderWindow window)
    {   // så den bara körs en gång
        if (!appliedEvent)
        {
            appliedEvent = true;
            window.TextEntered += (sender, e) =>
            {
                if (doneWriting == false)
                {
                    string text = e.Unicode;
                    //om man trycker på backspace
                    if (text == "\b" && playerName.Length > 0)
                    {
                        playerName = playerName.Remove(playerName.Length - 1); //tar bort sista karaktären i listan
                    }
                    //om man trycker på space
                    else if (text == " ")
                    {
                        SaveHighScore();
                        doneWriting = true;
                    }
                    else if (!string.IsNullOrEmpty(text) && !char.IsControl(text, 0))
                    {
                        playerName += text;
                    }
                }
            };
        }


        window.Draw(highScoreText);

        window.Draw(instructions);
    }
    public override void Update(float deltaTime)
    {
        highScoreText.DisplayedString = playerName;

        if (doneWriting)
        {
            instructions.DisplayedString = "Saved HighScore!!! ;>";
        }
    }


}