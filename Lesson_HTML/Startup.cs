using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lesson_HTML
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

            SimpleDatabase students = new SimpleDatabase { };
            students.Generate(10);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/{controller=home}/{action=get}/{id?}", async context => // контроллер/действие/параметр
                {

                    string id = (string)context.Request.RouteValues["id"];
                    XElement html = new XElement("html",
                        new XElement("body", "error pagge")
                        );

                    if (id == null)
                    {
                    html = new XElement("html",
                    new XElement("head",
                    new XElement("body",
                    new XElement("table",
                    new XAttribute("border", 1),
                    new XElement("tr", students.Elements()
                       .Select(x => new XElement("td",
                       new XElement("a", x.name, new XAttribute("href", $"/home/get/{x.id}")))),
                       new XElement("tr", students.Elements()

                       .Select(x => new XElement("td", $"{x.age}"))))))));
                    }
                    else
                    {
                        var student = students.Elements().Where(x => x.id.ToString() == id)
                        .First();

                        html = new XElement("html",
                            new XElement("body",
                            new XElement("span", $"id={student.id}"),
                            new XElement("span", $"name = {student.name}"),
                            new XElement("a", "back", new XAttribute("href", "/home/get")))
                            );
                    }

                    await context.Response.WriteAsync(html.ToString());
                }

                );
            });
        }
    }
}
