global using SFML.Audio;
global using SFML.Graphics;
global using SFML.Window;
global using SFML.System;
global using System.Text.Json.Serialization;
global using System.Text.Json;

using System.IO;

static class Program
{
    static string windowName = "Sloppar spelet";

    static ControlLayout player1Controls = new(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space);
    static ControlLayout player2Controls = new(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl);

    static string playerDataFilePath = "data/playerData.json";
    static string seraphimDataFilePath = "data/seraphimData.json";
    static string viruteDataFilePath = "data/virtueData.json";
    static string archangelDataFilePath = "data/archangelData.json";

    public static Vector2u WindowSize = new(800, 1080);

    static public List<GameObject> gameObjects = new();

    static List<GameObject> buffer = new();

    public static void AddToGameList(GameObject gameObject)
    {
        buffer.Add(gameObject);
    }

    public static void AddToGameList(GameObject[] gameObject)
    {
        buffer.AddRange(gameObject);
    }

    static void AddBuffer()
    {
        gameObjects.AddRange(buffer);
        buffer.Clear();
    }

    static void SpawnAngel(string dataFilepath)
    {
        SpaceShipData stats = new SpaceShipData(dataFilepath);
        SpaceShip.SpawnEnemy(stats, new(WindowSize.X / 2, -stats.size.X / 2));
    }

    static void SpawnPlayer1()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath), new(WindowSize.X / 2, 700), player1Controls);
    }

    static void SpawnPlayer2()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath) { sprite = "köttigbild" }, new(WindowSize.X / 2, 700), player2Controls);
    }

    static void SpawnObjects()
    {
        SpawnPlayer1();
        SpawnPlayer2();
        SpawnAngel(seraphimDataFilePath);
    }

    static void UpdateGameObjects(float deltaTime)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            //först uppdatera alla värden
            gameObjects[i].Update(deltaTime);
            // Console.Write(gameObjects);
        }

        //   Console.Write("\n");

        // tar bort alla objekt efter man har itererat så inte listan förstörs
        gameObjects.RemoveAll(obj => obj.remove == true);
    }

    static void DrawGameObjects(RenderWindow window)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            //kollar så att objektet finns i skärmen innan den ritar det
            if (gameObjects[i].IsInView()) gameObjects[i].Draw(window);
        }
    }

    public static void Main()
    {
        SpriteDrawer.InitilizeAllSprites();

        using (RenderWindow window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), windowName))
        {
            Clock clock = new Clock();
            using Font fpsFont = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf"));
            using Text fpsText = new("FPS: 0", fpsFont)
            {
                CharacterSize = 20,
                FillColor = Color.White,
                Position = new(10, 10)
            };
            float fpsElapsed = 0;
            int fpsFrames = 0;

            window.SetFramerateLimit(60);
            window.Closed += (o, e) => window.Close();            //input debug
            window.KeyPressed += (sender, e) => Console.WriteLine("Key pressed " + e.Code);

            SpawnObjects();

            while (window.IsOpen)
            {

                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                window.Clear(new(10, 10, 10));

                fpsElapsed += deltaTime;
                fpsFrames++;
                if (fpsElapsed >= 0.5f)
                {
                    fpsText.DisplayedString = $"FPS: {fpsFrames / fpsElapsed:0}";
                    fpsElapsed = 0;
                    fpsFrames = 0;
                }

                UpdateGameObjects(deltaTime);
                DrawGameObjects(window);
                window.Draw(fpsText);

                AddBuffer();

                window.Display();
            }
        }
    }

    //Returns the normalized vector
    public static Vector2f Normalize(Vector2f vector)
    {
        float length = MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);

        if (length == 0f)
            return new Vector2f(0f, 0f);

        return new Vector2f(vector.X / length, vector.Y / length);
    }

    //Returns the length of the vector
    public static float Length(Vector2f vector) => MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
    
    //returns the inverse of the vector
    public static Vector2f Inverse(Vector2f vector) => new(vector.X * -1, vector.Y * -1);

    public static float VectorToAngle(Vector2f rotation)
    {
        return MathF.Atan2(rotation.Y, rotation.X) * 180f / MathF.PI;
    }

    public static Vector2f AngleToVector(float angle)
    {
        //matte slop
        float radians = angle * MathF.PI / 180f;
        Vector2f rotatedVector = new(MathF.Cos(radians), MathF.Sin(radians));
        return rotatedVector;
    }

    public static class GlobalTags
    { //maybe have an algoritm that makes these strings
        public const string Player = "1";

        public const string Enemy = "2";

        public const string Neutral = "sloppigt";

        public const string Asteroid = "4";
    }
}
