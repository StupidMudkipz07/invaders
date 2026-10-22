class GuiBar : GameObject, IDrawable
{
    RectangleShape bar;
    RectangleShape behindBar;
    IFloatReturnable value;

    Color barColor = Color.Red;
    Color behindColor = Color.Black;
    Color outlineColor = Color.Cyan;

    public GuiBar(IFloatReturnable floatReturnable, Vector2f position, Vector2f size)
    {
        value = floatReturnable;
        this.position = position;
        this.size = size;

        MakeBehindBar();
    }

    void MakeBar()
    {
        bar = new RectangleShape()
        {
            Size = new(value.GetFloat() * size.X, size.Y),
            Position = position,
            FillColor = barColor,
        };

    }
    void MakeBehindBar()
    {
        behindBar = new RectangleShape()
        {
            Size = new(value.GetFloat() * size.X, size.Y),
            Position = position,
            FillColor = behindColor,
            OutlineThickness = 3,
            OutlineColor = outlineColor,
        };
    }


    public override void Update(float deltaTime)
    {
        MakeBar();
    }

    public void Draw(RenderWindow widnow)
    {
        widnow.Draw(behindBar);
        widnow.Draw(bar);
    }
}