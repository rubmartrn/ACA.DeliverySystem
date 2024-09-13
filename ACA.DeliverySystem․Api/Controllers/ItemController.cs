using ACA.DeliverySystem.Api.Models;
using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
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
        public async Task<IEnumerable<ItemViewModelDTO>> GetAll(CancellationToken token)
        {
            var items = await _itemService.GetAll(token);
            return items.Select(x => _mapper.Map<ItemViewModelDTO>(x));
        }

        [HttpGet("OrderItems")]
        public async Task<IEnumerable<ItemViewModelDTO>> GetAll([FromQuery] int orderId, CancellationToken token)
        {
            var items = await _itemService.GetAll(token);
            return items.Select(x => _mapper.Map<ItemViewModelDTO>(x));
        }

        [HttpGet("{itemId}")]
        public async Task<ItemViewModelDTO> GetById(int itemId, CancellationToken token)
        {
            var item = await _itemService.GetById(itemId, token);
            return _mapper.Map<ItemViewModelDTO>(item);

        }

        [HttpGet("{itemId}/{orderId}")]
        public async Task<ItemViewModelDTO> GetById(int itemId, int orderId, CancellationToken token)
        {
            var item = await _itemService.GetById(itemId, token);
            return _mapper.Map<ItemViewModelDTO>(item);

        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id, CancellationToken token)
        {
            var result = await _itemService.Delete(id, token);
            if (!result.Success)
            {
                if (result.ErrorType == ErrorType.NotFound)
                {
                    return NotFound(result.ErrorMessage);
                }
                return BadRequest(result.ErrorMessage);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemAddModelDTO model, CancellationToken token)
        {
            var item = _mapper.Map<ItemAddModel>(model);
            await _itemService.CreateItem(item, token);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] ItemUpdateModelDTO model, CancellationToken token)
        {
            var mappedModel = _mapper.Map<ItemUpdateModel>(model);
            await _itemService.Update(id, mappedModel, token);
            return Ok();
        }
    }
}
