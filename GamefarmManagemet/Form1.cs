using System;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // PictureBox behavior
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // Button design
            button1.Text = "Get Started";
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.Size = new Size(150, 50);
            button1.Anchor = AnchorStyles.None;

            // Center the button initially
            CenterButton();

            // Handle form resizing to keep the button centered
            this.Resize += new EventHandler(Form1_Resize);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CenterButton(); // In case it's needed at load
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterButton();
        }

        private void CenterButton()
        {
            // Position it around 70% of the width and 60% of the height
            button1.Left = (int)(this.ClientSize.Width * 0.8) - (button1.Width / 2);
            button1.Top = (int)(this.ClientSize.Height * 0.7) - (button1.Height / 2);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();  // create an instance of Form2
            form2.Show();               // show Form2
            this.Hide();                // hide current form
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Optional: add logic if the image should do something when clicked
        }
    }
}
