using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBancariaAppNS
{
    public partial class GestionBancariaApp : Form
    {
        private double saldo;
    

        public GestionBancariaApp(double saldo = 0)
        {
            InitializeComponent();
            if(saldo > 0)
                this.saldo = saldo;
            else
                this.saldo = 0;
            txtSaldo.Text = ObtenerSaldo().ToString();
            txtCantidad.Text = "0";
        }

        public double ObtenerSaldo() { return saldo; }
        public const String ERR_CANTIDAD_NO_VALIDA = "Cantidad no válida";
        public const String ERR_SALDO_INSUFICIENTE = "Saldo insuficiente";

        public int RealizarReintegro(double cantidad)
        {
            if(cantidad <= 0)
                throw new ArgumentOutOfRangeException(ERR_CANTIDAD_NO_VALIDA);
            if(saldo < cantidad)
                throw new ArgumentOutOfRangeException(ERR_SALDO_INSUFICIENTE);
            saldo -= cantidad;
            return 0;
        }

        public int RealizarIngreso(double cantidad)
        {
            if(cantidad < 0)
                throw new ArgumentOutOfRangeException("La cantidad indicada no es válida.");
            return saldo += cantidad;
        }

        private void btOperar_Click(object sender, EventArgs e)
        {
            double cantidad = Convert.ToDouble(txtCantidad.Text); // Cogemos la cantidad del TextBox y la pasamos a número
            if(rbReintegro.Checked)
            {
                try
                {
                    RealizarReintegro(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch(Exception err)
                {
                    if(err.Message.Equals(ERR_SALDO_INSUFICIENTE))
                        MessageBox.Show("No se ha podido realizar la operación (¿Saldo insuficiente ?)");
                    else if(err.Message.Equals(ERR_CANTIDAD_NO_VALIDA))
                        MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                }

            }
            else
            {
                try
                {
                    RealizarIngreso(cantidad);
                    MessageBox.Show("Transacción realizada.");
                }
                catch(Exception err)
                {
                    if(err.Message.Equals(ERR_CANTIDAD_NO_VALIDA))
                        MessageBox.Show("Cantidad no válida, sólo se admiten cantidades positivas.");
                }
                txtSaldo.Text = ObtenerSaldo().ToString();
            }
        }
    }
}

