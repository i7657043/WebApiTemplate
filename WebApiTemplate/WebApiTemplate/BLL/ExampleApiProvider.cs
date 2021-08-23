using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace WebApiTemplate
{
    public class ExampleApiProvider : IExampleApiProvider
    {
        private readonly IExampleApiRepository _apiRepository;

        public ExampleApiProvider(IExampleApiRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public async Task<ProviderResponse<List<string>>> GetAsync(int id)
        {
            await Task.Delay(500);

            List<string> response = await _apiRepository.GetAsync(id);

            return new ProviderResponse<List<string>>(HttpStatusCode.OK, response);
        }
    }
}
