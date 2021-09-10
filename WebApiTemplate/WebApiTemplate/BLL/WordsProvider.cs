using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    public class WordsProvider : IWordsProvider
    {
        private readonly IWordsRepository _apiRepository;

        public WordsProvider(IWordsRepository apiRepository)
        {
            _apiRepository = apiRepository;
        }

        public async Task<List<string>> GetAsync(int id)
        {
            List<string> response = await _apiRepository.GetAsync(id);

            if (response.Count == 0)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return response;
        }
        public Task<Word> AddAsync(Word word)
        {
            return _apiRepository.AddAsync(word);
        }

    }
}
