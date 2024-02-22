namespace Backend.DTOs
{
    public class BeerUpdateDto
    {
        public string Name { get; set; }

        public int BrandId { get; set; }

        public decimal Alcohol { get; set; }
    }
}
