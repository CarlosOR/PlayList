using PlayListProject.Domain.Context;
using PlayListProject.Domain.Entities;
using PlayListProject.Application.IServices;

namespace PlayListProject.Application.Services
{
    public class PlayListSongApplicationService : IPlayListSongApplicationService
    {
        private readonly PlayListProjectDbContext _context;
        public PlayListSongApplicationService(PlayListProjectDbContext context)
        {
            _context = context;
        }


        public async Task<List<PlayListSong>> SaveSongPlayListByIdSong(IEnumerable<int> idsPlayLists, int idSong)
        {
            List<PlayListSong> playListSongs = idsPlayLists.Select(id => new PlayListSong
            {
                PlayListId = id,
                SongId = idSong
            }).ToList();

            if (idSong != 0)
                return await AvoidItemsRepeated(playListSongs, pls => pls.SongId == idSong);
            return playListSongs;
        }

        public async Task<List<PlayListSong>> SaveSongPlayListByIdPlayList(IEnumerable<int> idsSong, int idPlayList)
        {
            List<PlayListSong> playListSongs = idsSong.Select(id => new PlayListSong
            {
                PlayListId = idPlayList,
                SongId = id
            }).ToList();

            if (idPlayList != 0)
                return await AvoidItemsRepeated(playListSongs, pls => pls.PlayListId == idPlayList);
            return playListSongs;
        }

        private async Task<List<PlayListSong>> AvoidItemsRepeated(List<PlayListSong> playListSongs, Func<PlayListSong, bool> condition)
        {
            List<PlayListSong> itemsAlreadyExist = _context.PlayListSong.Where(condition)
                            .AsEnumerable()
                            .Where(pls => playListSongs.Any(pl => pls.PlayListId == pl.PlayListId && pls.SongId == pl.SongId))
                            .ToList();

            playListSongs = playListSongs.Where(pls =>
                !itemsAlreadyExist.Any(pl => pls.PlayListId == pl.PlayListId && pls.SongId == pl.SongId)
                ).ToList();

            return await Task.FromResult(playListSongs);
        }

        public async Task<List<PlayListSong>> ElementsThatHaveToDelete(List<int> ids, Func<int, PlayListSong> selector, Func<PlayListSong, bool> condition)
        {
            List<PlayListSong> playListSongs = ids.Select(selector).ToList();

            return await Task.FromResult(_context.PlayListSong.Where(condition)
                            .AsEnumerable()
                            .Where(pls => !playListSongs.Any(pl => pls.PlayListId == pl.PlayListId && pls.SongId == pl.SongId))
                            .ToList());
        }
    }
}
