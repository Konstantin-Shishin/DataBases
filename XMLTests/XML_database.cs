using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //потоки элементов
using System.Xml.Linq;
using Database;


namespace XMLTests
{
    class XML_database
    {
        static void Main(string[] args)
        {
            DatabaseToXML();
            //StartDatabaseXML();
        }

        public static void DatabaseToXML()
        {
            SimpleDatabase student = new SimpleDatabase { };
            student.Generate(10);
            string db = "";
            for (int k = 0; k < 10; k++)
            {
                db += student.students[k].name;
                db += ",";
                db += student.students[k].age.ToString();
                db += " ";
            }

            Console.WriteLine(db[10]);
            db = db.TrimEnd(); //Удаляем пробел в концк строки


            var flow = db.Split(' ').
                Select(student => new XElement("student", new XElement("name", student.Split(',')[0]), new XElement("age", student.Split(',')[1])));
            XElement dbxml = new XElement("dbxml", flow);
            Console.WriteLine(dbxml.ToString());
        }

        private static void StartDatabaseXML()
        {
            string personnames = "Иванов Петров Сидоров";
            var flow = personnames.Split(' ')
                .Select(name => new XElement("person", new XElement("name", name)));
            XElement db = new XElement("db", flow);

            Console.WriteLine(db.ToString()); // Пустое вложение у MyElement

            //Сдеаем выборку с помощью линк 
            var query = db.Elements()
                .Where(elem => elem.Element("name").Value == "Петров");
            foreach (var el in query)
            {
                Console.WriteLine(el.ToString());
            }
        }

        public static void StartXML()
        {
            Console.WriteLine("XML!");
            XElement element = new XElement("MyElement",
                new XAttribute("att1", "val1"),
                new XElement("Elem2"),
                "Text : Hello!");
            Console.WriteLine(element);
        }
    }
}
