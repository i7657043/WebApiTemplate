using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiTemplate.Libs;

namespace WebApiTemplate
{
    public interface IWordsProvider
    {
        Task<List<string>> GetAsync(int id);
        Task<Word> AddAsync(Word word);
    }
}
