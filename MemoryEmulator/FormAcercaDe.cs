/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 14/06/2012
 * Hora: 07:44 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;

namespace MemoryEmulator
{
	/// <summary>
	/// Description of FormAcercaDe.
	/// </summary>
	public partial class FormAcercaDe : Form
	{
		public FormAcercaDe()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void FormAcercaDeLoad(object sender, EventArgs e)
		{
			// TODO:
			// Obtener la versión de UMG Linear Programming Solver
			System.Reflection.Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
			Version versión = ensamblado.GetName().Version;
			System.IO.FileInfo informaciónApp = new System.IO.FileInfo(ensamblado.Location);
			DateTime fechaModificación = informaciónApp.LastWriteTime;
			string información = String.Empty;
			
			richTxtContenido.Text += Environment.NewLine;
			// Nombre del módulo
			//richTxtContenido.Text += ensamblado.GetName().Name;
			richTxtContenido.Text += "UMG Memory Emulator";
			richTxtContenido.Text += Environment.NewLine;
			// Versión del módulo
			richTxtContenido.Text += ("Versión: ").PadRight(22) + String.Format("{0}.{1}.{2}",versión.Major.ToString(),versión.Minor.ToString(),versión.Build.ToString());
			richTxtContenido.Text += Environment.NewLine;
			// Compilación del módulo
			richTxtContenido.Text += ("Compilación: ").PadRight(22) + versión.Revision.ToString();
			richTxtContenido.Text += Environment.NewLine;
			// Arquitectura
			richTxtContenido.Text += ("Arquitectura: ").PadRight(22) + "x86-x64";
			richTxtContenido.Text += Environment.NewLine;
			// Fecha de la compilación
			richTxtContenido.Text += ("Fecha:").PadRight(22) + String.Format("{0}",fechaModificación.ToString("dd/MM/yyyy hh:mm:ss",CultureInfo.CreateSpecificCulture("es-GT")));
			richTxtContenido.Text += Environment.NewLine;
			// Autor
			lbCopyright.Text = String.Format("{0} {1}","Edgar Gerardo Chinchilla Mazate, CopyLeft", DateTime.Now.Year.ToString());
		}
	}
}
