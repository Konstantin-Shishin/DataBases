using System;
using System.Linq;

namespace Database
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SimpleDatabase sb = new SimpleDatabase();
            sb.Generate(20);
           // sb.students[0].name = "Hello World!";
            sb.Generate(20);
            var query = sb.Elements()
                .Skip(3)
                .Take(2);
            foreach ( var el in query)
            {
                Console.WriteLine($"name: {el.name} age:{el.age}");
            }
        }
    }
}
