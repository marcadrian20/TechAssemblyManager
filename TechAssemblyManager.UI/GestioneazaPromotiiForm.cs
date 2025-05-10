using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TechAssemblyManager.UI.Properties;

namespace TechAssemblyManager.UI
{
    public partial class GestioneazaPromotiiForm : Form
    {
        private ListBox lstProduse;
        private ListBox lstProduseSelectate;
        private TextBox txtNumePromotie;
        private Button btnCreeaza, btnSterge;
        private ListBox lstPromotii;
        private MainForm mainForm;
        public GestioneazaPromotiiForm(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.Text = "Gestionare Promoții";
            this.Size = new System.Drawing.Size(700, 500);

            Label lblProduse = new Label { Text = "Produse disponibile:", Top = 20, Left = 20 };
            lstProduse = new ListBox { Top = 40, Left = 20, Width = 250, Height = 200 };
            Produs p = new Produs { Categorie = "Laptop", Descriere = "Laptop de gaming", Imagine =, Nume = "Laptop Asus", Pret = 5000, ScorCritici = 5 };
            AppState.AdaugaProdus(p);
            Produs p1 = new Produs { Categorie = "Imprimanta", Descriere = "performanta", Imagine =, Nume = "Imprimanta", Pret = 2000, ScorCritici = 4 };
            lstProduse.Items.AddRange(AppState.GetProduse().ToArray());

            Label lblSelectate = new Label { Text = "Produse promoție:", Top = 20, Left = 280 };
            lstProduseSelectate = new ListBox { Top = 40, Left = 280, Width = 250, Height = 200 };

            Button btnAdauga = new Button { Text = ">>", Top = 100, Left = 540 };
            btnAdauga.Click += (s, e) =>
            {
                if (lstProduse.SelectedItem is Produs produs)
                    lstProduseSelectate.Items.Add(produs);
            };

            Label lblNume = new Label { Text = "Nume promoție:", Top = 260, Left = 20 };
            txtNumePromotie = new TextBox { Top = 280, Left = 20, Width = 300 };

            btnCreeaza = new Button { Text = "Creează promoție", Top = 320, Left = 20 };
            btnCreeaza.Click += (s, e) =>
            {
                if (lstProduseSelectate.Items.Count == 0 || string.IsNullOrWhiteSpace(txtNumePromotie.Text))
                {
                    MessageBox.Show("Selectează produse și completează numele promoției.");
                    return;
                }

                var promotie = new Promotie
                {
                    Nume = txtNumePromotie.Text,
                    ProduseIncluse = lstProduseSelectate.Items.Cast<Produs>().ToList()
                };

                AppState.AdaugaPromotie(promotie);
                RefreshPromotiiList();
                MessageBox.Show("Promoție creată cu succes!");
            };

            lstPromotii = new ListBox { Top = 360, Left = 20, Width = 500, Height = 100 };
            btnSterge = new Button { Text = "Șterge promoție selectată", Top = 360, Left = 540 };
            btnSterge.Click += (s, e) =>
            {
                if (lstPromotii.SelectedItem is Promotie p)
                {
                    AppState.StergePromotie(p);
                    RefreshPromotiiList();
                    MessageBox.Show("Promoție ștearsă.");
                }
            };

            this.Controls.AddRange(new Control[] {
                lblProduse, lstProduse,
                lblSelectate, lstProduseSelectate,
                btnAdauga, lblNume, txtNumePromotie, btnCreeaza,
                lstPromotii, btnSterge
            });
            this.FormClosing += GestioneazaPromotiiForm_FormClosing;
            RefreshPromotiiList();
        }
        private void GestioneazaPromotiiForm_FormClosing(object sender, EventArgs e)
        {
           if(mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }
        private void RefreshPromotiiList()
        {
            lstPromotii.Items.Clear();
            foreach (var p in AppState.GetPromotii())
            {
                lstPromotii.Items.Add(p);
            }
        }
    }
}

