using SharpGL;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using System.Diagnostics;

using SharpGL.SceneGraph.Assets;

namespace SharpGL_CG_TDM
{
    /// <summary>
    /// The main form class.
    /// </summary>
    public partial class SharpGLForm : Form
    {

        Texture[] TextArray;

        OpenGL gl;

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

        int direcaoCobra;

        int fieldSize;

        System.Timers.Timer timer;
        System.Timers.Timer extrasTimer;
        System.Timers.Timer rotatetimer;

        int[,] foodMatrix, obstaclesMatrix;

        Dictionary<string, List<Modelo>> Matriz;

        Random rndNumber;

        int currentPont;
        int maxPont;

        public SharpGLForm()
        {
            InitializeComponent();

            TextArray = new Texture[5];

            gl = openGLControl.OpenGL;

            for (int i = 1; i <= 4; i++)
            {
                TextArray[i - 1] = new Texture();
                TextArray[i - 1].Create(gl, "..\\..\\Texturas\\Text" + i + ".bmp");
            }

            TextArray[4] = new Texture();
            TextArray[4].Create(gl, "..\\..\\Texturas\\Terrain.bmp");

            this.KeyDown += new KeyEventHandler(keyPress);

            TX = 0;
            TY = 0;
            TZ = 0;
            Escala = 1.0;
            Incremento_Escala = 0.1;
            Sentido = 2;
            Em_Movimento = true;

            Cobra = new Modelo();
            cobraLength = 1;
            CobraFull = new List<Modelo>();
            directionHistory = new List<int>();
            directionHistory.Add(1);

            direcaoCobra = 1;   //1: X+  2: Z+  3: X-  4: Z-

            LModelos = new List<Modelo>();
            Comida = new List<Modelo>();
            Obstaculos = new List<Modelo>();

            Cobra.LerFicheiro("..\\..\\loadModelos\\cubo.obj");

            fieldSize = 100;

            currentPont = 0;
            maxPont = 0;

            pontLabel.TextChanged += new System.EventHandler(this.updateScreenPont);

            foodMatrix = new int[fieldSize, fieldSize];
            obstaclesMatrix = new int[fieldSize, fieldSize];

            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timerMovement);
            timer.Interval = 60;
            timer.Enabled = true;

            extrasTimer = new System.Timers.Timer();
            extrasTimer.Elapsed += new ElapsedEventHandler(randomPosition);
            extrasTimer.Interval = 2000;
            extrasTimer.Enabled = true;
            extrasTimer.AutoReset = true;

            rotatetimer = new System.Timers.Timer();
            rotatetimer.Elapsed += new ElapsedEventHandler(rotateFunction);
            rotatetimer.Interval = 60;
            rotatetimer.Enabled = true;

            Matriz = new Dictionary<string, List<Modelo>>();
            Matriz["food"] = new List<Modelo>();
            Matriz["obstaculos"] = new List<Modelo>();

            rndNumber = new Random();

        }

        public void rotateFunction(object source, ElapsedEventArgs e)
        {
            foreach (Modelo M in Matriz["food"].ToArray())
            {
                for (int i = 1; i <= 6; i++)
                {
                    M.rotate(gl, 30 * i, 0, 30 * i);
                }
            }
        }

        public void incrementPoints()
        {
            currentPont += 10;
            if (currentPont > maxPont)
                maxPont = currentPont;

            //            pontLabel.Text = string.Format("Pontuação: " + currentPont);
            //          maxPontLabel.Text = string.Format("Pontuação máxima: " + maxPont);
        }

        private void updateScreenPont(object sender, EventArgs e)
        {

            pontLabel.Text = string.Format("Pontuação: " + currentPont);
            maxPontLabel.Text = string.Format("Pontuação máxima: " + maxPont);

        }

