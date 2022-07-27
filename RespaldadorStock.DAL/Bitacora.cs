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
    public class Bitacora
    {
        public static void InsertBitacora(Entity.Bitacora bitacora)
        {
            try
            {
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(@"Data Source=45.7.231.200\MSSQL2016;Initial Catalog=DB_BACKLINE_RESPALDOS;User ID=UsrBack;Password=Ed21041980");
                DbCommand dbCommand = db.GetStoredProcCommand("SP_BITACORA_INS");

                //BeginFields
                db.AddInParameter(dbCommand, "FECHA", DbType.String, bitacora.Fecha);
                db.AddInParameter(dbCommand, "OBSERVACION", DbType.String, bitacora.Glosa);
                db.AddInParameter(dbCommand, "ES_ERROR", DbType.Boolean, bitacora.EsError == false ? 0 : 1);
              
                //EndFields

                db.ExecuteNonQuery(dbCommand);


            }
            catch (Exception ex)
            {
                // GlobalesDAO.InsertErrores(ex);
                throw ex;
            }

        }
    }
}
