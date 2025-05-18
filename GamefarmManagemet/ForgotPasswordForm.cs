using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public class ForgotPasswordForm : Form
    {
        private TextBox textBoxEmail;
        private TextBox textBoxNewPassword;

        public ForgotPasswordForm()
        {
            this.Text = "Forgot Password";
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Email label and textbox
            Label labelEmail = new Label();
            labelEmail.Text = "Email:";
            labelEmail.Location = new Point(20, 30);
            labelEmail.Size = new Size(100, 25);

            textBoxEmail = new TextBox();
            textBoxEmail.Location = new Point(120, 30);
            textBoxEmail.Size = new Size(180, 25);

            // New password label and textbox
            Label labelNewPassword = new Label();
            labelNewPassword.Text = "New Password:";
            labelNewPassword.Location = new Point(20, 70);
            labelNewPassword.Size = new Size(100, 25);

            textBoxNewPassword = new TextBox();
            textBoxNewPassword.Location = new Point(120, 70);
            textBoxNewPassword.Size = new Size(180, 25);
            textBoxNewPassword.PasswordChar = '*';

            // Reset button
            Button buttonReset = new Button();
            buttonReset.Text = "Reset Password";
            buttonReset.Size = new Size(280, 40);
            buttonReset.Location = new Point(20, 120);
            buttonReset.BackColor = Color.Red;
            buttonReset.ForeColor = Color.White;
            buttonReset.Click += ButtonReset_Click;

            // Add controls to form
            this.Controls.Add(labelEmail);
            this.Controls.Add(textBoxEmail);
            this.Controls.Add(labelNewPassword);
            this.Controls.Add(textBoxNewPassword);
            this.Controls.Add(buttonReset);
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string newPassword = textBoxNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please fill out both fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connStr = "server=localhost;user=root;password=Leonard010504.;database=ex_db;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM users WHERE email = @Email";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Email", email);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        string updateQuery = "UPDATE users SET password = @Password WHERE email = @Email";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                        updateCmd.Parameters.AddWithValue("@Password", newPassword);
                        updateCmd.Parameters.AddWithValue("@Email", email);
                        updateCmd.ExecuteNonQuery();

                        MessageBox.Show("Password reset successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Email not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
