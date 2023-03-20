namespace MovieApp.Api.Data.DTOs
{
    /// <summary>
    /// User friendly version of the Actor object
    /// </summary>
    public class ActorDto
    {
        public ActorDto()
        {
            Movies = new List<string>();
        }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public List<string> Movies { get; set; }
    }
}
