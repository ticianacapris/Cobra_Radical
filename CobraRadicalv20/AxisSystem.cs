using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.Windows.Forms;

namespace SharpGLWinformsApplication1
{
    class AxisSystem : Objecto
    {
        float lenght;
        Texture TexturaXOZ; 
        Texture TexturaXOY; 
        Texture TexturaZOY;

        public AxisSystem(SharpGLForm fp, float dim)
            : base(fp)
        {
            lenght = dim;
            TexturaXOZ = new Texture();
            TexturaXOY = new Texture();
            TexturaZOY = new Texture();
            TexturaXOZ.Create(fp.GetGLForm(), "..\\..\\Texturas\\Fundo1.jpg");
            TexturaXOY.Create(fp.GetGLForm(), "..\\..\\Texturas\\Lateral.jpg");
            TexturaZOY.Create(fp.GetGLForm(), "..\\..\\Texturas\\Lateral.jpg");
            MessageBox.Show("Exisos!");
        }

        public void Draw(OpenGL Ecran_gl)
        {
            Ecran_gl.LineWidth(2.0f);
            Ecran_gl.Color(1.0f, 0.0f, 0.0f);
            SharpGLForm.Linha(Ecran_gl, 0.0f, 0.0f, 0.0f, lenght, 0.0f, 0.0f);
            Ecran_gl.Color(0.0f, 1.0f, 0.0f);
            SharpGLForm.Linha(Ecran_gl, 0.0f, 0.0f, 0.0f, 0.0f, lenght, 0.0f);
            Ecran_gl.Color(0.0f, 0.0f, 1.0f);
            SharpGLForm.Linha(Ecran_gl, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, lenght);

            Ecran_gl.LineWidth(0.5f);
            Ecran_gl.Color(1.0f, 1.0f, 1.0f);
            // Desenhar a Base
            float Inc = lenght / 5.0f;
            float dy = 0.01f;

            for (float X = -lenght; X <= lenght; X += Inc)
			{
                SharpGLForm.Linha(Ecran_gl, X, dy, -lenght, X, dy, lenght);
			};	
            
			for (float Z = -lenght; Z <= lenght; Z += Inc)
			{
                SharpGLForm.Linha(Ecran_gl, -lenght, dy, Z, lenght, dy, Z);
			};
            Ecran_gl.Enable(OpenGL.GL_TEXTURE_2D);
            TexturaXOZ.Bind(Ecran_gl);
            Ecran_gl.Begin(OpenGL.GL_QUADS);  // Plano XOZ
                Ecran_gl.TexCoord(0.0f, 0.0f);
                Ecran_gl.Vertex(-lenght, 0, -lenght);
                Ecran_gl.TexCoord(1.0f, 0.0f);
                Ecran_gl.Vertex(-lenght, 0, lenght);
                Ecran_gl.TexCoord(1.0f, 1.0f);
                Ecran_gl.Vertex(lenght, 0, lenght);
                Ecran_gl.TexCoord(0.0f, 1.0f);
                Ecran_gl.Vertex(lenght, 0, -lenght);
            Ecran_gl.End();
            TexturaXOY.Bind(Ecran_gl);
            Ecran_gl.Begin(OpenGL.GL_QUADS);  // Plano XOY
                Ecran_gl.TexCoord(0.0f, 0.0f);
                Ecran_gl.Vertex(lenght, 0, -lenght);
                Ecran_gl.TexCoord(1.0f, 0.0f);
                Ecran_gl.Vertex(lenght, 2 * lenght, -lenght);
                Ecran_gl.TexCoord(1.0f, 1.0f);
                Ecran_gl.Vertex(-lenght, 2 * lenght, -lenght);
                Ecran_gl.TexCoord(0.0f, 1.0f);
                Ecran_gl.Vertex(-lenght, 0, -lenght);
            Ecran_gl.End(); 
            TexturaZOY.Bind(Ecran_gl);
            Ecran_gl.Begin(OpenGL.GL_QUADS);  // Plano ZOY
                Ecran_gl.TexCoord(0.0f, 0.0f);
                Ecran_gl.Vertex(-lenght, 0, -lenght);
                Ecran_gl.TexCoord(1.0f, 0.0f);
                Ecran_gl.Vertex(-lenght, 2 * lenght, -lenght);
                Ecran_gl.TexCoord(1.0f, 1.0f);
                Ecran_gl.Vertex(-lenght, 2 * lenght, lenght);
                Ecran_gl.TexCoord(0.0f, 1.0f);
                Ecran_gl.Vertex(-lenght, 0, lenght);
            Ecran_gl.End();


            Ecran_gl.Disable(OpenGL.GL_TEXTURE_2D);
        }
    }
}
