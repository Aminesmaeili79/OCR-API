using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace OcrMicroservice.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        public OcrController() { }

        [HttpGet("ReadFile")]
        public async Task<IActionResult> ReadFile(string filename, string public_access_key, string secret_access_key)
        {

                var cmdRunner = new RunCmd();
                var scriptPath = "C:\\Users\\Amin\\source\\repos\\OcrMicroservice\\OcrMicroservice\\script\\ocr-script.py"; // Replace with your actual script path
                var imagePath = $"C:\\Users\\Amin\\source\\repos\\OcrMicroservice\\OcrMicroservice\\Images\\{filename}.jpg"; // Replace with your actual image path
                var pak = public_access_key;
                var sak = secret_access_key;

                var result = await cmdRunner.RunAsync(scriptPath, imagePath, pak, sak);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to run Python script.");
                }
        }

        public class RunCmd
        {
            public async Task<string> RunAsync(string cmd, string arg1, string arg2, string arg3)
            {
                try
                {
                    ProcessStartInfo start = new ProcessStartInfo
                    {
                        FileName = "python",
                        Arguments = string.Format("\"{0}\" \"{1}\"", cmd, arg1, arg2, arg3),
                        UseShellExecute = false,
                        CreateNoWindow = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    using (Process process = Process.Start(start))
                    {
                        if (process != null)
                        {
                            string result = await ReadOutputAsync(process);
                            string stderr = await ReadErrorAsync(process);

                            if (!string.IsNullOrEmpty(stderr))
                            {
                                Console.WriteLine("Python Error Output:");
                                Console.WriteLine(stderr);
                            }

                            return result;
                        }
                        else
                        {
                            throw new Exception("Process is null");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while running the Python script:");
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            private async Task<string> ReadOutputAsync(Process process)
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    return await reader.ReadToEndAsync();
                }
            }

            private async Task<string> ReadErrorAsync(Process process)
            {
                using (StreamReader reader = process.StandardError)
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }

    }
}
