using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL_CG_TDM
{
    public class Vertice 
    {
        double X, Y, Z;
        public Vertice(double xp, double yp, double zp)
        {
            X = xp;
            Y = yp;
            Z = zp;
        }
        public double GetX() { return X; }
        public double GetY() { return Y; }
        public double GetZ() { return Z; }
    }
}