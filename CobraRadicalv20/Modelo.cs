using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;

namespace SharpGL_CG_TDM
{
    public class Modelo
    {
        float Escala;
        float escalaX, escalaY, escalaZ;
        List<Vertice> LV;
        List<Vertice> lVT;
        List<Face> LF;
        // List<Triangulo> LT;
        List<Textura> LT;
        double Xmin, Xmax, Ymin, Ymax, Zmin, Zmax;
        double TX, TY, TZ;

        System.Timers.Timer rotatetimer;

        int Rx, Ry, Rz;

        public Modelo()
        {
            Escala = 1f;
            escalaX = escalaY = escalaZ = 1f;
            LV = new List<Vertice>();
            LF = new List<Face>();
            LT = new List<Textura>();
            TX = TY = TZ = 0;

            Rx = Ry = Rz = 0;

        }

        public double getX() { return this.TX; }
        public double getY() { return this.TY; }
        public void setY(double y_) { this.TY = y_; }
        public double getZ() { return this.TZ; }

        public void rotate(OpenGL gl, int Rx_, int Ry_, int Rz_)
        {
            gl.Rotate(Rx_, Ry_, Rz_);
        }

        public void setScale(OpenGL gl, float novaEscalaX, float novaEscalaY, float novaEscalaZ)
        {
            escalaX += novaEscalaX;
            escalaY += novaEscalaY;
            escalaZ += novaEscalaZ;


        }

