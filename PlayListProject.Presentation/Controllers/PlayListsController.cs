using Microsoft.AspNetCore.Mvc;
using PlayListProject.Application.Dtos;
using PlayListProject.Application.IServices;

namespace PlayListProject.Presentation.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PlayListsController : ControllerBase
    {
        private readonly IPlayListApplicationService _listApplicationService;
        public PlayListsController(IPlayListApplicationService listApplicationService)
        {
            _listApplicationService = listApplicationService;
        }

        [HttpGet(nameof(PlayListsController.GetAll))]
        public async Task<IEnumerable<DtoPlayList>> GetAll()
        {
            return await _listApplicationService.GetAllPlayList();
        }

        [HttpGet(nameof(PlayListsController.Get) + "/{id}")]
        public async Task<DtoPlayList> Get(int id)
        {
            return await _listApplicationService.GetPlayList(id);
        }

        [HttpPost(nameof(PlayListsController.Add))]
        public async Task<DtoPlayList> Add(DtoPlayList dtoPlayList)
        {
            return await _listApplicationService.SavePlaysList(dtoPlayList);
        }

        [HttpPost(nameof(PlayListsController.AddList))]
        public async Task<IEnumerable<DtoPlayList>> AddList(List<DtoPlayList> dtoPlayLists)
        {
            return await _listApplicationService.SavePlaysLists(dtoPlayLists);
        }

        [HttpPut(nameof(PlayListsController.Update))]
        public async Task<DtoPlayList> Update(DtoPlayList dtoPlayLists)
        {
            return await _listApplicationService.UpdatePlayList(dtoPlayLists);
        }

        [HttpDelete(nameof(PlayListsController.Delete))]
        public async Task<IActionResult> Delete(int id)
        {
            await _listApplicationService.DeletePlayList(id);
            return NoContent();
        }

    }
}
