using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

//Classe que armazena os comandos
namespace IrReceiver {
    public class Comandos {
        
        public string rightArrow;
        public string leftArrow;
        public string upArrow;
        public string downArrow;
        public string volumeUp;
        public string volumeDown;
        public string enter;
        public string mute;
        public string playPause;
        public string mediaNext;
        public string mediaPrevious;
        public string fullScreen;
        public string hibernate;
        public string shutdown;
        public string project;
        public string ultimaPortaConectada;

        public Comandos CarregarComandos ()
        {
            Comandos comandos;
            try
            {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\comandos.json");
                comandos = JsonConvert.DeserializeObject<Comandos>(json);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            return comandos;
        }

        //Salva os comandos em json
        public void SalvarComandos()
        {
            var json_serializado = JsonConvert.SerializeObject(this);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\comandos.json", json_serializado);
           
        }


    }

    
}
