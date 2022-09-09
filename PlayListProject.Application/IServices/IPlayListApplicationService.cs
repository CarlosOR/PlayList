using PlayListProject.Application.Dtos;

namespace PlayListProject.Application.IServices
{
    public interface IPlayListApplicationService
    {
        Task<IQueryable<DtoPlayList>> GetAllPlayList();
        Task<DtoPlayList> GetPlayList(int id);
        Task<List<DtoPlayList>> SavePlaysLists(List<DtoPlayList> playLists);
        Task<DtoPlayList> SavePlaysList(DtoPlayList dtoPlayList);
        Task<DtoPlayList> UpdatePlayList(DtoPlayList dtoPlayList);
        Task DeletePlayList(int id);
    }
}
