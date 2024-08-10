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

        [HttpPost]
        public async Task<IActionResult> Create(OrderAddModelDTO model, CancellationToken token)
        {
            var order = _mapper.Map<OrderAddModel>(model);
            await _orderService.CreateOrder(order, token);
            return Ok(order);
        }



    }
}
