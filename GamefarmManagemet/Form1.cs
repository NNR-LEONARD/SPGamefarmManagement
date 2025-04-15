namespace GamefarmManagemet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Get Started";
            button1.BackColor = Color.Red;
            button1.ForeColor = Color.Black;
            button1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0; // Remove border
            button1.Size = new Size(150, 50);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();  // create an instance of Form2
            form2.Show();               // show Form2
            this.Hide();
        }
    }
}
