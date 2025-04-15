using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class HandlerManagement : Form
    {
        private Label titleLabel;
        private DataGridView handlersGrid;
        private Button btnAddHandler;
        private Button btnEditHandler;
        private Button btnDeleteHandler;
        private Button btnBackToMenu;

        public HandlerManagement()
        {
            InitializeLayout();
            ApplyDarkMode();
            LoadHandlers(); // Load initial handler data
        }

        private void InitializeLayout()
        {
            this.Size = new Size(800, 500);
            this.Text = "Handlers Management";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Handlers Management",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            handlersGrid = new DataGridView()
            {
                Location = new Point(20, 70),
                Size = new Size(740, 300),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            handlersGrid.Columns.Add("HandlerID", "Handler ID");
            handlersGrid.Columns.Add("HandlerName", "Handler Name");
            handlersGrid.Columns.Add("HandlerRole", "Handler Role");
            handlersGrid.Columns.Add("HandlerSalary", "Handler Salary");

            btnAddHandler = new Button()
            {
                Text = "Add Handler",
                Location = new Point(20, 390),
                Size = new Size(120, 30)
            };

            btnEditHandler = new Button()
            {
                Text = "Edit Handler",
                Location = new Point(160, 390),
                Size = new Size(120, 30)
            };

            btnDeleteHandler = new Button()
            {
                Text = "Delete Handler",
                Location = new Point(300, 390),
                Size = new Size(120, 30)
            };

            btnBackToMenu = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(640, 390),
                Size = new Size(120, 30)
            };

            btnAddHandler.Click += BtnAddHandler_Click;
            btnEditHandler.Click += BtnEditHandler_Click;
            btnDeleteHandler.Click += BtnDeleteHandler_Click;
            btnBackToMenu.Click += BtnBackToMenu_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(handlersGrid);
            this.Controls.Add(btnAddHandler);
            this.Controls.Add(btnEditHandler);
            this.Controls.Add(btnDeleteHandler);
            this.Controls.Add(btnBackToMenu);
        }

        private void LoadHandlers()
        {
            var handlers = new List<(string id, string name, string role, decimal salary)>
            {
                ("H001", "Juan Dela Cruz", "Feeder", 25000),
                ("H002", "Maria Santos", "Trainer", 30000),
                ("H003", "Pedro Reyes", "Gaffer", 20000),
                ("H004", "Ana Lopez", "Feeder", 25000),
                ("H005", "Jose Garcia", "Trainer", 32000)
            };

            foreach (var handler in handlers)
            {
                handlersGrid.Rows.Add(handler.id, handler.name, handler.role, handler.salary.ToString("C"));
            }
        }

        private void BtnAddHandler_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Handler functionality goes here.");
        }

        private void BtnEditHandler_Click(object sender, EventArgs e)
        {
            if (handlersGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = handlersGrid.SelectedRows[0];
                string handlerID = selectedRow.Cells["HandlerID"].Value.ToString();
                string handlerName = selectedRow.Cells["HandlerName"].Value.ToString();
                string handlerRole = selectedRow.Cells["HandlerRole"].Value.ToString();
                string handlerSalary = selectedRow.Cells["HandlerSalary"].Value.ToString();

                MessageBox.Show($"Edit Handler: {handlerID}, {handlerName}, {handlerRole}, {handlerSalary}");
            }
            else
            {
                MessageBox.Show("Please select a handler to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteHandler_Click(object sender, EventArgs e)
        {
            if (handlersGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = handlersGrid.SelectedRows[0];
                string handlerID = selectedRow.Cells["HandlerID"].Value.ToString();

                MessageBox.Show($"Handler {handlerID} has been deleted.");
            }
            else
            {
                MessageBox.Show("Please select a handler to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 menuForm = new Form2(); // Change to your actual menu form name
            menuForm.Show();
        }

        private void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.Gainsboro;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label || ctrl is Button)
                {
                    ctrl.ForeColor = Color.Gainsboro;
                    ctrl.BackColor = Color.FromArgb(45, 45, 45);
                }

                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.DimGray;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
                }
            }

            handlersGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            handlersGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            handlersGrid.DefaultCellStyle.ForeColor = Color.White;
            handlersGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            handlersGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            handlersGrid.EnableHeadersVisualStyles = false;
        }
    }
}
