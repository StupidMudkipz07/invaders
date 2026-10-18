abstract class MoveableObject : Object
{
    public Vector2f position;
    public Vector2f size;
    public Vector2f direction;
    public Vector2f facingDirection;

    protected Sprite sprite;

    //should be called in constructor
    protected void InitializeVariables(string spriteName, Vector2f position, Vector2f size, string tag)
    {
        sprite = SpriteDrawer.GetStaticSprite(spriteName);
        this.position = position;
        this.size = size;
        this.tag = tag;
        facingDirection = new(0, 0);
    }



    protected void OutOfBoundsCheck(Vector2f velocity)
    {
        float borderMultiplier = 1.3f;

        // tar bort objekt som är utanför skärman och kommer att åka iväg
        if (position.X > (handler.PlayArea.X * borderMultiplier) + (size.X / 2) && velocity.X > 0)
        {
            remove = true;
        }
        if (position.X < -(size.X / 2) && velocity.X < 0)
        {
            remove = true;
        }
        if (position.Y < -(size.Y / 2) && velocity.Y < 0)
        {
            remove = true;
        }
        if (position.Y > (handler.PlayArea.Y * borderMultiplier) + (size.Y / 2) && velocity.Y > 0)
        {
            remove = true;
        }
    }

    protected void Move(float deltaTime, Vector2f velocity)
    {
        Vector2f finalVelocityCalc = velocity * deltaTime;

        position += finalVelocityCalc;

        if (VectorMath.Length(finalVelocityCalc) > 0) OutOfBoundsCheck(velocity);
    }

    public bool IsInView()
    {
        if (position.X < -size.X ||
        position.X > handler.PlayArea.X + size.X ||
        position.Y < -size.Y ||
        position.X > handler.PlayArea.Y + size.Y)
        {
            return false;
        }
        else return true;
    }

    void DebugDraw(RenderWindow window)
    {

        if (KeyboardHandler.IsKeyDown(Keyboard.Key.LShift))
        {
            RectangleShape shape = GetHitbox();
            shape.FillColor = new Color(30, 235, 30, 180);

            CircleShape point = new CircleShape(2f)
            {
                Position = new Vector2f(position.X, position.Y),
                FillColor = Color.Red
            };

            window.Draw(shape);
            window.Draw(point);
        }
    }

    protected void DrawObject(Sprite sprite, Vector2f size, RenderWindow window)
    {
        SpriteDrawer.DrawSprite(sprite, position, size, window, VectorMath.VectorToAngle(facingDirection));
        DebugDraw(window);
    }

    protected void DrawObject(Sprite sprite, Vector2f size, RenderWindow window, float rotation)
    {
        SpriteDrawer.DrawSprite(sprite, position, size, window, rotation);

        DebugDraw(window);
    }

    //här kommer kollision
    RectangleShape GetHitbox()
    {
        RectangleShape hitbox = new RectangleShape()
        {
            Size = size,
            Origin = new Vector2f(size.X / 2, size.Y / 2),
            Position = position,
        };

        return hitbox;
    }

    bool CollisionCheckBetweenGameObjects(MoveableObject other)
    {
        if (GetHitbox().GetGlobalBounds().Intersects(other.GetHitbox().GetGlobalBounds()))
            return true;
        else return false;
    }

    MoveableObject? IterateHitboxList(List<MoveableObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (ReferenceEquals(list[i], this))
                continue;

            if (CollisionCheckBetweenGameObjects(list[i]))
                return list[i];
        }
        //om man inte träffade något så returnar den null
        return null;
    }

    // returnar objektet som kollideras med
    public MoveableObject? GetCollidingObject()
    {
        List<MoveableObject> collisionList = handler.GetAllGameObjects().OfType<MoveableObject>().ToList();
        return IterateHitboxList(collisionList);
    }

    //returnar objektet som kollideras med och matcher taggen
    public MoveableObject? GetCollidingObject(string targetTag)
    {
        List<MoveableObject> CollisionList = handler.GetAllGameObjects().OfType<MoveableObject>().Where(gameObject => gameObject.tag == targetTag).ToList();

        return IterateHitboxList(CollisionList);
    }

    //returnar objektet som kollideras med och matcher någon avtaggarna
    public Object? GetCollidingObject(string[] targetTags)
    {
        List<MoveableObject> CollisionList = new();

        foreach (MoveableObject slop in handler.GetAllGameObjects().OfType<MoveableObject>())
        {
            if (targetTags.Contains(slop.tag))
                CollisionList.Add(slop);
        }
        return IterateHitboxList(CollisionList);
    }

    public bool CollidingWithTag(string targetTag)
    {
        MoveableObject? gameObject = GetCollidingObject(targetTag);
        if (gameObject != null)
        {
            string tag = gameObject.tag;
            if (tag == targetTag)
            {
                return true;
            }
        }
        return false;
    }
    //slut på kollision
}