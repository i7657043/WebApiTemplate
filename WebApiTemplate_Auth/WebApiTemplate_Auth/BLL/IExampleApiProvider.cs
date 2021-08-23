using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTemplate_Auth
{
    public interface IExampleApiProvider
    {
        Task<ProviderResponse<List<string>>> GetAsync(int id);
    }
}
