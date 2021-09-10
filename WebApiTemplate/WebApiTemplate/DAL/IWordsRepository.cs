using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    public interface IWordsRepository
    {
        Task<List<string>> GetAsync(int id);
        Task<Word> AddAsync(Word word);
    }
}
