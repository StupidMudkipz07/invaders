interface IGameObjectHandleable
{
    public void AddToGameHandlerList(GameObject gameObject);

    public void UpdateGameObjects(float deltaTime);

    public void DrawGameObjects(RenderWindow window);

    public void AddBuffer();

    public List<GameObject> GetAllGameObjects();
}