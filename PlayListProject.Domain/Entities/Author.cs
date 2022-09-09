using System.ComponentModel.DataAnnotations.Schema;

namespace PlayListProject.Domain.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Song> Songs { get; set; }
    }
}
