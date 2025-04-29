using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PrintingApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPlanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateStickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stickersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.planToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rePrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSizeToolStripMenuItem,
            this.addPlanToolStripMenuItem,
            this.addProductToolStripMenuItem,
            this.generateStickerToolStripMenuItem,
            this.stickersToolStripMenuItem,
            this.printToolStripMenuItem,
            this.rePrintToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1370, 31);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addSizeToolStripMenuItem
            // 
            this.addSizeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addSizeToolStripMenuItem.Name = "addSizeToolStripMenuItem";
            this.addSizeToolStripMenuItem.Size = new System.Drawing.Size(82, 25);
            this.addSizeToolStripMenuItem.Text = "Add Size";
            this.addSizeToolStripMenuItem.Click += new System.EventHandler(this.addSizeToolStripMenuItem_Click);
            // 
            // addPlanToolStripMenuItem
            // 
            this.addPlanToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPlanToolStripMenuItem.Name = "addPlanToolStripMenuItem";
            this.addPlanToolStripMenuItem.Size = new System.Drawing.Size(84, 25);
            this.addPlanToolStripMenuItem.Text = "Add Plan";
            this.addPlanToolStripMenuItem.Click += new System.EventHandler(this.addPlanToolStripMenuItem_Click);
            // 
            // addProductToolStripMenuItem
            // 
            this.addProductToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addProductToolStripMenuItem.Name = "addProductToolStripMenuItem";
            this.addProductToolStripMenuItem.Size = new System.Drawing.Size(108, 25);
            this.addProductToolStripMenuItem.Text = "Add Product";
            this.addProductToolStripMenuItem.Click += new System.EventHandler(this.addProductToolStripMenuItem_Click);
            // 
            // generateStickerToolStripMenuItem
            // 
            this.generateStickerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generateStickerToolStripMenuItem.Name = "generateStickerToolStripMenuItem";
            this.generateStickerToolStripMenuItem.Size = new System.Drawing.Size(135, 25);
            this.generateStickerToolStripMenuItem.Text = "Generate Sticker";
            this.generateStickerToolStripMenuItem.Click += new System.EventHandler(this.generateStickerToolStripMenuItem_Click);
            // 
            // stickersToolStripMenuItem
            // 
            this.stickersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.UnitToolStripMenuItem,
            this.monoToolStripMenuItem1,
            this.planToolStripMenuItem});
            this.stickersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stickersToolStripMenuItem.Name = "stickersToolStripMenuItem";
            this.stickersToolStripMenuItem.Size = new System.Drawing.Size(75, 25);
            this.stickersToolStripMenuItem.Text = "Stickers";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.allToolStripMenuItem.Text = "All";
            this.allToolStripMenuItem.Click += new System.EventHandler(this.allToolStripMenuItem_Click);
            // 
            // UnitToolStripMenuItem
            // 
            this.UnitToolStripMenuItem.Name = "UnitToolStripMenuItem";
            this.UnitToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.UnitToolStripMenuItem.Text = "Unit";
            this.UnitToolStripMenuItem.Click += new System.EventHandler(this.UnitToolStripMenuItem_Click);
            // 
            // monoToolStripMenuItem1
            // 
            this.monoToolStripMenuItem1.Name = "monoToolStripMenuItem1";
            this.monoToolStripMenuItem1.Size = new System.Drawing.Size(180, 26);
            this.monoToolStripMenuItem1.Text = "Mono";
            this.monoToolStripMenuItem1.Click += new System.EventHandler(this.monoToolStripMenuItem1_Click);
            // 
            // planToolStripMenuItem
            // 
            this.planToolStripMenuItem.Name = "planToolStripMenuItem";
            this.planToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.planToolStripMenuItem.Text = "Plan";
            this.planToolStripMenuItem.Click += new System.EventHandler(this.planToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(55, 25);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // rePrintToolStripMenuItem
            // 
            this.rePrintToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rePrintToolStripMenuItem.Name = "rePrintToolStripMenuItem";
            this.rePrintToolStripMenuItem.Size = new System.Drawing.Size(79, 25);
            this.rePrintToolStripMenuItem.Text = "Re-Print";
            this.rePrintToolStripMenuItem.Click += new System.EventHandler(this.rePrintToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 709);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Printing Barcode Application";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem stickersToolStripMenuItem;
        private ToolStripMenuItem allToolStripMenuItem;
        private ToolStripMenuItem UnitToolStripMenuItem;
        private ToolStripMenuItem monoToolStripMenuItem1;
        private ToolStripMenuItem planToolStripMenuItem;
        private ToolStripMenuItem rePrintToolStripMenuItem;
        private ToolStripMenuItem printToolStripMenuItem;
        private ToolStripMenuItem addSizeToolStripMenuItem;
        private ToolStripMenuItem addPlanToolStripMenuItem;
        private ToolStripMenuItem addProductToolStripMenuItem;
        private ToolStripMenuItem generateStickerToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip1;
    }
}
