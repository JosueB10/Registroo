using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Registroo
{
    public partial class Form1 : Form
    {
        private Inventario inventario;
        public DataGridView DataGridView1
        {
            get { return dgvInventario; }
        }

        public void clean() { 
        
        cboPro.Text=string.Empty;
         txtID.Text=string.Empty;
         txtCantidad.Text=string.Empty;
         txtPrecio.Text=string.Empty;

        
        
        
        }

        public Form1()
        {
            InitializeComponent();
            inventario= new Inventario();   
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        
       


       
        private void button1_Click(object sender, EventArgs e)
        {


            string id = txtID.Text;

            for (int i = 0; i < dgvInventario.Rows.Count; i++)
            {
                if (txtID.Text == dgvInventario.Rows[i].Cells[0].Value.ToString())
                {

                    MessageBox.Show("ID repetido");
                    clean();
                    return;

                }
            }

            Producto producto = new Producto(id, cboPro.Text, double.Parse(txtPrecio.Text), int.Parse(txtCantidad.Text));
            inventario.AgregarProducto(producto);
            // Actualizar la lista de productos en el formulario
            ActualizarListaProductos();
            clean();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form2 form2 = new Form2();

            // llamar al método "SetDataGridViewData" del segundo formulario y pasar el objeto DataGridView del primer formulario como parámetro
            form2.SetDataGridViewData(DataGridView1);

            // mostrar el segundo formulario
            form2.Show();


        }
        private void button3_Click(object sender, EventArgs e)
            {



            }


        public void ActualizarListaProductos()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = inventario.productos;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            

        }
    }
}
