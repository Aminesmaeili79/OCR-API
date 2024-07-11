using Microsoft.AspNetCore.Mvc;
using RunCmd; // Ensure this matches the namespace used in RunCmd.cs

[Route("api/[controller]")]
[ApiController]
public class PythonController : ControllerBase
{
    [HttpGet("run-script")]
    public IActionResult RunPythonScript()
    {
        try
        {
            var cmdRunner = new RunCmd();
            var scriptPath = "..\\..\\..\\..\\..\\script\\ocr-script.py"; // Replace with your actual script path
            var imagePath = "..\\..\\..\\..\\..\\images\\kimlik.jpg"; // Replace with your actual image path

            var result = cmdRunner.Run(scriptPath, imagePath);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to run Python script.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
}
