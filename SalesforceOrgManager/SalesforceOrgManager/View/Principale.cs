﻿using System;
using System.Windows.Forms;
using SalesforceOrgManager.Model;

namespace SalesforceOrgManager.View
{
    public partial class Principale : Form
    {
        public Principale()
        {
            InitializeComponent();            
        }
        private void Principale_Load(object sender, EventArgs e)
        {
            mainInit();
        }
        public void mainInit()
        {
            ShoppingList.workspaces = Program.getWorkspaces();
            ShoppingList.defaultProjectContent = Program.getDefaultProjectContent();
            ShoppingList.logins = Program.getLogins();
            cmbWorkspace.DataSource = ShoppingList.workspaces;
            cmbWorkspace.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void cmbWorkspace_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedWorkspace = (string) cmbWorkspace.SelectedItem;
            if (comboBox.SelectedIndex != -1 && ShoppingList.currentOperation != (int) ShoppingList.currentOp.Initial)
            {
                var selection = System.IO.Directory.GetDirectories(selectedWorkspace);

                if (ShoppingList.currentOperation == (int)ShoppingList.currentOp.Open)
                {
                    lstProject.DataSource = selection;
                    lstProject.Visible = true;

                    if (lstProject.Items.Count > 0)
                    {
                        btnOpenProject.Visible = true;
                    }
                    else
                    {
                        btnOpenProject.Visible = false;
                    }
                }
                else
                {
                    lstProject.Visible = false;
                    txtProjectName.Visible = true;
                }
            }
        }
        private void picOpenProject_Click(object sender, EventArgs e)
        {
            ShoppingList.currentOperation = (int) ShoppingList.currentOp.Open;
            btnOpenProject.Text = "OPEN";
            cmbWorkspace.Visible = true;
            txtProjectName.Visible = false;

            if (cmbWorkspace.Items.Count == 1) {
                cmbWorkspace_SelectedIndexChanged(cmbWorkspace, EventArgs.Empty);
            }
        }
        private void btnOpenProject_Click(object sender, EventArgs e)
        {
            ShoppingList.workspaceDir = Convert.ToString(cmbWorkspace.SelectedItem);
            if (ShoppingList.currentOperation == (int)ShoppingList.currentOp.Open)
            {
                Program.initProject((string)lstProject.SelectedItem);
                Program.readExistingProjectContent();
            }
            else
            {
                Program.initProject(ShoppingList.workspaceDir + "\\" + (string)txtProjectName.Text);
            }
            //Form loginForm = new SalesforceOrgManager.frmLogin();
            ShoppingList.LoginPointer = new frmLogin();
            this.Hide();
            ShoppingList.LoginPointer.Show();
            //loginForm.Show();
        }
        private void picNewProject_Click(object sender, EventArgs e)
        {
            ShoppingList.currentOperation = (int)ShoppingList.currentOp.Create;
            lstProject.Visible = false;
            cmbWorkspace.Visible = true;
            btnOpenProject.Text = "CREATE";
            btnOpenProject.Visible = true;

            if (cmbWorkspace.Items.Count == 1)
            {
                cmbWorkspace_SelectedIndexChanged(cmbWorkspace, EventArgs.Empty);
            }
        }
        private void picSettings_Click(object sender, EventArgs e)
        {
            BoxSettings bs = new BoxSettings();
            bs.Show();
        }
    }
}