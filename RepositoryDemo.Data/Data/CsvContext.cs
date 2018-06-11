using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using RepositoryDemo.Data.Entities;
using RepositoryDemo.Data.Helpers;

namespace RepositoryDemo.Data.Data
{
    public class CsvContext<TEntity> : IDataContext<TEntity> where TEntity: IEntity
    {
        public CsvContext(IFileReader reader)
        {
            Reader = reader;
            Reader.Configure(typeof(TEntity).InferFileName("csv"));
        }

        private IFileReader Reader { get; }
        
        public TEntity FindById(Guid id)
        {
            return GetEntities().FirstOrDefault(e => e.Id == id);
        }

       
        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return GetEntities().Where(predicate.Compile()).ToList();
        }
        
        private IEnumerable<TEntity> GetEntities()
        {
            var data = Reader.ReadLines();

            if (data.Length == 0) return null;
            
            var entities = new List<TEntity>();
            var headers = data[0].Split(",", StringSplitOptions.RemoveEmptyEntries);

            for (var i = 1; i < data.Length; ++i)
            {
                entities.Add(data[i].Split(",", StringSplitOptions.RemoveEmptyEntries).ToEntity<TEntity>(headers));
            }

            return entities;
        }
    }
}