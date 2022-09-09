using AutoMapper;
using PlayListProject.Domain.Context;
using PlayListProject.Domain.Entities;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;
using Microsoft.EntityFrameworkCore;
using PlayListProject.Application.CustomsExceptions;
using PlayListProject.Application.Validations;

namespace PlayListProject.Application.Services
{
    public class SongApplicationService : ISongApplicationService
    {
        private readonly PlayListProjectDbContext _context;
        private readonly IPlayListSongApplicationService _playListSongApplication;
        private readonly IMapper _mapper;
        public SongApplicationService(PlayListProjectDbContext context, IMapper mapper, IPlayListSongApplicationService playListSongApplication)
        {
            _context = context;
            _mapper = mapper;
            _playListSongApplication = playListSongApplication;
        }

        public async Task DeleteSong(int id)
        {
            ExistElement(id);
            _context.Songs.Remove(await _context.Songs.Include(s => s.PlayListSongs).FirstAsync(s => s.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<DtoSong>> GetAllSongs() =>
            await Task.FromResult(_context.Songs
                .Include(s => s.Author)
                .Include(s => s.PlayListSongs)
                .ThenInclude(ps => ps.PlayList)
                .Select(s => new DtoSong
                {
                    Id = s.Id,
                    Name = s.Name,
                    AuthorId = s.Author == null ? null : s.Author.Id,
                    AuthorName = s.Author == null ? string.Empty : s.Author.Name,
                    PlayList = _mapper.Map<List<DtoPlayList>>(s.PlayListSongs.Select(pls => pls.PlayList).ToList())
                }));


        public async Task<DtoSong> GetSong(int id)
        {
            ExistElement(id);
            return await (await GetAllSongs()).FirstAsync(s => s.Id == id);
        }

        public async Task<List<DtoSong>> SaveSongs(List<DtoSong> dtoSongs)
        {
            dtoSongs.All(s => ValidateModel(s, false));
            List<Song> songs = _mapper.Map<List<Song>>(dtoSongs);
            int count = 0;
            foreach (Song song in songs)
            {
                IEnumerable<int> idsPlayLists = dtoSongs[count].PlayList.Select(p => p.Id);
                song.PlayListSongs = await _playListSongApplication.SaveSongPlayListByIdSong(idsPlayLists, song.Id);
                count++;
            }
            await _context.Songs.AddRangeAsync(songs);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<DtoSong>>(songs);
        }

        public async Task<DtoSong> SaveSong(DtoSong dtoSong)
        {
            ValidateModel(dtoSong, false);
            IEnumerable<int> idsPlayLists = dtoSong.PlayList.Select(p => p.Id);
            dtoSong.PlayList = new List<DtoPlayList>();
            Song song = _mapper.Map<Song>(dtoSong);
            song.PlayListSongs = await _playListSongApplication.SaveSongPlayListByIdSong(idsPlayLists, song.Id);
            await _context.Songs.AddAsync(song);
            await _context.SaveChangesAsync();
            return _mapper.Map<DtoSong>(song);
        }


        public async Task<DtoSong> UpdateSong(DtoSong dtoSong)
        {
            ValidateModel(dtoSong);
            ExistElement(dtoSong.Id);
            IEnumerable<int> idsPlayLists = dtoSong.PlayList.Select(p => p.Id);
            Song song = _mapper.Map<Song>(dtoSong);
            song.PlayListSongs = await _playListSongApplication.SaveSongPlayListByIdSong(idsPlayLists, song.Id);
            _context.PlayListSong.RemoveRange(await ElementsToDelete(idsPlayLists.ToList(), song.Id));
            await _context.PlayListSong.AddRangeAsync(song.PlayListSongs);
            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
            return _mapper.Map<DtoSong>(song);
        }

        private async Task<List<PlayListSong>> ElementsToDelete(List<int> idsPlayLists, int idSong)
        {
            return await _playListSongApplication.ElementsThatHaveToDelete(idsPlayLists,
                id => new PlayListSong
                {
                    PlayListId = id,
                    SongId = idSong
                }, pls => pls.SongId == idSong);
        }

        private bool ExistElement(int id)
        {
            return _context.Songs.Any(s => s.Id == id) ? true : throw new NotFoundException();
        }

        private bool ValidateModel(DtoSong dtoSong, bool validateId = true)
        {
            if (!new ValidatorDtoSong(validateId).Validate(dtoSong).IsValid)
            {
                throw new BadRequestException();
            }
            if (dtoSong.PlayList is null)
                dtoSong.PlayList = new List<DtoPlayList>();
            return true;
        }

    }
}
