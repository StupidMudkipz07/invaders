global using SFML.Audio;
global using SFML.Graphics;
global using SFML.Window;
global using SFML.System;
global using System.Text.Json.Serialization;
global using System.Text.Json;

static class Program
{
    static IGameObjectHandleable objectHandler;

    static string windowName = "Sloppar spelet";

    public static Vector2u WindowSize = new(800, 1080);

    public static Color clearColor = new(20, 20, 20);

    public static void Main()
    {
        SpriteDrawer.InitilizeAllSprites();

        using (RenderWindow window = new RenderWindow(new VideoMode(WindowSize.X, WindowSize.Y), windowName))
        {
            Clock clock = new Clock();

            window.SetFramerateLimit(60);
            window.Closed += (o, e) => window.Close();
            window.KeyPressed += (sender, e) => Console.WriteLine("Key pressed " + e.Code); //input debug

           objectHandler = new GameObjectHandler(WindowSize);
           Console.WriteLine(WindowSize); 
           
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float deltaTime = clock.Restart().AsSeconds();
                window.Clear(clearColor);

                objectHandler.UpdateGameObjects(deltaTime);
                objectHandler.DrawGameObjects(window);

                objectHandler.AddBuffer();

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
