class Projectile : GameObject
{
    float moveSpeed = 800;
    float damage = 1;
    string targetTag;

    void CheckForTarget()
    {
        GameObject? gameObject = GetCollidingObject(targetTag);

        if (gameObject != null)
        {
            if (gameObject is IDamageable)
            {
                IDamageable g = gameObject as IDamageable;
                g.OnTakeDamage(damage);
                remove = true;
            }
        }
    }

    public Projectile(Vector2f position, Vector2f Direction, string targetTag)
    {
        direction = Direction;
        InitializeVariables("projectile", position, new(50, 50), Program.GlobalTags.Neutral);
        this.targetTag = targetTag;
        facingDirection = direction;
    }

    public override void Update(float deltaTime)
    {
        CheckForTarget();

        // viktigt att normalisera vector
        Move(deltaTime, Program.Normalize(direction) * moveSpeed);
    }

    public override void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window, Program.VectorToAngle(direction)+90);
    }
}