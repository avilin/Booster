using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Booster.Util
{
    public class Camera2D
    {
        public Matrix Transform { get; set; }
        private Vector2 position;
        private Vector2 screenCenter;
        private Viewport viewport;
        private float scale;
        private float rotation;

        public void Initialize(Viewport viewport)
        {
            this.viewport = viewport;
            this.position = Vector2.Zero;
            screenCenter = new Vector2(viewport.Width / 2, viewport.Height / 2);
            scale = 1f;
            rotation = 0f;
        }

        public void Update(Vector2 position, int width, int height)
        {
            this.position = position;

            float x = MathHelper.Clamp(this.position.X, screenCenter.X, width - screenCenter.X);
            float y = MathHelper.Clamp(this.position.Y, screenCenter.Y, height - screenCenter.Y);

            this.position = new Vector2(x, y);

            Transform = Matrix.Identity *
                Matrix.CreateTranslation(-this.position.X, -this.position.Y, 0) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0) *
                Matrix.CreateScale(scale, scale, scale);
        }

        public bool IsInView(Rectangle entity)
        {
            Rectangle rect = new Rectangle((int)this.position.X - viewport.Width / 2, (int)this.position.Y - viewport.Height / 2, viewport.Width, viewport.Height);
            if(entity.Intersects(rect))
            {
                return true;
            }
            return false;
        }
    }
}