        public void aumentarCobra()
        {
            Modelo cobraPart = new Modelo();
            cobraPart.LerFicheiro("..\\..\\loadModelos\\snakeBody.obj");
            if (LModelos.Count > 0)
            {

                switch (directionHistory[directionHistory.Count - 1 - LModelos.Count])
                {
                    case 1:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX() - 1, 0, LModelos[LModelos.Count - 1].getZ());
                        break;
                    case 2:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX(), 0, LModelos[LModelos.Count - 1].getZ() - 1);
                        break;
                    case 3:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX() + 1, 0, LModelos[LModelos.Count - 1].getZ());
                        break;
                    case 4:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX(), 0, LModelos[LModelos.Count - 1].getZ() + 1);
                        break;
                }
            }
            else
            {
                switch (directionHistory[directionHistory.Count - 1])
                {
                    case 1:
                        cobraPart.Translacao(Cobra.getX() - 1, 0, Cobra.getZ());
                        break;
                    case 2:
                        cobraPart.Translacao(Cobra.getX(), 0, Cobra.getZ() - 1);
                        break;
                    case 3:
                        cobraPart.Translacao(Cobra.getX() + 1, 0, Cobra.getZ());
                        break;
                    case 4:
                        cobraPart.Translacao(Cobra.getX(), 0, Cobra.getZ() + 1);
                        break;
                }
            }

            LModelos.Add(cobraPart);
            cobraLength++;

            removeFood(Cobra.getX(), Cobra.getZ());

            incrementPoints();
        }

        public void removeFood(double coordX, double coordZ)
        {
            foreach (Modelo M in Matriz["food"].ToArray())
            {
                if (M.getX() == coordX && M.getZ() == coordZ)
                {
                    Matriz["food"].Remove(M);
                    foodMatrix[Convert.ToInt32(coordX), Convert.ToInt32(coordZ)] = 0;
                }
            }
        }
        public void randomPosition(object source, ElapsedEventArgs e)
        {
            bool foodOcupado = true;
            bool obstOcupado = true;

            while (foodOcupado == true)
            {
                int newValueX = rndNumber.Next(1, fieldSize);
                int newValueY = rndNumber.Next(1, fieldSize);

                if (foodMatrix[newValueX, newValueY] != 1)
                {
                    Modelo comida = new Modelo();
                    comida.LerFicheiro("..\\..\\loadModelos\\maça.obj");
                    comida.Translacao(newValueX, 0.5, newValueY);
                    foodMatrix[newValueX, newValueY] = 1;
                    Matriz["food"].Add(comida);
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
                    obstaculo.LerFicheiro("..\\..\\loadModelos\\cubo.obj");
                    obstaculo.Translacao(newValueX, 0.5, newValueY);
                    obstaclesMatrix[newValueX, newValueY] = 1;
                    Matriz["obstaculos"].Add(obstaculo);
                    obstOcupado = false;
                }
            }
        }

        public void timerMovement(object source, ElapsedEventArgs e)
        {
            switch (direcaoCobra)
            {
                case 1:
                    directionHistory.Add(1);
                    Cobra.Translacao(1, 0, 0);
                    break;
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
            }

            foreach (Modelo M in Matriz["food"].ToArray())
            {
                if (Cobra.Colide(M))
                {
                    Console.WriteLine("aumentar cobra");
                    aumentarCobra();
                }
            }

            foreach (Modelo M in Matriz["obstaculos"].ToArray())
            {
                if (Cobra.Colide(M))
                {
                    Console.WriteLine("perder pontos");
                    incrementPoints();
                }
            }

            for (int i = 1; i <= LModelos.Count; i++)
            {
                if (directionHistory.Count > LModelos.Count)
                {
                    switch (directionHistory[directionHistory.Count - i - 1])
                    {
                        case 1:
                            LModelos[i - 1].Translacao(1, 0, 0);
                            break;
                        case 2:
                            LModelos[i - 1].Translacao(0, 0, 1);
                            break;
                        case 3:
                            LModelos[i - 1].Translacao(-1, 0, 0);
                            break;
                        case 4:
                            LModelos[i - 1].Translacao(0, 0, -1);
                            break;
                    }
                }
                else
                {
                    switch (directionHistory[directionHistory.Count - 1])
                    {
                        case 1:
                            LModelos[0].Translacao(1, 0, 0);
                            break;
                        case 2:
                            LModelos[0].Translacao(0, 0, 1);
                            break;
                        case 3:
                            LModelos[0].Translacao(-1, 0, 0);
                            break;
                        case 4:
                            LModelos[0].Translacao(0, 0, -1);
                            break;
                    }
                }
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

            //gl.Enable(OpenGL.GL_TEXTURE_2D);
            //TextArray[4].Bind(gl);

            gl.Color(0.9f, 0.9f, 0.9f);


            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    gl.Vertex((float)(j - 1), 0, (i - 1));
                    gl.Vertex((float)(j - 1), 0, (float)(i));
                    gl.Vertex((float)(j), 0, (float)(i - 1));

                    gl.Vertex((float)(j - 1), 0, (float)(i));
                    gl.Vertex((float)(j), 0, (float)(i - 1));
                    gl.Vertex((float)(j), 0, (float)(i));
                }
            }
            //gl.Disable(OpenGL.GL_TEXTURE_2D);
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
            // gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f); // origin of the line
            gl.Vertex(TAM, 0.0f, 0.0f); // ending point of the line
            gl.End();

            //eixo y
            gl.Begin(OpenGL.GL_LINES);
            // gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(0.0f, 0.0f, 0.0f); // origin of the line
            gl.Vertex(0.0f, TAM, 0.0f); // ending point of the line
            gl.End();

            //eixo z
            gl.Begin(OpenGL.GL_LINES);
            // gl.Color(0.0f, 0.0f, 1.0f);
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

            DesenharFundo(gl);


            gl.Enable(OpenGL.GL_TEXTURE_2D);
            TextArray[0].Bind(gl);

            Cobra.Desenhar(gl);

            gl.Disable(OpenGL.GL_TEXTURE_2D);

            foreach (Modelo M in LModelos)
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                TextArray[0].Bind(gl);

                M.Desenhar(gl);

                gl.Disable(OpenGL.GL_TEXTURE_2D);
            }

            foreach (Modelo M in Matriz["food"].ToArray())
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                TextArray[2].Bind(gl);

                M.Desenhar(gl);

                gl.Disable(OpenGL.GL_TEXTURE_2D);
            }

            foreach (Modelo M in Matriz["obstaculos"].ToArray())
            {
                gl.Enable(OpenGL.GL_TEXTURE_2D);
                TextArray[3].Bind(gl);
                M.Desenhar(gl);
                gl.Disable(OpenGL.GL_TEXTURE_2D);

            }
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  TODO: Initialise OpenGL here.
            //  Get the OpenGL object.
            Console.WriteLine("Passei em openGLControl_OpenGLInitialized");

            OpenGL gl = openGLControl.OpenGL;
            //  Set the clear color.
            gl.ClearColor(1.0f, 1.0f, 1.0f, 1);

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


            timer.Stop();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Modelo Jogador = new Modelo();
            //foreach (Modelo M in LModelos)
            //    if (Jogador.Colide(M))
            //    {

            //    }
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
