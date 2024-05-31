using MinimalWebApiDemo;

namespace MinimalWebApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            List<Student> students = new List<Student>();
            
            app.MapGet("/Student", ()=>
            {
                return students;
            });

            app.MapGet("/Student/{id}", (int id) =>
            {
                return students.FirstOrDefault(x => x.Id == id);
            });

            app.MapPost("Student", (Student student) =>
            {
                students.Add(student);
            });

            app.MapPut("/Students/{id}", (int id, Student student) =>
            {
                int index = students.FindIndex(x => x.Id == id);
                if(index > 0)
                    students[index] = student;
            });

            app.MapDelete("/Students/{id}", (int id) =>
            {
                students.RemoveAll(x => x.Id == id);
            });
            app.Run();
        }
    }
}

