using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace SharpGL_CG_TDM
{
    public class Triangulo
    {
        Vertice P1, P2, P3;
        public Triangulo(Vertice Ap, Vertice Bp, Vertice Cp)
        {
            P1 = Ap;
            P2 = Bp;
            P3 = Cp;
        }
        public void Desenhar(OpenGL Ecran_gl)
        {
            Uteis.Linha(Ecran_gl, P1, P2);
            Uteis.Linha(Ecran_gl, P2, P3);
            Uteis.Linha(Ecran_gl, P3, P1);
        }
        public Vertice GetP1() { return P1; }
        public Vertice GetP2() { return P2; }
        public Vertice GetP3() { return P3; }
    }
}
