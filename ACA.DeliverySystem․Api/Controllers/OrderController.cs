using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ACA.DeliverySystem_Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OrderViewModelDTO>> GetAll(CancellationToken token)
        {
            var orders = await _orderService.GetAll(token);
            return orders.Select(x => _mapper.Map<OrderViewModelDTO>(x));
        }

        [HttpGet("{orderId}")]

        public async Task<OrderViewModelDTO> GetById(int orderId, CancellationToken token)
        {
            var order = await _orderService.Get(orderId, token);
            return _mapper.Map<OrderViewModelDTO>(order);
        }


        [HttpDelete]

        public async Task Delete([FromQuery] int id, CancellationToken token)
        {
            await _orderService.Delete(id, token);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderAddModelDTO model, CancellationToken token)
        {
            var order = _mapper.Map<OrderAddModel>(model);
            await _orderService.CreateOrder(order, token);
            return Ok(order);
        }

        [HttpPost("/addItemInOrder")]
        public async Task<IActionResult> AddItemInOrder([FromQuery] int orderId, [FromQuery] int itemId, CancellationToken token)
        {
            await _orderService.AddItemInOrder(orderId, itemId, token);
            return Ok();
        }

        [HttpDelete("/removeItemFromOrder")]
        public async Task<IActionResult> RemoveItemFromOrder([FromQuery] int orderId, [FromQuery] int itemId, CancellationToken token)
        {
            await _orderService.RemoveItemFromOrder(orderId, itemId, token);
            return Ok();
        }
    }
}
