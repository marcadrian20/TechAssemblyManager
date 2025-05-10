using System;
using System.Linq;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class VizualizareCereriForm : Form
    {
        private ListBox lstCereri;
        private Button btnRezolva;
        private MainForm mainForm;
        public VizualizareCereriForm(MainForm.User user,MainForm mainForm)
        {
            if (mainForm == null || mainForm.Instance == null)
            {
                throw new ArgumentNullException(nameof(mainForm), "MainForm or its Instance cannot be null.");
            }
            this.mainForm = mainForm;
            this.Text = "Cereri Service";
            this.Size = new System.Drawing.Size(600, 400);

            lstCereri = new ListBox { Left = 10, Top = 10, Width = 560, Height = 300 };
            btnRezolva = new Button { Text = "Marchează ca rezolvată", Left = 10, Top = 320 };

            btnRezolva.Click += (s, e) =>
            {
                if (lstCereri.SelectedItem is CerereService cerere)
                {
                    cerere.Status = "Rezolvată";
                    RefreshCereri();
                }
            };
            this.FormClosing += VizualizareCereriForm_FormClosing;
            this.Controls.Add(lstCereri);
            this.Controls.Add(btnRezolva);
            RefreshCereri();
        }
        private void VizualizareCereriForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
            {
                MessageBox.Show("MainForm nu este disponibil.");
            }
        }
        private void RefreshCereri()
        {
            lstCereri.Items.Clear();
            foreach (var c in AppState.GetCereri())
                lstCereri.Items.Add(c);
        }
    }
}
