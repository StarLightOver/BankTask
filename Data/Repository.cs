using System.Collections.Generic;

namespace BankTask.Data
{
    public class Repository<TInputEntity,TOutputEntity> : IRepository<TInputEntity,TOutputEntity>
    {
        protected readonly AppDbContext Context;

        protected Repository(AppDbContext context)
        {
            Context = context;
        }
        
        public bool SaveChanges()
        {
            return Context.SaveChanges() >= 0;
        }

        public virtual void CreateOrUpdate(TInputEntity client)
        {
            throw new System.NotImplementedException();
        }

        public virtual IEnumerable<TOutputEntity> Get()
        {
            throw new System.NotImplementedException();
        }

        public virtual TOutputEntity Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}