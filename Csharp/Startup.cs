using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;
using Database; //добавил ссылку на проект Database
using XMLTests;
using Microsoft.AspNetCore.Http.Extensions;

namespace Csharp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            SimpleDatabase student = new SimpleDatabase { };
            student.Generate(10);

            XElement html = new XElement("html",
                new XElement("head"),
                new XElement("body",
                new XElement("table",
                new XAttribute("border",1),
                new XElement("tr", student.Elements()
                   .Select(x => new XElement("td",
                   new XElement("a", x.name, new XAttribute("href", x.name+".html")))), 
                   new XElement("tr", student.Elements()
                   .Select(x => new XElement("td", $"{x.age}"))))))); //пробел с помощью div

            var files = Enumerable.Range(0, 10)
                .Select(x => new XDocument(new XElement("html",new XElement ("head", new XElement("body", x))))
               );

            int i = 0;
            foreach (var elem in files){
                elem.Save($"Student{i}.ml");
                    i++;
            }




          
            XDocument doc =new XDocument (new XElement ( "html",
                new XElement("head",
                 new XElement("body",
                new XElement("age", "sljgsljg")))));

            XElement el = new XElement("html",
                 new XElement("head",
                new XElement("body",
                doc.Element("age"))));
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string path = context.Request.Path; //запрос котoрый посылается
                    await context.Response.WriteAsync(html.ToString());
                });
            });
        }
    }
}