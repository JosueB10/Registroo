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


using Microsoft.Office.Interop.Excel;
using static System.Net.WebRequestMethods;
using objExcel = Microsoft.Office.Interop.Excel;
using System.Drawing.Printing;
using System.IO;
using System.Xml.Linq;


using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Registroo
{
    public partial class Form2 : Form
    {

        Form1 Form1 = new Form1();
        



        List<Producto> productosRegistrados = new List<Producto>();
        
      

        public void SetDataGridViewData(DataGridView dgv)
        {
            dgvDestino.DataSource = dgv.DataSource;
        }

        public Form2()
        {
            InitializeComponent();
           
        }
        string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        private void Catalogo_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {

            string Cliente =txtCliente.Text;
            string id = textBoxid.Text;
            string nombre = comboBoxProductos.SelectedItem.ToString();
            int cantidadDisponible = int.Parse(txtDisponible.Text);
            string metodo = comboBox1.Text.ToString();
            int precio = int.Parse(textBoxprecio.Text);
            int cantidadAVender = int.Parse(txtCantidadComprada.Text);

            // Verificar que hay suficiente cantidad disponible para la venta
            if (cantidadAVender > cantidadDisponible)
            {
                MessageBox.Show("No hay suficiente cantidad disponible para la venta.", "Error de venta");
                comboBoxProductos.SelectedIndex = -1;
                textBoxid.Clear();
                textBoxprecio.Clear();
                txtDisponible.Clear();
                txtCantidadComprada.Clear();
                comboBox1.SelectedIndex = -1;
                return;
            }

            // Agregar una fila al DataGridView de ventas
            dataGridView1.Rows.Add(Cliente,id,metodo, nombre,  precio, cantidadAVender);

            // Actualizar la cantidad disponible del producto en el DataGridView de productos registrados
            foreach (DataGridViewRow row in dgvDestino.Rows)
            {
                if (row.Cells["Id"].Value.ToString() == id)
                {
                    row.Cells["LibrasDisponibles"].Value = cantidadDisponible - cantidadAVender;
                    break;
                }
            }

            // Limpiar los TextBox y ComboBox
            comboBoxProductos.SelectedIndex = -1;
            textBoxid.Clear();
            textBoxprecio.Clear();
            txtDisponible.Clear();
            txtCantidadComprada.Clear();
            comboBox1.SelectedIndex= -1;    


        }

        private void txtBuscarxid_TextChanged(object sender, EventArgs e)
        {
            dgvDestino.CurrentCell = null;

            if (txtBuscarxid.Text != "")
            {


                foreach (DataGridViewRow r in dgvDestino.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in dgvDestino.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {



                        if (c.Value != null && c.Value.ToString().ToUpper().IndexOf(txtBuscarxid.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }


                    }
                }




            }
            else
            {

                foreach (DataGridViewRow r in dgvDestino.Rows)
                {
                    r.Visible = true;
                }


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            objExcel.Application objAplicacion = new objExcel.Application();
            Workbook objLibro = objAplicacion.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet objHoja = (Worksheet)objAplicacion.ActiveSheet;

            objAplicacion.Visible = false;



            foreach (DataGridViewColumn columna in dataGridView1.Columns)
            {
                objHoja.Cells[1, columna.Index + 1] = columna.HeaderText;
                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    objHoja.Cells[fila.Index + 2, columna.Index + 1] = fila.Cells[columna.Index].Value;

                }
            }
            MessageBox.Show("Los datos se han exportado correctamente.", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            objLibro.SaveAs(ruta + "\\VentaClientes.xlsx");
            objLibro.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);

            pdfTable.DefaultCell.Padding = 3;

            pdfTable.WidthPercentage = 70;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;
            //Adding Header row
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }

            //Adición de fila de datos
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                    {

                    }
                    else
                    {
                        pdfTable.AddCell(cell.Value.ToString());

                    }


                }
            }
            //Exporting to PDF
            string folderPath = "D:\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "RegistroClientes.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);

                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();

                MessageBox.Show("Los datos se han exportado correctamente.", "Exito ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
    
}
