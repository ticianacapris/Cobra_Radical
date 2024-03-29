﻿using SharpGL;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Forms;
using System.Diagnostics;

using System.Threading;

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
            Cobra.Translacao(0, 0.5f, 0);

            fieldSize = 100;

            currentPont = 0;
            maxPont = 0;

            pontLabel.TextChanged += new System.EventHandler(this.updateScreenPont);

            foodMatrix = new int[fieldSize, fieldSize];
            obstaclesMatrix = new int[fieldSize, fieldSize];

            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(timerMovement);
            timer.Interval = 100;
            timer.Enabled = true;

            extrasTimer = new System.Timers.Timer();
            extrasTimer.Elapsed += new ElapsedEventHandler(randomPosition);
            extrasTimer.Interval = 6000;
            extrasTimer.Enabled = true;
            extrasTimer.AutoReset = true;

            rotatetimer = new System.Timers.Timer();
            rotatetimer.Elapsed += new ElapsedEventHandler(rotateFunction);
            rotatetimer.Interval = 100;
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

            Invoke(new MethodInvoker(delegate ()
            {
                pontLabel.Text = string.Format("Pontuação: " + currentPont);
                maxPontLabel.Text = string.Format("Pontuação máxima: " + maxPont);
            }));
        }

        public void decrementPoints()
        {
            currentPont -= 30;

            Invoke(new MethodInvoker(delegate ()
            {
                pontLabel.Text = string.Format("Pontuação: " + currentPont);
            }));

            if (currentPont < 0)
            {
                DialogResult resultado = MessageBox.Show("Vamos jogar mais uma vez?", "Fim do jogo", MessageBoxButtons.OK);
                if (resultado == DialogResult.OK)
                    resetGame();
            }
        }

        public void resetGame()
        {
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

            Cobra.Translacao(0, 0.5f, 0);

            fieldSize = 100;

            currentPont = 0;

            Matriz = new Dictionary<string, List<Modelo>>();
            Matriz["food"] = new List<Modelo>();
            Matriz["obstaculos"] = new List<Modelo>();

            Invoke(new MethodInvoker(delegate ()
            {
                pontLabel.Text = string.Format("Pontuação: " + currentPont);
                maxPontLabel.Text = string.Format("Pontuação máxima: " + maxPont);
            }));
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
                switch (directionHistory[directionHistory.Count - 1 - LModelos.Count - 1])
                {
                    case 1:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX() - 1, LModelos[LModelos.Count - 1].getY(), LModelos[LModelos.Count - 1].getZ());
                        break;
                    case 2:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX(), LModelos[LModelos.Count - 1].getY(), LModelos[LModelos.Count - 1].getZ() - 1);
                        break;
                    case 3:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX() + 1, LModelos[LModelos.Count - 1].getY(), LModelos[LModelos.Count - 1].getZ());
                        break;
                    case 4:
                        cobraPart.Translacao(LModelos[LModelos.Count - 1].getX(), LModelos[LModelos.Count - 1].getY(), LModelos[LModelos.Count - 1].getZ() + 1);
                        break;
                }
            }
            else
            {
                switch (directionHistory[directionHistory.Count - 1])
                {
                    case 1:
                        cobraPart.Translacao(Cobra.getX() - 2, Cobra.getY(), Cobra.getZ());
                        break;
                    case 2:
                        cobraPart.Translacao(Cobra.getX(), Cobra.getY(), Cobra.getZ() - 2);
                        break;
                    case 3:
                        cobraPart.Translacao(Cobra.getX() + 2, Cobra.getY(), Cobra.getZ());
                        break;
                    case 4:
                        cobraPart.Translacao(Cobra.getX(), Cobra.getY(), Cobra.getZ() + 2);
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
        public void removeObstacle(double coordX, double coordZ)
        {
            foreach (Modelo M in Matriz["obstaculos"].ToArray())
            {
                if (M.getX() == coordX && M.getZ() == coordZ)
                {
                    Matriz["obstaculos"].Remove(M);
                    obstaclesMatrix[Convert.ToInt32(coordX), Convert.ToInt32(coordZ)] = 0;
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

                if (foodMatrix[newValueX, newValueY] != 1 && obstaclesMatrix[newValueX, newValueY] != 1)
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

                if (obstaclesMatrix[newValueX, newValueY] != 1 && foodMatrix[newValueX, newValueY] != 1)
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
                    if (Cobra.getX() < fieldSize - 2)
                    {
                        Cobra.Translacao(1, 0, 0);
                        directionHistory.Add(1);
                    }
                    break;
                case 2:
                    if (Cobra.getZ() < fieldSize - 2)
                    {
                        Cobra.Translacao(0, 0, 1);
                        directionHistory.Add(2);
                    }
                    break;
                case 3:
                    if (Cobra.getX() > 0)
                    {
                        Cobra.Translacao(-1, 0, 0);
                        directionHistory.Add(3);
                    }
                    break;
                case 4:
                    if (Cobra.getZ() > 0)
                    {
                        Cobra.Translacao(0, 0, -1);
                        directionHistory.Add(4);
                    }
                    break;
            }

            foreach (Modelo M in Matriz["food"].ToArray())
            {
                if (Cobra.Colide(M))
                {
                    aumentarCobra();
                    removeFood(M.getX(), M.getZ());
                    incrementPoints();
                }
            }

            for (int i = 0; i < LModelos.Count - 1; i++)
            {
                if (Cobra.Colide(LModelos[i]))
                {
                    DialogResult resultado = MessageBox.Show("Vamos jogar mais uma vez?", "Fim do jogo", MessageBoxButtons.OK);
                    if (resultado == DialogResult.OK)
                        resetGame();
                }
            }

            foreach (Modelo M in Matriz["obstaculos"].ToArray())
            {
                if (Cobra.Colide(M))
                {
                    removeObstacle(M.getX(), M.getZ());
                    decrementPoints();
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

        private void maxPontLabel_Click(object sender, EventArgs e)
        {

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
            gl.Perspective(70.0f, (double)Width / (double)Height, 0.01, 1000.0);
            //  Use the 'look at' helper function to position and aim the camera.

            if (Cobra != null)
            {
                gl.LookAt(Cobra.getX(), Cobra.getY(), Cobra.getZ(),
                 Cobra.getX(), Cobra.getY() + 2, Cobra.getZ(),
                0, 1, 0);
            }
            else
            {
                //gl.LookAt(-4, 4, -4,
                //    2, 0, 2,
                //    -4, 6, -4);
                gl.LookAt(-4, 4, -4,
                    2, 0, 2,
                    0, 1, 0);
            }
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        // Method associated to initial draw test 

    }
}
