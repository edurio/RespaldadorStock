using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RespaldadorStock
{
    public partial class Form1 : Form
    {
        List<Entity.Bitacora> _listaBitacoras = new List<Entity.Bitacora>();
        int _id = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRespaldar_Click(object sender, EventArgs e)
        {
            AccionRespaldar();



        }

        void AccionRespaldar()
        {
            int contador = 1;
            int contadorEmpresas = 0;
            DateTime inicio = DateTime.Now;
            int fechaInt = Utiles.FechaToInteger(inicio);

            try
            {
                var lista = DAL.Empresa.GetEmpresas();
                progressBarControl1.Properties.Maximum = lista.Count;
                progressBarControl1.Properties.Step = 1;

                foreach (var emp in lista)
                {
                    progressBarControl1.PerformStep();
                    labelControl1.Text = "Procesando " + emp.Nombre;

                    var stock = DAL.Stock.GetStock(emp.Id);

                    progressBarControl2.Properties.Maximum = stock.Count;
                    progressBarControl2.Properties.Step = 1;

                    foreach (var s in stock)
                    {
                        labelControl2.Text = "Procesando " + s.Descripcion;
                        s.Contador = contador;
                        s.Fecha = fechaInt;
                        s.EmpId = emp.Id;
                        DAL.Stock.InsertStockBodega(s);
                        progressBarControl2.PerformStep();
                        Application.DoEvents();
                        contador++;
                    }
                    contadorEmpresas++;
                    Application.DoEvents();
                }

                DateTime termino = DateTime.Now;

                TimeSpan diferencia = termino - inicio;
                var diferenciaenminutos = diferencia.Minutes;

                progressBarControl1.EditValue = 0;
                progressBarControl2.EditValue = 0;
                labelControl1.Text = "...";
                labelControl2.Text = "...";

                //Bitacora
                Entity.Bitacora bitacora = new Entity.Bitacora();
                bitacora.Id = _id;
                bitacora.Fecha = fechaInt;
                bitacora.Glosa = "El Respaldo del día " + inicio.ToString() + " El proceso demoró " + diferenciaenminutos.ToString() + " Minutos, se procesaron " + contador.ToString() + " Registros de " + contadorEmpresas.ToString() + " Entidades";
                bitacora.EsError = false;
                DAL.Bitacora.InsertBitacora(bitacora);
                _listaBitacoras.Add(bitacora);
                grdDatos.DataSource = _listaBitacoras;
                grdDatos.MainView.LayoutChanged();
                _id++;

                Utiles.EnviaMensajeria("eduardo.rios@erex.cl", "Respaldo Ejecutado con Éxito", bitacora.Glosa);
                Utiles.EnviaMensajeria("eduardo.rios@backlinespa.com", "Respaldo Ejecutado con Éxito", bitacora.Glosa);
                Utiles.EnviaMensajeria("claudia.espinoza@backlinespa.com", "Respaldo Ejecutado con Éxito", bitacora.Glosa);
                Utiles.EnviaMensajeria("soporte@backlinespa.com", "Respaldo Ejecutado con Éxito", bitacora.Glosa);
                Utiles.EnviaMensajeria("denis.rubio@backlinespa.com", "Respaldo Ejecutado con Éxito", bitacora.Glosa);
                Utiles.EnviaMensajeria("alexis.chauquiante@backlinespa.com", "Respaldo Ejecutado con Éxito", bitacora.Glosa);

                //MessageBox.Show("El proceso demoró :" + diferenciaenminutos.ToString() + " Minutos");
            }
            catch (Exception ex)
            {
                //Bitacora              
                Entity.Bitacora bitacora = new Entity.Bitacora();
                bitacora.Id = _id;
                bitacora.Fecha = fechaInt;
                bitacora.Glosa = "Error:" + ex.Message.ToString();
                bitacora.EsError = true;
                DAL.Bitacora.InsertBitacora(bitacora);

                _listaBitacoras.Add(bitacora);
                grdDatos.DataSource = _listaBitacoras;
                grdDatos.MainView.LayoutChanged();
                _id++;

                Utiles.EnviaMensajeria("eduardo.rios@erex.cl","Respaldo con Error", "Error:" + ex.Message.ToString());
                Utiles.EnviaMensajeria("eduardo.rios@backlinespa.com", "Respaldo con Error", "Error:" + ex.Message.ToString());
                Utiles.EnviaMensajeria("claudia.espinoza@backlinespa.com", "Respaldo con Error", "Error:" + ex.Message.ToString());
                Utiles.EnviaMensajeria("soporte@backlinespa.com", "Respaldo con Error", "Error:" + ex.Message.ToString());
                Utiles.EnviaMensajeria("denis.rubio@backlinespa.com", "Respaldo con Error", "Error:" + ex.Message.ToString());
                Utiles.EnviaMensajeria("alexis.chauquiante@backlinespa.com", "Respaldo con Error", "Error:" + ex.Message.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            
            if (hh == 23 && mm == 0 && ss== 0)
            {                
                AccionRespaldar();
            }
            txtHora.Text = DateTime.Now.ToString();
        }
    }
}
