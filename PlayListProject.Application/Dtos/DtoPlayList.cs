using PlayListProject.Domain.Entities;

namespace PlayListProject.Application.Dtos
{
    public record DtoPlayList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DtoSong> Songs { get; set; }
    }
}
