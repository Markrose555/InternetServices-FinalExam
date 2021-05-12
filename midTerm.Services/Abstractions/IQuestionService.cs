using System.Collections.Generic;
using System.Threading.Tasks;
using midTerm.Models.Models.Question;

namespace midTerm.Services.Abstractions
{
    public interface IQuestionService
    {
        Task<QuestionModelExtended> GetById(int id);

        Task<IEnumerable<QuestionModelBase>> Get();
        
        Task<QuestionModelBase> Insert(QuestionCreateModel model);

        Task<QuestionModelBase> Update(QuestionUpdateModel model);

        Task<bool> Delete(int id);
    }
}
