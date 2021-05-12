using System.Collections.Generic;
using System.Threading.Tasks;
using midTerm.Models.Models.Option;

namespace midTerm.Services.Abstractions
{
    public interface IOptionService
    {
        Task<OptionModelExtended> GetById(int id);

        Task<IEnumerable<OptionBaseModel>> Get();

        Task<IEnumerable<OptionModelExtended>> GetByQuestionId(int id);

        Task<OptionBaseModel> Insert(OptionCreateModel model);

        Task<OptionBaseModel> Update(OptionUpdateModel model);

        Task<bool> Delete(int id);
    }
}
