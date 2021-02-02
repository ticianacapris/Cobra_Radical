using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace SharpGL_CG_TDM
{
    public class Face
    {
        //List<int> LVerticesFace; // {  1 2 3 }
        List<Vertice> LVerticesFace; //{V1, V2, V3}
        List<Textura> LTextura;

        public Face()
        {
            LVerticesFace = new List<Vertice>();
            LTextura = new List<Textura>();
        }
        public void Add(Vertice vv) { LVerticesFace.Add(vv); }

        public void AddTexture(Textura tt) { LTextura.Add(tt); }
        public int GetNVertices() { return LVerticesFace.Count; }
        public void Desenhar(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < LVerticesFace.Count; i++)
            {
                gl.TexCoord(LTextura[i].GetX(), LTextura[i].GetY());
                gl.Vertex(LVerticesFace[i].GetX(), LVerticesFace[i].GetY(), LVerticesFace[i].GetZ());
            }

            gl.End();
        }
    }
}
