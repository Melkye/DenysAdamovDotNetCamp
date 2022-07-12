using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    internal class Matrix : IEnumerable<int>
    {
        public int[,] _elements;
        public Matrix(int rows, int columns)
        {
            _elements = new int[rows, columns];
        }
        //public int[,] Elements
        //{
        //    get
        //    {
        //        int[,] copyElements = new int[Rows, Columns];
        //        for (int i = 0; i < Rows; i++)
        //        {
        //            for (int j = 0; j < Columns; j++)
        //            {
        //                copyElements[i, j] = _elements[i, j];
        //            }
        //        }
        //        return copyElements;
        //    }
        //    private set => _elements = value;
        //}

        public int Rows => _elements.GetLength(0);
        public int Columns => _elements.GetLength(1);
        // 1  2  3  4
        // 8  7  6  5
        // 9 10 11 12
        public void FillHorizontalSnake(int[] numbers)
        {
            int numbersIndex = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (i % 2 == 0)
                    {
                        _elements[i, j] = numbers[numbersIndex];
                    }
                    else
                    {
                        _elements[i, Columns - 1 - j] = numbers[numbersIndex];
                    }
                    numbersIndex++;
                }
            }
        }
        // 1 6 7 12
        // 2 5 8 11
        // 3 4 9 10
        public void FillVerticalSnake(int[] numbers)
        {
            int numbersIndex = 0;
            for (int j = 0; j < Columns; j++)
            {
                for (int i = 0; i < Rows; i++)
                {
                    if (j % 2 == 0)
                    {
                        _elements[i, j] = numbers[numbersIndex];
                    }
                    else
                    {
                        _elements[Rows - 1 - i, j] = numbers[numbersIndex];
                    }
                    numbersIndex++;
                }
            }
        }
        //  1  2  6  7
        //  3  5  8 13
        //  4  9 12 14
        // 10 11 15 16

        // moves: right, down-left, down, up-right
        // prev      pos     move

        // up-right  row 0   right
        // right     row 0   down-left
        // down-left col 0   down
        // down      col 0   up-right

        // down-left row end right
        // right     row end up-right
        // up-right  col end down
        // down      col end down-left

        // down-left -       down-left
        // up-right  -       up-right

        public void FillDiagonalSnake(int[] numbers)
        {
            int i = 0;
            int j = 0;
            DiagonalSnakeMove prevMove = DiagonalSnakeMove.UpRight;
            for (int k = 0; k < Rows * Columns; k++)
            {
                _elements[i, j] = numbers[k];
                MoveDiagonalSnake(ref i, ref j, ref prevMove);
            }
        }
        private void MoveDiagonalSnake(ref int i, ref int j, ref DiagonalSnakeMove prevMove)
        {

            if (prevMove == DiagonalSnakeMove.UpRight)
            {
                if (j == Columns - 1)
                {
                    i++;
                    prevMove = DiagonalSnakeMove.Down;
                }
                else if (i == 0)
                {
                    j++;
                    prevMove = DiagonalSnakeMove.Right;
                }
                else
                {
                    i--;
                    j++;
                    prevMove = DiagonalSnakeMove.UpRight;
                }
            }
            else if (prevMove == DiagonalSnakeMove.Right)
            {
                if (i == 0)
                {
                    i++;
                    j--;
                    prevMove = DiagonalSnakeMove.DownLeft;
                }
                else if (i == Rows - 1)
                {
                    i--;
                    j++;
                    prevMove = DiagonalSnakeMove.UpRight;
                }
            }
            else if (prevMove == DiagonalSnakeMove.DownLeft)
            {
                if (i == Rows - 1)
                {
                    j++;
                    prevMove = DiagonalSnakeMove.Right;
                }
                else if (j == 0)
                {
                    i++;
                    prevMove = DiagonalSnakeMove.Down;
                }
                else
                {
                    i++;
                    j--;
                    prevMove = DiagonalSnakeMove.DownLeft;
                }
            }
            else if (prevMove == DiagonalSnakeMove.Down)
            {
                if (j == 0)
                {
                    i--;
                    j++;
                    prevMove = DiagonalSnakeMove.UpRight;
                }
                else if (j == Columns - 1)
                {
                    i++;
                    j--;
                    prevMove = DiagonalSnakeMove.DownLeft;
                }
            }
        }

        // 1 10  9  8
        // 2 11 12  7
        // 3  4  5  6

        // moves: up, down, left, right
        // prev     pos                 move
        // down     -                   down
        // down     down-left corner    right
        // right    -                   right
        // right    down-right corner   up
        // up       -                   up
        // up       up-right corner     left
        // left     -                   left
        // left     i=lap, j=lap+1      down, lap++

        public void FillSpiralSnakeClockwise(int[] numbers)
        {
            int i = 0;
            int j = 0;
            int lap = 0;
            SpiralSnakeMove prevMove = SpiralSnakeMove.Down;
            for (int k = 0; k < Rows * Columns; k++)
            {
                _elements[i, j] = numbers[k];
                MoveSpiralSnakeClockwise(ref i, ref j, ref prevMove, ref lap);
            }
        }
        private void MoveSpiralSnakeClockwise(ref int i, ref int j, ref SpiralSnakeMove prevMove, ref int lap)
        {
            if (prevMove == SpiralSnakeMove.Down)
            {
                if (i == Rows - 1 - lap && j == lap)
                {
                    j++;
                    prevMove = SpiralSnakeMove.Right;
                }
                else
                {
                    i++;
                    prevMove = SpiralSnakeMove.Down;
                }
            }
            else if (prevMove == SpiralSnakeMove.Right)
            {
                if (i == Rows - 1 - lap && j == Columns - 1 - lap)
                {
                    i--;
                    prevMove = SpiralSnakeMove.Up;
                }
                else
                {
                    j++;
                    prevMove = SpiralSnakeMove.Right;
                }
            }
            else if (prevMove == SpiralSnakeMove.Up)
            {
                if (i == lap && j == Columns - 1 - lap)
                {
                    j--;
                    prevMove = SpiralSnakeMove.Left;
                }
                else
                {
                    i--;
                    prevMove = SpiralSnakeMove.Up;
                }
            }
            else if (prevMove == SpiralSnakeMove.Left)
            {
                if (i == lap && j == lap + 1)
                {
                    i++;
                    lap++;
                    prevMove = SpiralSnakeMove.Down;
                }
                else
                {
                    j--;
                    prevMove = SpiralSnakeMove.Left;
                }
            }
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    s += ($"{_elements[i, j],2} ");
                }
                s += "\n";
            }
            return s;
        }
        // diaginal snake
        public IEnumerator<int> GetEnumerator()
        {
            int i = 0;
            int j = 0;
            DiagonalSnakeMove prevMove = DiagonalSnakeMove.UpRight;
            for (int k = 0; k < Rows * Columns; k++)
            {
                yield return _elements[i, j];
                MoveDiagonalSnake(ref i, ref j, ref prevMove);
            }
        }
        // horizontal snake
        //public IEnumerator<int> GetEnumerator() 
        //{
        //    for (int i = 0; i < Rows;i++)
        //    {
        //        for (int j = 0; j < Columns;j++)
        //        {
        //            if (i % 2 == 0)
        //            {
        //                yield return _elements[i, j];
        //            }
        //            else
        //            {
        //                yield return _elements[i, Columns - 1 - j];
        //            }
        //        }
        //    }
        //}
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
