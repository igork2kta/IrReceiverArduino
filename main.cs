using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using WindowsInput.Native;
using WindowsInput;
using Microsoft.Win32;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace IrReceiver {
    
    public partial class main : Form {
        
        //Inicia o serviço de simulação de teclas
        readonly InputSimulator teclado = new InputSimulator();
        //String que armazena os dados recebidos
        public string recebido;
        //Variável para verificação de dispositivo
        bool connected = false;
        //Classe que armazena os codigos dos comandos
        Comandos comandos = new Comandos();
        
        public main() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            
            //Tenta carregar as configurações, caso não existam, emite uma mensagem
            try{
                comandos = comandos.CarregarComandos();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Configure seu controle para começar a usar!", "Seja bem vindo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Botões do tray icon
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Iniciar conexão", BtnIniciar_Click));
            contextMenu.MenuItems.Add(new MenuItem("Parar conexão", BtnParar_Click));
            contextMenu.MenuItems.Add(new MenuItem("Fechar", Fechar));
            notifyIcon1.ContextMenu = contextMenu;

            //Adiciona as portas seriais encontradas na lista
            string[] ports = SerialPort.GetPortNames();
            cboPorts.Items.AddRange(ports);
            //Verifica os valores para os campos Conectar automaticamente e Iniciar com o windows
            cbConectarAutomaticamente.Checked = Properties.Settings.Default.cbConectarAutomaticamente;
            cbIniciarComWindows.Checked = Properties.Settings.Default.cbAutoStart;

            if (cbConectarAutomaticamente.Checked == true) {

                //Tenta conectar automaticamente na porta utilizada na ultima conexão
                try {
                    if (comandos.ultimaPortaConectada != null && comandos.ultimaPortaConectada != "")
                    {
                        cboPorts.SelectedItem = comandos.ultimaPortaConectada;
                        Conectar();
                        if (connected) return;
                    }
                }
                catch { }

                if (cboPorts.Items.Count == 0) {
                    //Caso não seja encontrado nada na serial
                    MessageBox.Show("Nenhum dispositivo encontrado.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "Não conectado";
                    lblStatus.ForeColor = Color.Red;
                }
                for (int i = 0; i < cboPorts.Items.Count; i++) {
                    try {
                        if (connected == true) break;
                        if (serialPort1.IsOpen) serialPort1.Close();

                        cboPorts.SelectedIndex = i;

                        bool maisTentativas = i == (cboPorts.Items.Count - 1) ? false : true;
                        Conectar(maisTentativas);

                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message, "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Seta o status como não conectado
                        Desconectar();
                    }
                }

            }
        }

        //Botão fechar do tray icon
        private void Fechar(object sender, EventArgs e) {
            Application.Exit();
        }

        //Atualiza as portas seriais disponíveis
        private void BtnUpdatePorts_Click(object sender, EventArgs e) {
            cboPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cboPorts.Items.AddRange(ports);
        }
        //Inicia a conexão com a porta serial
        private void BtnIniciar_Click(object sender, EventArgs e) {
            Conectar();
        }

        /// <summary>
        /// Conecta com um dispositivo compatível
        /// </summary>
        /// <param name="maisTentativas"> Opcional, utiliza-se false quando deseja-se realizar mais tentativas antes de retornar erro </param>
        private void Conectar([Optional] bool maisTentativas) {
            Desconectar();
            serialPort1.PortName = cboPorts.Text;

            //Abre e cecha a porta, pois por algum motivo o arduino não responde na primeira conexão.
            serialPort1.Open();
            serialPort1.DiscardInBuffer(); //teste
            serialPort1.Close();
            Thread.Sleep(1500);
            //---------------------------------------------------------------------------------------

            serialPort1.Open();
            //Escreve "123;" na serial, caso o retorno seja "Sou eu!" mantém a conexão
            serialPort1.WriteLine("123;");
            string teste = serialPort1.ReadLine();

            if (teste.Contains("Sou eu!"))
            {   
                connected = true;
                notifyIcon1.ShowBalloonTip(2, "Conectado!", "Conectado a " + cboPorts.SelectedItem, ToolTipIcon.Info);
                lblStatus.Text = "Conectado a " + cboPorts.SelectedItem;
                lblStatus.ForeColor = Color.Green;
                this.WindowState = FormWindowState.Minimized;

                //Salva a ultima porta conectada
                if (comandos.ultimaPortaConectada != cboPorts.Text)
                {
                    comandos.ultimaPortaConectada = cboPorts.Text;
                    comandos.SalvarComandos();
                }
                
            }
            else if(!maisTentativas)
            {
                MessageBox.Show("O dispositivo não foi" +
                   " identificado.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Desconectar();
            }
            
        }

        //Desconecta a porta serial
        private void Desconectar(){

            if (serialPort1.IsOpen) serialPort1.Close();
            connected = false;
            //Seta o status como não conectado
            lblStatus.Text = "Não conectado";
            lblStatus.ForeColor = Color.Red;
        }
        


        //Fecha a conexão com a porta serial
        private void BtnParar_Click(object sender, EventArgs e) {
            Desconectar();
        }
       
        //Verifica os comandos recebidos
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            Thread.Sleep(50);
            recebido = Convert.ToString(serialPort1.ReadExisting());
            
            if (recebido == comandos.volumeUp) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
            }
            else if (recebido == comandos.volumeDown) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
            }
            else if (recebido == comandos.mute) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
            }
            else if (recebido == comandos.rightArrow) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.RIGHT);
            }
            else if (recebido == comandos.leftArrow) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.LEFT);
            }
            else if (recebido == comandos.upArrow) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.UP);
            }
            else if (recebido == comandos.downArrow) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.DOWN);
            }
            else if (recebido == comandos.enter) {
                //Não sei porque mas return é enter
                teclado.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
            else if (recebido == comandos.playPause) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
            }
            else if (recebido == comandos.mediaNext) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
            }
            else if (recebido == comandos.mediaPrevious) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
            }
            else if (recebido == comandos.fullScreen) {
                teclado.Keyboard.KeyPress(VirtualKeyCode.F11);                   
            }
            else if (recebido == comandos.hibernate) {
                //Hibernar
                System.Diagnostics.Process.Start("shutdown.exe", "/h");               
            }
            else if (recebido == comandos.shutdown) {
                //Desligar
                System.Diagnostics.Process.Start("shutdown.exe", "/s /t 0");
            }
            else if (recebido == comandos.project) {
                //Mudar monitor (projetar), a cada ver pressionado muda uma vez
                //Atalho Windows + P
                teclado.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_P);
                //1 segundo para o Windows o menu
                Thread.Sleep(1000);
                //Seta para baixo
                teclado.Keyboard.KeyPress(VirtualKeyCode.DOWN);
                //Enter para selecionar a proxima opção de projeção
                teclado.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                //Esc para sumir o menu
                teclado.Keyboard.KeyPress(VirtualKeyCode.ESCAPE);
            }
            
        }
                  
        private void CbIniciarAutomaticamente_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.cbConectarAutomaticamente = cbConectarAutomaticamente.Checked;
            Properties.Settings.Default.Save();
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void NotifyIcon1_BalloonTipClicked(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Form1_SizeChanged(object sender, EventArgs e) {
            if (FormWindowState.Minimized == this.WindowState)
                this.ShowInTaskbar = false; 
        }

        private void BtnConfigurar_Click(object sender, EventArgs e) {
            if (serialPort1.IsOpen) {
                serialPort1.Close();
                //Chama o formulario de configurações de comandos e passa a classe comandos por referência
                button_config configuracoes = new button_config(cboPorts.Text, ref comandos);
                configuracoes.ShowDialog();
                configuracoes.Close();
                serialPort1.Open();

            }
            else {
                MessageBox.Show("Você precisa estar conectado a um sensor para configurar", "Não Conectado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        

        //Inicia o software com o windows
        private void CbIniciarComWindows_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.cbAutoStart = cbIniciarComWindows.Checked;
            Properties.Settings.Default.Save();
            if (cbIniciarComWindows.Checked == true) {
                RegistryKey Reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                Reg.SetValue("IrReceiver", Application.ExecutablePath.ToString());
            }
            else {
                RegistryKey Reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                Reg.DeleteValue("IrReceiver");
            }
        }
    }
}
