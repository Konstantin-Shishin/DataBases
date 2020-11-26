using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


namespace Lesson4
{
    class sort
    {
        public struct Student // Пришлось сделать public
        {
            public string name { get; set; } //надо с большой буквы

            public int age { get; set; }

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

                int randage = rand.Next(17, 25); //создаем рандомный возраст студента
                db[j].name = randname;
                db[j].age = randage;//добавляем в базу данных

            }
            //конец цикла создания базы данных
            Console.WriteLine("База данных сгенерирована");
            return db;
        }
        static void Main(string[] args)
        {
            /*Console.WriteLine("Hello World!");
            Stream streamreader = new FileStream("C:/home/database.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite); // порождение обЪекта
            BinaryReader binreader = new BinaryReader(streamreader);
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();
            stopwatch.Start();
            for (long i = 0; i < 1000; i++)
            {
                streamreader.Position = i * 8;
                long num = binreader.ReadInt64();
            }
            stopwatch.Stop();
            Student[] db = GenerateDataBase();
            Console.WriteLine($"RunTime { Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");*/

            Student[] db = GenerateDataBase();
            GenerateBinary(db, "C:/home/sort.bin"); // бинарный
            Stream streamreader = new FileStream("C:/home/sort.bin", FileMode.Open, FileAccess.Read); //порождение обЪекта
            BinaryReader binreader = new BinaryReader(streamreader);            
            long[] offsets = new long[db.Length];
            int[] ages = new int[db.Length];
            string[] names = new string[db.Length];
            streamreader.Position = 0;
            long length = binreader.ReadInt64(); // читается записанная длина массива
            for (int i = 0; i< db.Length ; i++)
            {
                offsets[i] = streamreader.Position;
                string namebin = binreader.ReadString();
                names[i] = namebin;
                int agebin = binreader.ReadInt32();
                ages[i] = agebin;
            }

            for (int i = 0; i < ages.Length; i++)
            {
                Console.WriteLine($"Student {i}");
                Console.WriteLine($"name :{names[i]} age: {ages[i]} offsets : {offsets[i]}");
            }

            Array.Sort(ages, offsets);

            Console.WriteLine("Afer sort ages");

            for (int i = 0; i < ages.Length; i++)
            {
                streamreader.Position = offsets[i];
                string namebin = binreader.ReadString();
                names[i] = namebin;
                int agebin = binreader.ReadInt32();
                ages[i] = agebin;
                Console.WriteLine($"Student {i}");
                Console.WriteLine($"name :{names[i]} age: {ages[i]} offsets : {offsets[i]}");
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            Stream streamwriter = new FileStream("C:/home/diapazon.bin", FileMode.OpenOrCreate, FileAccess.Write); //порождение обЪекта        
            BinaryWriter binwriter = new BinaryWriter(streamwriter);
            Console.WriteLine("Diapazone");
           /* for (int i = Array.IndexOf(ages, 18); i < Array.IndexOf(ages, 24); i++)
            {
                Console.WriteLine($"Student {i}");
                Console.WriteLine($"name :{names[i]} age: {ages[i]} offsets : {offsets[i]}");
                binwriter.Write(5);
                binwriter
            }*/

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Array.Sort(names, offsets);

            Console.WriteLine("After sort names");
            for (int i = 0; i < ages.Length; i++)
            {
                streamreader.Position = offsets[i];
                string namebin = binreader.ReadString();
                names[i] = namebin;
                int agebin = binreader.ReadInt32();
                ages[i] = agebin;
                Console.WriteLine($"Student {i}");
                Console.WriteLine($"name :{names[i]} age: {ages[i]} offsets : {offsets[i]}");               
            }

            
        }
    }
}
