using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ACA.DeliverySystem_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<ItemViewModel>> GetAll(CancellationToken token)
        {
            var items = await _itemService.GetAll(token);
            return items.Select(x => _mapper.Map<ItemViewModel>(x));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id, CancellationToken token)
        {
            var item = await _itemService.GetById(id, token);
            if (item == null)
            {
                return NotFound();
            }
            _itemService.Delete(item.Id, token);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemAddModel model, CancellationToken token)
        {
            var item = _mapper.Map<Item>(model);
            await _itemService.CreateItem(item, token);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] ItemUpdateModel model, CancellationToken token)
        {
            var item = await _itemService.GetById(id, token);
            if (item == null)
            {
                return NotFound();
            }
            item = _mapper.Map<Item>(model);
            await _itemService.Update(item, token);
            return Ok();
        }
    }
}
