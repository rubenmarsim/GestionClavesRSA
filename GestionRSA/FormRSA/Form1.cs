using System;
using GestionRSA;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormRSA
{
    public partial class frmRSA : Form
    {
        GestionRSA.GestionRSA _CGestionRSA;
        public frmRSA()
        {
            InitializeComponent();
        }

        private void frmRSA_Load(object sender, EventArgs e)
        {
            _CGestionRSA = new GestionRSA.GestionRSA();
        }

        private void btnCreateRSA_Click(object sender, EventArgs e)
        {
            _CGestionRSA.CreateAndWriteRSAKeys();
        }

        private void btnReadRSA_Click(object sender, EventArgs e)
        {
            _CGestionRSA.ReadRSAKeys();
        }
    }
}
