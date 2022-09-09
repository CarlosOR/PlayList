using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PlayListProject.Domain.Context;
using PlayListProject.Domain.Entities;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;
using PlayListProject.Application.Validations;
using PlayListProject.Application.CustomsExceptions;

namespace PlayListProject.Application.Services
{
    public class PlayListApplicationService : IPlayListApplicationService
    {
        private readonly PlayListProjectDbContext _context;
        private readonly IPlayListSongApplicationService _playListSongApplication;
        private readonly IMapper _mapper;
        public PlayListApplicationService(PlayListProjectDbContext context, IMapper mapper, IPlayListSongApplicationService playListSongApplication)
        {
            _context = context;
            _mapper = mapper;
            _playListSongApplication = playListSongApplication;
        }

        public async Task<IQueryable<DtoPlayList>> GetAllPlayList() =>
             await Task.FromResult(_context.PlayLists
                 .Include(pl => pl.PlayListSongs)
                 .ThenInclude(s => s.Song)
                 .Select(pl => new DtoPlayList
                 {
                     Id = pl.Id,
                     Name = pl.Name,
                     Description = pl.Description,
                     Songs = _mapper.Map<List<DtoSong>>(pl.PlayListSongs.Select(pls => pls.Song).ToList())
                 }));


        public async Task<DtoPlayList> GetPlayList(int id)
        {
            ExistElement(id);
            return await (await GetAllPlayList()).FirstAsync(s => s.Id == id);
        }

        public async Task<DtoPlayList> SavePlaysList(DtoPlayList dtoPlayList)
        {
            ValidateModel(dtoPlayList, false);
            IEnumerable<int> idsSongs = dtoPlayList.Songs.Select(p => p.Id);
            PlayList newPlayList = _mapper.Map<PlayList>(dtoPlayList);
            newPlayList.PlayListSongs = await _playListSongApplication.SaveSongPlayListByIdPlayList(idsSongs, newPlayList.Id);
            await _context.PlayLists.AddAsync(newPlayList);
            await _context.SaveChangesAsync();
            dtoPlayList.Id = newPlayList.Id;
            return dtoPlayList;
        }

        public async Task<List<DtoPlayList>> SavePlaysLists(List<DtoPlayList> playLists)
        {
            playLists.All(pl => ValidateModel(pl, false));
            List<PlayList> newPlayLists = _mapper.Map<List<PlayList>>(playLists);
            int count = 0;
            foreach (PlayList playList in newPlayLists)
            {
                IEnumerable<int> idsSongs = playLists[count].Songs.Select(p => p.Id);
                playList.PlayListSongs = await _playListSongApplication.SaveSongPlayListByIdPlayList(idsSongs, playList.Id);
                count++;
            }

            await _context.PlayLists.AddRangeAsync(newPlayLists);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<DtoPlayList>>(newPlayLists);
        }

        public async Task<DtoPlayList> UpdatePlayList(DtoPlayList dtoPlayList)
        {
            ValidateModel(dtoPlayList);
            ExistElement(dtoPlayList.Id);
            IEnumerable<int> idsSongs = dtoPlayList.Songs.Select(p => p.Id);
            PlayList playList = _mapper.Map<PlayList>(dtoPlayList);
            playList.PlayListSongs = await _playListSongApplication.SaveSongPlayListByIdPlayList(idsSongs, playList.Id);
            _context.PlayListSong.RemoveRange(await ElementsToDelete(idsSongs.ToList(), playList.Id));
            await _context.PlayListSong.AddRangeAsync(playList.PlayListSongs);
            _context.PlayLists.Update(playList);
            await _context.SaveChangesAsync();
            return _mapper.Map<DtoPlayList>(playList);
        }

        public async Task DeletePlayList(int id)
        {
            ExistElement(id);
            _context.PlayLists.Remove(await _context.PlayLists.Include(s => s.PlayListSongs).FirstAsync(pl => pl.Id == id));
            await _context.SaveChangesAsync();
        }

        private async Task<List<PlayListSong>> ElementsToDelete(List<int> idsSongs, int idPlayList)
        {
            return await _playListSongApplication.ElementsThatHaveToDelete(idsSongs,
                id => new PlayListSong
                {
                    PlayListId = idPlayList,
                    SongId = id
                }, pls => pls.PlayListId == idPlayList);
        }

        private bool ExistElement(int id)
        {
            return _context.PlayLists.Any(s => s.Id == id) ? true : throw new NotFoundException();
        }

        private bool ValidateModel(DtoPlayList dtoPlayList, bool validateId = true)
        {
            if (!new ValidatorDtoPlayList(validateId).Validate(dtoPlayList).IsValid)
            {
                throw new BadRequestException();
            }
            if (dtoPlayList.Songs is null)
                dtoPlayList.Songs = new List<DtoSong>();
            return true;
        }

    }
}
