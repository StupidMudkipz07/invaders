class MenuHandler : GameObject, IDrawable
{
    Vector2i mousePosition;

    public MenuHandler()
    {
    
    }

    bool MouseIsInsideButton(Button button)
    {
        return button.GetHitbox().GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);
    }

    void CheckButtons()
    {
        foreach (Button button in handler.GetAllGameObjects().OfType<Button>())
        {
            if (MouseIsInsideButton(button))
            {
                button.OnHover();
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    button.OnClick();
                }
            }

        }
    }


    public override void Update(float deltaTime)
    {
        CheckButtons();
    }

    public override void Draw(RenderWindow widnow)
    {
        mousePosition = Mouse.GetPosition(widnow);
    }

    public void DrawBåt(RenderWindow widnow)
    {
        mousePosition = Mouse.GetPosition(widnow);
    }
}