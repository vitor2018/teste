using DBVirtual.Models;
using DBVirtual.Util;
using System;
using System.Linq;

namespace DBVirtual.DAO
{
    public static class Usuario
    {
        public static bool Gravar(Usuarios model)
        {
            bool flag = false;
            try
            {
                using (var obj = new Repositorio<Usuarios>())
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

        public static Usuarios Get(string nome, string senha)
        {
            Usuarios usuario = null;
            try
            {
                using (var obj = new Repositorio<Usuarios>())
                {
                    obj.Contexto.Configuration.LazyLoadingEnabled = false;
                    usuario = obj.GetFirstOrDefault(u => u.NM_Usuario.Equals(nome) && u.NM_Senha.Equals(senha));
                    obj.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return usuario;
        }

        public static bool VerificaNome(string nome)
        {
            bool flag = false;
            try
            {
                using (var obj = new Repositorio<Usuarios>())
                {                    
                    if (obj.GetListAll().Any(u => u.NM_Usuario.Equals(nome)))
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