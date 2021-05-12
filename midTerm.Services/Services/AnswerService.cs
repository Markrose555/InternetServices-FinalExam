using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using midTerm.Data;
using midTerm.Data.Entities;
using midTerm.Models.Models.Answers;
using midTerm.Services.Abstractions;

namespace midTerm.Services.Services
{
    public class AnswerService 
        : IAnswerService
    {
        private readonly MidTermDbContext _context;
        private readonly IMapper _mapper;
      
        public AnswerService(MidTermDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AnswersExtended> GetById(int id)
        {
            var question = await _context.Answers.FindAsync(id);
            return _mapper.Map<AnswersExtended>(question);
        }

        public async Task<IEnumerable<AnswersBaseModel>> Get()
        {
            var questions = await _context.Answers.ToListAsync();
            return _mapper.Map<IEnumerable<AnswersBaseModel>>(questions);
        }

        public async Task<IEnumerable<AnswersExtended>> GetByUserId(int id)
        {
            var answers = await _context.Answers
                .Include(a=>a.User)
                .Where(a=>a.UserId == id).ToListAsync();

            return _mapper.Map<IEnumerable<AnswersExtended>>(answers);
        }

        public async Task<AnswersBaseModel> Insert(AnswerCreateModel model)
        {
            var entity = _mapper.Map<Answers>(model);

            await _context.Answers.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<AnswersBaseModel>(entity);
        }

        public async Task<AnswersBaseModel> Update(AnswersUpdateModel model)
        {
            var entity = await _context.Answers.FindAsync(model.Id);
            if (entity == null)
            {
                throw new Exception("Answer not found");
            }
            _mapper.Map(model, entity);

            _context.Answers.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();

            return _mapper.Map<AnswersBaseModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(entity);
            return await SaveAsync() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
