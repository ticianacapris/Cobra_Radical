using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Assets;


namespace SharpGLWinformsApplication1
{
    public class Cubo : Objecto
    {
        Ponto []PC;
        float lado;
        float TX, TY, TZ;
        float RX, RY, RZ;
        float incrx, incry, incrz;
        Texture TexturaActiva;
        public Cubo(SharpGLForm fp, float l, float dx, float dy, float dz,
                             float rotx, float roty, float rotz)
            : base(fp)
        {
            lado = l;
            TX = dx; TY = dy; TZ = dz;
            incrx = rotx; incry = roty; incrz = rotz;
            PC = new Ponto[8];
            PC[0] = new Ponto(fp, 0.0f, 0.0f, 0.0f);
            PC[1] = new Ponto(fp, lado, 0.0f, 0.0f);
            PC[2] = new Ponto(fp, lado, lado, 0.0f);
            PC[3] = new Ponto(fp, 0, lado, 0.0f);

            PC[4] = new Ponto(fp, 0.0f, 0.0f, lado);
            PC[5] = new Ponto(fp, lado, 0.0f, lado);
            PC[6] = new Ponto(fp, lado, lado, lado);
            PC[7] = new Ponto(fp, 0, lado, lado);
            TexturaActiva = new Texture();
            TexturaActiva.Create(fp.GetGLForm(), "..\\..\\Texturas\\Crate.bmp");
        }
        override public void Desenhar(OpenGL Ecran_gl)
        {
            Ecran_gl.Enable(OpenGL.GL_LIGHTING);
            Ecran_gl.LineWidth(2.0f);
            Ecran_gl.PushMatrix();
            Ecran_gl.Translate(TX, TY, TZ); 
                Ecran_gl.Rotate(RX, RY, RZ);
 //               Ecran_gl.Color(0.5f, 0.2f, 1.0f);
                Ecran_gl.Enable(OpenGL.GL_TEXTURE_2D);
                TexturaActiva.Bind(Ecran_gl);
                Ecran_gl.Begin(OpenGL.GL_QUADS);
                    Ecran_gl.TexCoord(0.0f, 0.0f);
                    Ecran_gl.Vertex(PC[0].GetX(), PC[0].GetY(), PC[0].GetZ());
                    Ecran_gl.TexCoord(1.0f, 0.0f);
                    Ecran_gl.Vertex(PC[1].GetX(), PC[1].GetY(), PC[1].GetZ());
                    Ecran_gl.TexCoord(1.0f, 1.0f);
                    Ecran_gl.Vertex(PC[2].GetX(), PC[2].GetY(), PC[2].GetZ());
                    Ecran_gl.TexCoord(0.0f, 1.0f);
                    Ecran_gl.Vertex(PC[3].GetX(), PC[3].GetY(), PC[3].GetZ());
                Ecran_gl.End();
                Ecran_gl.Disable(OpenGL.GL_TEXTURE_2D);

                Ecran_gl.Color(1.0f, 0.0f, 1.0f);
                SharpGLForm.Linha(Ecran_gl, PC[0], PC[1]); 
                SharpGLForm.Linha(Ecran_gl, PC[1], PC[2]);
                SharpGLForm.Linha(Ecran_gl, PC[2], PC[3]);
                SharpGLForm.Linha(Ecran_gl, PC[3], PC[0]);

                SharpGLForm.Linha(Ecran_gl, PC[4], PC[5]);
                SharpGLForm.Linha(Ecran_gl, PC[5], PC[6]);
                SharpGLForm.Linha(Ecran_gl, PC[6], PC[7]);
                SharpGLForm.Linha(Ecran_gl, PC[7], PC[4]);

                SharpGLForm.Linha(Ecran_gl, PC[0], PC[4]);
                SharpGLForm.Linha(Ecran_gl, PC[1], PC[5]);
                SharpGLForm.Linha(Ecran_gl, PC[2], PC[6]);
                SharpGLForm.Linha(Ecran_gl, PC[3], PC[7]);
            Ecran_gl.PopMatrix();
            RX += incrx;
            RY += incry;
            RZ += incrz;
            Ecran_gl.Disable(OpenGL.GL_LIGHTING);
        }
    }
}
