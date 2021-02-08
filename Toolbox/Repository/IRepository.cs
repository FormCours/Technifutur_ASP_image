using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Repository
{
    public interface IRepository<TKey, TEntity>
        where TEntity: IEntity<TKey>
    {
        // Create
        TKey Insert(TEntity entity);

        // Read
        TEntity Get(TKey id);
        IEnumerable<TEntity> GetAll();

        // Update
        bool Update(TKey id, TEntity entity);

        // Delete
        bool Delete(TKey id);
    }
}
