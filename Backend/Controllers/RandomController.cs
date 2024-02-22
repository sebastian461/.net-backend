using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomServiceSingleton;
        private IRandomService _randomServiceScoped;
        private IRandomService _randomServiceTransient;

        private IRandomService _randomServiceTwoSingleton;
        private IRandomService _randomServiceTwoScoped;
        private IRandomService _randomServiceTwoTransient;

        public RandomController([FromKeyedServices("randomSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("randomSingleton")] IRandomService randomServiceTwoSingleton,
            [FromKeyedServices("randomScoped")] IRandomService randomServiceScoped,
            [FromKeyedServices("randomScoped")] IRandomService randomServiceTwoScoped,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTransient,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTwoTransient)
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScoped = randomServiceScoped;
            _randomServiceTransient = randomServiceTransient;

            _randomServiceTwoSingleton = randomServiceTwoSingleton;
            _randomServiceTwoScoped = randomServiceTwoScoped;
            _randomServiceTwoTransient = randomServiceTwoTransient;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get() 
        {
            var result = new Dictionary<string, int>();

            result.Add("Singleton 1 ", _randomServiceSingleton.Value);
            result.Add("Scoped 1", _randomServiceScoped.Value);
            result.Add("Transient 1", _randomServiceTransient.Value);

            result.Add("Singleton 2 ", _randomServiceTwoSingleton.Value);
            result.Add("Scoped 2", _randomServiceTwoScoped.Value);
            result.Add("Transient 2", _randomServiceTwoTransient.Value);

            return result;
        }

    }
}
