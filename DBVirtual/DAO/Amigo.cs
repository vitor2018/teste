using DBVirtual.Models;
using DBVirtual.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBVirtual.DAO
{
    public static class Amigo
    {
        public static bool Gravar(Amigos model)
        {
            bool flag = false;
            try
            {
                using (var obj = new Repositorio<Amigos>())
                {
                    obj.Contexto.Configuration.ProxyCreationEnabled = false;
                    obj.Inserir(model);
                    obj.Salvar();
                    obj.Dispose();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public static List<Amigos> GetAllByIdUsuario(int idUsuario)
        {
            List<Amigos> amigo = null;
            try
            {
                using (var obj = new Repositorio<Amigos>())
                {
                    obj.Contexto.Configuration.LazyLoadingEnabled = false;
                    amigo = obj.GetList(a => a.ID_Usuario == idUsuario);
                    obj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return amigo;
        }

        public static bool VerificaCep(string cep)
        {
            bool flag = false;
            try
            {
                using (var obj = new Repositorio<Amigos>())
                {
                    if (obj.GetListAll().Any(a => a.NR_CEP.Equals(cep)))
                    {
                        flag = true;
                    }
                    obj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }
    }
}