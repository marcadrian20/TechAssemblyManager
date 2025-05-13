namespace TechAssemblyManager.UI
{
    partial class DebugWindowForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStatus = new Label();
            mainTabControl = new TabControl();
            tabPage1 = new TabPage();
            panel1 = new Panel();
            label1 = new Label();
            showPasswordCheckBox = new CheckBox();
            label2 = new Label();
            phoneNumberTextBox = new TextBox();
            emailTextBox = new TextBox();
            label7 = new Label();
            passwordTextBox = new TextBox();
            addressTextBox = new TextBox();
            signUpButton = new Button();
            label6 = new Label();
            label3 = new Label();
            userNameTextBox = new TextBox();
            firstNameTextBox = new TextBox();
            label5 = new Label();
            label4 = new Label();
            lastNameTextBox = new TextBox();
            tabPage2 = new TabPage();
            panel2 = new Panel();
            loginUserOrEmailTextBox = new TextBox();
            logInButton = new Button();
            loginShowPasswordCheckBox = new CheckBox();
            loginPasswordTextBox = new TextBox();
            label9 = new Label();
            loginUserOrEmailLabel = new Label();
            tabPage3 = new TabPage();
            panel3 = new Panel();
            isSeniorEmployeeCheckbox = new CheckBox();
            getUserDetailsButton = new Button();
            detailsPhoneNumberTextBox = new TextBox();
            label10 = new Label();
            detailsEmailTextBox = new TextBox();
            detailsPasswordTextBox = new TextBox();
            label11 = new Label();
            detailsAddressTextBox = new TextBox();
            label12 = new Label();
            detailsUserNameTextBox = new TextBox();
            label13 = new Label();
            detailsFirstNameTextBox = new TextBox();
            label14 = new Label();
            detailsLastNameTextBox = new TextBox();
            label15 = new Label();
            label16 = new Label();
            checkUserLabel = new Label();
            tabPage4 = new TabPage();
            panel4 = new Panel();
            label8 = new Label();
            comboBoxSort = new ComboBox();
            flowLayoutPanelProducts = new FlowLayoutPanel();
            label17 = new Label();
            tabPage5 = new TabPage();
            panel5 = new Panel();
            label19 = new Label();
            productIsActiveCheckBox = new CheckBox();
            label20 = new Label();
            productIdTextBox = new TextBox();
            productCategoryIDTextBox = new TextBox();
            productAddButton = new Button();
            label23 = new Label();
            productPriceTextBox = new TextBox();
            productNameTextBox = new TextBox();
            label24 = new Label();
            label25 = new Label();
            productDescriptionTextBox = new TextBox();
            tabPage6 = new TabPage();
            panel6 = new Panel();
            label18 = new Label();
            categoryIDTextBox = new TextBox();
            categoryAddButton = new Button();
            label21 = new Label();
            categoryTypeTextBox = new TextBox();
            categoryNameTextBox = new TextBox();
            label22 = new Label();
            label26 = new Label();
            categoryDescriptionTextBox = new TextBox();
            tabPage8 = new TabPage();
            panel7 = new Panel();
            promotionEndDatePicker = new DateTimePicker();
            promotionStartDatePicker = new DateTimePicker();
            promotionNameTextBox = new TextBox();
            label33 = new Label();
            label34 = new Label();
            label27 = new Label();
            promotionIsActiveCheckBox = new CheckBox();
            label28 = new Label();
            promotionIdTextBox = new TextBox();
            promotionAddButton = new Button();
            promotionDiscountPercentageTextBox = new TextBox();
            label30 = new Label();
            label31 = new Label();
            promotionDescriptionTextBox = new TextBox();
            mainTabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            panel1.SuspendLayout();
            tabPage2.SuspendLayout();
            panel2.SuspendLayout();
            tabPage3.SuspendLayout();
            panel3.SuspendLayout();
            tabPage4.SuspendLayout();
            panel4.SuspendLayout();
            tabPage5.SuspendLayout();
            panel5.SuspendLayout();
            tabPage6.SuspendLayout();
            panel6.SuspendLayout();
            tabPage8.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 503);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(49, 20);
            lblStatus.TabIndex = 23;
            lblStatus.Text = "Status";
            // 
            // mainTabControl
            // 
            mainTabControl.Controls.Add(tabPage1);
            mainTabControl.Controls.Add(tabPage2);
            mainTabControl.Controls.Add(tabPage3);
            mainTabControl.Controls.Add(tabPage4);
            mainTabControl.Controls.Add(tabPage5);
            mainTabControl.Controls.Add(tabPage6);
            mainTabControl.Controls.Add(tabPage8);
            mainTabControl.Location = new Point(8, 20);
            mainTabControl.Name = "mainTabControl";
            mainTabControl.SelectedIndex = 0;
            mainTabControl.Size = new Size(948, 484);
            mainTabControl.TabIndex = 24;
            mainTabControl.SelectedIndexChanged += mainTabControl_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel1);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(940, 451);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "SignUp";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.WhiteSmoke;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(showPasswordCheckBox);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(phoneNumberTextBox);
            panel1.Controls.Add(emailTextBox);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(passwordTextBox);
            panel1.Controls.Add(addressTextBox);
            panel1.Controls.Add(signUpButton);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(userNameTextBox);
            panel1.Controls.Add(firstNameTextBox);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(lastNameTextBox);
            panel1.Location = new Point(6, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(451, 251);
            panel1.TabIndex = 17;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 12);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Email:";
            // 
            // showPasswordCheckBox
            // 
            showPasswordCheckBox.AutoSize = true;
            showPasswordCheckBox.Location = new Point(318, 144);
            showPasswordCheckBox.Name = "showPasswordCheckBox";
            showPasswordCheckBox.Size = new Size(128, 24);
            showPasswordCheckBox.TabIndex = 16;
            showPasswordCheckBox.Text = "ShowPassword";
            showPasswordCheckBox.UseVisualStyleBackColor = true;
            showPasswordCheckBox.CheckedChanged += showPasswordCheckBox_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 144);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 1;
            label2.Text = "Password:";
            // 
            // phoneNumberTextBox
            // 
            phoneNumberTextBox.Location = new Point(125, 207);
            phoneNumberTextBox.Name = "phoneNumberTextBox";
            phoneNumberTextBox.Size = new Size(187, 27);
            phoneNumberTextBox.TabIndex = 15;
            // 
            // emailTextBox
            // 
            emailTextBox.Location = new Point(125, 9);
            emailTextBox.Name = "emailTextBox";
            emailTextBox.Size = new Size(187, 27);
            emailTextBox.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(3, 210);
            label7.Name = "label7";
            label7.Size = new Size(107, 20);
            label7.TabIndex = 14;
            label7.Text = "PhoneNumber:";
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(125, 141);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '*';
            passwordTextBox.Size = new Size(187, 27);
            passwordTextBox.TabIndex = 3;
            // 
            // addressTextBox
            // 
            addressTextBox.Location = new Point(125, 174);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(187, 27);
            addressTextBox.TabIndex = 13;
            // 
            // signUpButton
            // 
            signUpButton.Location = new Point(333, 177);
            signUpButton.Name = "signUpButton";
            signUpButton.Size = new Size(94, 29);
            signUpButton.TabIndex = 4;
            signUpButton.Text = "SignUp";
            signUpButton.UseVisualStyleBackColor = true;
            signUpButton.Click += signUpButton_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(3, 177);
            label6.Name = "label6";
            label6.Size = new Size(65, 20);
            label6.TabIndex = 12;
            label6.Text = "Address:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 45);
            label3.Name = "label3";
            label3.Size = new Size(79, 20);
            label3.TabIndex = 6;
            label3.Text = "FirstName:";
            // 
            // userNameTextBox
            // 
            userNameTextBox.Location = new Point(125, 108);
            userNameTextBox.Name = "userNameTextBox";
            userNameTextBox.Size = new Size(187, 27);
            userNameTextBox.TabIndex = 11;
            // 
            // firstNameTextBox
            // 
            firstNameTextBox.Location = new Point(125, 42);
            firstNameTextBox.Name = "firstNameTextBox";
            firstNameTextBox.Size = new Size(187, 27);
            firstNameTextBox.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 111);
            label5.Name = "label5";
            label5.Size = new Size(81, 20);
            label5.TabIndex = 10;
            label5.Text = "UserName:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 78);
            label4.Name = "label4";
            label4.Size = new Size(78, 20);
            label4.TabIndex = 8;
            label4.Text = "LastName:";
            // 
            // lastNameTextBox
            // 
            lastNameTextBox.Location = new Point(125, 75);
            lastNameTextBox.Name = "lastNameTextBox";
            lastNameTextBox.Size = new Size(187, 27);
            lastNameTextBox.TabIndex = 9;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(panel2);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(940, 451);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "LogIn";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(loginUserOrEmailTextBox);
            panel2.Controls.Add(logInButton);
            panel2.Controls.Add(loginShowPasswordCheckBox);
            panel2.Controls.Add(loginPasswordTextBox);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(loginUserOrEmailLabel);
            panel2.Location = new Point(6, 6);
            panel2.Name = "panel2";
            panel2.Size = new Size(470, 111);
            panel2.TabIndex = 18;
            // 
            // loginUserOrEmailTextBox
            // 
            loginUserOrEmailTextBox.Location = new Point(148, 9);
            loginUserOrEmailTextBox.Name = "loginUserOrEmailTextBox";
            loginUserOrEmailTextBox.Size = new Size(187, 27);
            loginUserOrEmailTextBox.TabIndex = 20;
            // 
            // logInButton
            // 
            logInButton.Location = new Point(187, 75);
            logInButton.Name = "logInButton";
            logInButton.Size = new Size(94, 29);
            logInButton.TabIndex = 19;
            logInButton.Text = "LogIn";
            logInButton.UseVisualStyleBackColor = true;
            logInButton.Click += logInButton_Click;
            // 
            // loginShowPasswordCheckBox
            // 
            loginShowPasswordCheckBox.AutoSize = true;
            loginShowPasswordCheckBox.Location = new Point(341, 41);
            loginShowPasswordCheckBox.Name = "loginShowPasswordCheckBox";
            loginShowPasswordCheckBox.Size = new Size(128, 24);
            loginShowPasswordCheckBox.TabIndex = 19;
            loginShowPasswordCheckBox.Text = "ShowPassword";
            loginShowPasswordCheckBox.UseVisualStyleBackColor = true;
            loginShowPasswordCheckBox.CheckedChanged += loginShowPasswordCheckBox_CheckedChanged;
            // 
            // loginPasswordTextBox
            // 
            loginPasswordTextBox.Location = new Point(148, 39);
            loginPasswordTextBox.Name = "loginPasswordTextBox";
            loginPasswordTextBox.PasswordChar = '*';
            loginPasswordTextBox.Size = new Size(187, 27);
            loginPasswordTextBox.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(13, 42);
            label9.Name = "label9";
            label9.Size = new Size(70, 20);
            label9.TabIndex = 2;
            label9.Text = "Password";
            // 
            // loginUserOrEmailLabel
            // 
            loginUserOrEmailLabel.AutoSize = true;
            loginUserOrEmailLabel.Location = new Point(13, 9);
            loginUserOrEmailLabel.Name = "loginUserOrEmailLabel";
            loginUserOrEmailLabel.Size = new Size(99, 20);
            loginUserOrEmailLabel.TabIndex = 1;
            loginUserOrEmailLabel.Text = "User Or Email";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(panel3);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(940, 451);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "UserDetails";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(isSeniorEmployeeCheckbox);
            panel3.Controls.Add(getUserDetailsButton);
            panel3.Controls.Add(detailsPhoneNumberTextBox);
            panel3.Controls.Add(label10);
            panel3.Controls.Add(detailsEmailTextBox);
            panel3.Controls.Add(detailsPasswordTextBox);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(detailsAddressTextBox);
            panel3.Controls.Add(label12);
            panel3.Controls.Add(detailsUserNameTextBox);
            panel3.Controls.Add(label13);
            panel3.Controls.Add(detailsFirstNameTextBox);
            panel3.Controls.Add(label14);
            panel3.Controls.Add(detailsLastNameTextBox);
            panel3.Controls.Add(label15);
            panel3.Controls.Add(label16);
            panel3.Controls.Add(checkUserLabel);
            panel3.Location = new Point(6, 6);
            panel3.Name = "panel3";
            panel3.Size = new Size(469, 266);
            panel3.TabIndex = 19;
            // 
            // isSeniorEmployeeCheckbox
            // 
            isSeniorEmployeeCheckbox.AutoCheck = false;
            isSeniorEmployeeCheckbox.AutoSize = true;
            isSeniorEmployeeCheckbox.Location = new Point(317, 4);
            isSeniorEmployeeCheckbox.Name = "isSeniorEmployeeCheckbox";
            isSeniorEmployeeCheckbox.Size = new Size(149, 24);
            isSeniorEmployeeCheckbox.TabIndex = 20;
            isSeniorEmployeeCheckbox.Text = "isSeniorEmployee";
            isSeniorEmployeeCheckbox.UseVisualStyleBackColor = true;
            // 
            // getUserDetailsButton
            // 
            getUserDetailsButton.Location = new Point(341, 227);
            getUserDetailsButton.Name = "getUserDetailsButton";
            getUserDetailsButton.Size = new Size(125, 29);
            getUserDetailsButton.TabIndex = 19;
            getUserDetailsButton.Text = "Get User Details";
            getUserDetailsButton.UseVisualStyleBackColor = true;
            getUserDetailsButton.Click += getUserDetailsButton_Click;
            // 
            // detailsPhoneNumberTextBox
            // 
            detailsPhoneNumberTextBox.Location = new Point(148, 229);
            detailsPhoneNumberTextBox.Name = "detailsPhoneNumberTextBox";
            detailsPhoneNumberTextBox.ReadOnly = true;
            detailsPhoneNumberTextBox.Size = new Size(187, 27);
            detailsPhoneNumberTextBox.TabIndex = 25;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(24, 31);
            label10.Name = "label10";
            label10.Size = new Size(49, 20);
            label10.TabIndex = 21;
            label10.Text = "Email:";
            // 
            // detailsEmailTextBox
            // 
            detailsEmailTextBox.Location = new Point(148, 31);
            detailsEmailTextBox.Name = "detailsEmailTextBox";
            detailsEmailTextBox.ReadOnly = true;
            detailsEmailTextBox.Size = new Size(187, 27);
            detailsEmailTextBox.TabIndex = 19;
            // 
            // detailsPasswordTextBox
            // 
            detailsPasswordTextBox.Location = new Point(148, 163);
            detailsPasswordTextBox.Name = "detailsPasswordTextBox";
            detailsPasswordTextBox.PasswordChar = '*';
            detailsPasswordTextBox.ReadOnly = true;
            detailsPasswordTextBox.Size = new Size(187, 27);
            detailsPasswordTextBox.TabIndex = 20;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(24, 163);
            label11.Name = "label11";
            label11.Size = new Size(73, 20);
            label11.TabIndex = 22;
            label11.Text = "Password:";
            // 
            // detailsAddressTextBox
            // 
            detailsAddressTextBox.Location = new Point(148, 196);
            detailsAddressTextBox.Name = "detailsAddressTextBox";
            detailsAddressTextBox.ReadOnly = true;
            detailsAddressTextBox.Size = new Size(187, 27);
            detailsAddressTextBox.TabIndex = 24;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(24, 229);
            label12.Name = "label12";
            label12.Size = new Size(107, 20);
            label12.TabIndex = 27;
            label12.Text = "PhoneNumber:";
            // 
            // detailsUserNameTextBox
            // 
            detailsUserNameTextBox.Location = new Point(148, 130);
            detailsUserNameTextBox.Name = "detailsUserNameTextBox";
            detailsUserNameTextBox.ReadOnly = true;
            detailsUserNameTextBox.Size = new Size(187, 27);
            detailsUserNameTextBox.TabIndex = 23;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(24, 196);
            label13.Name = "label13";
            label13.Size = new Size(65, 20);
            label13.TabIndex = 26;
            label13.Text = "Address:";
            // 
            // detailsFirstNameTextBox
            // 
            detailsFirstNameTextBox.Location = new Point(148, 64);
            detailsFirstNameTextBox.Name = "detailsFirstNameTextBox";
            detailsFirstNameTextBox.ReadOnly = true;
            detailsFirstNameTextBox.Size = new Size(187, 27);
            detailsFirstNameTextBox.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(24, 64);
            label14.Name = "label14";
            label14.Size = new Size(79, 20);
            label14.TabIndex = 23;
            label14.Text = "FirstName:";
            // 
            // detailsLastNameTextBox
            // 
            detailsLastNameTextBox.Location = new Point(148, 97);
            detailsLastNameTextBox.Name = "detailsLastNameTextBox";
            detailsLastNameTextBox.ReadOnly = true;
            detailsLastNameTextBox.Size = new Size(187, 27);
            detailsLastNameTextBox.TabIndex = 22;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(24, 130);
            label15.Name = "label15";
            label15.Size = new Size(81, 20);
            label15.TabIndex = 25;
            label15.Text = "UserName:";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(24, 97);
            label16.Name = "label16";
            label16.Size = new Size(78, 20);
            label16.TabIndex = 24;
            label16.Text = "LastName:";
            // 
            // checkUserLabel
            // 
            checkUserLabel.AutoSize = true;
            checkUserLabel.Location = new Point(3, 4);
            checkUserLabel.Name = "checkUserLabel";
            checkUserLabel.Size = new Size(143, 20);
            checkUserLabel.TabIndex = 20;
            checkUserLabel.Text = "Current User Details:";
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(panel4);
            tabPage4.Location = new Point(4, 29);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(940, 451);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Catalog";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(label8);
            panel4.Controls.Add(comboBoxSort);
            panel4.Controls.Add(flowLayoutPanelProducts);
            panel4.Controls.Add(label17);
            panel4.Location = new Point(6, 6);
            panel4.Name = "panel4";
            panel4.Size = new Size(928, 439);
            panel4.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(196, 11);
            label8.Name = "label8";
            label8.Size = new Size(59, 20);
            label8.TabIndex = 3;
            label8.Text = "Sort by:";
            // 
            // comboBoxSort
            // 
            comboBoxSort.FormattingEnabled = true;
            comboBoxSort.Items.AddRange(new object[] { "Category [A -> Z]", "Category [Z -> A]", "Price [Low -> High]", "Price [High -> Low]", "Name [A -> Z]", "Name [Z -> A]" });
            comboBoxSort.Location = new Point(261, 8);
            comboBoxSort.Name = "comboBoxSort";
            comboBoxSort.Size = new Size(151, 28);
            comboBoxSort.TabIndex = 2;
            comboBoxSort.SelectedIndexChanged += comboBoxSort_SelectedIndexChanged;
            // 
            // flowLayoutPanelProducts
            // 
            flowLayoutPanelProducts.BackColor = Color.Gainsboro;
            flowLayoutPanelProducts.Location = new Point(3, 45);
            flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            flowLayoutPanelProducts.Size = new Size(918, 387);
            flowLayoutPanelProducts.TabIndex = 1;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(3, 11);
            label17.Name = "label17";
            label17.Size = new Size(69, 20);
            label17.TabIndex = 0;
            label17.Text = "Products:";
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(panel5);
            tabPage5.Location = new Point(4, 29);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(940, 451);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Products";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(label19);
            panel5.Controls.Add(productIsActiveCheckBox);
            panel5.Controls.Add(label20);
            panel5.Controls.Add(productIdTextBox);
            panel5.Controls.Add(productCategoryIDTextBox);
            panel5.Controls.Add(productAddButton);
            panel5.Controls.Add(label23);
            panel5.Controls.Add(productPriceTextBox);
            panel5.Controls.Add(productNameTextBox);
            panel5.Controls.Add(label24);
            panel5.Controls.Add(label25);
            panel5.Controls.Add(productDescriptionTextBox);
            panel5.Location = new Point(6, 6);
            panel5.Name = "panel5";
            panel5.Size = new Size(338, 251);
            panel5.TabIndex = 18;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(3, 12);
            label19.Name = "label19";
            label19.Size = new Size(78, 20);
            label19.TabIndex = 0;
            label19.Text = "ProductID:";
            // 
            // productIsActiveCheckBox
            // 
            productIsActiveCheckBox.AutoSize = true;
            productIsActiveCheckBox.Location = new Point(22, 177);
            productIsActiveCheckBox.Name = "productIsActiveCheckBox";
            productIsActiveCheckBox.Size = new Size(82, 24);
            productIsActiveCheckBox.TabIndex = 16;
            productIsActiveCheckBox.Text = "isActive";
            productIsActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(3, 144);
            label20.Name = "label20";
            label20.Size = new Size(87, 20);
            label20.TabIndex = 1;
            label20.Text = "CategoryID:";
            // 
            // productIdTextBox
            // 
            productIdTextBox.Location = new Point(125, 9);
            productIdTextBox.Name = "productIdTextBox";
            productIdTextBox.Size = new Size(187, 27);
            productIdTextBox.TabIndex = 2;
            // 
            // productCategoryIDTextBox
            // 
            productCategoryIDTextBox.Location = new Point(125, 141);
            productCategoryIDTextBox.Name = "productCategoryIDTextBox";
            productCategoryIDTextBox.Size = new Size(187, 27);
            productCategoryIDTextBox.TabIndex = 3;
            // 
            // productAddButton
            // 
            productAddButton.Location = new Point(166, 177);
            productAddButton.Name = "productAddButton";
            productAddButton.Size = new Size(94, 29);
            productAddButton.TabIndex = 4;
            productAddButton.Text = "Add";
            productAddButton.UseVisualStyleBackColor = true;
            productAddButton.Click += productAddButton_Click;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new Point(3, 45);
            label23.Name = "label23";
            label23.Size = new Size(52, 20);
            label23.TabIndex = 6;
            label23.Text = "Name:";
            // 
            // productPriceTextBox
            // 
            productPriceTextBox.Location = new Point(125, 108);
            productPriceTextBox.Name = "productPriceTextBox";
            productPriceTextBox.Size = new Size(187, 27);
            productPriceTextBox.TabIndex = 11;
            // 
            // productNameTextBox
            // 
            productNameTextBox.Location = new Point(125, 42);
            productNameTextBox.Name = "productNameTextBox";
            productNameTextBox.Size = new Size(187, 27);
            productNameTextBox.TabIndex = 7;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new Point(3, 111);
            label24.Name = "label24";
            label24.Size = new Size(44, 20);
            label24.TabIndex = 10;
            label24.Text = "Price:";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Location = new Point(3, 78);
            label25.Name = "label25";
            label25.Size = new Size(88, 20);
            label25.TabIndex = 8;
            label25.Text = "Description:";
            // 
            // productDescriptionTextBox
            // 
            productDescriptionTextBox.Location = new Point(125, 75);
            productDescriptionTextBox.Name = "productDescriptionTextBox";
            productDescriptionTextBox.Size = new Size(187, 27);
            productDescriptionTextBox.TabIndex = 9;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(panel6);
            tabPage6.Location = new Point(4, 29);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(940, 451);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Categories";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            panel6.BackColor = Color.WhiteSmoke;
            panel6.BorderStyle = BorderStyle.Fixed3D;
            panel6.Controls.Add(label18);
            panel6.Controls.Add(categoryIDTextBox);
            panel6.Controls.Add(categoryAddButton);
            panel6.Controls.Add(label21);
            panel6.Controls.Add(categoryTypeTextBox);
            panel6.Controls.Add(categoryNameTextBox);
            panel6.Controls.Add(label22);
            panel6.Controls.Add(label26);
            panel6.Controls.Add(categoryDescriptionTextBox);
            panel6.Location = new Point(6, 6);
            panel6.Name = "panel6";
            panel6.Size = new Size(338, 189);
            panel6.TabIndex = 19;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(3, 12);
            label18.Name = "label18";
            label18.Size = new Size(87, 20);
            label18.TabIndex = 1;
            label18.Text = "CategoryID:";
            // 
            // categoryIDTextBox
            // 
            categoryIDTextBox.Location = new Point(125, 9);
            categoryIDTextBox.Name = "categoryIDTextBox";
            categoryIDTextBox.Size = new Size(187, 27);
            categoryIDTextBox.TabIndex = 3;
            // 
            // categoryAddButton
            // 
            categoryAddButton.Location = new Point(174, 141);
            categoryAddButton.Name = "categoryAddButton";
            categoryAddButton.Size = new Size(94, 29);
            categoryAddButton.TabIndex = 4;
            categoryAddButton.Text = "Add";
            categoryAddButton.UseVisualStyleBackColor = true;
            categoryAddButton.Click += categoryAddButton_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(3, 45);
            label21.Name = "label21";
            label21.Size = new Size(52, 20);
            label21.TabIndex = 6;
            label21.Text = "Name:";
            // 
            // categoryTypeTextBox
            // 
            categoryTypeTextBox.Location = new Point(125, 75);
            categoryTypeTextBox.Name = "categoryTypeTextBox";
            categoryTypeTextBox.Size = new Size(187, 27);
            categoryTypeTextBox.TabIndex = 11;
            // 
            // categoryNameTextBox
            // 
            categoryNameTextBox.Location = new Point(125, 42);
            categoryNameTextBox.Name = "categoryNameTextBox";
            categoryNameTextBox.Size = new Size(187, 27);
            categoryNameTextBox.TabIndex = 7;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(3, 78);
            label22.Name = "label22";
            label22.Size = new Size(43, 20);
            label22.TabIndex = 10;
            label22.Text = "Type:";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Location = new Point(3, 111);
            label26.Name = "label26";
            label26.Size = new Size(88, 20);
            label26.TabIndex = 8;
            label26.Text = "Description:";
            // 
            // categoryDescriptionTextBox
            // 
            categoryDescriptionTextBox.Location = new Point(125, 108);
            categoryDescriptionTextBox.Name = "categoryDescriptionTextBox";
            categoryDescriptionTextBox.Size = new Size(187, 27);
            categoryDescriptionTextBox.TabIndex = 9;
            // 
            // tabPage8
            // 
            tabPage8.Controls.Add(panel7);
            tabPage8.Location = new Point(4, 29);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(940, 451);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "Promotions";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            panel7.BackColor = Color.WhiteSmoke;
            panel7.BorderStyle = BorderStyle.Fixed3D;
            panel7.Controls.Add(promotionEndDatePicker);
            panel7.Controls.Add(promotionStartDatePicker);
            panel7.Controls.Add(promotionNameTextBox);
            panel7.Controls.Add(label33);
            panel7.Controls.Add(label34);
            panel7.Controls.Add(label27);
            panel7.Controls.Add(promotionIsActiveCheckBox);
            panel7.Controls.Add(label28);
            panel7.Controls.Add(promotionIdTextBox);
            panel7.Controls.Add(promotionAddButton);
            panel7.Controls.Add(promotionDiscountPercentageTextBox);
            panel7.Controls.Add(label30);
            panel7.Controls.Add(label31);
            panel7.Controls.Add(promotionDescriptionTextBox);
            panel7.Location = new Point(6, 6);
            panel7.Name = "panel7";
            panel7.Size = new Size(383, 258);
            panel7.TabIndex = 19;
            // 
            // promotionEndDatePicker
            // 
            promotionEndDatePicker.Location = new Point(126, 144);
            promotionEndDatePicker.Name = "promotionEndDatePicker";
            promotionEndDatePicker.Size = new Size(250, 27);
            promotionEndDatePicker.TabIndex = 24;
            // 
            // promotionStartDatePicker
            // 
            promotionStartDatePicker.Location = new Point(126, 111);
            promotionStartDatePicker.Name = "promotionStartDatePicker";
            promotionStartDatePicker.Size = new Size(250, 27);
            promotionStartDatePicker.TabIndex = 23;
            // 
            // promotionNameTextBox
            // 
            promotionNameTextBox.Location = new Point(126, 174);
            promotionNameTextBox.Name = "promotionNameTextBox";
            promotionNameTextBox.Size = new Size(250, 27);
            promotionNameTextBox.TabIndex = 22;
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Location = new Point(4, 177);
            label33.Name = "label33";
            label33.Size = new Size(52, 20);
            label33.TabIndex = 21;
            label33.Text = "Name:";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Location = new Point(4, 144);
            label34.Name = "label34";
            label34.Size = new Size(69, 20);
            label34.TabIndex = 19;
            label34.Text = "EndDate:";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(3, 12);
            label27.Name = "label27";
            label27.Size = new Size(97, 20);
            label27.TabIndex = 0;
            label27.Text = "PromotionID:";
            // 
            // promotionIsActiveCheckBox
            // 
            promotionIsActiveCheckBox.AutoSize = true;
            promotionIsActiveCheckBox.Location = new Point(17, 212);
            promotionIsActiveCheckBox.Name = "promotionIsActiveCheckBox";
            promotionIsActiveCheckBox.Size = new Size(82, 24);
            promotionIsActiveCheckBox.TabIndex = 16;
            promotionIsActiveCheckBox.Text = "isActive";
            promotionIsActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Location = new Point(4, 111);
            label28.Name = "label28";
            label28.Size = new Size(75, 20);
            label28.TabIndex = 1;
            label28.Text = "StartDate:";
            // 
            // promotionIdTextBox
            // 
            promotionIdTextBox.Location = new Point(125, 9);
            promotionIdTextBox.Name = "promotionIdTextBox";
            promotionIdTextBox.Size = new Size(250, 27);
            promotionIdTextBox.TabIndex = 2;
            // 
            // promotionAddButton
            // 
            promotionAddButton.Location = new Point(173, 207);
            promotionAddButton.Name = "promotionAddButton";
            promotionAddButton.Size = new Size(94, 29);
            promotionAddButton.TabIndex = 4;
            promotionAddButton.Text = "Add";
            promotionAddButton.UseVisualStyleBackColor = true;
            promotionAddButton.Click += promotionAddButton_Click;
            // 
            // promotionDiscountPercentageTextBox
            // 
            promotionDiscountPercentageTextBox.Location = new Point(126, 75);
            promotionDiscountPercentageTextBox.Name = "promotionDiscountPercentageTextBox";
            promotionDiscountPercentageTextBox.Size = new Size(250, 27);
            promotionDiscountPercentageTextBox.TabIndex = 11;
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Location = new Point(4, 78);
            label30.Name = "label30";
            label30.Size = new Size(82, 20);
            label30.TabIndex = 10;
            label30.Text = "Discount%:";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Location = new Point(4, 45);
            label31.Name = "label31";
            label31.Size = new Size(88, 20);
            label31.TabIndex = 8;
            label31.Text = "Description:";
            // 
            // promotionDescriptionTextBox
            // 
            promotionDescriptionTextBox.Location = new Point(126, 42);
            promotionDescriptionTextBox.Name = "promotionDescriptionTextBox";
            promotionDescriptionTextBox.Size = new Size(250, 27);
            promotionDescriptionTextBox.TabIndex = 9;
            // 
            // DebugWindowForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(965, 525);
            Controls.Add(mainTabControl);
            Controls.Add(lblStatus);
            Name = "DebugWindowForm";
            Text = "DebugWindowForm";
            Load += DebugWindowForm_Load;
            mainTabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabPage2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabPage3.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tabPage4.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            tabPage5.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            tabPage6.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            tabPage8.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStatus;
        private TabControl mainTabControl;
        private TabPage tabPage1;
        private Panel panel1;
        private Label label1;
        private CheckBox showPasswordCheckBox;
        private Label label2;
        private TextBox phoneNumberTextBox;
        private TextBox emailTextBox;
        private Label label7;
        private TextBox passwordTextBox;
        private TextBox addressTextBox;
        private Button signUpButton;
        private Label label6;
        private Label label3;
        private TextBox userNameTextBox;
        private TextBox firstNameTextBox;
        private Label label5;
        private Label label4;
        private TextBox lastNameTextBox;
        private TabPage tabPage2;
        private Panel panel2;
        private TextBox loginUserOrEmailTextBox;
        private Button logInButton;
        private CheckBox loginShowPasswordCheckBox;
        private TextBox loginPasswordTextBox;
        private Label label9;
        private Label loginUserOrEmailLabel;
        private TabPage tabPage3;
        private Panel panel3;
        private CheckBox isSeniorEmployeeCheckbox;
        private Button getUserDetailsButton;
        private TextBox detailsPhoneNumberTextBox;
        private Label label10;
        private TextBox detailsEmailTextBox;
        private TextBox detailsPasswordTextBox;
        private Label label11;
        private TextBox detailsAddressTextBox;
        private Label label12;
        private TextBox detailsUserNameTextBox;
        private Label label13;
        private TextBox detailsFirstNameTextBox;
        private Label label14;
        private TextBox detailsLastNameTextBox;
        private Label label15;
        private Label label16;
        private Label checkUserLabel;
        private TabPage tabPage4;
        private Panel panel4;
        private Label label8;
        private ComboBox comboBoxSort;
        private FlowLayoutPanel flowLayoutPanelProducts;
        private Label label17;
        private TabPage tabPage5;
        private Panel panel5;
        private Label label19;
        private CheckBox productIsActiveCheckBox;
        private Label label20;
        private TextBox productIdTextBox;
        private TextBox productCategoryIDTextBox;
        private Button productAddButton;
        private Label label23;
        private TextBox productPriceTextBox;
        private TextBox productNameTextBox;
        private Label label24;
        private Label label25;
        private TextBox productDescriptionTextBox;
        private TabPage tabPage6;
        private Panel panel6;
        private Label label18;
        private TextBox categoryIDTextBox;
        private Button categoryAddButton;
        private Label label21;
        private TextBox categoryTypeTextBox;
        private TextBox categoryNameTextBox;
        private Label label22;
        private Label label26;
        private TextBox categoryDescriptionTextBox;
        private TabPage tabPage8;
        private Panel panel7;
        private DateTimePicker promotionEndDatePicker;
        private DateTimePicker promotionStartDatePicker;
        private TextBox promotionNameTextBox;
        private Label label33;
        private Label label34;
        private Label label27;
        private CheckBox promotionIsActiveCheckBox;
        private Label label28;
        private TextBox promotionIdTextBox;
        private Button promotionAddButton;
        private TextBox promotionDiscountPercentageTextBox;
        private Label label30;
        private Label label31;
        private TextBox promotionDescriptionTextBox;
    }
}