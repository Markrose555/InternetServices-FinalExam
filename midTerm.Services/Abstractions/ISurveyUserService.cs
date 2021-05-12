using System.Collections.Generic;
using System.Threading.Tasks;
using midTerm.Models.Models.SurveyUser;

namespace midTerm.Services.Abstractions
{
    public interface ISurveyUserService
    {
        Task<SurveyUserExtended> GetById(int id);

        Task<IEnumerable<SurveyUserBaseModel>> Get();

        Task<SurveyUserBaseModel> Insert(SurveyUserCreate model);

        Task<SurveyUserBaseModel> Update(SurveyUserUpdate model);

        Task<bool> Delete(int id);
    }
}