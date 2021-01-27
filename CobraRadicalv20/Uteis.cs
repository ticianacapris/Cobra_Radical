using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace SharpGL_CG_TDM
{
    public class Uteis
    {
        static public void Linha(OpenGL gl, float x0, float y0, float z0, float x1, float y1, float z1)
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(x0, y0, z0);
            gl.Vertex(x1, y1, z1);
            gl.End();
        }
        static public void Linha(OpenGL gl, double x0, double y0, double z0, double x1, double y1, double z1)
        {
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(x0, y0, z0);
            gl.Vertex(x1, y1, z1);
            gl.End();
        }
        static public void Linha(OpenGL gl, Vertice A, Vertice B)
        {
            gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(A.GetX(), A.GetY(), A.GetZ());
                gl.Vertex(B.GetX(), B.GetY(), B.GetZ());
            gl.End();
        }
        static public void Sphere(OpenGL gl, float x0, float y0, float z0, float raio)
        {
            gl.PointSize(raio);
            gl.Begin(OpenGL.GL_POINTS);
            gl.Vertex(x0, y0, z0);
            gl.End();
        }
    }
}
