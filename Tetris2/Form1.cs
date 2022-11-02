using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris2
{
    public partial class Form1 : Form
    {
        Panel[,] map = new Panel[20, 10];
        int[,] backMap = new int[20, 10];
        Shape currentPiece;
        Timer timer = new Timer();
        int currentX = 5;
        int currentY = 0;
        int score = 0;
        bool isRotated = false;
        bool isGameOver = false;
        Color currentColor;
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            textBox1.Enabled = false;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                MovePiece(Direction.Left);
            }
            else if (e.KeyCode == Keys.Right)
            {
                MovePiece(Direction.Right);
            }
            else if (e.KeyCode == Keys.Down)
            {
                MovePiece(Direction.Down);
            }
            else if (e.KeyCode == Keys.R)
            {
                DeletePiece();
                SpawnPieceAgain();
                currentPiece.Rotate();
                // idee: sterge piesa, width <-> height, pune iar piesa
            }
        }

        private void SpawnPieceAgain()
        {
            if (!isRotated)
            {
                int altI = 0, altJ = 0;

                for (int i = currentY; i < currentY + currentPiece.width; i++)
                {
                    for (int j = currentX; j < currentX + currentPiece.height; j++)
                    {
                        backMap[i, j] = currentPiece.dots[altI, altJ];
                        altI++;
                    }

                    altJ++;
                    altI = 0;
                }
            }
            else
            {
                //int altI = 0, altJ = 0;

                //for (int i = currentY; i < currentY + currentPiece.height; i++)
                //{
                //    for (int j = currentX; j < currentX + currentPiece.width; j++)
                //    {
                //        backMap[i, j] = currentPiece.dots[altJ, altI];
                //        altJ++;
                //    }

                //    altI++;
                //    altJ = 0;
                //}
            }

            isRotated = !isRotated;
        }

        private void DeletePiece()
        {
            for (int i = currentY; i < currentY + currentPiece.height; i++)
            {
                for (int j = currentX; j < currentX + currentPiece.width; j++)
                {
                    backMap[i, j] = 0;
                }
            }
        }

        public void RenderMapFromBackMap()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j].BackColor = Colors.GetColor(backMap[i, j]);
                }
            }
        }

        public void SpawnPiece()
        {
            if (isGameOver)
            {
                return;
            }
            var colorNumber = rand.Next(6);
            currentColor = Colors.GetColor(colorNumber);
            currentPiece = GetRandomShape();

            int altI = 0, altJ = 0;
            for (int i = currentY; i < currentY + currentPiece.height; i++)
            {
                for (int j = currentX; j < currentX + currentPiece.width; j++)
                {
                    if (backMap[i, j] != 0)
                    {
                        EndGame();
                        return;
                    }

                    backMap[i, j] = currentPiece.dots[altI, altJ] != 0 ? colorNumber : 0;
                    altJ++;
                }

                altI++;
                altJ = 0;
            }
        }

        private void EndGame()
        {
            if (!isGameOver)
            {
                isGameOver = true;
                MessageBox.Show("GAME OVER!");
            }
        }

        private Shape GetRandomShape()
        {
            var shape = GetNextShape();

            currentX = 4;
            currentY = 0;

            return shape;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CanMovePieceDown() && !isGameOver)
            {
                MovePiece(Direction.Down);
                RenderMapFromBackMap();
            }

            textBox1.Clear();
            ShowBackMapInTextBox();
        }

        private void ShowBackMapInTextBox()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    textBox1.Text += backMap[i, j];
                    textBox1.Text += ' ';
                }

                textBox1.Text += System.Environment.NewLine;
            }

            Focus();
        }

        private bool CheckLineUnderPieceIsEmpty()
        {
            for (int x = currentX; x < currentX + currentPiece.width; x++)
            {
                if (backMap[currentY + currentPiece.height, x] != 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CanMovePieceDown()
        {
            if (currentY + currentPiece.height == 20 || CheckLineUnderPieceIsEmpty())
            {
                SpawnPiece();
                return false;
            }

            return true;
        }

        private bool CheckLineBeforePieceIsEmpty()
        {
            for (int y = currentY; y < currentY + currentPiece.height; y++)
            {
                if (backMap[y, currentX - 1] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckLineAfterPieceIsEmpty()
        {
            for (int y = currentY; y < currentY + currentPiece.height; y++)
            {
                if (currentX + currentPiece.width > 9 || backMap[y, currentX + currentPiece.width] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanMovePieceLeft()
        {
            if (currentX > 0 && CheckLineBeforePieceIsEmpty())
            {
                return true;
            }

            return false;
        }

        private bool CanMovePieceRight()
        {
            if (currentX < 9 && CheckLineAfterPieceIsEmpty())
            {
                return true;
            }

            return false;
        }

        private void MovePieceDown()
        {
            if (!CanMovePieceDown())
            {
                return;
            }

            for (int i = currentY + currentPiece.height; i >= currentY; i--)
            {
                for (int j = currentX; j < currentX + currentPiece.width; j++)
                {
                    if (i > 0)
                    {
                        backMap[i, j] = backMap[i - 1, j];
                        backMap[i - 1, j] = 0;
                    }
                    else
                    {
                        backMap[i, j] = 0;
                    }
                }
            }

            currentY++;
        }

        private void MovePieceLeft()
        {
            if (!CanMovePieceLeft())
            {
                return;
            }

            for (int i = currentY; i < currentY + currentPiece.height; i++)
            {
                for (int j = currentX; j < currentX + currentPiece.width; j++)
                {
                    backMap[i, j - 1] = backMap[i, j];
                    backMap[i, j] = 0;
                }
            }

            currentX--;
        }

        private void MovePieceRight()
        {
            if (!CanMovePieceRight())
            {
                return;
            }

            for (int i = currentY; i < currentY + currentPiece.height; i++)
            {
                for (int j = currentX + currentPiece.width; j >= currentX; j--)
                {
                    if (j > 0)
                    {
                        backMap[i, j] = backMap[i, j - 1];
                        backMap[i, j - 1] = 0;
                    }
                    else
                    {
                        backMap[i, j] = 0;
                    }
                }
            }

            currentX++;
        }

        private void MovePiece(Direction direction)
        {
            if (direction == Direction.Down)
            {
                MovePieceDown();
            }
            else if (direction == Direction.Left)
            {
                MovePieceLeft();
            }
            else if (direction == Direction.Right)
            {
                MovePieceRight();
            }

            RenderMapFromBackMap();
            CheckIfAnyLineIsFull();
        }

        private void CheckIfAnyLineIsFull()
        {
            for (int i = 0; i < 20; i++)
            {
                bool isLineFull = true;
                for (int j = 0; j < 10; j++)
                {
                    if (backMap[i, j] == 0)
                    {
                        isLineFull = false;
                        break;
                    }
                }
                if (isLineFull)
                {
                    DeleteLine(i);
                }
            }
        }

        private void DeleteLine(int lineNumber)
        {
            for (int i = lineNumber; i >= 1; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    backMap[i, j] = backMap[i - 1, j];
                }
            }

            for (int j = 0; j < 10; j++)
            {
                backMap[0, j] = 0;
            }

            score += 10;
            tbScore.Text = $"{score}";
        }

        public void GenerateMap()
        {
            int x = 10, y = 10;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = new Panel()
                    {
                        Height = 30,
                        Width = 30,
                        Location = new Point(x, y)
                    };

                    Controls.Add(map[i, j]);
                    backMap[i, j] = 0;

                    x += 32;

                    if (j == 9)
                    {
                        x = 10;
                        y += 32;
                    }
                }
            }
        }

        private Shape GetNextShape()
        {
            return new ShapesHandler().GetRandomShape();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer.Tick += Timer_Tick;
            timer.Interval = 1000;
            timer.Start();

            this.KeyDown += OnKeyDown; ;


            btnStart.Hide();
            tbScore.Show();
            tbScore.TabStop = false;
            tbScore.Text = "0";
            this.Focus();

            GenerateMap();
            RenderMapFromBackMap();

            SpawnPiece();
        }
    }
}
