using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace API.Util
{
    public class Repositorio
    {
        public class RepositorioBase<T> : IDisposable where T : class
        {
            public LocalizaAmigosEntities Contexto { get; } = new LocalizaAmigosEntities();

            public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
            {
                T retorno = Contexto.Set<T>().Where(predicate).FirstOrDefault();
                return retorno;
            }

            public T GetLastOrDefault(Expression<Func<T, bool>> predicate)
            {
                T retorno = Contexto.Set<T>().Where(predicate).ToArray().Last();
                return retorno;
            }

            public List<T> GetList(Expression<Func<T, bool>> predicate)
            {
                List<T> retorno = Contexto.Set<T>().Where(predicate).ToList();
                return retorno;
            }

            public List<T> GetListAll()
            {
                List<T> retorno = Contexto.Set<T>().ToList();
                return retorno;
            }

            public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate)
            {
                IQueryable<T> retorno = Contexto.Set<T>().Where(predicate);
                return retorno;
            }

            public IQueryable<T> GetQueryable()
            {
                IQueryable<T> retorno = Contexto.Set<T>();
                return retorno;
            }

            public void Update(T objeto)
            {
                Contexto.Entry(objeto).State = EntityState.Modified;
            }

            public void Inserir(T objeto)
            {
                Contexto.Set<T>().Add(objeto);
            }

            public void Remover(T objeto)
            {
                Contexto.Set<T>().Remove(objeto);
            }

            public void Salvar()
            {
                Contexto.SaveChanges();
            }

            public void Dispose()
            {
                Contexto.Dispose();
            }
        }
    }
}