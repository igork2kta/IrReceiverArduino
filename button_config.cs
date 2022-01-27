using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace IrReceiver {
    public partial class button_config : Form {
        string recebido;
        message_box janela = new message_box();

        Comandos comandos = new Comandos();

        public button_config(string porta, ref Comandos comandosReferencia) {
            InitializeComponent();
            serialPort1.PortName = porta;
            serialPort1.Open();
            comandos = comandosReferencia;
            
        }

        private void bntVolumeUp_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.volumeUp = recebido;
        }

        private void bntVolumeDown_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.volumeDown = recebido;
        }

        private void bntMute_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.mute = recebido;
        }

        private void btnRightArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.rightArrow = recebido;
        }

        private void btnLeftArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.leftArrow = recebido;
        }

        private void btnUpArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.upArrow = recebido;
        }

        private void btnDownArrow_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.downArrow = recebido;
        }

        private void btnEnter_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.enter = recebido;
        }

        private void btnPlayPause_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.playPause = recebido;
        }

        private void btnMediaNext_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.mediaNext = recebido;
        }

        private void btnMediaPrevious_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.mediaPrevious = recebido;
        }

        private void btnTelaCheia_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.fullScreen = recebido;
        }                

        private void btnHibernar_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.hibernate = recebido;
        }

        private void btnDesligar_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.shutdown = recebido;
        }

        private void btnProjetar_Click(object sender, EventArgs e) {
            janela.ShowDialog();
            comandos.project = recebido;
            
        }
        //Recebe o valor na porta serial
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e) {
            Thread.Sleep(50);
            recebido = Convert.ToString(serialPort1.ReadExisting());
            
            if (Application.OpenForms.OfType<message_box>().Count()>0) {
                BeginInvoke((MethodInvoker)(() => { janela.Close(); }));
            }
        }


        private void btnRstConfig_Click(object sender, EventArgs e) {

            if (MessageBox.Show("Tem certeza de que deseja resetar todas as configurações?", "Confirmação!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes) {
                comandos.rightArrow = "";
                comandos.leftArrow = "";
                comandos.upArrow = "";
                comandos.downArrow = "";
                comandos.volumeUp = "";
                comandos.volumeDown = "";
                comandos.mute = "";
                comandos.playPause = "";
                comandos.mediaNext = "";
                comandos.mediaPrevious = "";
                comandos.fullScreen = "";
                comandos.hibernate = "";
                comandos.shutdown = "";
                comandos.project = "";
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

        private void Form2_Load(object sender, EventArgs e) {

        }

        private void bntSalvar_Click(object sender, EventArgs e) {
            //Salva os comandos em json
            var json_serializado = JsonConvert.SerializeObject(comandos);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\comandos.json", json_serializado);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) {
            this.Close();
        }

        
    }
}
