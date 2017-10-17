using System.Windows.Forms;
using Influence.Services;

namespace Influence.WinClient
{
    public partial class Form1 : Form
    {
        private readonly GameMaster _gameMaster = new GameMaster();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnViewSessions_Click(object sender, System.EventArgs e)
        {
            var sessions = _gameMaster.GetSessions();
        }
    }
}