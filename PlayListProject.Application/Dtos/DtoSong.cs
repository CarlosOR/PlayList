
namespace PlayListProject.Application.Dtos
{
    public class DtoSong
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<DtoPlayList> PlayList { get; set; }
    }
}
