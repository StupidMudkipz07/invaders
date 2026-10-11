struct ControlLayout
{
    public Keyboard.Key up;
    public Keyboard.Key down;
    public Keyboard.Key left;
    public Keyboard.Key right;
    public Keyboard.Key shoot;

    public ControlLayout(Keyboard.Key up, Keyboard.Key down, Keyboard.Key left, Keyboard.Key right, Keyboard.Key shoot)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.shoot = shoot;
    }
}