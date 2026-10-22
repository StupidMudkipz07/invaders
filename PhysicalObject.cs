abstract class
    PhysicalObject : GameObject
{
    public Vector2f direction;
    public Vector2f facingDirection;

    protected Sprite sprite;

    //should be called in constructor
    protected void InitializeVariables(string spriteName, Vector2f position, Vector2f size, string tag, bool canMoveOffScreen)
    {
        this.canMoveOffScreen = canMoveOffScreen;
        sprite = SpriteDrawer.GetStaticSprite(spriteName);
        this.position = position;
        this.size = size;
        this.tag = tag;
        facingDirection = new(0, 0);
    }

    bool canMoveOffScreen = true;
    
    protected void OutOfBoundsCheck(Vector2f velocity)
    {
        float borderMultiplier = 1.3f;
        if (canMoveOffScreen)
        {
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
        else
        {
            position = new(Math.Clamp(position.X, 0 + size.X / 2, handler.PlayArea.X - size.X / 2), Math.Clamp(position.Y, 0 + size.Y / 2, handler.PlayArea.Y - size.Y / 2));
        }
    }

    protected void Move(float deltaTime, Vector2f velocity)
    {
        Vector2f finalVelocityCalc = velocity * deltaTime;

        position += finalVelocityCalc;

        if (VectorMath.Length(finalVelocityCalc) > 0) OutOfBoundsCheck(velocity);
    }

    public void DebugDraw(RenderWindow window)
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

    bool CollisionCheckBetweenGameObjects(PhysicalObject other)
    {
        if (GetHitbox().GetGlobalBounds().Intersects(other.GetHitbox().GetGlobalBounds()))
            return true;
        else return false;
    }

    PhysicalObject? IterateHitboxList(List<PhysicalObject> list)
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
    public PhysicalObject? GetCollidingObject()
    {
        List<PhysicalObject> collisionList = handler.GetAllGameObjects().OfType<PhysicalObject>().ToList();
        return IterateHitboxList(collisionList);
    }

    //returnar objektet som kollideras med och matcher taggen
    public PhysicalObject? GetCollidingObject(string targetTag)
    {
        List<PhysicalObject> CollisionList = handler.GetAllGameObjects().OfType<PhysicalObject>().Where(gameObject => gameObject.tag == targetTag).ToList();

        return IterateHitboxList(CollisionList);
    }

    public bool CollidingWithTag(string targetTag)
    {
        PhysicalObject? gameObject = GetCollidingObject(targetTag);
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