using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;  // Add this for WebView2
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class Form2 : Form
    {
        private WebView2 webView;

        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load; // Attach load event
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ApplyHoverEffectToButtons(this);

            // Initialize and add WebView2
            webView = new WebView2();
            webView.Dock = DockStyle.Fill;
            // If you want WebView2 behind buttons, add it first:
            this.Controls.Add(webView);
            this.Controls.SetChildIndex(webView, this.Controls.Count - 1); // Send to back

            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            await webView.EnsureCoreWebView2Async(null);

            // To load an online video, uncomment this:
            

            // Or to load a local video, replace with your file path and uncomment this:
            /*
            string localVideoPath = @"C:\Videos\yourvideo.mp4"; 
            var videoUri = new Uri(localVideoPath).AbsoluteUri;
            string html = $@"
                <html>
                <body style='margin:0px; overflow:hidden;'>
                    <video width='100%' height='100%' controls autoplay>
                        <source src='{videoUri}' type='video/mp4'>
                        Your browser does not support the video tag.
                    </video>
                </body>
                </html>";
            webView.NavigateToString(html);
            */
        }

        private void ApplyHoverEffectToButtons(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button btn)
                {
                    btn.MouseEnter += Button_MouseEnter;
                    btn.MouseLeave += Button_MouseLeave;

                    // Optional styling
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(45, 45, 45); // Default dark background
                    btn.ForeColor = Color.White;
                }

                if (control.HasChildren)
                {
                    ApplyHoverEffectToButtons(control);
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.Red;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                btn.BackColor = Color.FromArgb(45, 45, 45); // Default dark background
            }
        }

        // Your existing button click handlers below...
        private void button9_Click(object sender, EventArgs e)
        {
            FightRecords fightrecordsForm = new FightRecords();
            fightrecordsForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Attendance attendanceForm = new Attendance();
            attendanceForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            HandlerManagement handlerForm = new HandlerManagement();
            handlerForm.Show();
        }

        private void button1_Click(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e) { }

        private void button5_Click(object sender, EventArgs e) { }

        private void button7_Click(object sender, EventArgs e) { }

        private void button8_Click(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void button10_Click(object sender, EventArgs e)
        {
            Form targetForm = new FightRecords();
            this.Hide();
            targetForm.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GamefowlsRecords recordsForm = new GamefowlsRecords();
            recordsForm.Show();
            this.Hide();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            BreedingRecords menuForm = new BreedingRecords();
            menuForm.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Form1 menuForm = new Form1();
            menuForm.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            TrainingLogs menuForm = new TrainingLogs();
            menuForm.Show();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AuditLogs menuForm = new AuditLogs();
            menuForm.Show();
        }

        private void label1_Click(object sender, EventArgs e) { }

        //private void pictureBox2_Click(object sender, EventArgs e) { }
    }
}
