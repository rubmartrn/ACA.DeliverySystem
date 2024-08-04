using ACA.DeliverySystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACA.DeliverySystem.Business.Services
{
    public interface IUserService
    {
        Task Delete(int id, CancellationToken token);
        Task<IEnumerable<User>> GetAllItem(CancellationToken token);
        Task<User> GetItemById(int id, CancellationToken token);
        Task Update(User user, CancellationToken token);
    }
}
