abstract class GameObject
{
    protected static IGameObjectHandleable handler;
    //this function should be called in a objectHandler class 
    public static void SetObjectHandler(IGameObjectHandleable objecthandler)
    {
        handler = objecthandler;
    }

    public bool remove = false;
    public string tag = "";
    public Vector2f position;
    public Vector2f size;

    public GameObject()
    {
        handler.AddToGameHandlerList(this);
    }

    public abstract void Update(float deltaTime);

    //public abstract void Draw(RenderWindow widnow);

    protected class GlobalTags
    { 
        public const string Player = "1";

        public const string Enemy = "2";

        public const string Neutral = "sloppigt";

        public const string Asteroid = "4";
    }
}