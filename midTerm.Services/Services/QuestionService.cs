using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using midTerm.Data;
using midTerm.Data.Entities;
using midTerm.Models.Models.Question;
using midTerm.Services.Abstractions;

namespace midTerm.Services.Services
{
    public class QuestionService 
        : IQuestionService
    {
        private readonly MidTermDbContext _context;
        private readonly IMapper _mapper;

        public QuestionService(MidTermDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QuestionModelBase>> Get()
        {
            var questions = await _context.Questions.ToListAsync();
            return _mapper.Map<IEnumerable<QuestionModelBase>>(questions);
        }

        public async Task<QuestionModelExtended> GetById(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);

            return _mapper.Map<QuestionModelExtended>(question);
        }

        public async Task<IEnumerable<QuestionModelExtended>> GetFull()
        {
            var questions = await _context.Questions.ToListAsync();
            return _mapper.Map<IEnumerable<QuestionModelExtended>>(questions);
        }

        public async Task<QuestionModelBase> Insert(QuestionCreateModel model)
        {
            var entity = _mapper.Map<Question>(model);

            await _context.Questions.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<QuestionModelBase>(entity);
        }

        public async Task<QuestionModelBase> Update(QuestionUpdateModel model)
        {
            var entity = await _context.Questions.FindAsync(model.Id);
            if (entity == null)
            {
                throw new Exception("Question not found");
            }
            _mapper.Map(model, entity);

            _context.Questions.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();

            return _mapper.Map<QuestionModelBase>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(entity);
            return await SaveAsync() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
