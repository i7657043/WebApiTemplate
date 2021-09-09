using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    public interface IExampleApiProvider
    {
        Task<List<string>> GetAsync(int id);
    }
}
