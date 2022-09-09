using AutoMapper;
using PlayListProject.Domain.Context;
using PlayListProject.Domain.Entities;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;

namespace PlayListProject.Application.Services
{
    public class AuthorApplicationService : IAuthorApplicationService
    {
        private readonly PlayListProjectDbContext _context;
        private readonly IMapper _mapper;
        public AuthorApplicationService(PlayListProjectDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; 
        }

        public async Task DeleteAuthor(int id)
        {
            ExistElement(id);
            _context.Authors.Remove(await _context.Authors.FindAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<DtoAuthor>> GetAllAuthors() =>
            await Task.FromResult(_mapper.Map<List<DtoAuthor>>(_context.Authors.ToList()));

        public async Task<DtoAuthor> GetAuthor(int id) =>
            _mapper.Map<DtoAuthor>(await _context.Authors.FindAsync(id));

        public async Task<DtoAuthor> SaveAuthor(DtoAuthor dtoAuthor)
        {
            Author author = _mapper.Map<Author>(dtoAuthor);
            await _context.Authors.AddRangeAsync(author);
            await _context.SaveChangesAsync();
            return _mapper.Map<DtoAuthor>(author);
        }

        public async Task<List<DtoAuthor>> SaveAuthors(List<DtoAuthor> dtoAuthors)
        {
            List<Author> authors = _mapper.Map<List<Author>>(dtoAuthors);
            await _context.Authors.AddRangeAsync(authors);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<DtoAuthor>>(authors);
        }

        public async Task<DtoAuthor> UpdateAuthor(DtoAuthor dtoAuthor)
        {
            ExistElement(dtoAuthor.Id);
            Author author = _mapper.Map<Author>(dtoAuthor);
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return _mapper.Map<DtoAuthor>(author);
        }

        private bool ExistElement(int id)
        {
            return _context.Authors.Any(s => s.Id == id) ? true : throw new Exception();
        }
    }
}
