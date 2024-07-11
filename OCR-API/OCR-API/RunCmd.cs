using System;
using System.Diagnostics;
using System.IO;

namespace RunCmd
{
    public class RunCmd
    {
        public string Run(string cmd, string args)
        {
            try
            {
                ProcessStartInfo start = new ProcessStartInfo
                {
                    FileName = "python",
                    Arguments = string.Format("\"{0}\" \"{1}\"", cmd, args),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(start))
                {
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        string stderr = process.StandardError.ReadToEnd(); // Read any error output

                        if (!string.IsNullOrEmpty(stderr))
                        {
                            Console.WriteLine("Python Error Output:");
                            Console.WriteLine(stderr);
                        }

                        return result;
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
    }
}
