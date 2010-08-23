namespace RedBadger.Xpf.Graphics
{
    using System.Windows;
    using System.Windows.Media;

    using Vector = RedBadger.Xpf.Presentation.Vector;

    public interface ISpriteBatch
    {
        void Draw(ITexture2D texture2D, Rect rect, Color color);

        void DrawString(ISpriteFont spriteFont, string text, Vector position, Color color);
    }
}