using System;
using System.IO;

namespace ProjectsForStudents
{
    class test
    {
        /// <summary>
        /// Программа проверки работы кеша при работе с большими данными
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            Random rnd = new Random();
            Console.WriteLine("Very Simple Database access!");
            Stream stream = new FileStream(@"D:\home\data\VerySimpleDatabase\largefile.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            int nrecords = 100_000_000;

            bool tobuild = false;
            if (tobuild)
            {
                sw.Restart();
                stream.Position = 0L;
                BinaryWriter bw = new BinaryWriter(stream);
                for (int i = 0; i < nrecords; i++)
                {
                    bw.Write((long)i);
                }
                bw.Flush();
                sw.Stop();
                Console.WriteLine($"Write {nrecords} records ok. duration={sw.ElapsedMilliseconds} ms");
            }

            int ntests = 1000;
            BinaryReader br = new BinaryReader(stream);
            sw.Restart();
            for (int i = 0; i < ntests; i++)
            {
                int nom = rnd.Next(nrecords);
                stream.Position = (long)nom * 8L;
                long value = br.ReadInt64();
                if (value != nom) throw new Exception($"Err: different nom={nom} and value={value}");
            }
            sw.Stop();
            Console.WriteLine($"Access to {ntests} of {nrecords} records ok. duration={sw.ElapsedMilliseconds} ms");

            // ===== Результаты nrecords=100_000_000 ntests=1000 =====
            // tobuild=true; Время записи 5620 мс., время 1000 чтений 26 мс
            // tobuild= false; Время 1000 чтений 10 мс
            // перезагрузка компьютера
            // tobuild= false; Время 1000 чтений 9934 мс
            // скопировал файл проводнике
            // tobuild= false; Время 1000 чтений 8 мс
        }
    }
}
