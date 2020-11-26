using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //потоки элементов

namespace Lesson5
{

    public struct Cell // ячейка
    {
        public readonly int X, Y;

        public Cell(int x, int y) { X = x; Y = y; }
    }

    public class Life : IEnumerable<Cell>
    {
        private List<Cell> _cells = new List<Cell>();

        private static readonly int[] Liveables = { 2, 3 }; // для не смерти должно быть 2 или 3 живых рядом

        public bool Next()
        {
            var died = _cells
                .Where(cell => !Liveables.Contains(Count(cell)))    // Условие смерти
                .ToArray();

            var born = _cells
                .SelectMany(Ambit)                                  // Все окружающие ячейки
                .Distinct()                                         // ...без дубликатов Возвращает различающиеся элементы данной последовательности
                .Except(_cells)                                     // ...пустые Находит разность множеств, представленных двумя последовательностями
                .Where(cell => Count(cell) == 3)                    // Условие рождения
                .ToArray();

            if (died.Length == 0 && born.Length == 0)
                return false; // Нет изменений

            _cells = _cells
                .Except(died) //Находит разность множеств, представленных двумя последовательностями.
                .Concat(born) // Соединяет две коллекции
                .ToList();


            return _cells.Any(); // Не все еще умерли? Проверяет существование хотя бы одного элемента в последовательности
        }

        private int Count(Cell cell) // Считает живые клетки
        {
            return Ambit(cell)
                .Intersect(_cells) // пересечение с живыми
                .Count(); // их число
        }

        private static IEnumerable<Cell> Ambit(Cell cell) // диапазон ( поток ячеек кроме центральной, то есть квадрат вокруг данной ячейки )
        {
            return Enumerable.Range(-1, 3) // Генерирует последовательность целых чисел в заданном диапазоне. Второй аргумент - количество генерируемых последовательных целых чисел.
                .SelectMany(x => Enumerable.Range(-1, 3) // -1 0 1
                    .Where(y => x != 0 || y != 0) 					// Кроме центральной клетки
                    .Select(y => new Cell(cell.X + x, cell.Y + y)));
        }

        public override string ToString()
        {
            if (_cells.Count == 0)
                return string.Empty;

            var xmin = _cells.Min(cell => cell.X);
            var xmax = _cells.Max(cell => cell.X);
            var ymin = _cells.Min(cell => cell.Y);
            var ymax = _cells.Max(cell => cell.Y);

            var matrix = Enumerable.Range(xmin, xmax - xmin + 1)
                .Select(x => Enumerable.Range(ymin, ymax - ymin + 1)
                    .Select(y => _cells.Contains(new Cell(x, y))));

            return string.Join(Environment.NewLine,
                matrix.Select(row =>
                    string.Join("",
                        row.Select(b => b ? "X" : "."))));
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(int x, int y)
        {
            _cells.Add(new Cell(x, y));
        }
    }

    class flow
    {
        static void Main(string[] args)
        {
            Life();
            
            // Жизнь Конвея

            //Calculator();            
        }

        private static void Calculator() // демонострирует работу SelectMany
        {
            var query =
           Enumerable
               .Range(1, 10)
               .SelectMany(ints => Enumerable.Range(1, 10), (a, b) => $"{a} * {b} = {a * b} ")
               .ToArray();

            Console.WriteLine(string.Join(Environment.NewLine, query));

            Console.Read();
        }

        private static void Life()
        {
            var life = new Life { { 1, 1 }, { 2, 2 }, { 3, 3 }, { 1, 2 }, { 1, 3 } }; // начальное положение крестики живые

            var i = 0;
            do
            {
                Console.WriteLine("#{0}\r\n{1}", i++, life);

                Console.ReadLine();

            } while (life.Next());

            Console.WriteLine(life.Any() ? "* Stagnation!" : "* Extinction!");
        }

        private static void Training()
        {
            Console.WriteLine("StartLesson5!");
            IEnumerable<int> flow = new int[] { 2, 3, 5, 0, 40 }; // поток
            int count = flow.Count();
            var max = flow
                .Where(x => x % 2 != 0)
                .Select(x => x * x) // функиональное преобразование
                .Aggregate((x, a) => x + a); // а - аккумулятор
                                             //.Aggregate((x,a)=>x>a?x:a)
            Console.WriteLine(max);


            IEnumerable<string> flowstr = new string[] { "Петя", "Вася", "Лена", "Катя", "Саша" };
            var str = flowstr
                .Select(x => x.ToLower())
                .OrderByDescending(x => x)
                .Aggregate((x, a) => a + " " + x);
            Console.WriteLine(str);
        }
    }
}
