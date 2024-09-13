using ACA.DeliverySystem.Api.Models;
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

        public async Task<IActionResult> Delete([FromQuery] int id, CancellationToken token)
        {
            var result = await _orderService.Delete(id, token);
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
        public async Task<IActionResult> Create(OrderAddModelDTO model, CancellationToken token)
        {
            var order = _mapper.Map<OrderAddModel>(model);
            await _orderService.CreateOrder(order, token);
            return Ok(order);
        }

        [HttpPost("addItemInOrder")]
        public async Task<IActionResult> AddItemInOrder([FromQuery] int orderId, [FromQuery] int itemId, CancellationToken token)
        {
            var result = await _orderService.AddItemInOrder(orderId, itemId, token);
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

        [HttpDelete("removeItemFromOrder")]
        public async Task<IActionResult> RemoveItemFromOrder([FromQuery] int orderId, [FromQuery] int itemId, CancellationToken token)
        {
            var result = await _orderService.RemoveItemFromOrder(orderId, itemId, token);
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

        [HttpGet("payment")]

        public async Task<IActionResult> Pay([FromQuery] int orderId, [FromQuery] decimal amount, CancellationToken token)
        {
            var result = await _orderService.PayForOrder(orderId, amount, token);
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


        [HttpPost("orderCompleted")]

        public async Task<IActionResult> OrderCompleted([FromQuery] int orderId, CancellationToken token)
        {
            var result = await _orderService.OrderCompleted(orderId, token);
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


        [HttpPost("cancelOrder")]

        public async Task<IActionResult> CancelOrder([FromQuery] int orderId, CancellationToken token)
        {
            var result = await _orderService.CancelOrder(orderId, token);
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

    }
}
