using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace FileProject
{
    class files
    {

        public struct Student // Пришлось сделать public
        {
            public string name { get; set; } //надо с большой буквы

            public int age { get; set; }

        }
        public static void GetInfo(Student student) // Пришлось сделать static
        {
            Console.WriteLine($"Name : {student.name} Age : {student.age}");
        }

        public static void GetAllInfo(Student[] student) // Пришлось сделать static
        {
            for (int i = 0; i < student.Length; i++) GetInfo(student[i]);
        }
        public static void AddStudent(Student[] arrstudent, string name, int age) // добавить студента в конец
        {
            arrstudent[arrstudent.Length - 1] = new Student();
            arrstudent[arrstudent.Length - 1].name = name;
            arrstudent[arrstudent.Length - 1].age = age;
        }


        delegate void Message(); //Обявление делегата. Делегаты - это указатели на методы и с помощью делегатов мы можем вызвать данные методы
        static void Main(string[] args)
        {
            /*Message mes; // переменная делегата
            if (DateTime.Now.Hour < 12) mes = GoodMorning; // ссылка на метод
            else mes = GoodEvening;
            Console.WriteLine($"Hour : {DateTime.Now.Hour}");
            mes();*/ // вызов делегата

            //Benchmark();

            Stream stream = new FileStream("C:/home/database", FileMode.OpenOrCreate, FileAccess.Write); // порождение обЪекта
            BinaryWriter binwriter = new BinaryWriter(stream);
            int N = 1000000;
            for(int i=0; i <= N; i++)
            {
                binwriter.Write(i);
            }
            binwriter.Flush();


        }

        public static void GoodMorning ()
        {
            Console.WriteLine("GoodMorning");
        }
        public static void GoodEvening()
        {
            Console.WriteLine("GoodEvening");
        }

        private static void Benchmark()
        {
            Student[] db = GenerateDataBase();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Наэмите клавишу чтобы продолжить");
            Console.ReadKey();

            //запись

            stopwatch.Start();
            GenerateBinary(db, "C:/home/testbinwrite"); // бинарный
            stopwatch.Stop();
            Console.WriteLine("For write binary:");
            Console.WriteLine($"RunTime {Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            stopwatch.Reset();
            stopwatch.Start();
            GenerateFile(db, "C:/home/testtextwrite"); // текстовый
            stopwatch.Stop();
            Console.WriteLine("For write text:");
            Console.WriteLine($"RunTime {Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");
            ts = stopwatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);



            //чтение

            stopwatch.Reset();
            stopwatch.Start();
            ReadFile("C:/home/testtextwrite"); // текстовый
            stopwatch.Stop();
            Console.WriteLine("For read text:");
            Console.WriteLine($"RunTime {Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");
            ts = stopwatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            stopwatch.Reset();
            stopwatch.Start();
            ReadBinaryFile("C:/home/testbinwrite"); // бинарный
            stopwatch.Stop();
            Console.WriteLine("For read binary:");
            Console.WriteLine($"RunTime {Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");
            ts = stopwatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        public static Student[] GenerateDataBase()
        {

            //homework : сравнить время чтения и записи в/из файла в текстовом и бинарном форматах

            //генерируем базу данных для теста


            // Создаем массив букв, которые мы будем использовать
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();// массив смимволов для создания имени
            Random rand = new Random(); //генератор случайных чисел
            Console.WriteLine("Введите количество человек в базе данных");
            string dblength = Console.ReadLine();
            Student[] db = new Student[int.Parse(dblength)];

            //начало цикла создания базы данных
            for (int j = 0; j < db.Length; j++)
            {
                string randname = ""; //создаем рандомное имя
                int randlength = rand.Next(2, 10); // длина имени от 2 до 10 символов
                for (int i = 0; i < randlength; i++)
                {
                    randname += letters[rand.Next(0, letters.Length - 1)]; // выбираем символ из letters
                }

                int randage = rand.Next(17, 30); //создаем рандомный возраст студента
                db[j].name = randname;
                db[j].age = randage;//добавляем в базу данных

            }
            //конец цикла создания базы данных
            Console.WriteLine("База данных сгенерирована");
            return db;
        }

        private static void ReadFile(string path)
        {
            List<Student> students = new List<Student>(); //когда не знаем количество студентов
            // Student[] prov = new Student[]; ошибка так как надо указать размер массива
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read); //порождение обЪекта
            TextReader reader = new StreamReader(stream);
            string line = reader.ReadLine();

            while (line != null)
            {
                string[] arr = line.Split(',');
                Student st = new Student();
                st.name = arr[0];
                st.age = int.Parse(arr[1]);
                students.Add(st);
                line = reader.ReadLine();
            }
            Student[] arrstud = students.ToArray(); // преобразование в массив из лист
        }

       
        private static void GenerateFile(Student[] db, string path)
        {
            //Student[] db = new Student[] { new Student { name = "Tom", age = 22 }, new Student { name = "John", age = 20 }, new Student { name = "Zack", age = 26 }, new Student { name = "Kate", age = 18 }, new Student { name = "Jordan", age = 29 } };
            Console.WriteLine("StartFile!");
            Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write); //порождение обЪекта
            TextWriter writer = new StreamWriter(stream); //через него пишем в файл
            for (int i = 0; i < db.Length; i++)
            {
                Student st = db[i];
                writer.WriteLine($"{st.name},{st.age}"); // запятая в качестве разделителя
            }
            writer.Flush();//сброс буферов, после записи
            stream.Close();
        }

        private static void GenerateBinary(Student[] db, string path)
        {
            Console.WriteLine("StartFile!");
            Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write); // порождение обЪекта
            BinaryWriter binwriter = new BinaryWriter(stream);
            binwriter.Write((long)db.Length);
            for (int i = 0; i < db.Length; i++)
            {
                Student st = db[i];
                binwriter.Write(st.name);
                binwriter.Write(st.age);

            }
            binwriter.Flush();
            binwriter.Close();
            stream.Close();

        }

        private static void ReadBinaryFile(string path)
        {
            List<Student> students = new List<Student>();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read); //порождение обЪекта
            BinaryReader reader = new BinaryReader(stream);
            long length = reader.ReadInt64(); // читается записанная длина массива
            string name = reader.ReadString(); // читается первое имя
            int age = reader.ReadInt32();
            while (reader.PeekChar() > -1) //Следующий доступный символ или значение -1, если в потоке больше нет символов, или поток не поддерживает поиск.
            {              
                Student st = new Student();
                st.name = name;
                st.age = age;
                students.Add(st);
                name = reader.ReadString();
                age = reader.ReadInt32();
            }
            Student[] arrstud = students.ToArray(); // преобразование в массив из лист
        }
    }
}
