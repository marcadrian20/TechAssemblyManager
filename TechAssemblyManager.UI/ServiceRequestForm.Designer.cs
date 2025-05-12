using System;
using System.Windows.Forms;
using System.Drawing;
namespace TechAssemblyManager.UI
{
    public partial class ServiceRequestForm:Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AutoScaleMode AutoScaleMode { get; private set; }
        public Size ClientSize { get; private set; }
        public string Text { get; private set; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Ensure 'components' is checked for null before calling Dispose
                components?.Dispose();
            }
            // Call base.Dispose only if the base class implements IDisposable
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "ServiceRequestForm";
        }

        #endregion
    }
}