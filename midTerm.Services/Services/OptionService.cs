using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using midTerm.Data;
using midTerm.Data.Entities;
using midTerm.Models.Models.Option;
using midTerm.Services.Abstractions;

namespace midTerm.Services.Services
{
    public class OptionService
        : IOptionService
    {
        private readonly MidTermDbContext _context;
        private readonly IMapper _mapper;
        public OptionService(MidTermDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OptionModelExtended> GetById(int id)
        {
            var option = await _context.Options
                .Include(o => o.Question)
                .FirstOrDefaultAsync(o => o.Id == id);

            return _mapper.Map<OptionModelExtended>(option);
        }

        public async Task<IEnumerable<OptionModelExtended>> GetByQuestionId(int id)
        {
            var options = await _context.Options
                .Include(o => o.Question)
                .Where(o => o.QuestionId == id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OptionModelExtended>>(options);
        }

        public async Task<IEnumerable<OptionBaseModel>> Get()
        {
            var options = await _context.Options.ToListAsync();
            return _mapper.Map<IEnumerable<OptionBaseModel>>(options);
        }

        public async Task<OptionBaseModel> Insert(OptionCreateModel model)
        {
            var entity = _mapper.Map<Option>(model);

            await _context.Options.AddAsync(entity);
            await SaveAsync();

            return _mapper.Map<OptionBaseModel>(entity);
        }

        public async Task<OptionBaseModel> Update(OptionUpdateModel model)
        {

            var entity = await _context.Options.FindAsync(model.Id);
            if (entity == null)
            {
                throw new Exception("Option not found");
            }
            _mapper.Map(model, entity);

            _context.Options.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();

            return _mapper.Map<OptionBaseModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Options.FindAsync(id);
            _context.Options.Remove(entity);
            return await SaveAsync() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
