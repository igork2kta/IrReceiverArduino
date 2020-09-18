using System;
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
        //Recebe o valor na porta serial
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
            recebido = Convert.ToString(serialPort1.ReadExisting());
            BeginInvoke((MethodInvoker)(() => { janela.Close(); }));
        }

        private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            serialPort1.Close();
        }

        private void label5_Click(object sender, EventArgs e) {

        }
        
    }
}
