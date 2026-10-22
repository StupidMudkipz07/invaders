class Explosion : GameObject, IDrawable
{
    CircleShape boom;
    float explosionMultipler = 120;

    public Explosion(Vector2f position, Vector2f size)
    {
        this.position = position;
        this.size = size;
        boom = new()
        {
            Radius = size.X * 0.1f,
            Position = position,
            FillColor = new(255, 255, 255, 255)
        };
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(boom);
    }

    public override void Update(float deltaTime)
    {
        boom.Radius += deltaTime * explosionMultipler;

        byte alpha = (byte)MathF.Max(0, boom.FillColor.A - 255 * deltaTime);
        boom.FillColor = new Color(boom.FillColor.R, boom.FillColor.G, boom.FillColor.B, alpha);

        if (alpha == 0)
        {
            remove = true;
        }
    }
}