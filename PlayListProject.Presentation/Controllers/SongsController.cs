using Microsoft.AspNetCore.Mvc;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;

namespace PlayListProject.Presentation.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SongsController: ControllerBase
    {
        private readonly ISongApplicationService _songApplicationService;
        public SongsController(ISongApplicationService songApplicationService)
        {
            _songApplicationService = songApplicationService;
        }

        [HttpGet(nameof(SongsController.GetAll))]
        public async Task<IEnumerable<DtoSong>> GetAll()
        {
            return await _songApplicationService.GetAllSongs();
        }

        [HttpGet(nameof(SongsController.Get) + "/{id}")]
        public async Task<DtoSong> Get(int id)
        {
            return await _songApplicationService.GetSong(id);
        }

        [HttpPost(nameof(SongsController.Add))]
        public async Task<DtoSong> Add(DtoSong dtoSong)
        {
            return await _songApplicationService.SaveSong(dtoSong);
        }

        [HttpPost(nameof(SongsController.AddList))]
        public async Task<IEnumerable<DtoSong>> AddList(List<DtoSong> dtoSongs)
        {
            return await _songApplicationService.SaveSongs(dtoSongs);
        }

        [HttpPut(nameof(SongsController.Update))]
        public async Task<DtoSong> Update(DtoSong dtoSongs)
        {
            return await _songApplicationService.UpdateSong(dtoSongs);
        }

        [HttpDelete(nameof(SongsController.Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            await _songApplicationService.DeleteSong(id);
            return Ok();
        }
    }
}
