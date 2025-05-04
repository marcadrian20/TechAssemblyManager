using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class AccountForm : Form
    {
        private Form f;
        private MainForm _mainForm;
        private ProductViewerForm _productViewerForm;
        public MainForm Instance { get; }

        public AccountForm(MainForm mainForm,Form f)
        {
            InitializeComponent();
            this.f = f;
            _mainForm = mainForm;
        }

        public AccountForm(MainForm instance)
        {
            Instance = instance;
        }

        public AccountForm(ProductViewerForm productViewerForm)
        {
            _productViewerForm = productViewerForm;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delogat cu succes!");
            f.Hide();
            _mainForm.Show();
            this.Hide();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            lblUser.Text = "Utilizator: Guest";
        }
    }
}
