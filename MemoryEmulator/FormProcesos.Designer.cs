/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 14/06/2012
 * Hora: 02:40 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
namespace MemoryEmulator
{
	partial class FormProcesos
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcesos));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnAgregar = new System.Windows.Forms.ToolStripButton();
			this.btnQuitar = new System.Windows.Forms.ToolStripButton();
			this.btnGuardar = new System.Windows.Forms.ToolStripButton();
			this.txtNombre1 = new System.Windows.Forms.TextBox();
			this.txtTamano1 = new System.Windows.Forms.TextBox();
			this.txtTiempo1 = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtEntrada1 = new System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Nombre:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(170, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "Tamaño (Kb):";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(276, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "Tiempo (seg):";
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.btnAgregar,
									this.btnQuitar,
									this.btnGuardar});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(495, 54);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnAgregar
			// 
			this.btnAgregar.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregar.Image")));
			this.btnAgregar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAgregar.Name = "btnAgregar";
			this.btnAgregar.Size = new System.Drawing.Size(53, 51);
			this.btnAgregar.Text = "Agregar";
			this.btnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnAgregar.Click += new System.EventHandler(this.BtnAgregarClick);
			// 
			// btnQuitar
			// 
			this.btnQuitar.Image = ((System.Drawing.Image)(resources.GetObject("btnQuitar.Image")));
			this.btnQuitar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnQuitar.Name = "btnQuitar";
			this.btnQuitar.Size = new System.Drawing.Size(44, 51);
			this.btnQuitar.Text = "Quitar";
			this.btnQuitar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnQuitar.Click += new System.EventHandler(this.BtnQuitarClick);
			// 
			// btnGuardar
			// 
			this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
			this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnGuardar.Name = "btnGuardar";
			this.btnGuardar.Size = new System.Drawing.Size(53, 51);
			this.btnGuardar.Text = "Guardar";
			this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnGuardar.Click += new System.EventHandler(this.BtnGuardarClick);
			// 
			// txtNombre1
			// 
			this.txtNombre1.Location = new System.Drawing.Point(4, 78);
			this.txtNombre1.Name = "txtNombre1";
			this.txtNombre1.Size = new System.Drawing.Size(160, 20);
			this.txtNombre1.TabIndex = 4;
			// 
			// txtTamano1
			// 
			this.txtTamano1.Location = new System.Drawing.Point(171, 78);
			this.txtTamano1.Name = "txtTamano1";
			this.txtTamano1.Size = new System.Drawing.Size(100, 20);
			this.txtTamano1.TabIndex = 5;
			// 
			// txtTiempo1
			// 
			this.txtTiempo1.Location = new System.Drawing.Point(278, 78);
			this.txtTiempo1.Name = "txtTiempo1";
			this.txtTiempo1.Size = new System.Drawing.Size(100, 20);
			this.txtTiempo1.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(385, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Entrada (seg):";
			// 
			// txtEntrada1
			// 
			this.txtEntrada1.Location = new System.Drawing.Point(385, 78);
			this.txtEntrada1.Name = "txtEntrada1";
			this.txtEntrada1.Size = new System.Drawing.Size(100, 20);
			this.txtEntrada1.TabIndex = 8;
			// 
			// FormProcesos
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(495, 268);
			this.Controls.Add(this.txtEntrada1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtTiempo1);
			this.Controls.Add(this.txtTamano1);
			this.Controls.Add(this.txtNombre1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MinimumSize = new System.Drawing.Size(505, 300);
			this.Name = "FormProcesos";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Procesos";
			this.Load += new System.EventHandler(this.FormProcesosLoad);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox txtEntrada1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtTiempo1;
		private System.Windows.Forms.TextBox txtTamano1;
		private System.Windows.Forms.TextBox txtNombre1;
		private System.Windows.Forms.ToolStripButton btnGuardar;
		private System.Windows.Forms.ToolStripButton btnQuitar;
		private System.Windows.Forms.ToolStripButton btnAgregar;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
