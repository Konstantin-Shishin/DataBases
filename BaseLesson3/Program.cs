using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace BaseLesson3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Writebinarymiion();
            Stopwatch stopwatch = new Stopwatch(); 
            Stream streamreader = new FileStream("C:/home/database.bin", FileMode.Open, FileAccess.Read); // порождение обЪекта
            BinaryReader binreader = new BinaryReader(streamreader);

            stopwatch.Restart();
            stopwatch.Start();
            streamreader.Position = 0;
            for (long i = 0; i < 1000 ; i++)
            {
                streamreader.Position = i * 8;
                long num = binreader.ReadInt64();
            }
            binreader.Close();

            stopwatch.Stop();
            Console.WriteLine($"RunTime { Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");






        }

        private static Stopwatch Writebinarymiion()
        {

            Console.WriteLine("Start data base!");
            Stream stream = new FileStream("C:/home/database.bin", FileMode.OpenOrCreate, FileAccess.Write); // порождение обЪекта
            BinaryWriter binwriter = new BinaryWriter(stream);
            long N = 1000000;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (long i = 0; i < N; i++)
            {
                binwriter.Write(i);
            }
            binwriter.Flush();
            binwriter.Close();
            stopwatch.Stop();

            Console.WriteLine($"RunTime {Convert.ToString(stopwatch.ElapsedMilliseconds)} milliseconds ");
            return stopwatch;
        }
    }
}
