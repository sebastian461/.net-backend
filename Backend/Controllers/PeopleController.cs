using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        /* Esto está mal, porque si la clase cambia la manera en la que se construye
         * se debe actualizar en cada lugar que se haga uso de la clase
         * por eso se hace uso de la inyección de dependecias en el archivo "Program"
        public PeopleController()
        {
            _peopleService = new PeopleService();
        }*/

        /* Este sería el construcctor con el uso de una dependencia sin key
         * C# internamente sabe que debe hacer uso de la inyección de dependencias
        public PeopleController(IPeopleService peopleService) 
        {
            _peopleService = peopleService;
        } */

        //Constructor con el uso de dependencia con una key
        public PeopleController([FromKeyedServices("peopleService")]IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        /* Este metodo retorna un objeto si es que lo encuentra, sino lo encuentra puede lanzar una expecíon
        [HttpGet("{id}")]
        public People Get(int id) => Repository.People.First(p => p.Id == id);*/

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == id); //First devuelve el objeto, FirstOrDefault devuelve un objeto o un null

            if (people == null)
            {
                return NotFound(); //Esto retorna un http 4xx
            }

            return Ok(people); //200 con el objeto
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) => 
            Repository.People.Where(p => p.Name.ToLower().Contains(search.ToLower())).ToList();

        [HttpPost]
        public IActionResult Add(People people) 
        {
            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();
        }
    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,
                Name = "Sebastián",
                Birthdate = new DateTime(1998, 11, 27)
            },
            new People()
            {
                Id = 2,
                Name = "Omar",
                Birthdate = new DateTime(1997, 11, 03)
            },
            new People()
            {
                Id = 3,
                Name = "Stalin",
                Birthdate = new DateTime(2000, 05, 27)
            }
        };
    }

    public class People
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
