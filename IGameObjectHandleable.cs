interface IGameObjectHandleable
{
    public void AddToGameHandlerList(Object gameObject);

    public void UpdateGameObjects(float deltaTime);

    public void DrawGameObjects(RenderWindow window);

    public Vector2u PlayArea { get; }
    
    public List<Object> GetAllGameObjects();

    public void SpawnObjects();
}