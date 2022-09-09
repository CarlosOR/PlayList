
namespace PlayListProject.Domain.Entities
{
    public class PlayListSong
    {
        public int PlayListId {get;set;}
        public PlayList PlayList {get;set;}
        public int SongId {get;set;}
        public Song Song {get;set;}
    }
}
