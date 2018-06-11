using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Reflection;
using System.Collections;
using System.Data;

namespace DBVirtual.Util
{
    public class Repositorio<T> : IDisposable where T : class
    {
        private Contexto _contexto = new Contexto();

        public Contexto Contexto
        {
            get
            {
                return _contexto;
            }
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            T retorno = _contexto.Set<T>().Where(predicate).FirstOrDefault();
            return retorno;
        }

        public T GetLastOrDefault(Expression<Func<T, bool>> predicate)
        {
            T retorno = _contexto.Set<T>().Where(predicate).ToArray().Last();
            return retorno;
        }

        public List<T> GetList(Expression<Func<T, bool>> predicate)
        {
            List<T> retorno = _contexto.Set<T>().Where(predicate).ToList();
            return retorno;
        }

        public List<T> GetListAll()
        {
            List<T> retorno = _contexto.Set<T>().ToList();
            return retorno;
        }

        public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> retorno = _contexto.Set<T>().Where(predicate);
            return retorno;
        }

        public IQueryable<T> GetQueryable()
        {
            IQueryable<T> retorno = _contexto.Set<T>();
            return retorno;
        }
        
        public void Update(T objeto)
        {            
            _contexto.Entry(objeto).State = EntityState.Modified;
        }
        
        public void Inserir(T objeto)
        {        
            _contexto.Set<T>().Add(objeto);
        }

        public void Remover(T objeto)
        {
            _contexto.Set<T>().Remove(objeto);
        }

        public void Salvar()
        {
            _contexto.SaveChanges();
        }

        public void Dispose()
        {
            _contexto.Dispose();
        }
    }
}