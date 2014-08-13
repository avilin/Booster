using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.Util
{
    public class Camera2D
    {
        public Matrix Transform { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 ScreenCenter { get; set; }
        public Viewport Viewport { get; set; }
        public float Scale { get; set; }
        public float Rotation { get; set; }

        public void Initialize(Viewport viewport)
        {
            Viewport = viewport;
            Position = Vector2.Zero;
            ScreenCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
            Scale = 1f;
            Rotation = 0f;
        }

        public void Update(Vector2 position, int width, int height)
        {
            Position = position;

            float x = MathHelper.Clamp(Position.X, ScreenCenter.X, width - ScreenCenter.X);
            float y = MathHelper.Clamp(Position.Y, ScreenCenter.Y, height - ScreenCenter.Y);

            Position = new Vector2(x, y);

            Transform = Matrix.Identity *
                Matrix.CreateTranslation(-Position.X, -Position.Y, 0) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(ScreenCenter.X, ScreenCenter.Y, 0) *
                Matrix.CreateScale(Scale, Scale, Scale);
        }

        public bool isInView(Rectangle entity)
        {
            Rectangle rect = new Rectangle((int)Position.X - Viewport.Width / 2, (int)Position.Y - Viewport.Height / 2  , Viewport.Width, Viewport.Height);
            if(entity.Intersects(rect))
            {
                return true;
            }
            return false;
        }
    }
}