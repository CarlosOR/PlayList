using Microsoft.AspNetCore.Mvc;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;

namespace PlayListProject.Presentation.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorApplicationService _authorApplicationService;
        public AuthorController(IAuthorApplicationService authorApplicationService)
        {
            _authorApplicationService = authorApplicationService;
        }

        [HttpGet(nameof(AuthorController.Get))]
        public async Task<IEnumerable<DtoAuthor>> Get()
        {
            return await _authorApplicationService.GetAllAuthors();
        }

        [HttpPost(nameof(AuthorController.Add))]
        public async Task<DtoAuthor> Add(DtoAuthor dtoAuthor)
        {
            return await _authorApplicationService.SaveAuthor(dtoAuthor);
        }

        [HttpPost(nameof(AuthorController.AddList))]
        public async Task<IEnumerable<DtoAuthor>> AddList(List<DtoAuthor> dtoAuthors)
        {
            return await _authorApplicationService.SaveAuthors(dtoAuthors);
        }

        [HttpPut(nameof(AuthorController.Update))]
        public async Task<DtoAuthor> Update(DtoAuthor dtoAuthors)
        {
            return await _authorApplicationService.UpdateAuthor(dtoAuthors);
        }

        [HttpDelete(nameof(AuthorController.Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorApplicationService.DeleteAuthor(id);
            return Ok();
        }
    }
}
