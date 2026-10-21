class GuiBar : GameObject, IDrawable
{
    SpaceShip player;

    RectangleShape bar;
    RectangleShape behindBar;
    IFloatReturnable floatReturnable;
    float sizeMultiplier = 100;

    public GuiBar(IFloatReturnable floatReturnable)
    {
        this.floatReturnable = floatReturnable;
        bar = new RectangleShape()
        {
            Size = new(floatReturnable.GetFloat()*sizeMultiplier, 40),
            Position = new(20, 20),
            FillColor = Color.Red,
        };
        behindBar = new RectangleShape()
        {
            Size = new(floatReturnable.GetFloat()*sizeMultiplier, 40),
            Position = new(20, 20),
            FillColor = Color.Black,
            OutlineThickness = 3,
            OutlineColor = Color.Blue,
        };
    }

    public override void Update(float deltaTime)
    {
        bar.Size = new(floatReturnable.GetFloat() *sizeMultiplier, 40);
    }

    public void DrawBåt(RenderWindow widnow)
    {
        widnow.Draw(behindBar);
        widnow.Draw(bar);
    }
}