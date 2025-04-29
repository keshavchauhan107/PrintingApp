using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PrintingApp
{
    public class CheckedComboBox : UserControl
    {
        private TextBox txtDisplay;
        private Button btnDropDown;
        private Form dropDownForm;
        private CheckedListBox clbItems;
        private bool dropDownOpen = false;
        private bool skipNextToggle = false;
        // Symbols for dropdown states.
        private const string DownArrow = "▼";
        private const string UpArrow = "▲";

        public CheckedComboBox()
        {
            InitializeControl();
        }

        private void InitializeControl()
        {
            // Initialize components.
            txtDisplay = new TextBox();
            btnDropDown = new Button();
            clbItems = new CheckedListBox();
            dropDownForm = new Form();

            // Configure TextBox.
            txtDisplay.ReadOnly = true;
            txtDisplay.BorderStyle = BorderStyle.None;
            // Increase the text size for better readability.
            txtDisplay.Font = new Font(txtDisplay.Font.FontFamily, 12F, txtDisplay.Font.Style);
            txtDisplay.Location = new Point(3, 3);
            txtDisplay.Width = this.Width - 22;
            txtDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // Toggle dropdown when text box is clicked.
            txtDisplay.Click += Control_Click;

            // Configure Button.
            btnDropDown.Text = DownArrow; // Set initial symbol.
            btnDropDown.Width = 20;
            btnDropDown.Dock = DockStyle.Right;
            btnDropDown.Click += BtnDropDown_Click;

            // Add TextBox and Button to the UserControl.
            this.Controls.Add(txtDisplay);
            this.Controls.Add(btnDropDown);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Height = txtDisplay.Height + 6;
            this.Resize += CheckedComboBox_Resize;

            // Note: Removing this.Click event handler minimizes unintended toggles.
            // If you wish to toggle from other areas of the control, you can selectively add click events
            // to specific sub-controls rather than the entire UserControl.

            // Configure the DropDown Form.
            dropDownForm.FormBorderStyle = FormBorderStyle.None;
            dropDownForm.StartPosition = FormStartPosition.Manual;
            dropDownForm.ShowInTaskbar = false;
            dropDownForm.Deactivate += DropDownForm_Deactivate;
            dropDownForm.Controls.Add(clbItems);

            // Configure CheckedListBox inside the drop-down.
            clbItems.Dock = DockStyle.Fill;
            clbItems.CheckOnClick = true;
            // Optionally, increase the font size for the drop-down list.
            clbItems.Font = new Font(clbItems.Font.FontFamily, 12F, clbItems.Font.Style);
            clbItems.ItemCheck += ClbItems_ItemCheck;
        }

        // Update the TextBox width when the control is resized.
        private void CheckedComboBox_Resize(object sender, EventArgs e)
        {
            txtDisplay.Width = this.Width - btnDropDown.Width - 6;
        }

        // Handle clicks on the arrow button.
        private void BtnDropDown_Click(object sender, EventArgs e)
        {
            ToggleDropDown();
        }

        // Handle clicks on the text box.
        private void Control_Click(object sender, EventArgs e)
        {
            ToggleDropDown();
        }

        // Toggle the dropdown open or closed.
        private void ToggleDropDown()
        {
            if (!dropDownOpen)
            {
              
                // Open the dropdown.
                Point location = this.Parent.PointToScreen(new Point(this.Left, this.Bottom));
                dropDownForm.Location = location;
                dropDownForm.Width = this.Width;
                dropDownForm.Height = 150; // Set an appropriate height for the dropdown.
                dropDownForm.Show();
                dropDownOpen = true;
                btnDropDown.Text = UpArrow;
            }
            else
            {
                
                // Close the dropdown.
                dropDownForm.Hide();
                dropDownOpen = false;
                btnDropDown.Text = DownArrow;
            }
        }

        // Hide the dropdown when it loses focus.
        private void DropDownForm_Deactivate(object sender, EventArgs e)
        {
            dropDownForm.Hide();
            dropDownOpen = false;
            btnDropDown.Text = DownArrow;
        }

        // Schedule update of the TextBox display text after checked state changes.
        private void ClbItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((Action)(UpdateDisplayText));
        }

        private void UpdateDisplayText()
        {
            // Join selected items with commas.
            txtDisplay.Text = string.Join(", ", clbItems.CheckedItems.Cast<object>());
        }

        // Public method to add items to the dropdown list.
        public void AddItem(object item)
        {
            clbItems.Items.Add(item);
        }

        // Optionally expose the selected items.
        //public object[] GetSelectedItems()
        //{
        //    return clbItems.CheckedItems.Cast<object>().ToArray();
        //}

        public List<object> GetSelectedItems()
        {
            return clbItems.CheckedItems.Cast<object>().ToList();
        }
    }
}
