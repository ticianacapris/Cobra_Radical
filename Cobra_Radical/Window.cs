﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cobra_Radical
{
    public partial class Window : Form
    {
        private const int WIDTH = 30;
        private const int HEIGHT = 30;
        private int SCORE_INT;
        private int high_SCORE_INT;
        private const string SCORE_STRING = "Pontuação: {0}";
        private const string HIGHSCORE_STRING = "Pontuação máxima: {0}";
        private readonly Color m_BackgroundColor = Color.Black;
        private readonly Game m_Game;
        private readonly Bitmap m_GameField;
        private readonly Graphics m_GameGraphics;

        public Window()
        {
            InitializeComponent();
            m_GameField = new Bitmap(WIDTH * Piece.SIDE, HEIGHT * Piece.SIDE);
            m_GameGraphics = Graphics.FromImage(m_GameField); m_GameGraphics.PageUnit = GraphicsUnit.Pixel;
            ClientSize = new Size(m_GameField.Width, m_GameField.Height + m_RestartBtn.Height);
            m_Game = new Game(WIDTH, HEIGHT);
            m_Timer.Start();
        }

        private void UpdateScore()
        {

            SCORE_INT = m_Game.GetScore();
            scoreLbl.Text = string.Format(SCORE_STRING, m_Game.GetScore());

            if (SCORE_INT > high_SCORE_INT)
            {
                high_SCORE_INT = SCORE_INT;
                label_highscore.Text = string.Format(HIGHSCORE_STRING, m_Game.GetScore());
            }
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (m_Game.SnakeHasGrown())
            {
                UpdateScore();
            }
            if (m_Game.Lost())
            {
                m_Timer.Stop();
                m_RestartBtn.Enabled = true;
                MessageBox.Show("Perdeu :( \nVamos jogar outra vez!");
            }
            Invalidate();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    m_Game.ChangeSnakeDIrection(Direction.Left);
                    break;
                case Keys.Right:
                    m_Game.ChangeSnakeDIrection(Direction.Right);
                    break;
                case Keys.Up:
                    m_Game.ChangeSnakeDIrection(Direction.Up);
                    break;
                case Keys.Down:
                    m_Game.ChangeSnakeDIrection(Direction.Down);
                    break;
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            m_GameGraphics.Clear(m_BackgroundColor);
            m_Game.Draw(m_GameGraphics);
            e.Graphics.DrawImage(m_GameField, 0, m_RestartBtn.Height);
        }

        private void OnRestartBtnClick(object sender, EventArgs e)
        {
            m_RestartBtn.Enabled = false;
            m_Game.Restart();
            UpdateScore();
            m_Timer.Start();
        }

        private void scoreLbl_Click(object sender, EventArgs e)
        {

        }
    }
}

