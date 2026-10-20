class Button : GameObject, IClickable
{
    Action OnClikcAction;
    public Color color;
    public Text text;

    public Button(Vector2f position, Vector2f size, Action onClick)
    {
        color = Color.Red;
        this.position = position;
        this.size = size;
        OnClikcAction = onClick;
        text = new();
    }

    public RectangleShape GetHitbox() => new() { Position = position, Size = size, FillColor = color };

    public void OnHover()
    {
        color.G = 255;
        //System.Console.WriteLine("hovering");
    }

    public void OnClick()
    {
        OnClikcAction();
    }

    public override void Update(float deltaTime)
    {
        color.B += (byte)(1 + deltaTime * 30);
    }

    public override void Draw(RenderWindow widnow)
    {
        widnow.Draw(GetHitbox());
        widnow.Draw(text);
    }
}