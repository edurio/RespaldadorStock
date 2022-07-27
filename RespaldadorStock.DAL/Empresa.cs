using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using RespaldadorStock.Entity;



namespace RespaldadorStock.DAL
{
    public class Empresa
    {
        public static List<Entity.Empresa> GetEmpresas()
        {
            List<Entity.Empresa> lista = new List<Entity.Empresa>();
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(@"Data Source=190.196.223.90\MSSQL2016_ORBYTA;Initial Catalog=DB_BACKLINE_FARMACIAS;User ID=sa;Password=Lovecraft314159265");
            DbCommand dbCommand = db.GetSqlStringCommand("SELECT ID, ALIAS FROM EMP_EMPRESAS WHERE REAL=1");
           
            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int Id = reader.GetOrdinal("ID");
                int Alias = reader.GetOrdinal("ALIAS");
               


                while (reader.Read())
                {
                    Entity.Empresa obj = new Entity.Empresa();
                    //BeginFields
                    obj.Id = (Int32)(!reader.IsDBNull(Id) ? reader.GetValue(Id) : 0);
                    obj.Nombre = (string)(!reader.IsDBNull(Alias) ? reader.GetValue(Alias) : 0);
                   
                    //EndFields

                    lista.Add(obj);
                }
            }
            catch (Exception ex)
            {              
                throw ex;
            }
            finally
            {
                reader.Close();
            }

            return lista;
        }
    }
}
