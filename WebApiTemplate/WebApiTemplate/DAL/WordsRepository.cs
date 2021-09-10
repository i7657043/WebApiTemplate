using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    public class WordsRepository : IWordsRepository
    {
        public async Task<List<string>> GetAsync(int id)
        {
            await Task.Delay(500);

            return new List<string>();
        }

        public async Task<Word> AddAsync(Word word)
        {
            await Task.Delay(500);

            return new Word{ Id = 1, Text = word.Text  };
        }
    }
}
