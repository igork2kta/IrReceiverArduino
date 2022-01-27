namespace IrReceiver {
    partial class main {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.cboPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btnUpdatePorts = new System.Windows.Forms.Button();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnParar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cbIniciarAutomaticamente = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnConfigurar = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbIniciarComWindows = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboPorts
            // 
            this.cboPorts.AccessibleDescription = "";
            this.cboPorts.FormattingEnabled = true;
            this.cboPorts.Location = new System.Drawing.Point(43, 20);
            this.cboPorts.Name = "cboPorts";
            this.cboPorts.Size = new System.Drawing.Size(61, 21);
            this.cboPorts.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Porta";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // btnUpdatePorts
            // 
            this.btnUpdatePorts.Location = new System.Drawing.Point(43, 47);
            this.btnUpdatePorts.Name = "btnUpdatePorts";
            this.btnUpdatePorts.Size = new System.Drawing.Size(61, 23);
            this.btnUpdatePorts.TabIndex = 3;
            this.btnUpdatePorts.Text = "Atualizar";
            this.btnUpdatePorts.UseVisualStyleBackColor = true;
            this.btnUpdatePorts.Click += new System.EventHandler(this.btnUpdatePorts_Click);
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(119, 20);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(75, 23);
            this.btnIniciar.TabIndex = 4;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnParar
            // 
            this.btnParar.Location = new System.Drawing.Point(200, 20);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(75, 23);
            this.btnParar.TabIndex = 5;
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(271, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "Desenvolvido por Igor Pinheiro da Silva - igork2kta@hotmail.com";
            // 
            // cbIniciarAutomaticamente
            // 
            this.cbIniciarAutomaticamente.AutoSize = true;
            this.cbIniciarAutomaticamente.Location = new System.Drawing.Point(119, 51);
            this.cbIniciarAutomaticamente.Name = "cbIniciarAutomaticamente";
            this.cbIniciarAutomaticamente.Size = new System.Drawing.Size(184, 17);
            this.cbIniciarAutomaticamente.TabIndex = 7;
            this.cbIniciarAutomaticamente.Text = "Iniciar Conexão Automaticamente";
            this.cbIniciarAutomaticamente.UseVisualStyleBackColor = true;
            this.cbIniciarAutomaticamente.CheckedChanged += new System.EventHandler(this.cbIniciarAutomaticamente_CheckedChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Receptor Infravermelho";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.BalloonTipClicked += new System.EventHandler(this.notifyIcon1_BalloonTipClicked);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Status:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(81, 96);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 9;
            // 
            // btnConfigurar
            // 
            this.btnConfigurar.AccessibleDescription = "";
            this.btnConfigurar.BackColor = System.Drawing.Color.Transparent;
            this.btnConfigurar.BackgroundImage = global::IrReceiver.Properties.Resources.config;
            this.btnConfigurar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnConfigurar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfigurar.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnConfigurar.Location = new System.Drawing.Point(287, 115);
            this.btnConfigurar.Name = "btnConfigurar";
            this.btnConfigurar.Size = new System.Drawing.Size(24, 24);
            this.btnConfigurar.TabIndex = 10;
            this.btnConfigurar.UseVisualStyleBackColor = false;
            this.btnConfigurar.Click += new System.EventHandler(this.btnConfigurar_Click);
            // 
            // cbIniciarComWindows
            // 
            this.cbIniciarComWindows.AutoSize = true;
            this.cbIniciarComWindows.Location = new System.Drawing.Point(118, 74);
            this.cbIniciarComWindows.Name = "cbIniciarComWindows";
            this.cbIniciarComWindows.Size = new System.Drawing.Size(133, 17);
            this.cbIniciarComWindows.TabIndex = 11;
            this.cbIniciarComWindows.Text = "Iniciar com o Windows";
            this.cbIniciarComWindows.UseVisualStyleBackColor = true;
            this.cbIniciarComWindows.CheckedChanged += new System.EventHandler(this.cbIniciarComWindows_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 149);
            this.Controls.Add(this.cbIniciarComWindows);
            this.Controls.Add(this.btnConfigurar);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbIniciarAutomaticamente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.btnUpdatePorts);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receptor Infravermelho";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboPorts;
        private System.Windows.Forms.Label label1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btnUpdatePorts;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbIniciarAutomaticamente;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnConfigurar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbIniciarComWindows;
    }
}

