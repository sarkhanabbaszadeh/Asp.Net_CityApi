namespace Asp.Net_CityApi.Dtos
{
	public class PhotoForCreationDto
	{
        public PhotoForCreationDto()
        {
            DateAdded=DateTime.Now;
        }
        public string Url { get; set; }

		public IFormFile File { get; set; }

		public string Description { get; set; }

		public DateTime DateAdded { get; set; }

		public string PuplicId { get; set; }
	}
}
