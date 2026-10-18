abstract class Object
{
    protected static IGameObjectHandleable handler;

    //this function should be called in a objectHandler class 
    public static void SetObjectHandler(IGameObjectHandleable objecthandler)
    {
        handler = objecthandler;
    }

    public bool remove = false;
    public string tag = "";
    
    public Object()
    {
        handler.AddToGameHandlerList(this);
    }

    public abstract void Update(float deltaTime);

    public abstract void Draw(RenderWindow widnow);
}