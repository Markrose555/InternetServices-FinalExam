using System.Collections.Generic;
using System.Threading.Tasks;
using midTerm.Models.Models.Answers;

namespace midTerm.Services.Abstractions
{
    public interface IAnswerService
    {
        Task<AnswersExtended> GetById(int id);

        Task<IEnumerable<AnswersBaseModel>> Get();

        Task<IEnumerable<AnswersExtended>> GetByUserId(int id);
        
        Task<AnswersBaseModel> Insert(AnswerCreateModel model);

        Task<AnswersBaseModel> Update(AnswersUpdateModel model);

        Task<bool> Delete(int id);
    }
}