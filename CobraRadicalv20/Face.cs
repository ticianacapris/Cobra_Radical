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

        public Face()
        {
            LVerticesFace = new List<Vertice>();
        }
        public void Add(Vertice vv) { LVerticesFace.Add(vv); }
        public int GetNVertices() { return LVerticesFace.Count; }
        public void Desenhar(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            /*
            gl.Vertex(LVerticesFace[0].GetX(), LVerticesFace[0].GetY(), LVerticesFace[0].GetZ());
            gl.Vertex(LVerticesFace[1].GetX(), LVerticesFace[1].GetY(), LVerticesFace[1].GetZ());
            gl.Vertex(LVerticesFace[2].GetX(), LVerticesFace[2].GetY(), LVerticesFace[2].GetZ());
           */

            foreach(Vertice V in LVerticesFace)
                gl.Vertex(V.GetX(), V.GetY(), V.GetZ());

            gl.End();

        }
    }
}
