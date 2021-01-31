using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace SharpGL_CG_TDM
{
    public class Modelo
    {
        float Escala;
        float escalaX, escalaY, escalaZ;
        List<Vertice> LV;
        List<Face> LF;
        List<Triangulo> LT;
        double Xmin, Xmax, Ymin, Ymax, Zmin, Zmax;
        double TX, TY, TZ;

        public Modelo()
        {
            Escala = 1f;
            escalaX = escalaY = escalaZ = 1f;
            LV = new List<Vertice>();
            LF = new List<Face>();
            LT = new List<Triangulo>();
            TX = TY = TZ = 0;
        }

        public double getX() { return this.TX; }
        public double getY() { return this.TY; }
        public double getZ() { return this.TZ; }


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

            //-----
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
                                    // Console.WriteLine("Tenho um FACE");
                                    // Pode/deve ser feita uma melhoria na leitura das faces!
                                    // Neste caso só está a ler triangulos
                                    Face F = new Face();
                                    int V1 = Convert.ToInt32(Dados[1]);
                                    int V2 = Convert.ToInt32(Dados[2]);
                                    int V3 = Convert.ToInt32(Dados[3]);
                                    F.Add(LV[V1 - 1]);
                                    F.Add(LV[V2 - 1]);
                                    F.Add(LV[V3 - 1]);
                                    LF.Add(F);
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
            if (M2.Ymax > Ymin) return false;
            if (M2.Ymax < Ymin) return false;
            if (M2.Zmax > Zmin) return false;
            if (M2.Zmax < Zmin) return false;


            //------ continuar
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
        public void DesenharArestas(OpenGL gl, float[] color_)
        {

            gl.Color(color_);

            // Debug.WriteLine("TPC : DesenharArestas");
        }
        //-------------------------------
        public void DesenharFaces(OpenGL gl, float[] color_)
        {
            gl.Color(color_);
            foreach (Face F in LF)
            {
                F.Desenhar(gl);
            }
        }
        //-------------------------------
        public void Desenhar(OpenGL gl, float[] color_ = null)
        {

            if (color_ == null)
            {
                color_ = new float[3] { 0.1f, 0.1f, 0.1f };
            }


            gl.Scale(escalaX, escalaY, escalaZ);

            DesenharEnvolvente(gl);
            gl.PushMatrix();
            gl.Translate(TX, TY, TZ);
            DesenharFaces(gl, color_);
            DesenharArestas(gl, color_);

            gl.PopMatrix();
        }
        //-------------------------------
        public void DesenharEnvolvente(OpenGL gl)
        {
            Uteis.Linha(gl, Xmin, Ymin, Zmin, Xmax, Ymin, Zmin);
            Uteis.Linha(gl, Xmax, Ymin, Zmin, Xmax, Ymax, Zmin);
            Uteis.Linha(gl, Xmax, Ymax, Zmin, Xmin, Ymax, Zmin);
            // Fazer as outras linhas da envolvente!
            // total 12
        }
    }
}
