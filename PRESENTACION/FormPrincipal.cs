using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PRESENTACION
{
    public partial class FormPrincipal : Form

    {
        
        public FormPrincipal()
        {
            InitializeComponent();
            Diseñoprincipal();
          
        }
        private void Diseñoprincipal()
        {

       
            plsubproductos1.Visible = false;
            plsubproveedores1.Visible = false;
            plsubcompras1.Visible = false;
            plsubventas1.Visible = false;
            plsubuser.Visible = false;
            plsubusuarios.Visible = false;
            plsubproveedores.Visible = false;
            plconfiguracion.Visible = false;





        }
        private void hidesub()
        { 
            
            if (plsubproductos1.Visible == true)
            
                plsubproductos1.Visible = false;

            if (plconfiguracion.Visible == true)

                plconfiguracion.Visible = false;

            if (plsubproveedores.Visible == true)

                plsubproveedores.Visible = false;

            if (plsubusuarios.Visible == true)

                plsubusuarios.Visible = false;


            if (plsubproveedores1.Visible == true)

                plsubproveedores1.Visible = false;

            if (plsubcompras1.Visible == true)

                plsubcompras1.Visible = false;

            if (plsubventas1.Visible == true)

                plsubventas1.Visible = false;

            if (plsubuser.Visible == true)

                plsubuser.Visible = false;

        }

        private void Mostrarsub(Panel sub) {
            if (sub.Visible == false)
            {
                hidesub();
                sub.Visible = true;
            }
            else
                sub.Visible = false;

        }

        private Form activof = null;
        private void abrirformhijo(Form formhijo)
        {
            if (activof != null)
            
                activof.Close();
                activof = formhijo;
                formhijo.TopLevel = false;
                formhijo.FormBorderStyle = FormBorderStyle.None;
                formhijo.Dock = DockStyle.Fill;
                panelformularios.Controls.Add(formhijo);
                panelformularios.Tag = formhijo;
                formhijo.BringToFront();
                formhijo.Show();
            
        }




        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            //Cargadatosuser();
            //if (Cachesesionuser.Cargo == Cargosuser.Empleado)
            //{
            //    btnproveedores.Enabled = false;
            //    btnproductos1.Enabled = false;
            //    btncompras1.Enabled = false;
            //    btnreportes1.Enabled = false;
            //    btntrabajadores1.Enabled = false;
            //}
            //if (Cachesesionuser.Cargo == Cargosuser.CONTADOR)
            //{
              
            //    btnreportes1.Enabled = false;
            //    btntrabajadores1.Enabled = false;
            //}
        }
        #region Funcionalidades del formulario
        //RESIZE METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO EN TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 12;
        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL 
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));

            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);

            region.Exclude(sizeGripRectangle);
            this.panelContenedor.Region = region;
            this.Invalidate();
        }
        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(244, 244, 244));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);

            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir de la aplicación?", "Advertencia",
          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                Application.Exit();
        }
        //Capturar posicion y tamaño antes de maximizar para restaurar
        int lx, ly;
        int sw, sh;
        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
    
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
  
            this.Size = new Size(sw,sh);
            this.Location = new Point(lx,ly);
        }

        private void panelBarraTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //METODO PARA ARRASTRAR EL FORMULARIO---------------------------------------------------------------------
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

      

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            btnclientes.BackColor = Color.FromArgb(12, 61, 92);
        }

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {


        }

        private void panelformularios_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelBarraTitulo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hidesub();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea cerrar sesion?", "Advertencia",
          MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
                    }
        #endregion
        //METODO PARA ABRIR FORMULARIOS DENTRO DEL PANEL
        private void AbrirFormulario<MiForm>() where MiForm : Form, new() {
            Form formulario;
            formulario = panelformularios.Controls.OfType<MiForm>().FirstOrDefault();//Busca en la colecion el formulario
            //si el formulario/instancia no existe
            if (formulario == null)
            {
                formulario = new MiForm();
                formulario.TopLevel = false;
                formulario.FormBorderStyle = FormBorderStyle.None;
                formulario.Dock = DockStyle.Fill;
                panelformularios.Controls.Add(formulario);
                panelformularios.Tag = formulario;
                formulario.Show();
                formulario.BringToFront();
                formulario.FormClosed += new FormClosedEventHandler(CloseForms );
            }
            //si el formulario/instancia existe
            else {
                formulario.BringToFront();
            }
        }

        private void lbcargo_Click(object sender, EventArgs e)
        {

        }

        private void lbcargo_Click_1(object sender, EventArgs e)
        {

        }

        private void lbnombre_Click(object sender, EventArgs e)
        {

        }

    
        private void button7_Click(object sender, EventArgs e)
        {
            hidesub();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            hidesub();
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            hidesub();
        }

        private void btnregistrarcompra_Click(object sender, EventArgs e)
        {
            hidesub();
        }

        private void btnagregarventas_Click(object sender, EventArgs e)
        {
            hidesub();
        }

        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            hidesub();
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
           
        }

        private void btncompras_Click(object sender, EventArgs e)
        {
            //abrirformhijo(new Formcompra()) ;
        }

        private void btnproveedores_Click(object sender, EventArgs e)
        {
      
        }

      
        private void plsubventa_Paint(object sender, PaintEventArgs e)
        {
                    }

        private void plsubventa_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnventas_Click(object sender, EventArgs e)
        {
        
        }

        private void btnagregarpre_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
     
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btncategoria_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
       
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void plsubreportes_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btntrabajadores_Click(object sender, EventArgs e)
        {
     

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click_1(object sender, EventArgs e)
        {
                    }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubproductos1);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void panel4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
         

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubventas1);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void btnpresentacion1_Click(object sender, EventArgs e)
        {
       
        }

        private void btncategoria1_Click(object sender, EventArgs e)
        {
      

        }

        private void btnagregarproductos1_Click(object sender, EventArgs e)
        {

        }

        private void btnlistadoproductos_Click(object sender, EventArgs e)
        {
       
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnlistadoproveedores_Click(object sender, EventArgs e)
        {
     
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubproveedores1); 
        }

        private void btncompras1_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubcompras1);
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
      
        }

        private void btnrealizarcompra_Click(object sender, EventArgs e)
        {
  
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btntrabajadores1_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubuser);
        }

        private void btnagreartrabajador1_Click(object sender, EventArgs e)
        {
         
        }

        private void btnlistatrabajador_Click(object sender, EventArgs e)
        {
      
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
 
        }

        private void btncategoria1_Click_1(object sender, EventArgs e)
        {
            abrirformhijo(new Form1());
        }

        private void btnproductos1_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubproductos1);
        }

        private void btnproveedores_Click_1(object sender, EventArgs e)
        {
            Mostrarsub(plsubproveedores1);
        }

        private void btncompras1_Click_1(object sender, EventArgs e)
        {
            Mostrarsub(plsubcompras1);
        }

        private void btnventas1_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubventas1);
        }

        private void btntrabajadores1_Click_1(object sender, EventArgs e)
        {
            Mostrarsub(plsubuser);
        }

        private void btnreportes1_Click(object sender, EventArgs e)
        {
 
        }

        private void btnpresentacion1_Click_1(object sender, EventArgs e)
        {
     
        }

        private void btnagregarproductos1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnagregarproveedor_Click(object sender, EventArgs e)
        {
            
        }

        private void btnlistadoproveedores_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnagregarcompras1_Click(object sender, EventArgs e)
        {
            //Formcompras fordl = Formcompras.GetInstancia();
            //fordl.TopLevel = false;
            //fordl.FormBorderStyle = FormBorderStyle.None;
            //fordl.Dock = DockStyle.Fill;
            //panelformularios.Controls.Add(fordl);
            //panelformularios.Tag = fordl;
            //fordl.BringToFront();
            //fordl.Show();

          
        }

        private void btnagregarcompra_Click(object sender, EventArgs e)
        {
           
        }

        private void btnrealizarcompra_Click_1(object sender, EventArgs e)
        {

       
        }

        private void btnagreartrabajador1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void btnlistatrabajador_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btnlistadocompras_Click(object sender, EventArgs e)
        {
       
        }

        private void btnrproductos_Click(object sender, EventArgs e)
        {
       
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            Mostrarsub(plsubusuarios);
        }

        private void btnproveedores1_Click(object sender, EventArgs e)
        {
            Mostrarsub(plsubproveedores);
        }

        private void lbcargo_Click_2(object sender, EventArgs e)
        {

        }

        private void plconfiguracion_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
           Mostrarsub(plconfiguracion);
        }

        private void CloseForms(object sender,FormClosedEventArgs e) {
            if (Application.OpenForms["Form1"] == null)
                btnventas1.BackColor = Color.FromArgb(4, 41, 68);
        
            if (Application.OpenForms["Form3"] == null)
                btnclientes.BackColor = Color.FromArgb(4, 41, 68);
        }
        private void Cargadatosuser()
        {
          
            //lbcargo.Text = Cachesesionuser.Cargo;
            //lbnombre.Text = Cachesesionuser.Nombres + " " + Cachesesionuser.Apellidos;
        }
        
       
            

    }
}
