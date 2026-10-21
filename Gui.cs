class Gui : GameObject, IDrawable
{
    SpaceShip player;

    RectangleShape playerHealthBar;

    public Gui(SpaceShip playership)
    {
        player = playership;
    }

    public override void Update(float deltaTime)
    {

    }

    public void DrawBåt(RenderWindow widnow)
    {

    }
}