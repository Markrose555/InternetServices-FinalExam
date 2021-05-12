using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using midTerm.Data;
using midTerm.Data.Entities;
using midTerm.Models.Models.SurveyUser;
using midTerm.Services.Abstractions;

namespace midTerm.Services.Services
{
    public class SurveyUserService 
        : ISurveyUserService
    {
        private readonly MidTermDbContext _context;
        private readonly IMapper _mapper;

        public SurveyUserService(MidTermDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SurveyUserExtended> GetById(int id)
        {
            var user = await _context.SurveyUsers.FindAsync(id);
            return _mapper.Map<SurveyUserExtended>(user);
        }

        public async Task<IEnumerable<SurveyUserBaseModel>> Get()
        {
            var questions = await _context.SurveyUsers.ToListAsync();
            return _mapper.Map<IEnumerable<SurveyUserBaseModel>>(questions);
        }

        public async Task<SurveyUserBaseModel> Insert(SurveyUserCreate model)
        {
            var entity = _mapper.Map<SurveyUser>(model);

            await _context.SurveyUsers.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<SurveyUserBaseModel>(entity);
        }

        public async Task<SurveyUserBaseModel> Update(SurveyUserUpdate model)
        {
            var entity = await _context.SurveyUsers.FindAsync(model.Id);
            if (entity == null)
            {
                throw new Exception("User not found");
            }
            _mapper.Map(model, entity);

            _context.SurveyUsers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();

            return _mapper.Map<SurveyUserBaseModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.SurveyUsers.FindAsync(id);
            _context.SurveyUsers.Remove(entity);
            return await SaveAsync() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
