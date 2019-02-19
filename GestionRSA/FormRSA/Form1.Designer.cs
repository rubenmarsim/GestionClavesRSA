namespace FormRSA
{
    partial class frmRSA
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateRSA = new System.Windows.Forms.Button();
            this.btnReadRSA = new System.Windows.Forms.Button();
            this.btnSaveToDB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateRSA
            // 
            this.btnCreateRSA.Location = new System.Drawing.Point(275, 61);
            this.btnCreateRSA.Name = "btnCreateRSA";
            this.btnCreateRSA.Size = new System.Drawing.Size(193, 89);
            this.btnCreateRSA.TabIndex = 0;
            this.btnCreateRSA.Text = "Create and Write RSA Keys";
            this.btnCreateRSA.UseVisualStyleBackColor = true;
            this.btnCreateRSA.Click += new System.EventHandler(this.btnCreateRSA_Click);
            // 
            // btnReadRSA
            // 
            this.btnReadRSA.Location = new System.Drawing.Point(275, 173);
            this.btnReadRSA.Name = "btnReadRSA";
            this.btnReadRSA.Size = new System.Drawing.Size(193, 78);
            this.btnReadRSA.TabIndex = 1;
            this.btnReadRSA.Text = "Read RSA Keys";
            this.btnReadRSA.UseVisualStyleBackColor = true;
            this.btnReadRSA.Click += new System.EventHandler(this.btnReadRSA_Click);
            // 
            // btnSaveToDB
            // 
            this.btnSaveToDB.Location = new System.Drawing.Point(275, 270);
            this.btnSaveToDB.Name = "btnSaveToDB";
            this.btnSaveToDB.Size = new System.Drawing.Size(193, 91);
            this.btnSaveToDB.TabIndex = 2;
            this.btnSaveToDB.Text = "Save To DB";
            this.btnSaveToDB.UseVisualStyleBackColor = true;
            this.btnSaveToDB.Click += new System.EventHandler(this.btnSaveToDB_Click);
            // 
            // frmRSA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSaveToDB);
            this.Controls.Add(this.btnReadRSA);
            this.Controls.Add(this.btnCreateRSA);
            this.Name = "frmRSA";
            this.Text = "frmRSA";
            this.Load += new System.EventHandler(this.frmRSA_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateRSA;
        private System.Windows.Forms.Button btnReadRSA;
        private System.Windows.Forms.Button btnSaveToDB;
    }
}

