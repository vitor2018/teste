using API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using static API.Util.Repositorio;

namespace API.DAO
{
    public class Usuario
    {
        public static List<Usuarios> GetAll()
        {
            List<Usuarios> usuarios;
            try
            {
                using (var obj = new RepositorioBase<Usuarios>())
                {
                    usuarios = obj.GetListAll();
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }

            return usuarios;
        }
    }
}