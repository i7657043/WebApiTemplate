using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTemplate_Auth
{
    public class ExampleApiRepository : IExampleApiRepository
    {
        public async Task<List<string>> GetAsync(int id)
        {
            await Task.Delay(500);

            return new List<string>();
        }
    }
}
