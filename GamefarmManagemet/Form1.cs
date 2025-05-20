using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class Form1 : Form
    {
        private Panel loginCard;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelPassword;
        private TextBox textBoxPassword;
        private Button buttonLogin;
        private LinkLabel linkForgotPassword;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Login Card Panel
            loginCard = new Panel();
            loginCard.Size = new Size(300, 320);
            loginCard.BackColor = Color.FromArgb(30, 30, 30);
            loginCard.BorderStyle = BorderStyle.None;
            loginCard.Paint += LoginCard_Paint;
            this.Controls.Add(loginCard);

            // Email Label
            labelEmail = new Label();
            labelEmail.Text = "Email:";
            labelEmail.ForeColor = Color.Red;
            labelEmail.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            labelEmail.AutoSize = true;
            labelEmail.Location = new Point(20, 30);
            loginCard.Controls.Add(labelEmail);

            // Email TextBox
            textBoxEmail = new TextBox();
            textBoxEmail.Width = 260;
            textBoxEmail.Font = new Font("Segoe UI", 11);
            textBoxEmail.Location = new Point(20, 60);
            textBoxEmail.Margin = new Padding(20);
            loginCard.Controls.Add(textBoxEmail);

            // Password Label
            labelPassword = new Label();
            labelPassword.Text = "Password:";
            labelPassword.ForeColor = Color.Red;
            labelPassword.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            labelPassword.AutoSize = true;
            labelPassword.Location = new Point(20, 110);
            loginCard.Controls.Add(labelPassword);

            // Password TextBox
            textBoxPassword = new TextBox();
            textBoxPassword.Width = 260;
            textBoxPassword.Font = new Font("Segoe UI", 11);
            textBoxPassword.Location = new Point(20, 140);
            textBoxPassword.PasswordChar = '*';
            textBoxPassword.Margin = new Padding(20);
            loginCard.Controls.Add(textBoxPassword);

            // Forgot Password Link
            linkForgotPassword = new LinkLabel();
            linkForgotPassword.Text = "Forgot Password?";
            linkForgotPassword.LinkColor = Color.Red;
            linkForgotPassword.ActiveLinkColor = Color.OrangeRed;
            linkForgotPassword.VisitedLinkColor = Color.DarkRed;
            linkForgotPassword.Location = new Point(20, 185);
            linkForgotPassword.AutoSize = true;
            linkForgotPassword.Cursor = Cursors.Hand;
            linkForgotPassword.LinkBehavior = LinkBehavior.HoverUnderline;
            linkForgotPassword.Click += LinkForgotPassword_Click;
            loginCard.Controls.Add(linkForgotPassword);

            // Login Button
            buttonLogin = new Button();
            buttonLogin.Text = "Log In";
            buttonLogin.BackColor = Color.Red;
            buttonLogin.ForeColor = Color.Black;
            buttonLogin.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            buttonLogin.FlatStyle = FlatStyle.Flat;
            buttonLogin.FlatAppearance.BorderSize = 0;
            buttonLogin.Size = new Size(260, 50);
            buttonLogin.Location = new Point(20, 210);
            buttonLogin.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, buttonLogin.Width, buttonLogin.Height, 15, 15));
            buttonLogin.Click += buttonLogin_Click;
            loginCard.Controls.Add(buttonLogin);

            // Center Card
            CenterLoginCard();
            this.Resize += (s, e) => CenterLoginCard();
        }

        private void CenterLoginCard()
        {
            loginCard.Left = (this.ClientSize.Width - loginCard.Width) / 2;
            loginCard.Top = (this.ClientSize.Height - loginCard.Height) / 2;
        }

        private void LoginCard_Paint(object sender, PaintEventArgs e)
        {
            int radius = 20;
            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle rect = loginCard.ClientRectangle;
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                loginCard.Region = new Region(path);
            }
        }

        private void LinkForgotPassword_Click(object sender, EventArgs e)
        {
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.ShowDialog();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string connStr = "server=localhost;user=root;password=Leonard010504.;database=ex_db;";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM users WHERE email = @Email AND password = @Password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        Form2 form2 = new Form2();
                        form2.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Importing CreateRoundRectRgn from GDI32.dll for rounded controls
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
