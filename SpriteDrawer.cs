
class SpriteDrawer
{
    static string spriteDirectory = "sprites";

    //all sprites should be stored in here
    static Dictionary<string, Sprite> AllSprites = new(StringComparer.OrdinalIgnoreCase);

    //get all sprites from the directory and store them here
    public static void InitilizeAllSprites()
    {
        AllSprites.Clear();

        foreach (string filePath in Directory.EnumerateFiles(spriteDirectory, "*.png"))
        {
            string name = Path.GetFileNameWithoutExtension(filePath);
            AllSprites[name] = new Sprite(new Texture(filePath));
        }
    }

    //gets a given sprite from the all sprites dictionary
    public static Sprite GetStaticSprite(string spriteName)
    {
        if (AllSprites.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        throw new KeyNotFoundException($"Hittade inte {spriteName} inuti sloppet: {spriteDirectory}.");
    }

    static public void DrawSprite(Sprite sprite, Vector2f position, Vector2f size, RenderWindow window)
    {
        sprite.Origin = new(sprite.Texture.Size.X / 2f, sprite.Texture.Size.Y / 2f);
        sprite.Position = position;
        sprite.Scale = new(size.X / sprite.Texture.Size.X, size.Y / sprite.Texture.Size.Y);
        window.Draw(sprite);
    }

    static public void DrawSprite(Sprite sprite, Vector2f position, Vector2f size, RenderWindow window, float angle)
    {
        sprite.Origin = new(sprite.Texture.Size.X / 2f, sprite.Texture.Size.Y / 2f);
        sprite.Position = position;
        sprite.Scale = new(size.X / sprite.Texture.Size.X, size.Y / sprite.Texture.Size.Y);
        sprite.Rotation = angle;

        window.Draw(sprite);
    }


    /*
        //here is all the sprites used by an object stored
        Dictionary<string, Sprite> sprites = new(StringComparer.OrdinalIgnoreCase);

        public Sprite GetSprite(string spriteName)
        {
            if (sprites.TryGetValue(spriteName, out Sprite sprite))
            {
                return sprite;
            }

            return GetSprite(spriteName);
        }

        public void InitializeSprites(string[] spriteNames)
        {
            for (int i = 0; i < spriteNames.Length; i++)
            {
                string spriteName = spriteNames[i];

                if (!sprites.ContainsKey(spriteName))
                {
                    sprites[spriteName] = GetStaticSprite(spriteName);
                }
            }
        }

        public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window)
        {
            sprite.Position = position;
            sprite.Scale = new Vector2f(size.X / sprite.Texture.Size.X, size.Y / sprite.Texture.Size.Y);
            window.Draw(sprite);
        }

        //this one is for rotation
        public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, Vector2f rotationVector, RenderWindow window, IntRect region)
        {
            Sprite sprite1 = DivideSprite(position, size, sprite, region);
            // nu gibbar vi lite matte 3
            float angle = MathF.Atan2(rotationVector.Y, rotationVector.X) * 180f / MathF.PI;

            sprite1.Position = position + size / 2f;
            sprite1.Rotation = angle;

            window.Draw(sprite1);
        }


        Sprite DivideSprite(Vector2f position, Vector2f size, Sprite sprite, IntRect region)
        {
            Sprite spriteToDraw = new Sprite(sprite);
            spriteToDraw.TextureRect = region;
            spriteToDraw.Origin = new Vector2f(region.Width / 2f, region.Height / 2f);
            spriteToDraw.Position = position + new Vector2f(size.X / 2f, size.Y / 2f);
            spriteToDraw.Scale = new Vector2f(size.X / region.Width, size.Y / region.Height);

            return spriteToDraw;
        }

        public void DrawSprite(Vector2f position, Vector2f size, Sprite sprite, RenderWindow window, IntRect region)
        {
            Sprite sprite1 = DivideSprite(position, size, sprite, region);

            window.Draw(sprite1);
        }*/
}