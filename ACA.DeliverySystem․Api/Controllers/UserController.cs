using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ACA.DeliverySystem_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;



        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserViewModelDTO>> GetAll(CancellationToken token)
        {
            var users = await _userService.GetAll(token);
            return users.Select(x => _mapper.Map<UserViewModelDTO>(x));
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAddModelDTO model, CancellationToken token)
        {
            var user = _mapper.Map<UserAddModel>(model);
            await _userService.Create(user, token);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UserUpdateModelDTO model, CancellationToken token)
        {

            var mappedModel = _mapper.Map<UserUpdateModel>(model);
            await _userService.Update(id, mappedModel, token);
            return Ok();
        }

        [HttpGet("{userId}/orders")]
        public async Task<IActionResult> GetUserOrders(int userId, CancellationToken token)
        {
            var orders = await _userService.GetOrdersByUserId(userId, token);

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            var orderViewModels = orders.Select(order => _mapper.Map<OrderViewModelDTO>(order));
            return Ok(orderViewModels);
        }

        [HttpPost("/addOrder")]
        public async Task<IActionResult> AddOrderInUser([FromQuery] int userId, [FromBody] OrderAddModelDTO model, CancellationToken token)
        {
            var mappedOrder = _mapper.Map<OrderAddModel>(model);
            await _userService.AddOrderInUser(userId, mappedOrder, token);
            return Ok();
        }

    }
}