        public void Translacao(double _tx, double _ty, double _tz)
        {
            TX += _tx;
            TY += _ty;
            TZ += _tz;

            Xmin += _tx;
            Xmax += _tx;
            Ymin += _ty;
            Ymax += _ty;
            Zmin += _tz;
            Zmax += _tz;
        }
        //-------------------------------
        public bool LerFicheiro(string ficheiro)
        {
            Console.WriteLine("lerFicheiro", ficheiro);

            // Formato OBJ
            // codigo de leitura do ficheiro!!!
            // Abrir o Ficheiro
            // # comentário - esquecer a linha
            // v  x  y   z
            try
            {
                using (StreamReader sr = new StreamReader(ficheiro))
                {
                    string linha;
                    bool Primeira_Passagem = true;


                    // Lê linha por linha até o final do arquivo
                    while ((linha = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(linha);

                        string[] Dados = linha.Split(' ');
                        if (Dados.Length != 0)
                        {
                            /*
                            for (int i = 0; i< Dados.Length;i++)
                            {
                                Console.WriteLine("Dados[" + i + "]=" + Dados[i]);
                            }
                            Console.WriteLine();
                            */
                            switch (Dados[0])
                            {
                                case "v":
                                    //  Console.WriteLine("Tenho um vertice");

                                    double X = Convert.ToDouble(Dados[1], System.Globalization.CultureInfo.InvariantCulture);
                                    double Y = Convert.ToDouble(Dados[2], System.Globalization.CultureInfo.InvariantCulture);
                                    double Z = Convert.ToDouble(Dados[3], System.Globalization.CultureInfo.InvariantCulture);


                                    Vertice V = new Vertice(X, Y, Z);
                                    LV.Add(V);
                                    if (Primeira_Passagem)
                                    {
                                        Xmin = Xmax = X;
                                        Ymin = Ymax = Y;
                                        Zmin = Zmax = Z;
                                        //*******
                                        Primeira_Passagem = false;
                                    }
                                    else
                                    {
                                        Xmin = Math.Min(Xmin, X);
                                        Xmax = Math.Max(Xmax, X);
                                        Ymin = Math.Min(Ymin, Y);
                                        Ymax = Math.Max(Ymax, Y);
                                        Zmin = Math.Min(Zmin, Z);
                                        Zmax = Math.Max(Zmax, Z);
                                        // Y, Z
                                    }
                                    break;
                                case "f":
                                    Face F = new Face();
                                    for (int i = 1; i < 4; i++)
                                    {
                                        string[] linha_face = Dados[i].Split('/');
                                        int V1 = Convert.ToInt32(linha_face[0]);
                                        int ft1 = Convert.ToInt32(linha_face[1]);
                                        F.Add(LV[V1 - 1]);
                                        F.AddTexture(LT[ft1 - 1]);
                                    }
                                    LF.Add(F);
                                    break;
                                case "vt":
                                    Textura textura = new Textura(Convert.ToDouble(Dados[1], System.Globalization.CultureInfo.InvariantCulture), Convert.ToDouble(Dados[2], System.Globalization.CultureInfo.InvariantCulture));
                                    LT.Add(textura);
                                    break;
                                case "#":
                                    //Console.WriteLine("Nao faças nada comentário");
                                    break;
                                default:
                                    break;
                            }

                        }
                        //Console.WriteLine(teste);
                    }
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problema: " + ex.Message);
                Console.WriteLine("Problema: " + ex);
            }
            //Console.WriteLine("XMin = " + Xmin);
            //Console.WriteLine("XMax = " + Xmax);

            // Fechar o Ficheiro         
            return true;
        }
        //-------------------------------
        public bool Colide(Modelo M2)
        {
            if (M2.Xmin > Xmax) return false;
            if (M2.Xmax < Xmin) return false;
            if (M2.Ymin > Ymax) return false;
            if (M2.Ymax < Ymin) return false;
            if (M2.Zmin > Zmax) return false;
            if (M2.Zmax < Zmin) return false;

            return true;
        }
        //-------------------------------
        public void Mostrar()
        {
            Debug.WriteLine("Mostrar do Modelo");
            Debug.WriteLine("NV = " + LV.Count);
            Debug.WriteLine("NF = " + LF.Count);
        }
        //-------------------------------
        public void DesenharArestas(OpenGL gl)
        {


            // Debug.WriteLine("TPC : DesenharArestas");
        }
        //-------------------------------
        public void DesenharFaces(OpenGL gl)
        {
            foreach (Face F in LF)
            {
                F.Desenhar(gl);
            }
        }

        public void Desenhar(OpenGL gl)
        {
          
            gl.Scale(escalaX, escalaY, escalaZ);

            DesenharEnvolvente(gl);
            gl.PushMatrix();
            gl.Translate(TX, TY, TZ);

            DesenharFaces(gl);
            DesenharArestas(gl);
            gl.PopMatrix();

            gl.PushMatrix();
        }
        
        public void DesenharEnvolvente(OpenGL gl)
        {


            //Variaçoes X
            Uteis.Linha(gl, Xmin, Ymin, Zmin, Xmax, Ymin, Zmin);
            Uteis.Linha(gl, Xmax, Ymax, Zmin, Xmin, Ymax, Zmin);
            Uteis.Linha(gl, Xmin, Ymin, Zmax, Xmax, Ymin, Zmax);
            Uteis.Linha(gl, Xmin, Ymax, Zmax, Xmax, Ymax, Zmax);

            //Variaçoes Y
            Uteis.Linha(gl, Xmax, Ymin, Zmax, Xmax, Ymax, Zmax);
            Uteis.Linha(gl, Xmin, Ymax, Zmin, Xmin, Ymin, Zmin);
            Uteis.Linha(gl, Xmin, Ymax, Zmax, Xmin, Ymin, Zmax);
            Uteis.Linha(gl, Xmax, Ymax, Zmin, Xmax, Ymin, Zmin);

            //Variaçoes Z
            Uteis.Linha(gl, Xmin, Ymin, Zmin, Xmin, Ymin, Zmax);
            Uteis.Linha(gl, Xmin, Ymax, Zmax, Xmin, Ymax, Zmin);
            Uteis.Linha(gl, Xmax, Ymin, Zmin, Xmax, Ymin, Zmax);
            Uteis.Linha(gl, Xmax, Ymax, Zmin, Xmax, Ymax, Zmax);













        }
    }
}
