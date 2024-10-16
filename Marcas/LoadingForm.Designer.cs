namespace Presentacion
{
    partial class LoadingForm
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
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.None;
            progressBar1.Location = new Point(301, 185);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(188, 46);
            progressBar1.TabIndex = 0;
            // 
            // LoadingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(19, 12, 70);
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoadingForm";
            Opacity = 0.9D;
            Text = "LoadingForm";
            Load += LoadingForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private ProgressBar progressBar1;
    }
}