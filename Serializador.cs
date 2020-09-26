using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace IrReceiver {
    public class Serializador {

		public  void Serializar(Configuracoes config) {

			//Local onde o arquivo será salvo
			//string path = Directory.GetCurrentDirectory();
			string arquivo = Application.StartupPath.ToString() + "configuracao.xml";

			using (var stream = new StreamWriter(arquivo)) {
				//Instanciação do serializador, passa como argumento a classe config
				var serializador = new XmlSerializer(config.GetType());
				serializador.Serialize(stream, config);
			}
		}

		public Configuracoes Deserializar(string arquivo) {

			Configuracoes retorno = null;
			using (var stream = new StreamReader(arquivo)) {
				var serializador = new XmlSerializer(typeof(Configuracoes));
				retorno = (Configuracoes)serializador.Deserialize(stream);
			}
			return retorno;
		}

	}
}
