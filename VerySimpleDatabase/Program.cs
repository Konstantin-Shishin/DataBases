using System;
using System.Runtime.CompilerServices;

namespace VerySimpleDatabase
{
    class Program
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
        public static void FindMinAge(Student[] arrstudent) // Самый молодой студент
        {
            int age = arrstudent[0].age;
            int key = 0; // номер нужной структуры
            for (int i = 1; i < arrstudent.Length; i++)
            {
                if (age > arrstudent[i].age)
                {
                    age = arrstudent[i].age;
                    key = i;
                }
            }
            for (int i = 0; i < arrstudent.Length; i++)
            {
                if (i == key) {
                    Console.WriteLine("The youngest student:");
                    GetInfo(arrstudent[i]); 
                }
            }
        }
        public static void FindMaxAge(Student[] arrstudent) // Самый cтарший студент
        {
            int age = arrstudent[0].age;
            int key = 0; // номер нужной структуры 
            for (int i = 1; i < arrstudent.Length; i++)
            {
                if (age < arrstudent[i].age)
                {
                    age = arrstudent[i].age;
                    key = i;
                }
            }
            for (int i = 0; i < arrstudent.Length; i++)
            {
                if (i == key)
                {
                    Console.WriteLine("The oldest student:");
                    GetInfo(arrstudent[i]);
                }
            }
        }
        public static void FindByString(Student[] arrstudent, string str) // Поиск по подстроке с начала
        {
            for (int i = 0; i < arrstudent.Length; i++)
            {
                if (arrstudent[i].name.StartsWith(str)) {
                    Console.WriteLine($"After FindByString {str} :");
                    GetInfo(arrstudent[i]); }
            }
        }

        public static void FindByName(Student[] arrstudent, string str) // Найти по имени
        {
            for (int i = 0; i < arrstudent.Length; i++)
            {
                if (arrstudent[i].name == str) {
                    Console.WriteLine($"Find {str}:");
                    GetInfo(arrstudent[i]); 
                }
            }
        }

        public static void SortByAge(Student[] arrstudent) // Упорядочить по возрасту начиная с младшего
        {
            int tmp_age;
            string tmp_name;
            for (int i = 0; i < arrstudent.Length - 1; i++)
            {
                for (int j = i+1; j < arrstudent.Length; j++ ) { // сравнивает и'тый элемент со всеми до конца
                    if (arrstudent[i].age > arrstudent[j].age)
                    {
                        tmp_age = arrstudent[j].age;
                        tmp_name = arrstudent[j].name;
                        arrstudent[j] = arrstudent[i];
                        arrstudent[i].age = tmp_age;
                        arrstudent[i].name = tmp_name;
                    }
                }
            }
        }
        
        public static void SortByAlphabet(Student[] arrstudent) // Упорядочить по алфавиту
        {
            int tmp_age;
            string tmp_name;
            for (int i = 0; i < arrstudent.Length - 1; i++)
            {
                for(int j = i+1; j< arrstudent.Length; j++)
                {
                        
                        
                        if (arrstudent[i].name[0] > arrstudent[j].name[0])
                        {
                        tmp_name= arrstudent[j].name;
                        tmp_age = arrstudent[j].age;
                        arrstudent[j].name = arrstudent[i].name;
                        arrstudent[j].age = arrstudent[i].age;
                        arrstudent[i].name = tmp_name;
                        arrstudent[i].age = tmp_age;                       
                        }  

                }
            }
        }

        public static void SortByAlphabet1(Student[] arrstudent) // Упорядочить по алфавиту
        {
            int tmp_age;
            string tmp_name;
            for (int i = 0; i < arrstudent.Length - 1; i++)
            {
                for (int j = i + 1; j < arrstudent.Length; j++)
                {

                    for (int s = 0; s < MinSymbols(arrstudent[i].name, arrstudent[j].name).Length; s++) { 
                    if (arrstudent[i].name[s] > arrstudent[j].name[s])
                    {
                        tmp_name = arrstudent[j].name;
                        tmp_age = arrstudent[j].age;
                        arrstudent[j].name = arrstudent[i].name;
                        arrstudent[j].age = arrstudent[i].age;
                        arrstudent[i].name = tmp_name;
                        arrstudent[i].age = tmp_age;
                            break;
                    }

                  }
                }
            }
        }

        public static void AddStudent(Student[] arrstudent, string name, int age) // добавить студента в конец
        {
            arrstudent[arrstudent.Length - 1] = new Student();
            arrstudent[arrstudent.Length - 1].name = name;
            arrstudent[arrstudent.Length - 1].age = age;
        }

        public static string MinSymbols(string str1, string str2) // Возвращает строку с наименьшим кол-вом символов
        {
            if (str1.Length < str2.Length) return str1;
            return str2;
        }


        static void Main(string[] args)
        {
            Student[] db = new Student[] { new Student { name = "Tom", age = 22 }, new Student { name = "John", age = 20 }, new Student { name = "Zack", age = 26 }, new Student { name = "Kate", age = 18 }, new Student { name = "Jordan", age = 29 } };
            Console.WriteLine($"Количество студентов : {db.Length}");
            FindMaxAge(db);            
            FindMinAge(db);
            FindByName(db, "John");         
            FindByString(db, "Jo");           
            SortByAge(db);
            Console.WriteLine("After SortByAge:");
            for (int i = 0; i < db.Length; i++) GetInfo(db[i]);
            SortByAlphabet(db);
            Console.WriteLine("After SortByAlphabet:");
            for (int i = 0; i < db.Length; i++) GetInfo(db[i]);
            AddStudent(db, "Sam", 38);
            Console.WriteLine("After AddStudent:");
            for (int i = 0; i < db.Length; i++) GetInfo(db[i]);
            SortByAlphabet(db);
            Console.WriteLine("After SortByAlphabet:");
            for (int i = 0; i < db.Length; i++) GetInfo(db[i]);
        }
    }
}
