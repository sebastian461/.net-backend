using Backend.Controllers;

namespace Backend.Services
{
    public class PeopleTwoService : IPeopleService
    {
        public bool Validate(People people)
        {
            if(string.IsNullOrEmpty(people.Name) || 
                people.Name.Length > 100 || 
                people.Name.Length <3)
            {
                return false;
            }

            return true;
        }
    }
}
