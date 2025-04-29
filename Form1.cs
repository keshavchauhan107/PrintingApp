using PrintingApp.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PrintingApp
{

    public partial class Form1 : Form
    {
        private Icon _defaultIcon;
        private string _defaultText;
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
        }


        private void OpenChild(Form childForm)
        {
            // Close any open MDI children
            foreach (var open in this.MdiChildren)
                open.Close();

            // Show the new one
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
            //this.Icon = ActiveMdiChild.Icon;
        }



        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new sticker());
        }

        private void planToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new sticker("Plan"));
        }

        private void UnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new sticker("Unit"));
        }



        private void addSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new Forms.addSize());
        }

        private void addPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new addPlan());
        }


        private void monoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenChild(new sticker("Mono"));
        }

        private void rePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new rePrint());
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new Forms.print());
        }

        private void addProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new Forms.addProduct());
        }

        private void generateStickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChild(new generateSticker());
        }
    }
}
