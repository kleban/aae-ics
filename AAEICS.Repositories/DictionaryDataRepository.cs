using AAEICS.Database.Context;

using AutoMapper;

namespace AAEICS.Repositories;

public class DictionaryDataRepository<TModel, TDto>(AAEICSDbContext dbContext, IMapper mapper)
    : GenericRepository<TModel, TDto>(dbContext, mapper)
    where TModel : class
    where TDto : class;
    