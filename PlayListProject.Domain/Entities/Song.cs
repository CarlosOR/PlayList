using System.ComponentModel.DataAnnotations.Schema;

namespace PlayListProject.Domain.Entities
{
    public class Song
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
        public ICollection<PlayListSong> PlayListSongs { get; set; }

    }
}
