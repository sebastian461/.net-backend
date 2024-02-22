using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync() //Método sincrono que retorna el tiempo que demora en procesarse
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexioón a base de datos exitosa!");

            Thread.Sleep(1000);
            Console.WriteLine("Envío de mail exitoso!");

            Console.WriteLine("Todo ha terminado");

            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync() //Definición de un método asíncrono
        {
            var task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexioón a base de datos exitosa!");
            });

            task1.Start();

            Console.WriteLine("Otra cosa"); ;

            await task1;

            Console.WriteLine("Todo ha terminado");

            return Ok();
        }
    }
}
