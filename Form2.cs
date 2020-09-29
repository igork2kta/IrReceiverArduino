using System;
using System.Linq;
using System.Windows.Forms;

namespace IrReceiver {
    public partial class Form2 : Form {
        string recebido;
        Form3 janela = new Form3();

        public Form2(string porta) {
            InitializeComponent();
            serialPort1.PortName = porta;
            serialPort1.Open();
        }

        private void bntVolumeUp_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.volumeup = recebido;
            Properties.Settings.Default.Save();
        }

        private void bntVolumeDown_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.volumedown = recebido;
            Properties.Settings.Default.Save();
        }

        private void bntMute_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.mute = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnRightArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.rightarrow = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnLeftArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.leftarrow = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnUpArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.uparrow = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnDownArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.downarrow = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnPlayPause_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.playpause = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnMediaNext_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.medianext = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnMediaPrevious_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.mediaprevious = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnTelaCheia_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.fullscreen = recebido;
            Properties.Settings.Default.Save();
        }                

        private void btnHibernar_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.hibernate = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnDesligar_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.shutdown = recebido;
            Properties.Settings.Default.Save();
        }

        private void btnProjetar_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            Properties.Settings.Default.project = recebido;
            Properties.Settings.Default.Save();
        }
        //Recebe o valor na porta serial
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
            recebido = Convert.ToString(serialPort1.ReadExisting());
            
            if (Application.OpenForms.OfType<Form3>().Count()>0) {
                BeginInvoke((MethodInvoker)(() => { janela.Close(); }));
            }
        }


        private void btnRstConfig_Click(object sender, EventArgs e) {

            if (MessageBox.Show("Tem certeza de que deseja resetar todas as configurações?", "Confirmação!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                Properties.Settings.Default.volumeup = "";
                Properties.Settings.Default.volumedown = "";
                Properties.Settings.Default.mute = "";
                Properties.Settings.Default.leftarrow = "";
                Properties.Settings.Default.rightarrow = "";
                Properties.Settings.Default.downarrow = "";
                Properties.Settings.Default.uparrow = "";
                Properties.Settings.Default.playpause = "";
                Properties.Settings.Default.medianext = "";
                Properties.Settings.Default.mediaprevious = "";
                Properties.Settings.Default.fullscreen = "";
                Properties.Settings.Default.hibernate = "";
                Properties.Settings.Default.shutdown = "";
                Properties.Settings.Default.project = "";
                Properties.Settings.Default.Save();
            }
        }

        private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            serialPort1.Close();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyValue.Equals(27)){ //ESC
                this.Close();
            }
        }
    }
}
