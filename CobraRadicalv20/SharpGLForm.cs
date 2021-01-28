using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SharpGL;

namespace SharpGL_CG_TDM
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        Modelo Jogador;
        double TX, TY, TZ;
        double Escala, Incremento_Escala;
        int Sentido;
        List<Modelo> LModelos;
        bool Em_Movimento;
        public SharpGLForm()
        {
            InitializeComponent();
            TX = 0;
            TY = 0;
            TZ = 0;
            Escala = 1.0;
            Incremento_Escala = 0.1;
            Sentido = 2;
            Em_Movimento = true;
            LModelos = new List<Modelo>();
            Console.WriteLine("Passei em SharpGLForm");



        }

        void DesenharFundo(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    gl.Color(1f, 1f, 1f);

                    gl.Vertex((float)j, 0, i);
                    gl.Vertex((float)j, 0, (float)(i + 1));
                    gl.Vertex((float)(j + 1), 0, (float)(i));

                    gl.Vertex((float)j, 0, (float)(i + 1));
                    gl.Vertex((float)(j + 1), 0, (float)(i));
                    gl.Vertex((float)(j + 1), 0, (float)(i + 1));
                }
            }
            gl.End();
        }

        private void Desenhar_Exemplo_Base(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 5.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();
        }

        private void DesenharEixos(OpenGL gl, float TAM = 5.0f)
        {
            // gl.Sphere()
            gl.LineWidth(2.0f);
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f); // origin of the line
            gl.Vertex(TAM, 0.0f, 0.0f); // ending point of the line
            gl.End();

            //eixo y
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f); // origin of the line
            gl.Vertex(0.0f, TAM, 0.0f); // ending point of the line
            gl.End();

            //eixo z
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f); // origin of the line
            gl.Vertex(0.0f, 0.0f, TAM); // ending point of the line
            gl.End();

            gl.LineWidth(1.0f);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //DesenharEixos(gl);

            //  Rotate around the Y axis.
            gl.Translate(TX, TY, TZ);
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
            gl.Scale(Escala, Escala, Escala);

            DesenharEixos(gl, 1.5f);
            //  Draw a coloured pyramid.
            //Desenhar_Exemplo_Base(gl);

            DesenharFundo(gl);

            foreach (Modelo M in LModelos)
                M.Desenhar(gl);

            //  Nudge the rotation.
            if (Em_Movimento)
            {
                rotation += Sentido;
                Escala += Incremento_Escala;
                if ((Escala > 8) || (Escala < 0.5f))
                    Incremento_Escala = -Incremento_Escala;
            }
            //if (Escala < 0.5f)
            //    Incremento_Escala = -Incremento_Escala;

           /// Console.WriteLine("Passei em openGLControl_OpenGLDraw rotation:"+ rotation+ " Escala: "+ Escala);
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.
            //  Get the OpenGL object.
            Console.WriteLine("Passei em openGLControl_OpenGLInitialized");

            OpenGL gl = openGLControl.OpenGL;
            //  Set the clear color.
            gl.ClearColor(0.7f, 0.7f, 0.7f, 1);


        }

        /// <summary>
        /// Handles the Resized event of the openGLControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.
            //  Get the OpenGL object.
            Console.WriteLine("Passei em openGLControl_Resized");

            OpenGL gl = openGLControl.OpenGL;
            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();
            //  Create a perspective transformation.
            gl.Perspective(60.0f, (double)Width / (double)Height, 0.01, 100.0);
            //  Use the 'look at' helper function to position and aim the camera.
            gl.LookAt(-1, 1, -1,
                2, 0, 2, 
                -4, 6, -4);
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        // Method associated to initial draw test 

        /// <summary>        /// The current rotation.
        /// </summary>
        private float rotation = 0.0f;

        private void Btn_TX_Mais_Click(object sender, EventArgs e)
        {
            TX += 1.0;
        }

        private void Btn_TX_Menos_Click(object sender, EventArgs e)
        {
            TX -= 1.0;
            if (Jogador != null)
                Jogador.Translacao(0.1, 0.0, 0.0);
        }

        private void Btn_Aumentar_Click(object sender, EventArgs e)
        {
            Escala += 0.2;
        }

        private void Btn_Diminuir_Click(object sender, EventArgs e)
        {
            Escala -= 0.2;
        }

        private void Btn_Experiencias_Click(object sender, EventArgs e)
        {
            /*
            int X = -10;
            Console.WriteLine("X = " + X);
            X = -X;
            Console.WriteLine("X = " + X);
            */
            String STR = "1.23";
            /*
            STR = STR.Replace('.', ',');
            double Y = Convert.ToDouble(STR);
            */
            double Y = Convert.ToDouble(STR, System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine("Y = " + Y);


        }

        private void Btn_Inverter_Escala_Click(object sender, EventArgs e)
        {
            Incremento_Escala = -Incremento_Escala;

        }

        private void Btn_Parar_Click(object sender, EventArgs e)
        {
            // Incremento_Escala = 0;
            // Sentido = 0;
            Em_Movimento = !Em_Movimento;
            if (Em_Movimento)
                Btn_Parar.Text = "PARAR";
            else
                Btn_Parar.Text = "ANDAR";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Modelo Jogador = new Modelo();
            foreach(Modelo M in LModelos)
                if (Jogador.Colide(M))
                {

                }
        }

        private void Btn_Inverter_Click(object sender, EventArgs e)
        {

            Console.WriteLine("invertersentifo");
            Sentido = -Sentido;


            //Cobra.setScale(0.1, 0.1, 0.1, gl);

        }

        private void Btn_Sair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Btn_LerModelo_Click(object sender, EventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;

            Modelo Cobra = new Modelo();

           // Cobra.LerFicheiro("C:\\Users\\pedro\\Documents\\GitHub\\Cobra_Radical\\CobraRadicalv20\\loadModelos\\cobraStartModel.obj");
            
            Cobra.LerFicheiro("..\\..\\loadModelos\\cobraStartModel.obj");

            LModelos.Add(Cobra);



          //  C:\Users\pedro\Documents\GitHub\Cobra_Radical\CobraRadicalv20\loadModelos\cobraStartModel.obj
          //  C:\Users\pedro\Documents\GitHub\Cobra_Radical\CobraRadicalv20\bin\Debug\SharpGL_CG_TDM.exe


            //Mod.LerFicheiro("..\\..\\..\\Modelos_OBJ\\ola.obj");
            //Modelo X = new Modelo();
            //X.LerFicheiro("..\\..\\..\\Modelos_OBJ\\Vaca.obj");
            //X.LerFicheiro("..\\..\\..\\Modelos_OBJ\\ola.obj");
            //X.Mostrar();
            //LModelos.Add(X);
            //X.Mostrar();
            //Jogador = new Modelo();
            //Jogador.LerFicheiro("..\\..\\..\\Modelos_OBJ\\ola.obj");
            //LModelos.Add(Jogador);
            /*
            Modelo A = new Modelo();
            Modelo B = new Modelo();
            if (A.Colide(B))
            {

            }
            */
        }
    }
}
