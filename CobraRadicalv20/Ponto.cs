using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;

namespace SharpGL_CG_TDM
{
    public class Ponto
    {
        float X, Y, Z;
        public Ponto(float xp, float yp, float zp)
        {
            X = xp;
            Y = yp;
            Z = zp;
        }
        public float GetX() { return X; }
        public float GetY() { return Y; }
        public float GetZ() { return Z; }
    }
}