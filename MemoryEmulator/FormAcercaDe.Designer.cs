/*
 * Creado por SharpDevelop.
 * Usuario: gchinchilla
 * Fecha: 14/06/2012
 * Hora: 07:44 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
namespace MemoryEmulator
{
	partial class FormAcercaDe
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
			this.lbCopyright = new System.Windows.Forms.Label();
			this.richTxtContenido = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// lbCopyright
			// 
			this.lbCopyright.Location = new System.Drawing.Point(104, 244);
			this.lbCopyright.Name = "lbCopyright";
			this.lbCopyright.Size = new System.Drawing.Size(237, 23);
			this.lbCopyright.TabIndex = 3;
			this.lbCopyright.Text = "...";
			// 
			// richTxtContenido
			// 
			this.richTxtContenido.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.richTxtContenido.Location = new System.Drawing.Point(2, 0);
			this.richTxtContenido.Name = "richTxtContenido";
			this.richTxtContenido.ReadOnly = true;
			this.richTxtContenido.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
			this.richTxtContenido.Size = new System.Drawing.Size(342, 227);
			this.richTxtContenido.TabIndex = 2;
			this.richTxtContenido.Text = "";
			// 
			// FormAcercaDe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(345, 268);
			this.Controls.Add(this.lbCopyright);
			this.Controls.Add(this.richTxtContenido);
			this.Name = "FormAcercaDe";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Acerca de...";
			this.Load += new System.EventHandler(this.FormAcercaDeLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.RichTextBox richTxtContenido;
		private System.Windows.Forms.Label lbCopyright;
	}
}
