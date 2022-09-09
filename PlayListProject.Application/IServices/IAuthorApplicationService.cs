using PlayListProject.Application.Dtos;

namespace PlayListProject.Application.IServices
{
    public interface IAuthorApplicationService
    {
        Task<List<DtoAuthor>> GetAllAuthors();
        Task<DtoAuthor> GetAuthor(int id);
        Task<List<DtoAuthor>> SaveAuthors(List<DtoAuthor> dtoAuthors);
        Task<DtoAuthor> SaveAuthor(DtoAuthor dtoAuthor);
        Task<DtoAuthor> UpdateAuthor(DtoAuthor dtoAuthor);
        Task DeleteAuthor(int id);
    }
}
