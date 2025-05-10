using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TechAssemblyManager.UI
{
    public partial class PromotiiForm : Form
    {
        private MainForm mainForm;
        public PromotiiForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.FormClosing +=PromotiiForm_FormClosing; // Explicitly specify the delegate
            InitializeComponent();
            AdaugaTextSubImagini();
        }

        private void PromotiiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }

        private void AdaugaTextSubImagini()
        {
            // Lista de promoții
            var promotii = new List<Promotion>
                {
                    new Promotion("Promoție 1", "Reducere de 10% la produsele electronice", 10),
                    new Promotion("Promoție 2", "Reducere de 20% la laptopuri", 20),
                    new Promotion("Promoție 3", "Reducere de 15% la telefoane", 15),
                    new Promotion("Promoție 4", "Reducere de 25% la accesorii", 25)
                };

            for (int i = 0; i < 4; i++)
            {
                // Găsim panel-ul (panel1, panel2, etc.) din flowLayoutPanel1
                Panel panel = flowLayoutPanel1.Controls.Find($"panel{i + 1}", true).FirstOrDefault() as Panel;

                if (panel != null && panel.Controls.OfType<PictureBox>().Any())
                {
                    PictureBox pictureBox = panel.Controls.OfType<PictureBox>().First();

                    // Label Nume
                    Label lblNume = new Label
                    {
                        Text = promotii[i].Name,
                        AutoSize = false,
                        Height = 20,
                        Width = panel.Width,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Location = new Point(0, pictureBox.Bottom + 5)
                    };

                    // Label Descriere
                    Label lblDescriere = new Label
                    {
                        Text = promotii[i].Description,
                        AutoSize = false,
                        Height = 40,
                        Width = panel.Width,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(0, lblNume.Bottom + 5)
                    };

                    // Label Discount
                    Label lblDiscount = new Label
                    {
                        Text = $"Reducere: {promotii[i].Discount}%",
                        AutoSize = false,
                        Height = 20,
                        Width = panel.Width,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Location = new Point(0, lblDescriere.Bottom + 5)
                    };

                    // Adăugăm label-urile în panel
                    panel.Controls.Add(lblNume);
                    panel.Controls.Add(lblDescriere);
                    panel.Controls.Add(lblDiscount);

                    // Setăm înălțimea panelului să încapă tot
                    panel.Height = lblDiscount.Bottom + 10;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
                this.Hide();
            }
        }
    }
}
