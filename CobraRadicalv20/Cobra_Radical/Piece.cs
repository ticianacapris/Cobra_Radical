using System.Drawing;

namespace Cobra_Radical
{
    public class Piece
    {
        public static int SIDE = 20;
        private readonly Brush m_Color;

        public int X { get; set; }
        public int Y { get; set; }

        public Piece(int a, int b, Brush color)
        {
            X = a;
            Y = b;
            m_Color = color;
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(m_Color, new Rectangle(X * SIDE, Y * SIDE, SIDE, SIDE));
        }
    }
}
