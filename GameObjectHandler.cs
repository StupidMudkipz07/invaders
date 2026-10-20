using System.Runtime.InteropServices;

class GameObjectHandler : IGameObjectHandleable
{
    List<GameObject> gameObjects = new();

    List<GameObject> buffer = new();

    Vector2u playArea;

    public Vector2u PlayArea
    {
        get { return playArea; }
        private set { playArea = value; }
    }

    public GameObjectHandler(Vector2u playArea)
    {
        PlayArea = playArea;
        GameObject.SetObjectHandler(this);
        new MenuHandler();
        new Button(new(200, 200), new(300, 200), () => new GameSpawner(PlayArea, 1))
        {
            color = Color.Black,
            text = new Text()
            {
                DisplayedString = "niklas",
                Position = new(200, 200),
                
            }
        };
        new Button(new(700, 200), new(300, 200), () => System.Console.WriteLine("clicked"));
    }

    public void AddBuffer()
    {
        gameObjects.AddRange(buffer);
        buffer.Clear();
    }

    public List<GameObject> GetAllGameObjects() => gameObjects;

    public void AddToGameHandlerList(GameObject gameObject)
    {
        buffer.Add(gameObject);
    }

    public void UpdateGameObjects(float deltaTime)
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

    public void DrawGameObjects(RenderWindow window)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].Draw(window);
        }
    }
}