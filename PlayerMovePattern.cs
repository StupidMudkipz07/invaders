class PlayerMovePattern : IVectorReturner
{
    // Keyboard.Key up = Keyboard.Key.W;
    // Keyboard.Key down = Keyboard.Key.S;
    // Keyboard.Key left = Keyboard.Key.A;
    // Keyboard.Key right = Keyboard.Key.D;

    Keyboard.Key up;
    Keyboard.Key down;
    Keyboard.Key left;
    Keyboard.Key right;


    public PlayerMovePattern(Keyboard.Key up, Keyboard.Key down, Keyboard.Key left, Keyboard.Key right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    public Vector2f MovePattern(Vector2f direction)
    {
        direction = new Vector2f(0, 0);
        if (KeyboardHandler.IsKeyDown(left))
            direction.X -= 1;
        if (KeyboardHandler.IsKeyDown(right))
            direction.X += 1;
        if (KeyboardHandler.IsKeyDown(down))
            direction.Y += 1;
        if (KeyboardHandler.IsKeyDown(up))
            direction.Y -= 1;

        return direction;

    }
}