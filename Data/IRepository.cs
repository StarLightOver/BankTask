using System.Collections.Generic;

namespace BankTask.Data
{
    public interface IRepository<TInputEntity,TOutputEntity>
    {
        bool SaveChanges();
        
        void CreateOrUpdate(TInputEntity client);

        IEnumerable<TOutputEntity> Get();
        
        TOutputEntity Get(int id);

        void Delete(int id);
    }
}