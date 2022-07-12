using Task2;

int[] numbers = new int[100];
for (int i = 0; i < numbers.Length; i++)
{
    numbers[i] = i + 1;
}
//Matrix matSq = new Matrix(4, 4);

//matSq.FillVerticalSnake(numbers);
//Console.WriteLine(matSq);
//Console.WriteLine();

//matSq.FillDiagonalSnake(numbers);
//Console.WriteLine(matSq);
//Console.WriteLine();

//matSq.FillSpiralSnakeClockwise(numbers);
//Console.WriteLine(matSq);
//Console.WriteLine();

//Console.WriteLine("-----------");

//Matrix mat3x4 = new Matrix(3, 4);

//mat3x4.FillVerticalSnake(numbers);
//Console.WriteLine(mat3x4);
//Console.WriteLine();

//mat3x4.FillDiagonalSnake(numbers);
//Console.WriteLine(mat3x4);
//Console.WriteLine();

//mat3x4.FillSpiralSnakeClockwise(numbers);
//Console.WriteLine(mat3x4);
//Console.WriteLine();

//Console.WriteLine("-----------");

//Matrix mat4x3 = new Matrix(4, 3);

//mat4x3.FillVerticalSnake(numbers);
//Console.WriteLine(mat4x3);
//Console.WriteLine();

//mat4x3.FillDiagonalSnake(numbers);
//Console.WriteLine(mat4x3);
//Console.WriteLine();

//mat4x3.FillSpiralSnakeClockwise(numbers);
//Console.WriteLine(mat4x3);
//Console.WriteLine();

//Console.WriteLine("-----------");

Matrix mat = new(4, 4);
mat.FillHorizontalSnake(numbers);
mat.FillSpiralSnakeClockwise(numbers);

foreach (int number in mat)
{
    Console.WriteLine(number);
}
Console.WriteLine();