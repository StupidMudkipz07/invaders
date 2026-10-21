class Button : GameObject, IClickable,IDrawable
{
    public Color setColor;
    public Text text;

    Action OnClikcAction;
    Color color;
    bool isHovered;

    public Button(Vector2f position, Vector2f size, Action onClick, Color color)
    {
        setColor = color;
        this.position = position;
        this.size = size;
        OnClikcAction = onClick;
        text = new();
        this.color = setColor;
    }

    public RectangleShape GetHitbox() => new() { Position = position, Size = size, FillColor = color };

    public void OnHover()
    {
        isHovered = true;
    }

    public void OnClick()
    {
        OnClikcAction();
    }

    public override void Update(float deltaTime)
    {
        if (!isHovered)
        {
            color = setColor;
        }
        else
        {
            color.R = (byte)(setColor.R - 20);
            color.B = (byte)(setColor.B - 20);
            color.G = (byte)(setColor.G - 20);
        }
        isHovered = false;
    }

    public override void Draw(RenderWindow widnow)
    {
        widnow.Draw(GetHitbox());
        widnow.Draw(text);
    }
    
    public  void DrawBåt(RenderWindow widnow)
    {
        widnow.Draw(GetHitbox());
        widnow.Draw(text);
    }
}