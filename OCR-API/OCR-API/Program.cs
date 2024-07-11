using System.Diagnostics;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

// Running the Python script
// var res = new RunCmd().Run("..\\..\\..\\..\\..\\script\\ocr-script.py", "..\\..\\..\\..\\..\\images\\kimlik1.jpg");
// Console.WriteLine(res);


app.Run();

// public class RunCmd
// {
//     public string Run(string cmd, string args)
//     {
//         ProcessStartInfo start = new ProcessStartInfo();
//         start.FileName = "python";
//         start.Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args);
//         start.UseShellExecute = false;
//         start.CreateNoWindow = true;
//         start.RedirectStandardOutput = true;
//         start.RedirectStandardError = true;
//         using (Process process = Process.Start(start))
//         {
//             using (StreamReader reader = process.StandardOutput)
//             {
//                 string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
//                 string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
//                 return result;
//             }
//         }
//     }
}
