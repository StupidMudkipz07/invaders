abstract class GameObject
{
    public Vector2f position;
    public Vector2f size;
    protected Sprite? sprite;
    public bool remove = false;
    public string Tag = string.Empty;

    public GameObject()
    {
        Program.AddToGameList(this);
    }

    //should be called in constructor
    protected void InitializeVariables(string spriteName, Vector2f position, Vector2f size, string tag)
    {
        sprite = SpriteDrawer.GetStaticSprite(spriteName);
        this.position = position;
        this.size = size;
        this.Tag = tag;
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

    bool CollisionCheckBetweenGameObjects(GameObject other)
    {
        if (GetHitbox().GetGlobalBounds().Intersects(other.GetHitbox().GetGlobalBounds()))
            return true;
        else return false;
    }

    GameObject? IterateHitboxList(List<GameObject> list)
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
    public GameObject? GetCollidingObject()
    {
        return IterateHitboxList(Program.gameObjects);
    }

    //returnar objektet som kollideras med och matcher taggen
    public GameObject? GetCollidingObject(string targetTag)
    {
        // goofy ass c# standard    
        //List<GameObject> CollisionList = [.. Program.gameObjects.Where(gameObject => gameObject.Tag == targetTag)];
        List<GameObject> CollisionList = Program.gameObjects.Where(gameObject => gameObject.Tag == targetTag).ToList();

        return IterateHitboxList(CollisionList);
    }

    //returnar objektet som kollideras med och matcher någon avtaggarna
    public GameObject? GetCollidingObject(string[] targetTags)
    {
        List<GameObject> CollisionList = new();

        foreach (GameObject slop in Program.gameObjects)
        {
            if (targetTags.Contains(slop.Tag))
                CollisionList.Add(slop);
        }

        return IterateHitboxList(CollisionList);
    }

    public bool CollidingWithTag(string targetTag)
    {
        GameObject? gameObject = GetCollidingObject(targetTag);
        if (gameObject != null)
        {
            string tag = gameObject.Tag;
            if (tag == targetTag)
            {
                return true;
            }
        }
        return false;
    }

    
    //slut på kollision

    protected void OutOfBoundsCheck(Vector2f velocity)
    {
        float borderMultiplier = 1.2f;

        // tar bort objekt som är utanför skärman och kommer att åka iväg
        if (position.X > (Program.WindowSize.X * borderMultiplier) + (size.X / 2) && velocity.X > 0)
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
        if (position.Y > (Program.WindowSize.Y * borderMultiplier) + (size.Y / 2) && velocity.Y > 0)
        {
            remove = true;
        }
    }

    protected void Move(float deltaTime, Vector2f velocity)
    {
        Vector2f finalVelocityCalc = velocity * deltaTime;

        position += finalVelocityCalc;

        if (Program.Length(finalVelocityCalc) > 0) OutOfBoundsCheck(velocity);
    }

    public bool IsInView()
    {
        if (position.X < -size.X ||
        position.X > Program.WindowSize.X + size.X ||
        position.Y < -size.Y ||
        position.X > Program.WindowSize.Y + size.Y)
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
        SpriteDrawer.DrawSprite(sprite, position, size, window);
        DebugDraw(window);
    }

    protected void DrawObject(Sprite sprite, Vector2f size, RenderWindow window, float rotation)
    {
        SpriteDrawer.DrawSprite(sprite, position, size, window, rotation);

        DebugDraw(window);
    }

    public abstract void Update(float deltaTime);

    public abstract void Draw(RenderWindow widnow);
}