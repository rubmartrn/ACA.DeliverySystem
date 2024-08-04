using ACA.DeliverySystem.Business.Models;
using ACA.DeliverySystem.Business.Services;
using ACA.DeliverySystem.Data.Models;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserAddModel model, CancellationToken token)
        {
            var user = _mapper.Map<User>(model);
            await _userService.Create(user, token);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] UserUpdateModel model, CancellationToken token)
        {
            var user = await _userService.GetById(id, token);
            if (user == null)
            {
                return NotFound();
            }
            user = _mapper.Map<User>(model);
            await _userService.Update(user, token);
            return Ok();
        }
    }
}
