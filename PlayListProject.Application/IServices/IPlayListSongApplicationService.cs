using PlayListProject.Domain.Entities;

namespace PlayListProject.Application.IServices
{
    public interface IPlayListSongApplicationService
    {
        /// <summary>
        /// Returns the list of PlayListSong to be added
        /// </summary>
        /// <param name="idsSong">Playlist ids</param>
        /// <param name="idPlayList">id of the song</param>
        /// <returns></returns>
        Task<List<PlayListSong>> SaveSongPlayListByIdSong(IEnumerable<int> idsPlayLists, int idSong);
        /// <summary>
        /// Returns the list of PlayListSong to be added
        /// </summary>
        /// <param name="idsSong">Songs ids</param>
        /// <param name="idPlayList">id of the playlist</param>
        /// <returns></returns>
        Task<List<PlayListSong>> SaveSongPlayListByIdPlayList(IEnumerable<int> idsSong, int idPlayList);
        /// <summary>
        /// Create a list of items that have to delete
        /// </summary>
        /// <param name="ids">Ids List</param>
        /// <param name="selector">Selector to iterate the ids list</param>
        /// <param name="condition">Condition to validate the elements to be deleted</param>
        /// <returns></returns>
        Task<List<PlayListSong>> ElementsThatHaveToDelete(List<int> ids, Func<int, PlayListSong> selector, Func<PlayListSong, bool> condition);
    }
}