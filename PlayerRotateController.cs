class PlayerRotateController : IMoveController
{
    ControlLayout controlLayout;
    float angle = 0;
    float turningSpeed = 2;

    public PlayerRotateController(ControlLayout controlLayout)
    {
        this.controlLayout = controlLayout;
    }

    public Vector2f GetMoveDirection()
    {
        Vector2f direction = new Vector2f(0, 0);

        if (KeyboardHandler.IsKeyDown(controlLayout.up))
            direction = GetFacingDirection();
        if (KeyboardHandler.IsKeyDown(controlLayout.down))
            direction = Program.Inverse(GetFacingDirection());

        return direction;
    }

    public Vector2f GetFacingDirection()
    {
        if (KeyboardHandler.IsKeyDown(controlLayout.left))
            angle += turningSpeed;
        if (KeyboardHandler.IsKeyDown(controlLayout.right))
            angle -= turningSpeed;

        return Program.AngleToVector(angle);
    }
}