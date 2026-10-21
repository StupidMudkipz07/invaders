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

    static Vector2u WindowSize = new(1100, 950);

    static Color clearColor = new(20, 20, 20);

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

                foreach (Object o in objectHandler.GetAllGameObjects())
                {
                    System.Console.Write(o + ", ");
                }
                System.Console.WriteLine();

                objectHandler.UpdateGameObjects(deltaTime);
                objectHandler.DrawGameObjects(window);
                objectHandler.AddBuffer();

                window.Display();
            }
        }
    }
}
