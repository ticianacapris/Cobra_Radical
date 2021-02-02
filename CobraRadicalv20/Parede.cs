using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Assets;

namespace SharpGLWinformsApplication1
{
    public class Parede : Objecto
    {
        Vertice v1, v2, v3, v4;
        Texture TexturaActiva;
        public Parede(SharpGLForm fp,Vertice p1, Vertice p2, Vertice p3, Vertice p4)
            : base(fp)
        {
            v1 = p1;
            v2 = p2;
            v3 = p3;
            v4 = p4;
            TexturaActiva = null;
        }
        public void SetTextura(Texture Textura)
        {
            TexturaActiva = Textura;
        }
        override public void Desenhar(OpenGL Ecran_gl)
        {
            if (TexturaActiva != null)
            {
                Ecran_gl.Enable(OpenGL.GL_TEXTURE_2D);
                TexturaActiva.Bind(Ecran_gl);
            }
            Ecran_gl.Begin(OpenGL.GL_QUADS);
                Ecran_gl.TexCoord(v1.GetTx(), v1.GetTy());
                Ecran_gl.Vertex(v1.GetX(), v1.GetY(), v1.GetZ());
                Ecran_gl.TexCoord(v2.GetTx(), v2.GetTy());
                Ecran_gl.Vertex(v2.GetX(), v2.GetY(), v2.GetZ());
                Ecran_gl.TexCoord(v3.GetTx(), v3.GetTy());
                Ecran_gl.Vertex(v3.GetX(), v3.GetY(), v3.GetZ());
                Ecran_gl.TexCoord(v4.GetTx(), v4.GetTy());
                Ecran_gl.Vertex(v4.GetX(), v4.GetY(), v4.GetZ());
            Ecran_gl.End();
            if (TexturaActiva != null)
                Ecran_gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
    }
}
