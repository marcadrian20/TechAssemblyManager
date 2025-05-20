using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TechAssemblyManager.BLL;
using TechAssemblyManager.Models;

namespace TechAssemblyManager.UI
{
    public partial class GestioneazaPromotiiForm : Form
    {
        private ListBox lstProduse;
        private ListBox lstProduseSelectate;
        private TextBox txtNumePromotie;
        private TextBox txtDescriere;
        private NumericUpDown numDiscount;
        private Button btnCreeaza, btnSterge;
        private ListBox lstPromotii;
        private MainForm mainForm;
        private PromotionManagerBLL promotionManagerBLL;
        private ProductManagerBLL productManagerBLL;
        private User currentUser;
        private List<Product> allProducts = new();
        private List<Promotion> allPromotions = new();

        public GestioneazaPromotiiForm(MainForm mainForm, PromotionManagerBLL promotionManagerBLL, ProductManagerBLL productManagerBLL, User currentUser)
        {
            this.mainForm = mainForm;
            this.promotionManagerBLL = promotionManagerBLL;
            this.productManagerBLL = productManagerBLL;
            this.currentUser = currentUser;
            this.Text = "Gestionare Promoții";
            this.Size = new System.Drawing.Size(700, 600);

            Label lblProduse = new Label { Text = "Produse disponibile:", Top = 20, Left = 20 };
            lstProduse = new ListBox { Top = 40, Left = 20, Width = 250, Height = 200 };

            Label lblSelectate = new Label { Text = "Produse promoție:", Top = 20, Left = 280 };
            lstProduseSelectate = new ListBox { Top = 40, Left = 280, Width = 250, Height = 200 };

            Button btnAdauga = new Button { Text = ">>", Top = 100, Left = 540 };
            btnAdauga.Click += (s, e) =>
            {
                if (lstProduse.SelectedItem is Product produs && !lstProduseSelectate.Items.Contains(produs))
                    lstProduseSelectate.Items.Add(produs);
            };

            Label lblNume = new Label { Text = "Nume promoție:", Top = 260, Left = 20 };
            txtNumePromotie = new TextBox { Top = 280, Left = 20, Width = 300 };

            Label lblDescriere = new Label { Text = "Descriere:", Top = 310, Left = 20 };
            txtDescriere = new TextBox { Top = 330, Left = 20, Width = 300 };

            Label lblDiscount = new Label { Text = "Discount %:", Top = 360, Left = 20 };
            numDiscount = new NumericUpDown { Top = 380, Left = 20, Width = 100, Minimum = 1, Maximum = 100, Value = 10 };

            btnCreeaza = new Button { Text = "Creează promoție", Top = 420, Left = 20 };
            btnCreeaza.Click += async (s, e) =>
            {
                if (lstProduseSelectate.Items.Count == 0 || string.IsNullOrWhiteSpace(txtNumePromotie.Text))
                {
                    MessageBox.Show("Selectează produse și completează numele promoției.");
                    return;
                }

                var promotie = new Promotion
                {
                    promotionId = Guid.NewGuid().ToString(),
                    name = txtNumePromotie.Text,
                    description = txtDescriere.Text,
                    discountPercentage = (float)numDiscount.Value,
                    isActive = true,
                    createdBy = currentUser?.userName ?? "admin"
                    // Optionally, add startDate/endDate if needed
                };

                // You may want to store product IDs in a separate table or as a property if your model supports it

                bool result = await promotionManagerBLL.AddPromotionAsync(promotie, currentUser);
                if (result)
                {
                    MessageBox.Show("Promoție creată cu succes!");
                    await RefreshPromotiiList();
                }
                else
                {
                    MessageBox.Show("Eroare la crearea promoției.");
                }
            };

            lstPromotii = new ListBox { Top = 460, Left = 20, Width = 500, Height = 100 };
            btnSterge = new Button { Text = "Șterge promoție selectată", Top = 460, Left = 540 };
            btnSterge.Click += async (s, e) =>
            {
                if (lstPromotii.SelectedItem is Promotion p)
                {
                    bool result = await promotionManagerBLL.DeletePromotionAsync(p.promotionId, currentUser);
                    if (result)
                    {
                        await RefreshPromotiiList();
                        MessageBox.Show("Promoție ștearsă.");
                    }
                    else
                    {
                        MessageBox.Show("Eroare la ștergere.");
                    }
                }
            };

            this.Controls.AddRange(new Control[] {
                lblProduse, lstProduse,
                lblSelectate, lstProduseSelectate,
                btnAdauga, lblNume, txtNumePromotie,
                lblDescriere, txtDescriere,
                lblDiscount, numDiscount,
                btnCreeaza, lstPromotii, btnSterge
            });
            this.FormClosing += GestioneazaPromotiiForm_FormClosing;
            _ = LoadProduseAsync();
            _ = RefreshPromotiiList();
        }

        private void GestioneazaPromotiiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mainForm != null && !mainForm.IsDisposed)
            {
                mainForm.Show();
            }
        }

        private async Task LoadProduseAsync()
        {
            allProducts = await productManagerBLL.GetAllActiveProductsAsync();
            lstProduse.Items.Clear();
            foreach (var p in allProducts)
                lstProduse.Items.Add(p);
        }

        private async Task RefreshPromotiiList()
        {
            allPromotions = await promotionManagerBLL.GetAllPromotionsAsync();
            lstPromotii.Items.Clear();
            foreach (var p in allPromotions)
                lstPromotii.Items.Add(p);
        }
    }
}