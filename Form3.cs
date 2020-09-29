using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IrReceiver {
    public partial class Form3 : Form {
        public Form3() {
            InitializeComponent();
        }

        private void Form3_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.KeyValue.Equals(27)) { //ESC
                this.Close();
            }
        }
    }
}
