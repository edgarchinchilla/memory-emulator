/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 04/06/2012
 * Hora: 09:27 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
namespace MemoryEmulator
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.sCerrar = new System.Windows.Forms.Button();
			this.sNueva = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.gbProcesador = new System.Windows.Forms.GroupBox();
			this.lbPQuantum = new System.Windows.Forms.Label();
			this.rDefinir = new System.Windows.Forms.Button();
			this.txtQuantum = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.gbAlgoritmo = new System.Windows.Forms.GroupBox();
			this.cbSalida = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cbEntrada = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gbMemoria = new System.Windows.Forms.GroupBox();
			this.lbTMemoria = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.mDefinir = new System.Windows.Forms.Button();
			this.txtTMemoria = new System.Windows.Forms.TextBox();
			this.gbProcesos = new System.Windows.Forms.GroupBox();
			this.pLimpiar = new System.Windows.Forms.Button();
			this.pDefinir = new System.Windows.Forms.Button();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.gbHerramientas = new System.Windows.Forms.GroupBox();
			this.btnCopiarPP = new System.Windows.Forms.Button();
			this.btnExportar = new System.Windows.Forms.Button();
			this.gbProcesar = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.pIniciar = new System.Windows.Forms.Button();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.aCerca = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tslTiempo = new System.Windows.Forms.ToolStripStatusLabel();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.gbProcesador.SuspendLayout();
			this.gbAlgoritmo.SuspendLayout();
			this.gbMemoria.SuspendLayout();
			this.gbProcesos.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.gbHerramientas.SuspendLayout();
			this.gbProcesar.SuspendLayout();
			this.tabPage4.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(1, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(792, 117);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(784, 91);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Inicio";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.sCerrar);
			this.groupBox1.Controls.Add(this.sNueva);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(94, 82);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Sesión";
			// 
			// sCerrar
			// 
			this.sCerrar.Location = new System.Drawing.Point(7, 50);
			this.sCerrar.Name = "sCerrar";
			this.sCerrar.Size = new System.Drawing.Size(75, 23);
			this.sCerrar.TabIndex = 1;
			this.sCerrar.Text = "Cerrar";
			this.sCerrar.UseVisualStyleBackColor = true;
			this.sCerrar.Click += new System.EventHandler(this.SCerrarClick);
			// 
			// sNueva
			// 
			this.sNueva.Location = new System.Drawing.Point(7, 20);
			this.sNueva.Name = "sNueva";
			this.sNueva.Size = new System.Drawing.Size(75, 23);
			this.sNueva.TabIndex = 0;
			this.sNueva.Text = "Nueva";
			this.sNueva.UseVisualStyleBackColor = true;
			this.sNueva.Click += new System.EventHandler(this.SNuevaClick);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.gbProcesador);
			this.tabPage2.Controls.Add(this.gbAlgoritmo);
			this.tabPage2.Controls.Add(this.gbMemoria);
			this.tabPage2.Controls.Add(this.gbProcesos);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(784, 91);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Memoria";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// gbProcesador
			// 
			this.gbProcesador.Controls.Add(this.lbPQuantum);
			this.gbProcesador.Controls.Add(this.rDefinir);
			this.gbProcesador.Controls.Add(this.txtQuantum);
			this.gbProcesador.Controls.Add(this.label4);
			this.gbProcesador.Enabled = false;
			this.gbProcesador.Location = new System.Drawing.Point(619, 3);
			this.gbProcesador.Name = "gbProcesador";
			this.gbProcesador.Size = new System.Drawing.Size(161, 82);
			this.gbProcesador.TabIndex = 7;
			this.gbProcesador.TabStop = false;
			this.gbProcesador.Text = "Procesador";
			// 
			// lbPQuantum
			// 
			this.lbPQuantum.Enabled = false;
			this.lbPQuantum.Location = new System.Drawing.Point(79, 19);
			this.lbPQuantum.Name = "lbPQuantum";
			this.lbPQuantum.Size = new System.Drawing.Size(75, 23);
			this.lbPQuantum.TabIndex = 4;
			this.lbPQuantum.Text = "label6";
			this.lbPQuantum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// rDefinir
			// 
			this.rDefinir.Location = new System.Drawing.Point(79, 48);
			this.rDefinir.Name = "rDefinir";
			this.rDefinir.Size = new System.Drawing.Size(75, 23);
			this.rDefinir.TabIndex = 3;
			this.rDefinir.Text = "Definir";
			this.rDefinir.UseVisualStyleBackColor = true;
			this.rDefinir.Click += new System.EventHandler(this.RDefinirClick);
			// 
			// txtQuantum
			// 
			this.txtQuantum.Location = new System.Drawing.Point(4, 48);
			this.txtQuantum.Name = "txtQuantum";
			this.txtQuantum.Size = new System.Drawing.Size(69, 20);
			this.txtQuantum.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Quantum (seg):";
			// 
			// gbAlgoritmo
			// 
			this.gbAlgoritmo.Controls.Add(this.cbSalida);
			this.gbAlgoritmo.Controls.Add(this.label3);
			this.gbAlgoritmo.Controls.Add(this.cbEntrada);
			this.gbAlgoritmo.Controls.Add(this.label2);
			this.gbAlgoritmo.Enabled = false;
			this.gbAlgoritmo.Location = new System.Drawing.Point(318, 3);
			this.gbAlgoritmo.Name = "gbAlgoritmo";
			this.gbAlgoritmo.Size = new System.Drawing.Size(286, 82);
			this.gbAlgoritmo.TabIndex = 6;
			this.gbAlgoritmo.TabStop = false;
			this.gbAlgoritmo.Text = "Algoritmos";
			// 
			// cbSalida
			// 
			this.cbSalida.FormattingEnabled = true;
			this.cbSalida.Location = new System.Drawing.Point(153, 47);
			this.cbSalida.Name = "cbSalida";
			this.cbSalida.Size = new System.Drawing.Size(126, 21);
			this.cbSalida.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(153, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Salida:";
			// 
			// cbEntrada
			// 
			this.cbEntrada.FormattingEnabled = true;
			this.cbEntrada.Location = new System.Drawing.Point(7, 48);
			this.cbEntrada.Name = "cbEntrada";
			this.cbEntrada.Size = new System.Drawing.Size(126, 21);
			this.cbEntrada.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(7, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Entrada:";
			// 
			// gbMemoria
			// 
			this.gbMemoria.Controls.Add(this.lbTMemoria);
			this.gbMemoria.Controls.Add(this.label1);
			this.gbMemoria.Controls.Add(this.mDefinir);
			this.gbMemoria.Controls.Add(this.txtTMemoria);
			this.gbMemoria.Enabled = false;
			this.gbMemoria.Location = new System.Drawing.Point(138, 3);
			this.gbMemoria.Name = "gbMemoria";
			this.gbMemoria.Size = new System.Drawing.Size(166, 82);
			this.gbMemoria.TabIndex = 5;
			this.gbMemoria.TabStop = false;
			this.gbMemoria.Text = "Memoria";
			// 
			// lbTMemoria
			// 
			this.lbTMemoria.Enabled = false;
			this.lbTMemoria.Location = new System.Drawing.Point(81, 19);
			this.lbTMemoria.Name = "lbTMemoria";
			this.lbTMemoria.Size = new System.Drawing.Size(75, 23);
			this.lbTMemoria.TabIndex = 3;
			this.lbTMemoria.Text = "label6";
			this.lbTMemoria.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(7, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Tamaño (Kb):";
			// 
			// mDefinir
			// 
			this.mDefinir.Location = new System.Drawing.Point(81, 48);
			this.mDefinir.Name = "mDefinir";
			this.mDefinir.Size = new System.Drawing.Size(75, 23);
			this.mDefinir.TabIndex = 1;
			this.mDefinir.Text = "Definir";
			this.mDefinir.UseVisualStyleBackColor = true;
			this.mDefinir.Click += new System.EventHandler(this.MDefinirClick);
			// 
			// txtTMemoria
			// 
			this.txtTMemoria.Location = new System.Drawing.Point(6, 48);
			this.txtTMemoria.Name = "txtTMemoria";
			this.txtTMemoria.Size = new System.Drawing.Size(69, 20);
			this.txtTMemoria.TabIndex = 0;
			// 
			// gbProcesos
			// 
			this.gbProcesos.Controls.Add(this.pLimpiar);
			this.gbProcesos.Controls.Add(this.pDefinir);
			this.gbProcesos.Enabled = false;
			this.gbProcesos.Location = new System.Drawing.Point(7, 3);
			this.gbProcesos.Name = "gbProcesos";
			this.gbProcesos.Size = new System.Drawing.Size(118, 82);
			this.gbProcesos.TabIndex = 4;
			this.gbProcesos.TabStop = false;
			this.gbProcesos.Text = "Procesos";
			// 
			// pLimpiar
			// 
			this.pLimpiar.Location = new System.Drawing.Point(7, 48);
			this.pLimpiar.Name = "pLimpiar";
			this.pLimpiar.Size = new System.Drawing.Size(105, 23);
			this.pLimpiar.TabIndex = 1;
			this.pLimpiar.Text = "Limpiar";
			this.pLimpiar.UseVisualStyleBackColor = true;
			this.pLimpiar.Click += new System.EventHandler(this.PLimpiarClick);
			// 
			// pDefinir
			// 
			this.pDefinir.Location = new System.Drawing.Point(7, 19);
			this.pDefinir.Name = "pDefinir";
			this.pDefinir.Size = new System.Drawing.Size(105, 23);
			this.pDefinir.TabIndex = 0;
			this.pDefinir.Text = "Definir";
			this.pDefinir.UseVisualStyleBackColor = true;
			this.pDefinir.Click += new System.EventHandler(this.PDefinirClick);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.gbHerramientas);
			this.tabPage3.Controls.Add(this.gbProcesar);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(784, 91);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Proceso";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// gbHerramientas
			// 
			this.gbHerramientas.Controls.Add(this.btnCopiarPP);
			this.gbHerramientas.Controls.Add(this.btnExportar);
			this.gbHerramientas.Enabled = false;
			this.gbHerramientas.Location = new System.Drawing.Point(114, 3);
			this.gbHerramientas.Name = "gbHerramientas";
			this.gbHerramientas.Size = new System.Drawing.Size(170, 82);
			this.gbHerramientas.TabIndex = 4;
			this.gbHerramientas.TabStop = false;
			this.gbHerramientas.Text = "Herramientas";
			// 
			// btnCopiarPP
			// 
			this.btnCopiarPP.Location = new System.Drawing.Point(7, 54);
			this.btnCopiarPP.Name = "btnCopiarPP";
			this.btnCopiarPP.Size = new System.Drawing.Size(157, 23);
			this.btnCopiarPP.TabIndex = 1;
			this.btnCopiarPP.Text = "Copiar al portapapeles";
			this.btnCopiarPP.UseVisualStyleBackColor = true;
			this.btnCopiarPP.Click += new System.EventHandler(this.BtnCopiarPPClick);
			// 
			// btnExportar
			// 
			this.btnExportar.Location = new System.Drawing.Point(7, 21);
			this.btnExportar.Name = "btnExportar";
			this.btnExportar.Size = new System.Drawing.Size(157, 23);
			this.btnExportar.TabIndex = 0;
			this.btnExportar.Text = "Exportar a TXT";
			this.btnExportar.UseVisualStyleBackColor = true;
			this.btnExportar.Click += new System.EventHandler(this.BtnExportarClick);
			// 
			// gbProcesar
			// 
			this.gbProcesar.Controls.Add(this.label5);
			this.gbProcesar.Controls.Add(this.pIniciar);
			this.gbProcesar.Enabled = false;
			this.gbProcesar.Location = new System.Drawing.Point(6, 4);
			this.gbProcesar.Name = "gbProcesar";
			this.gbProcesar.Size = new System.Drawing.Size(94, 82);
			this.gbProcesar.TabIndex = 3;
			this.gbProcesar.TabStop = false;
			this.gbProcesar.Text = "Proceso";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(7, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 23);
			this.label5.TabIndex = 1;
			this.label5.Text = "(Ejecutar)";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pIniciar
			// 
			this.pIniciar.Location = new System.Drawing.Point(6, 53);
			this.pIniciar.Name = "pIniciar";
			this.pIniciar.Size = new System.Drawing.Size(81, 23);
			this.pIniciar.TabIndex = 0;
			this.pIniciar.Text = "Iniciar";
			this.pIniciar.UseVisualStyleBackColor = true;
			this.pIniciar.Click += new System.EventHandler(this.PIniciarClick);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.groupBox6);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(784, 91);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Ayuda";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.aCerca);
			this.groupBox6.Location = new System.Drawing.Point(7, 6);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(118, 82);
			this.groupBox6.TabIndex = 6;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Acerca de...";
			// 
			// aCerca
			// 
			this.aCerca.Location = new System.Drawing.Point(7, 19);
			this.aCerca.Name = "aCerca";
			this.aCerca.Size = new System.Drawing.Size(105, 23);
			this.aCerca.TabIndex = 0;
			this.aCerca.Text = "Información";
			this.aCerca.UseVisualStyleBackColor = true;
			this.aCerca.Click += new System.EventHandler(this.ACercaClick);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(1, 122);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.ReadOnly = true;
			this.richTextBox1.Size = new System.Drawing.Size(792, 260);
			this.richTextBox1.TabIndex = 1;
			this.richTextBox1.Text = "";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1,
									this.tslTiempo});
			this.statusStrip1.Location = new System.Drawing.Point(0, 386);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(795, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 17);
			this.toolStripStatusLabel1.Text = "Sesión:";
			// 
			// tslTiempo
			// 
			this.tslTiempo.Name = "tslTiempo";
			this.tslTiempo.Size = new System.Drawing.Size(0, 17);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(795, 408);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(805, 440);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "UMG Memory Emulator";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.gbProcesador.ResumeLayout(false);
			this.gbProcesador.PerformLayout();
			this.gbAlgoritmo.ResumeLayout(false);
			this.gbMemoria.ResumeLayout(false);
			this.gbMemoria.PerformLayout();
			this.gbProcesos.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.gbHerramientas.ResumeLayout(false);
			this.gbProcesar.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label lbTMemoria;
		private System.Windows.Forms.Label lbPQuantum;
		private System.Windows.Forms.Button btnExportar;
		private System.Windows.Forms.Button btnCopiarPP;
		private System.Windows.Forms.GroupBox gbHerramientas;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtQuantum;
		private System.Windows.Forms.Button rDefinir;
		private System.Windows.Forms.GroupBox gbProcesador;
		private System.Windows.Forms.ToolStripStatusLabel tslTiempo;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Button aCerca;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.Button pIniciar;
		private System.Windows.Forms.GroupBox gbProcesar;
		private System.Windows.Forms.TextBox txtTMemoria;
		private System.Windows.Forms.Button mDefinir;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox gbMemoria;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbEntrada;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cbSalida;
		private System.Windows.Forms.GroupBox gbAlgoritmo;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button pDefinir;
		private System.Windows.Forms.Button pLimpiar;
		private System.Windows.Forms.GroupBox gbProcesos;
		private System.Windows.Forms.Button sNueva;
		private System.Windows.Forms.Button sCerrar;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
	}
}
