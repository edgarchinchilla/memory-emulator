/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 04/06/2012
 * Hora: 09:27 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;

namespace MemoryEmulator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		SqlConnection conexiónBD;
		string idSesion = String.Empty;
		string registro = String.Empty;
		FormProcesos frmProcess = new FormProcesos();
		ArrayList procesos = new ArrayList();
		double Quantum = 0;
		double mTamano = 0;
		bool loaded = false;
		bool listo = false;
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		enum TipoAlgoritmo
		{
			PrimeroEntrarPrimeroSalir = 1,
			TurnoRotatorio,
			PrimeroMásCorto,
			MenorTiempoRestante,
			MayorTasaRespuesta,
			UsadoHaceMásTiempo
		}
		
		private class Algoritmo
		{
			string nombre;
			public string Nombre
			{
				get { return nombre; }
				set { nombre = value; }
			}
			
			int valor;
			public int Valor
			{
				get { return valor; }
				set { valor = value; }
			}
			
			public Algoritmo (string nombre, int valor)
			{
				this.Nombre = nombre;
				this.Valor = valor;
			}
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			tslTiempo.Text = " Ninguna";
			if (!loaded)
			{
				// Definimos los tipos de algoritmos de entrada
				Algoritmo[] algoritmoEntrada = new Algoritmo[5];
				algoritmoEntrada[0] = new Algoritmo("Primero en entrar, primero en salir",1);
				algoritmoEntrada[1] = new Algoritmo("Turno rotatorio",2);
				algoritmoEntrada[2] = new Algoritmo("Primero el más corto",3);
				algoritmoEntrada[3] = new Algoritmo("Menor tiempo restante",4);
				algoritmoEntrada[4] = new Algoritmo("Mayor tasa de respuesta",5);
				// Definimos los tipos de algoritmos de salida
				Algoritmo[] algoritmoSalida = new Algoritmo[2];
				algoritmoSalida[0] = new Algoritmo("Primero en entrar, primero en salir",1);
				algoritmoSalida[1] = new Algoritmo("Usado hace más tiempo",6);
				
				// Llenamos los combobox con los valores que correspondan
				cbEntrada.Items.AddRange(algoritmoEntrada);
				cbEntrada.DataSource = algoritmoEntrada;
				cbEntrada.DisplayMember = "Nombre";
				cbEntrada.ValueMember = "Valor";
				cbSalida.Items.AddRange(algoritmoSalida);
				cbSalida.DataSource = algoritmoSalida;
				cbSalida.DisplayMember = "Nombre";
				cbSalida.ValueMember = "Valor";
				
				loaded = true;
			}
			
			lbPQuantum.Text = Quantum.ToString();
			lbTMemoria.Text = mTamano.ToString();
		}
		
		void NuevaSesión()
		{
			/*
			String sqlString = String.Format("INSERT INTO sesion (id,inicio,fin) VALUES ('{0}','{1}','{2}')",DateTime.Now.ToString("yyyyMMddhhmmss",CultureInfo.CreateSpecificCulture("es-GT")),DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss",CultureInfo.CreateSpecificCulture("es-GT")),DBNull.Value.ToString());
			idSesion = DateTime.Now.ToString("yyyyMMddhhmmss",CultureInfo.CreateSpecificCulture("es-GT"));
			Conexión con = new Conexión();
			conexiónBD = new SqlConnection(con.Obtener());
			try
			{
				conexiónBD.Open();
				SqlCommand orden = new SqlCommand(sqlString,conexiónBD);
				orden.ExecuteNonQuery();
				conexiónBD.Close();
				gbProcesos.Enabled = true;
				gbMemoria.Enabled = true;
				gbAlgoritmo.Enabled = true;
				gbProcesador.Enabled = true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("SQL:" + sqlString + Environment.NewLine + "Error " + Environment.NewLine + ex.ToString(),"Error en conexión con base de datos");
			}
			finally
			{
				richTextBox1.Text = String.Empty;
				tslTiempo.Text = " Activa";
				Restablecer();
				gbHerramientas.Enabled = false;
			}
			*/
			
			/*
			 * OMITIENDO EL GUARDADO DE LA SESIÓN EN LA BD 
			 */
			gbProcesos.Enabled = true;
			gbMemoria.Enabled = true;
			gbAlgoritmo.Enabled = true;
			gbProcesador.Enabled = true;
			richTextBox1.Text = String.Empty;
			tslTiempo.Text = "Activa";
			Restablecer();
			gbHerramientas.Enabled = false;
		}
		
		void CerrarSesión()
		{
			/*
			String sqlString = String.Format("UPDATE sesion SET fin='{0}' WHERE id = '{1}'",DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss",CultureInfo.CreateSpecificCulture("es-GT")),idSesion);
			Conexión con = new Conexión();
			conexiónBD = new SqlConnection(con.Obtener());
			try
			{
				conexiónBD.Open();
				SqlCommand orden = new SqlCommand(sqlString,conexiónBD);
				orden.ExecuteNonQuery();
				conexiónBD.Close();
				gbProcesos.Enabled = false;
				gbMemoria.Enabled = false;
				gbAlgoritmo.Enabled = false;
				gbProcesador.Enabled = false;
			}
			catch(Exception ex)
			{
				MessageBox.Show("Error " + ex.ToString(),"Error en conexión con base de datos");
			}
			finally
			{
				tslTiempo.Text = " Ninguna";
				Restablecer();
			}
			*/
			
			/*
			 * OMITIENDO EL GUARDADO DE LA SESIÓN EN LA BD 
			 */
			gbProcesos.Enabled = false;
			gbMemoria.Enabled = false;
			gbAlgoritmo.Enabled = false;
			gbProcesador.Enabled = false;
			tslTiempo.Text = "Ninguna";
			Restablecer();
		}
		
		void Actualizar()
		{
			// Verificamos si se han cumplido todos los requisitos para someter los procesos a memoria
			if ((mTamano!= 0) && (Quantum != 0) && (procesos.Count > 0))
			{
				listo = true;
				tabPage3.Focus();
			}
			else
				listo = false;
			
			// En caso de cumplirse los requisitos, activamos el groupbox ""
			gbProcesar.Enabled = listo;
		}
		
		void Restablecer()
		{
			frmProcess.Restablecer();
			mTamano = 0;
			Quantum = 0;
			lbTMemoria.Text = mTamano.ToString();
			lbPQuantum.Text = Quantum.ToString();
			procesos.Clear();
			Actualizar();
		}
		
		void SNuevaClick(object sender, EventArgs e)
		{
			NuevaSesión();
			tabPage2.Focus();
		}
		
		void SCerrarClick(object sender, EventArgs e)
		{
			CerrarSesión();
			tabPage1.Focus();
		}
		
		void PDefinirClick(object sender, EventArgs e)
		{
			ArrayList tempProcess = new ArrayList();
			frmProcess.Algoritmo = Convert.ToUInt32(cbEntrada.SelectedValue.ToString());
			frmProcess.Actualizar();
			frmProcess.ShowDialog(this);
			tempProcess.Clear();
			tempProcess.AddRange(frmProcess.Procesos);
			procesos.Clear();
			procesos.AddRange(tempProcess);
			tempProcess.Clear();
			//MessageBox.Show("Se recibieron " + procesos.Count + " procesos");
			Actualizar();
		}		
		
		void PLimpiarClick(object sender, EventArgs e)
		{
			frmProcess.Restablecer();
			Actualizar();
		}
		
		void ACercaClick(object sender, EventArgs e)
		{
			FormAcercaDe about = new FormAcercaDe();
			about.ShowDialog(this);
			about.Dispose();
		}
		
		void PIniciarClick(object sender, EventArgs e)
		{
			/*
			// Llenamos la tabla de base de datos con los procesos utilizados
			Conexión con = new Conexión();
			conexiónBD = new SqlConnection(con.Obtener());
			string sqlString = String.Empty;
			try
			{
				conexiónBD.Open();
				foreach (object Pr in procesos)
				{
					sqlString = String.Format("INSERT INTO procesos (sesion,id,nombre,tamano,tiempo,entrada) VALUES ('{0}','{1}','{2}',{3},{4},{5})",idSesion,DateTime.Now.ToString("yyyyMMddhhmmss",CultureInfo.CreateSpecificCulture("es-GT")),((FormProcesos.ListaProcesos)Pr).Nombre,((FormProcesos.ListaProcesos)Pr).Tamano,((FormProcesos.ListaProcesos)Pr).Tiempo,((FormProcesos.ListaProcesos)Pr).Entrada);
					SqlCommand orden = new SqlCommand(sqlString,conexiónBD);
					orden.ExecuteNonQuery();
					System.Threading.Thread.Sleep(1000);
				}
				conexiónBD.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("SQL: " + sqlString + Environment.NewLine + "Error " + ex.ToString(),"Error en conexión con base de datos");
			}
			 */
			
			Núcleo core = new Núcleo();
			core.Procesar(procesos,Convert.ToUInt32(cbEntrada.SelectedValue),Convert.ToUInt32(cbSalida.SelectedValue),mTamano,Quantum,out registro);
			richTextBox1.Text = registro;
			
			CerrarSesión();
			Restablecer();
			listo = false;
			// Le devolvemos el foco a la primera pestaña para que quede listo para iniciar una nueva sesión
			tabPage1.Focus();
			
			gbHerramientas.Enabled = true;
		}
		
		void MDefinirClick(object sender, EventArgs e)
		{
			// Definimos el tamaño de la memoria en Kb
			mTamano = Convert.ToDouble(txtTMemoria.Text);
			lbTMemoria.Text = mTamano.ToString();
			Actualizar();
		}
		
		void RDefinirClick(object sender, EventArgs e)
		{
			// Definimos el Quantum del procesador en segundos
			Quantum = Convert.ToDouble(txtQuantum.Text);
			lbPQuantum.Text = Quantum.ToString();
			Actualizar();
		}
		
		void BtnExportarClick(object sender, EventArgs e)
		{
			saveFileDialog1.Title = "Exportar archivo...";
			saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
			saveFileDialog1.Filter = "Texto Plano|*.txt|Arhivo registro|*.log";
			saveFileDialog1.FileName = "Ejecución " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss",CultureInfo.CreateSpecificCulture("es-GT"));

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
			    using (var writer = new StreamWriter(saveFileDialog1.FileName, true, System.Text.Encoding.Default))
			    {
			    	writer.WriteLine(registro);
			    }
			}
		}
		
		void BtnCopiarPPClick(object sender, EventArgs e)
		{
			Clipboard.SetText(richTextBox1.Text);
		}
	}
}
