class Projectile : PhysicalObject, IDrawable
{
    float moveSpeed;
    float damage;
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

    public Projectile(float moveSpeed, float damage, Vector2f position, Vector2f size, Vector2f Direction, string sprite, string targetTag)
    {
        direction = Direction;
        InitializeVariables(sprite, position, size, GlobalTags.Neutral, true);
        this.targetTag = targetTag;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        facingDirection = direction;
    }

    public override void Update(float deltaTime)
    {
        CheckForTarget();

        // viktigt att normalisera vector
        Move(deltaTime, VectorMath.Normalize(direction) * moveSpeed);
    }



    public void Draw(RenderWindow window)
    {
        DrawObject(sprite, size, window, VectorMath.VectorToAngle(direction) + 90);
    }
}