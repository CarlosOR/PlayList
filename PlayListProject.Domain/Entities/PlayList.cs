using System.ComponentModel.DataAnnotations.Schema;

namespace PlayListProject.Domain.Entities
{
    public class PlayList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PlayListSong> PlayListSongs { get; set; }

    }
}
