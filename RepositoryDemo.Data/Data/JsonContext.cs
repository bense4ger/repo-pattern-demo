using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json;
using RepositoryDemo.Data.Entities;
using RepositoryDemo.Data.Helpers;

namespace RepositoryDemo.Data.Data
{
    public class JsonContext<TEntity> : IDataContext<TEntity> where TEntity: IEntity
    {
        public JsonContext(IFileReader reader)
        {
            Reader = reader;
            Reader.Configure(typeof(TEntity).InferFileName("json"));
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
            var data = Reader.ReadText();

            return !string.IsNullOrEmpty(data)
                ? JsonConvert.DeserializeObject<IEnumerable<TEntity>>(data)
                : new List<TEntity>();
        }
    }
}