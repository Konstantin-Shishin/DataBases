using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson_HTML
{
    public struct Student // Пришлось сделать public
    {
        public string name { get; set; } //надо с большой буквы

        public int age { get; set; }

        public int id { get; set; }

    }
    public class SimpleDatabase
    {
        public List<Student> students;
        public SimpleDatabase()
        {
            students = new List<Student>();
        }
        public void Generate(int n_elements)
        {
            students = Enumerable.Range(0, n_elements)
                .Select(i => new Student() { age = 1, name = "Student" + i, id = i })
                .ToList();
        }
        public void Print()
        {
            Console.WriteLine($"Records:{students.Count}");
            for (int i = 0; i < students.Count; i++)
            {

                Console.WriteLine($"name: {students[i].name} age : {students[i].age}");
            }
        }
        public IEnumerable<Student> Elements()
        {

            for (int i = 0; i < students.Count; i++)
            {
                yield return students[i];
            }
        }

    }
}
