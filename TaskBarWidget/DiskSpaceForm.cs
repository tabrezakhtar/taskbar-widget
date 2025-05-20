using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TaskBarWidget
{
    public partial class DiskSpaceForm : Form
    {
        public DiskSpaceForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = ColorTranslator.FromHtml("#1C1C1C");
            this.ForeColor = Color.White;
            this.Size = new Size(200, 30);
            this.Location = new Point(10, Screen.PrimaryScreen.Bounds.Height - 52);

            this.ShowInTaskbar = false;

            diskLabel = new Label()
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Courier New", 8, FontStyle.Bold)
            };
            this.Controls.Add(diskLabel);

            UpdateDiskSpace(null, null);
        }

        private void UpdateDiskSpace(object sender, EventArgs e)
        {
            DriveInfo drive = new DriveInfo("C");
            long freeSpace = drive.AvailableFreeSpace / (1024 * 1024 * 1024);
            long totalSpace = drive.TotalSize / (1024 * 1024 * 1024);
            diskLabel.Text = $"C:\\ {freeSpace} of {totalSpace} GB free\n";

            drive = new DriveInfo("D");
            freeSpace = drive.AvailableFreeSpace / (1024 * 1024 * 1024);
            totalSpace = drive.TotalSize / (1024 * 1024 * 1024);
            diskLabel.Text += $"D:\\ {freeSpace} of {totalSpace} GB free";
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new DiskSpaceForm());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
            UpdateDiskSpace(null, null);
        }
    }
}
