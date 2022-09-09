
using PlayListProject.Application.Dtos;

namespace PlayListProject.Application.IServices
{
    public interface ISongApplicationService
    {
        Task<IQueryable<DtoSong>> GetAllSongs();
        Task<DtoSong> GetSong(int id);
        Task<List<DtoSong>> SaveSongs(List<DtoSong> dtoSongs);
        Task<DtoSong> SaveSong(DtoSong dtoSong);
        Task<DtoSong> UpdateSong(DtoSong dtoSong);
        Task DeleteSong(int id);
    }
}
