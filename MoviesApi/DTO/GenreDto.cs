namespace MoviesApi.DTO
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
