namespace Asp.Net_CityApi.Dtos
{
	public class PhotoForReturnDto
	{
		public string Id { get; set; }

		public string Url { get; set; }

		public string Description { get; set; }

		public DateTime DateAdded { get; set; }

		public bool IsMain { get; set; }

		public string PublicID { get; set; }

	}
}
