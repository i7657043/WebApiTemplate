using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTemplate
{
    public interface IExampleApiProvider
    {
        Task<ProviderResponse<List<string>>> GetAsync(int id);
    }
}
