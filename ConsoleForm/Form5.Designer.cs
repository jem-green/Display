
namespace ConsoleFrom
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.displayBox1 = new ConsoleLibrary.DisplayBox();
            this.SuspendLayout();
            // 
            // displayBox1
            // 
            this.displayBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayBox1.Location = new System.Drawing.Point(0, 0);
            this.displayBox1.Name = "displayBox1";
            this.displayBox1.Size = new System.Drawing.Size(1435, 1155);
            this.displayBox1.TabIndex = 0;
            this.displayBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.displayBox1_KeyDown);
            this.displayBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.displayBox1_MouseDown);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1435, 1155);
            this.Controls.Add(this.displayBox1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.ResumeLayout(false);

        }

        #endregion

        private ConsoleLibrary.DisplayBox displayBox1;
    }
}

