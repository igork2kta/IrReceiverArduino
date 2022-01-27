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

namespace IrReceiver {
    
    public partial class main : Form {
        //Inicia o serviço de simulação de teclas
        InputSimulator teclado = new InputSimulator();
        //String que armazena os dados recebidos
        public string recebido;
        //Variável para verificação de dispositivo
        bool connected = false;
        Comandos comandos = new Comandos();
        

        public main() {
            InitializeComponent();
            //comandos.Carregar();
            
        }

        private void Form1_Load(object sender, EventArgs e) {
            //Tenta carregar as configurações, caso não existam, emite uma mensagem
            try {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\comandos.json");
                comandos = JsonConvert.DeserializeObject<Comandos>(json);
            }
            catch {
                MessageBox.Show("Clique no ícone de engrenagem no canto inferior direito para configurar um controle!", "Nenhuma configuração encontrada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            //Botões do tray icon
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Iniciar conexão", btnIniciar_Click));
            contextMenu.MenuItems.Add(new MenuItem("Parar conexão", btnParar_Click));
            contextMenu.MenuItems.Add(new MenuItem("Fechar", fechar));
            notifyIcon1.ContextMenu = contextMenu;

            //Adiciona as portas seriais encontradas na lista
            string[] ports = SerialPort.GetPortNames();
            cboPorts.Items.AddRange(ports);
            cbIniciarAutomaticamente.Checked = Properties.Settings.Default.cbIniciarAutomaticamente;
            cbIniciarComWindows.Checked = Properties.Settings.Default.cbAutoStart;
            if (cbIniciarAutomaticamente.Checked == true) {
                if (cboPorts.Items.Count == 0) {
                    //Caso não haja nada conectado na serial
                    MessageBox.Show("Nenhum dispositivo encontrado.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = "Não conectado";
                    lblStatus.ForeColor = Color.Red;
                }
                for (int i = 0; i < cboPorts.Items.Count; i++) {
                    try {
                        if (connected == true) break;
                        if (serialPort1.IsOpen) serialPort1.Close();
                        cboPorts.SelectedIndex = i;
                        serialPort1.PortName = cboPorts.Text;
                        serialPort1.Open();
                        //Tempo para o arduino "pensar" após conectar
                        Thread.Sleep(1000);
                        serialPort1.Write("123;");
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message, "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Seta o status como não conectado
                        lblStatus.Text = "Não conectado";
                        lblStatus.ForeColor = Color.Red;
                    }
                }
                //Tempoa para a serial assimilar o que foi recebido
                Thread.Sleep(500);
                if (connected == true) {
                    notifyIcon1.ShowBalloonTip(2, "Conectado!", "Conectado a " + cboPorts.SelectedItem, ToolTipIcon.Info);
                    lblStatus.Text = "Conectado a " + cboPorts.SelectedItem;
                    lblStatus.ForeColor = Color.Green;
                    this.WindowState = FormWindowState.Minimized;
                }
                else {
                    serialPort1.Close();
                    MessageBox.Show("O dispositivo não foi" +
                       " identificado.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Seta o status como não conectado
                    lblStatus.Text = "Não conectado";
                    lblStatus.ForeColor = Color.Red;
                }

            }
        }

        //Botão fechar do tray icon
        private void fechar(object sender, EventArgs e) {
            Application.Exit();
        }

        //Atualiza as portas seriais disponíveis
        private void btnUpdatePorts_Click(object sender, EventArgs e) {
            cboPorts.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            cboPorts.Items.AddRange(ports);
        }
        //Inicia a conexão com a porta serial
        private void btnIniciar_Click(object sender, EventArgs e) {
                serialPort1.Close();
                serialPort1.PortName = cboPorts.Text;
                serialPort1.Open();
                serialPort1.WriteLine("123;");
                Thread.Sleep(1000);
                if (connected == true) {
                    notifyIcon1.ShowBalloonTip(2, "Conectado!", "Conectado a " + cboPorts.SelectedItem, ToolTipIcon.Info);
                    lblStatus.Text = "Conectado a " + cboPorts.SelectedItem;
                    lblStatus.ForeColor = Color.Green;
                    this.WindowState = FormWindowState.Minimized;
                }
                else {
                    serialPort1.Close();
                    MessageBox.Show("O dispositivo não foi" +
                       " identificado.", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Seta o status como não conectado
                    lblStatus.Text = "Não conectado";
                    lblStatus.ForeColor = Color.Red;
                }
        }
        //Fecha a conexão com a porta serial
        private void btnParar_Click(object sender, EventArgs e) {
            serialPort1.Close();
            connected = false;
            lblStatus.Text = "Não conectado";
            lblStatus.ForeColor = Color.Red;
        }

        //Verifica os comandos recebidos
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e) {
            Thread.Sleep(50);
            recebido = Convert.ToString(serialPort1.ReadExisting());
            //Verificação de conexão, se o arduino responder à mensagem, mantém a conexão
            if (connected == false) {
                if (recebido.Contains("Sou eu!"))
                    connected = true;
            }
            else{
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
        }
                  
        private void cbIniciarAutomaticamente_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.cbIniciarAutomaticamente = cbIniciarAutomaticamente.Checked;
            Properties.Settings.Default.Save();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
        private void Form1_SizeChanged(object sender, EventArgs e) {
            if (FormWindowState.Minimized == this.WindowState)
                this.ShowInTaskbar = false; 
        }

        private void btnConfigurar_Click(object sender, EventArgs e) {
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
        private void cbIniciarComWindows_CheckedChanged(object sender, EventArgs e) {
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
