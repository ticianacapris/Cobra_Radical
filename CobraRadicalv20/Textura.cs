using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpGL_CG_TDM
{
    public class Textura
    {
        double X, Y;
        public Textura(double xp, double yp)
        {
            X = xp;
            Y = yp;
        }
        public double GetX() { return X; }
        public double GetY() { return Y; }



    }
}