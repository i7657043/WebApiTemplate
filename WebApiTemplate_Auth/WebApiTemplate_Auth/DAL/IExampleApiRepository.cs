using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTemplate_Auth
{
    public interface IExampleApiRepository
    {
        Task<List<string>> GetAsync(int id);
    }
}
