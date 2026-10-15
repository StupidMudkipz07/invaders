interface IGameObjectHandleable
{
    public void AddToGameHandlerList(GameObject gameObject);

    public void UpdateGameObjects(float deltaTime);

    public void DrawGameObjects(RenderWindow window);

    public void AddBuffer();

    public Vector2u PlayArea { get; }
    
    public List<GameObject> GetAllGameObjects();
    
    //slop
}