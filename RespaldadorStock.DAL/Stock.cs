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
    public class Stock
    {
        public static void InsertStockBodega(Entity.Stock stock)
        {
            try
            {
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(@"Data Source=45.7.231.200\MSSQL2016;Initial Catalog=DB_BACKLINE_RESPALDOS;User ID=UsrBack;Password=Ed21041980");
                DbCommand dbCommand = db.GetStoredProcCommand("SP_INSERTAR_STOCK");

                //BeginFields
                db.AddInParameter(dbCommand, "CONTADOR", DbType.Int32, stock.Contador);
                db.AddInParameter(dbCommand, "CODIGO", DbType.String, stock.Codigo);
                db.AddInParameter(dbCommand, "PROD_ID", DbType.Int32, stock.ProdId);
                db.AddInParameter(dbCommand, "BODE_ID", DbType.Int32, stock.BodeId);
                db.AddInParameter(dbCommand, "FECHA_INT", DbType.Int32, stock.Fecha);
                db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, stock.EmpId);
                db.AddInParameter(dbCommand, "DESCRIPCION", DbType.String, stock.Descripcion);
                db.AddInParameter(dbCommand, "LOTE", DbType.String, stock.Lote);
                db.AddInParameter(dbCommand, "FECHA_VENCIMIENTO", DbType.DateTime, stock.FechaVencimiento);
                db.AddInParameter(dbCommand, "STOCK", DbType.Decimal, stock.StockLote);
                db.AddInParameter(dbCommand, "VALOR", DbType.Decimal, stock.Valor);
                //EndFields

                db.ExecuteNonQuery(dbCommand);


            }
            catch (Exception ex)
            {
               // GlobalesDAO.InsertErrores(ex);
                throw ex;
            }

        }
        public static List<Entity.Stock> GetStock(int EmpId)
        {
            List<Entity.Stock> lista = new List<Entity.Stock>();
            Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(@"Data Source=190.196.223.90\MSSQL2016_ORBYTA;Initial Catalog=DB_BACKLINE_FARMACIAS;User ID=sa;Password=Lovecraft314159265");
            DbCommand dbCommand = db.GetStoredProcCommand("SP_OBTIENE_STOCK_RESPALDO");

            db.AddInParameter(dbCommand, "EMP_ID", DbType.Int32, EmpId);

            IDataReader reader = (IDataReader)db.ExecuteReader(dbCommand);

            try
            {
                int ID = reader.GetOrdinal("ID");
                int CODIGO_USUARIO = reader.GetOrdinal("CODIGO_USUARIO");
                int DESCRIPCION = reader.GetOrdinal("DESCRIPCION");
                int BODE_ID = reader.GetOrdinal("BODE_ID");
                int LOTE = reader.GetOrdinal("LOTE");
                int FECHA_VENCIMIENTO = reader.GetOrdinal("FECHA_VENCIMIENTO");
                int STOCK = reader.GetOrdinal("STOCK");
                int VALOR = reader.GetOrdinal("VALOR");



                while (reader.Read())
                {
                    Entity.Stock obj = new Entity.Stock();
                    //BeginFields
                    obj.ProdId = (Int32)(!reader.IsDBNull(ID) ? reader.GetValue(ID) : 0);
                    obj.Codigo = (string)(!reader.IsDBNull(CODIGO_USUARIO) ? reader.GetValue(CODIGO_USUARIO) : "");
                    obj.Descripcion = (string)(!reader.IsDBNull(DESCRIPCION) ? reader.GetValue(DESCRIPCION) : "");
                    obj.BodeId = (Int32)(!reader.IsDBNull(BODE_ID) ? reader.GetValue(BODE_ID) : 0);
                    obj.Lote = (string)(!reader.IsDBNull(LOTE) ? reader.GetValue(LOTE) : "");
                    obj.FechaVencimiento = DateTime.Now;// (DateTime)(!reader.IsDBNull(FECHA_VENCIMIENTO) ? reader.GetValue(FECHA_VENCIMIENTO) : DateTime.MinValue);
                    obj.StockLote = (decimal)(!reader.IsDBNull(STOCK) ? reader.GetValue(STOCK) : decimal.Parse("0"));
                    obj.Valor = (double)(!reader.IsDBNull(VALOR) ? reader.GetValue(VALOR) : double.Parse("0"));

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
