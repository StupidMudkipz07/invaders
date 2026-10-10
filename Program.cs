global using SFML.Audio;
global using SFML.Graphics;
global using SFML.Window;
global using SFML.System;

static class Program
{
    static string windowName = "Sloppar spelet";

    public static Vector2u WindowSize = new(700, 1000);

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

    static void SpawnObjects()
    {
        // InitializeVariables("kallo", new(350, 700), new(130, 130), Program.GlobalTags.Player);
        //                                                                            lustig if sats👇👇👇
        new SpaceShip("kallo", new(350, 800), new(140, 100), new Vector2f(0, 0), GlobalTags.Player, 500, 3, true){OpposingTag = GlobalTags.Enemy};
        new SpaceShip("angel2", new(350, 100), new(150, 150), new Vector2f(Random.Shared.Next(2) == 0 ? -1 : 1, 1), GlobalTags.Enemy, 300, 3, false)
        {
            OpposingTag = GlobalTags.Player
        };
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
            window.SetFramerateLimit(60);
            window.Closed += (o, e) => window.Close();            //input debug
            window.KeyPressed += (sender, e) => Console.WriteLine("Key pressed " + e.Code);

            SpawnObjects();

            while (window.IsOpen)
            {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                window.Clear(new(10, 10, 10));

                UpdateGameObjects(deltaTime);
                DrawGameObjects(window);

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
    public static float Length(Vector2f vector)
    {
        return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
    }

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
        public const string Player = "sifhoigeohäepj";

        public const string Enemy = "fgafgzdfvaetra";

        public const string Neutral = "23ih423kj4";

        public const string Asteroid = "231231231223";
    }
}

