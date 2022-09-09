using Moq;
using PlayListProject.Domain.Entities;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.Services;
using PlayListProject.Application.IServices;
using Microsoft.EntityFrameworkCore;

namespace PlayListProject.Test.Application.Songs
{
    public class TestSongApplicationService : BaseTest
    {
        private readonly SongApplicationService _songApplicationService;
        private readonly Mock<IPlayListSongApplicationService> moqPlayListSongApplication = new Mock<IPlayListSongApplicationService>();
        public TestSongApplicationService()
        {
            _songApplicationService = new SongApplicationService(_context, _mapper, moqPlayListSongApplication.Object);
        }

        [Fact]
        public async Task ShoudRetournAllSongsAndTransformData()
        {
            List<DtoSong> songs = (await _songApplicationService.GetAllSongs()).ToList();
            var x = _context.Songs.ToList();
            Assert.True(songs.Count > 0);
            Assert.Equal(_context.Songs.Count(), songs.Count);
            Assert.Contains(songs, (s => s.PlayList != null && s.PlayList.Count > 0));
            Assert.Contains(songs, (s => !string.IsNullOrEmpty(s.AuthorId.ToString()) && !string.IsNullOrEmpty(s.AuthorName.ToString())));
        }

        [Fact]
        public async Task ShoudRetournOneSongAndTransformData()
        {
            int idSong = _context.PlayListSong.First().SongId;
            DtoSong song = await _songApplicationService.GetSong(idSong);
            Assert.Equal(idSong, song.Id);
            Assert.NotNull(song.AuthorId);
            Assert.False(string.IsNullOrEmpty(song.AuthorName));
            Assert.NotNull(song.PlayList);
        }

        [Fact]
        public async Task ShouldDeleteSong()
        {
            int initualCount = _context.Songs.Count();
            int idSong = _context.Songs.First().Id;
            await _songApplicationService.DeleteSong(idSong);
            Assert.Null(_context.Songs.Find(idSong));
            Assert.True((initualCount - 1) == _context.Songs.Count());
        }

        [Fact]
        public async Task ShouldUpdateSongAndPlayList()
        {
            int idSong = _context.Songs.First().Id;
            string newName = "new name";
            Song song = _context.Songs
                .Include(s => s.Author)
                .Include(s => s.PlayListSongs)
                .ThenInclude(s => s.PlayList)
                .First(s => s.Id == idSong);
            _context.ChangeTracker.Clear();
            DtoSong dtoSong = _mapper.Map<DtoSong>(song);
            dtoSong.Name = newName;
            dtoSong.PlayList = song.PlayListSongs.Select(pls => _mapper.Map<DtoPlayList>(pls.PlayList)).ToList();
            dtoSong.PlayList.Clear();
            DtoSong updatedSong = await _songApplicationService.UpdateSong(dtoSong);
            Assert.Equal(newName, updatedSong.Name);
            Assert.Equal(0, _context.Songs.Find(idSong)?.PlayListSongs.Count);
        }

        [Fact]
        public async Task ShouldUpdateSong()
        {
            int idSong = _context.Songs.First().Id;
            string newName = "new name";
            Song song = _context.Songs
                .Include(s => s.Author)
                .Include(s => s.PlayListSongs)
                .ThenInclude(s => s.PlayList)
                .First(s => s.Id == idSong);
            _context.ChangeTracker.Clear();
            List<int> idsPlayList = song.PlayListSongs.Select(pls => pls.PlayList).Select(pl => pl.Id).ToList();
            int newIdPlayList = _context.PlayLists.First(pl => !idsPlayList.Contains(pl.Id)).Id;
            DtoSong dtoSong = _mapper.Map<DtoSong>(song);
            dtoSong.Name = newName;
            dtoSong.PlayList = song.PlayListSongs.Select(pls => _mapper.Map<DtoPlayList>(pls.PlayList)).ToList();
            dtoSong.PlayList.Add(new DtoPlayList
                {
                        Id = newIdPlayList
                });
            moqPlayListSongApplication.Setup(plss => plss.SaveSongPlayListByIdSong(It.IsAny<IEnumerable<int>>(), idSong))
                .ReturnsAsync(
                new List<PlayListSong>
                {
                    new PlayListSong
                    {
                        PlayListId =  newIdPlayList,
                        SongId = idSong
                    }
                });
            DtoSong updatedSong = await _songApplicationService.UpdateSong(dtoSong);
            Assert.Equal(newName, updatedSong.Name);
            Assert.Equal(idsPlayList.Count, _context.Songs.Find(idSong)?.PlayListSongs.Count);
        }
    }
}
