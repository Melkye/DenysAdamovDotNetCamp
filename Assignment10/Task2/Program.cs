using Task2;

int[] numbers = new int[100];
for (int i = 0; i < numbers.Length; i++)
{
    numbers[i] = i + 1;
}

Matrix mat = new(4, 4);

mat.FillDiagonalSnake(numbers);

foreach (var number in mat)
{
    Console.WriteLine(number);
}
Console.WriteLine();