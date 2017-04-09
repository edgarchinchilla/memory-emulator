/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 14/06/2012
 * Hora: 02:40 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryEmulator
{
	/// <summary>
	/// Description of FormProcesos.
	/// </summary>
	public partial class FormProcesos : Form
	{
		int contador = 1;
		int drawpoint = 78;
		List<ObjetosTxt> objetos = new List<ObjetosTxt>();
		bool usarEntrada = false;
		ArrayList procesos = new ArrayList();
		
		public FormProcesos()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			objetos.Add(new ObjetosTxt(txtNombre1,txtTamano1,txtTiempo1,txtEntrada1));
			Actualizar();
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
		
		uint algoritmo;
		public uint Algoritmo
		{
			set { algoritmo = value; }
		}
		
		public ArrayList Procesos
		{
			get { return procesos; }
		}
		
		private class ObjetosTxt
		{
			public ObjetosTxt (object txtnombre, object txttamano, object txttiempo, object txtentrada)
			{
				this.TxtNombre = txtnombre;
				this.TxtTamano = txttamano;
				this.TxtTiempo = txttiempo;
				this.TxtEntrada = txtentrada;
			}
				
			public object TxtNombre { get; private set; }
			public object TxtTamano { get; private set; }
			public object TxtTiempo	{ get; private set; }
			public object TxtEntrada{ get; private set; }
		}
		
		public class ListaProcesos
		{
			public ListaProcesos (string nombre, double tamano, double tiempo, double entrada)
			{
				this.Nombre = nombre;
				this.Tamano = tamano;
				this.Tiempo = tiempo;
				this.Entrada = entrada;
			}
				
			public string Nombre { get; private set; }
			public double Tamano { get; private set; }
			public double Tiempo { get; private set; }
			public double Entrada{ get; private set; }
		}
		
		void FormProcesosLoad(object sender, EventArgs e)
		{
			Actualizar();
		}
		
		void BtnAgregarClick(object sender, EventArgs e)
		{
			drawpoint += 26;
			contador++;
			
				// Creamos dinámicamente los TextBox
				TextBox txtN = new TextBox();
				txtN.Name = "txtNombre" + contador.ToString();
				txtN.Visible = true;
				txtN.Size = new Size(160,20);
				txtN.Location =  new Point(4,drawpoint);
				this.Controls.Add(txtN);
				TextBox txtT = new TextBox();
				txtT.Name = "txtTamano" + contador.ToString();
				txtT.Size = new Size(100,20);
				txtT.Location = new Point(171,drawpoint);
				this.Controls.Add(txtT);
				TextBox txtP = new TextBox();
				txtP.Name = "txtTiempo" + contador.ToString();
				txtP.Size = new Size(100,20);
				txtP.Location =  new Point(278,drawpoint);
				this.Controls.Add(txtP);
				TextBox txtE = new TextBox();
				txtE.Name = "txtEntrada" + contador.ToString();
				txtE.Enabled = usarEntrada;
				txtE.Size = new Size(100,20);
				txtE.Location = new Point(385,drawpoint);
				this.Controls.Add(txtE);
				
			// Guardamos la referencia de los TextBox
			objetos.Add( new ObjetosTxt(txtN,txtT,txtP,txtE));
			
			// Le damos el foco al campo nombre de la nueva fila
			txtN.Focus();
		}
		
		void BtnQuitarClick(object sender, EventArgs e)
		{
			if (contador > 1)
			{
				this.Controls.Remove((TextBox)objetos[contador - 1].TxtNombre);
				this.Controls.Remove((TextBox)objetos[contador - 1].TxtTamano);
				this.Controls.Remove((TextBox)objetos[contador - 1].TxtTiempo);
				this.Controls.Remove((TextBox)objetos[contador - 1].TxtEntrada);
				
				// Eliminando permanentemente las referencias y actulizando contadores y puntos de dibujo
				objetos.RemoveAt(contador - 1);
				contador--;
				drawpoint -= 26;
				// Le damos el foco al campo nombre de la última fila
				((TextBox)objetos[contador - 1].TxtNombre).Focus();
			}
		}
		
		void BtnGuardarClick(object sender, EventArgs e)
		{
			bool informar = false;
			string entradaTemp = String.Empty;
			for (int ciclo = 0; ciclo < objetos.Count; ciclo++)
			{
				// Obtenemos el valor del campo entrada, ya que en caso de no ser un algoritmo que lo utilice,
				// éste tendrá un valor Null o Empty, de ser el caso, le damos valor string "0" para que al
				// convertirlo a Double no de problema.
				entradaTemp = ((TextBox)objetos[ciclo].TxtEntrada).Text;
				if ((entradaTemp == "") || (entradaTemp == String.Empty))
					entradaTemp = "0";
				
				if ( (((TextBox)objetos[ciclo].TxtNombre).Text != "") && (((TextBox)objetos[ciclo].TxtNombre).Text != String.Empty) && (((TextBox)objetos[ciclo].TxtTamano).Text != "") && (((TextBox)objetos[ciclo].TxtTamano).Text != String.Empty) && (((TextBox)objetos[ciclo].TxtTiempo).Text != "") && (((TextBox)objetos[ciclo].TxtTiempo).Text != String.Empty) )
				{
					if ( (TipoAlgoritmo)algoritmo == TipoAlgoritmo.TurnoRotatorio )
					{
						if ( (((TextBox)objetos[ciclo].TxtEntrada).Text != "") && (((TextBox)objetos[ciclo].TxtEntrada).Text != "") )
						{
							procesos.Add(new ListaProcesos( ((TextBox)objetos[ciclo].TxtNombre).Text, Convert.ToDouble(((TextBox)objetos[ciclo].TxtTamano).Text), Convert.ToDouble(((TextBox)objetos[ciclo].TxtTiempo).Text), Convert.ToDouble(((TextBox)objetos[ciclo].TxtEntrada).Text) ));
						}
						else
							informar = true;
					}
					else
						procesos.Add(new ListaProcesos( ((TextBox)objetos[ciclo].TxtNombre).Text, Convert.ToDouble(((TextBox)objetos[ciclo].TxtTamano).Text), Convert.ToDouble(((TextBox)objetos[ciclo].TxtTiempo).Text), Convert.ToDouble(entradaTemp) ));
				}
				else
					informar = true;
			}
			
			if (informar)
				MessageBox.Show("No se han tomado en cuenta todos los procesos porque alguno no tenían todos los valores necesarios." + Environment.NewLine + "Sólo se han guardado " + procesos.Count.ToString() + " procesos.","Atención",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			
			this.Close();
		}
		
		public void Restablecer()
		{
			txtNombre1.Text = String.Empty;
			txtTamano1.Text = String.Empty;
			txtTiempo1.Text = String.Empty;
			txtEntrada1.Text = String.Empty;
			int pos = 1;
			int elementos = objetos.Count;
			
			for (int ciclo = 1; ciclo < elementos; ciclo++)
			{
				this.Controls.Remove((TextBox)objetos[pos].TxtNombre);
				this.Controls.Remove((TextBox)objetos[pos].TxtTamano);
				this.Controls.Remove((TextBox)objetos[pos].TxtTiempo);
				this.Controls.Remove((TextBox)objetos[pos].TxtEntrada);
				
				objetos.RemoveAt(pos);
				contador--;
				drawpoint -= 26;
			}
			
			procesos.Clear();
			Actualizar();
		}
		
		public void Actualizar()
		{
			if ((TipoAlgoritmo)algoritmo == TipoAlgoritmo.TurnoRotatorio)
			{
				usarEntrada = true;
				txtEntrada1.Enabled = usarEntrada;
			}
			else
			{
				usarEntrada = false;
				txtEntrada1.Enabled = usarEntrada;
			}
						
			for (int ciclo = 0; ciclo < objetos.Count; ciclo++)
			{
				((TextBox)objetos[ciclo].TxtEntrada).Enabled = usarEntrada;
			}
		}
	}
}