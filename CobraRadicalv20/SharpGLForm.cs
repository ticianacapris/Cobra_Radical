using SharpGL;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using System.Diagnostics;

namespace SharpGL_CG_TDM
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {
        Modelo Cobra;
        int cobraLength;
        List<Modelo> CobraFull;
        List<int> directionHistory;

        double TX, TY, TZ;
        double Escala, Incremento_Escala;
        int Sentido;
        List<Modelo> LModelos;
        List<Modelo> Obstaculos;
        List<Modelo> Comida;
        bool Em_Movimento;

        float[] foodColor;
        float[] obstColor;

        int direcaoCobra;

        int fieldSize;

        System.Timers.Timer timer;
        System.Timers.Timer extrasTimer;

        int[,] foodMatrix, obstaclesMatrix;

        Dictionary<string, List<Modelo>> Matriz;

        Random rndNumber;

        public SharpGLForm()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(keyPress);

            TX = 0;
            TY = 0;
            TZ = 0;
            Escala = 1.0;
            Incremento_Escala = 0.1;
            Sentido = 2;
            Em_Movimento = true;

            foodColor = new float[3] { 0.6f, 0.6f, 0.6f };
            obstColor = new float[3] { 0.1f, 0.1f, 0.1f };

            Cobra = new Modelo();
            cobraLength = 3;
            CobraFull = new List<Modelo>();
            directionHistory = new List<int>();

            direcaoCobra = 1; //1: X+  2: Y+  3: X-  4: Y-

            LModelos = new List<Modelo>();
            Comida = new List<Modelo>();
            Obstaculos = new List<Modelo>();

            Cobra.LerFicheiro("..\\..\\loadModelos\\cobraStartModel.obj");
            LModelos.Add(Cobra);

            Console.WriteLine("Passei em SharpGLForm");

            fieldSize = 40;

            foodMatrix = new int[fieldSize, fieldSize];
            obstaclesMatrix = new int[fieldSize, fieldSize];

            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timerMovement);
            timer.Interval = 200;
            timer.Enabled = true;

            extrasTimer = new System.Timers.Timer();
            extrasTimer.Elapsed += new ElapsedEventHandler(randomPosition);
            extrasTimer.Interval = 4000;
            extrasTimer.Enabled = true;
            extrasTimer.AutoReset = true;

            Matriz = new Dictionary<string, List<Modelo>>();
            Matriz["Comida"] = new List<Modelo>();
            Matriz["obstaculos"] = new List<Modelo>();

            rndNumber = new Random();
        }

        public void randomPosition(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("randomPosition");
            bool foodOcupado = true;
            bool obstOcupado = true;

            while (foodOcupado == true)
            {
                int newValueX = rndNumber.Next(1, fieldSize);
                int newValueY = rndNumber.Next(1, fieldSize);


                if (foodMatrix[newValueX, newValueY] != 1)
                {
                    Console.WriteLine("Mais comida");
                    Modelo comida = new Modelo();
                    comida.LerFicheiro("..\\..\\loadModelos\\cobraStartModel.obj");
                    //Obstaculos.Add(obstaculo);
                    Matriz["Comida"].Add(comida);
                    comida.Translacao(newValueX, 0, newValueY);
                    foodMatrix[newValueX, newValueY] = 1;
                    foodOcupado = false;
                }
            }

            while (obstOcupado == true)
            {
                int newValueX = rndNumber.Next(1, fieldSize);
                int newValueY = rndNumber.Next(1, fieldSize);


                if (obstaclesMatrix[newValueX, newValueY] != 1)
                {
                    Modelo obstaculo = new Modelo();
                    obstaculo.LerFicheiro("..\\..\\loadModelos\\cobraStartModel.obj");
                    //Comida.Add(comida);
                    Matriz["obstaculos"].Add(obstaculo);
                    obstaculo.Translacao(newValueX, 0, newValueY);
                    obstaclesMatrix[newValueX, newValueY] = 1;
                    obstOcupado = false;
                }
            }
        }

        public void timerMovement(object source, ElapsedEventArgs e)
        {
            switch (direcaoCobra)
            {
                case 2:
                    Cobra.Translacao(0, 0, 1);
                    directionHistory.Add(2);
                    break;
                case 3:
                    Cobra.Translacao(-1, 0, 0);
                    directionHistory.Add(3);
                    break;
                case 4:
                    Cobra.Translacao(0, 0, -1);
                    directionHistory.Add(4);
                    break;
                default:
                    directionHistory.Add(1);
                    Cobra.Translacao(1, 0, 0);
                    break;
            }
        }

        private void keyPress(object sender, KeyEventArgs e)
        {


            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    if (direcaoCobra != 2)
                    {
                        direcaoCobra = 4;
                    }
                    break;
                case Keys.Down:
                case Keys.S:
                    if (direcaoCobra != 1)
                    {
                        direcaoCobra = 3;
                    }
                    break;
                case Keys.Right:
                case Keys.D:
                    if (direcaoCobra != 4)
                    {
                        direcaoCobra = 2;
                    }
                    break;
                case Keys.Up:
                case Keys.W:
                    if (direcaoCobra != 3)
                    {
                        direcaoCobra = 1;
                    }
                    break;
                default:
                    Console.WriteLine(e.KeyCode);
                    break;
            }
        }

        private void button1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    e.IsInputKey = true;
                    break;
            }
        }


        void DesenharFundo(OpenGL gl)
        {
            gl.Begin(OpenGL.GL_TRIANGLES);

            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (j % 2 == 0 && i % 2 != 0)
                        gl.Color(0.6f, 0.6f, 0.6f);
                    else
                        gl.Color(0.8f, 0.8f, 0.8f);

                    gl.Vertex((float)(j - 1), 0, (i - 1));
                    gl.Vertex((float)(j - 1), 0, (float)(i));
                    gl.Vertex((float)(j), 0, (float)(i - 1));

                    gl.Vertex((float)(j - 1), 0, (float)(i));
                    gl.Vertex((float)(j), 0, (float)(i - 1));
                    gl.Vertex((float)(j), 0, (float)(i));
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

            //Camara
            if (Cobra != null)
            {
                gl.LookAt(Cobra.getX(), Cobra.getY(), Cobra.getZ(),
                    0, 0, 0,
                 0, 1, 0);
            }
            /* else
             {
                 gl.LookAt(-4, 4, -4,
                     2, 0, 2,
                     -4, 6, -4);
             //}*/


            //DesenharEixos(gl);

            //  Rotate around the Y axis.
            /*            gl.Translate(TX, TY, TZ);
                        gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);
                        gl.Scale(Escala, Escala, Escala);*/

            // DesenharEixos(gl, 1.5f);
            //  Draw a coloured pyramid.
            //Desenhar_Exemplo_Base(gl);


            DesenharFundo(gl);
            /*
                        float[] obstaclesColor = new float[3] { 0.5f, 0.5f, 0.5f };
                        float[] snakeColor = new float[3] { 0.9f, 0.9f, 0.9f };
                        float[] foodColor = new float[3] { 0.1f, 0.1f, 0.1f };*/

            //extrasTimer.Stop();


            for (int i = 0; i < cobraLength; i++)
            {

            }


            foreach (Modelo M in LModelos)
                M.Desenhar(gl);

            foreach (Modelo M in Matriz["Comida"].ToArray())
                M.Desenhar(gl, foodColor);

            foreach (Modelo M in Matriz["obstaculos"].ToArray())
                M.Desenhar(gl, obstColor);

            // extrasTimer.Start();


            //  Nudge the rotation.
            /* if (Em_Movimento)
             {
                 rotation += Sentido;
                 Escala += Incremento_Escala;
                 if ((Escala > 8) || (Escala < 0.5f))
                     Incremento_Escala = -Incremento_Escala;
             }*/
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

            if (Cobra != null)
            {
                Console.WriteLine("coordenadas: " + Cobra.getX());
                gl.LookAt(Cobra.getX(), Cobra.getY(), Cobra.getZ(), 2, 0, 2, -4, 6, -4);
            }
            else
            {
                gl.LookAt(-4, 4, -4,
                    2, 0, 2,
                    -4, 6, -4);
            }
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        // Method associated to initial draw test 

        private float rotation = 0.0f;

        private void Btn_TX_Mais_Click(object sender, EventArgs e)
        {
            //  TX += 1.0;

            Cobra.Translacao(1, 0, 0);
        }

        private void Btn_TX_Menos_Click(object sender, EventArgs e)
        {

            Cobra.Translacao(-1, 0, 0);

            //TX -= 1.0;
            /*if (Jogador != null)
                Jogador.Translacao(0.1, 0.0, 0.0);*/
        }

        private void Btn_Aumentar_Click(object sender, EventArgs e)
        {
            // Escala += 0.2;
            OpenGL gl = openGLControl.OpenGL;


            Cobra.setScale(gl, 2f, 0f, 0f);
        }

        private void Btn_Diminuir_Click(object sender, EventArgs e)
        {
            // Escala -= 0.2;
            OpenGL gl = openGLControl.OpenGL;

            Cobra.setScale(gl, -2f, 0f, 0f);
        }

        private void Btn_Experiencias_Click(object sender, EventArgs e)
        {
            /*
            int X = -10;
            Console.WriteLine("X = " + X);
            X = -X;
            Console.WriteLine("X = " + X);
            */

            string STR = "1.23";

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
            foreach (Modelo M in LModelos)
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
