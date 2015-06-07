namespace rt
{
    partial class RayTracer
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RayTracer));
            this.RenderPicture = new System.Windows.Forms.PictureBox();
            this.StatusText = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LoadButton = new System.Windows.Forms.Button();
            this.PathText = new System.Windows.Forms.Label();
            this.RenderButton = new System.Windows.Forms.Button();
            this.FiltersTitle = new System.Windows.Forms.Label();
            this.FiltersContrastMore = new System.Windows.Forms.Button();
            this.FiltersBorderEnhancement = new System.Windows.Forms.Button();
            this.FiltersBlur = new System.Windows.Forms.Button();
            this.FiltersBorderDetect = new System.Windows.Forms.Button();
            this.FiltersBorderDetectMore = new System.Windows.Forms.Button();
            this.FiltersPush = new System.Windows.Forms.Button();
            this.FiltersSharpeness = new System.Windows.Forms.Button();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RenderPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderPicture
            // 
            this.RenderPicture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.RenderPicture.Location = new System.Drawing.Point(848, 51);
            this.RenderPicture.Name = "RenderPicture";
            this.RenderPicture.Size = new System.Drawing.Size(1024, 768);
            this.RenderPicture.TabIndex = 0;
            this.RenderPicture.TabStop = false;
            // 
            // StatusText
            // 
            this.StatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.StatusText.Location = new System.Drawing.Point(0, 852);
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(1920, 48);
            this.StatusText.TabIndex = 1;
            this.StatusText.Text = "Waiting for .obj file to be selected.";
            this.StatusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(16, 932);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(1888, 48);
            this.ProgressBar.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = ".obj";
            this.openFileDialog1.Filter = "Obj files (*.obj)|*.obj";
            this.openFileDialog1.Title = ".obj selector";
            // 
            // LoadButton
            // 
            this.LoadButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.LoadButton.Location = new System.Drawing.Point(576, 67);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(256, 48);
            this.LoadButton.TabIndex = 3;
            this.LoadButton.Text = "Load .obj file";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // PathText
            // 
            this.PathText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.PathText.Location = new System.Drawing.Point(48, 67);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(512, 48);
            this.PathText.TabIndex = 4;
            this.PathText.Text = "No file selected";
            this.PathText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RenderButton
            // 
            this.RenderButton.Enabled = false;
            this.RenderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RenderButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.RenderButton.Location = new System.Drawing.Point(54, 739);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(778, 80);
            this.RenderButton.TabIndex = 5;
            this.RenderButton.Text = "Render scene";
            this.RenderButton.UseVisualStyleBackColor = true;
            this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
            // 
            // FiltersTitle
            // 
            this.FiltersTitle.BackColor = System.Drawing.Color.Transparent;
            this.FiltersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersTitle.Location = new System.Drawing.Point(54, 144);
            this.FiltersTitle.Name = "FiltersTitle";
            this.FiltersTitle.Size = new System.Drawing.Size(778, 64);
            this.FiltersTitle.TabIndex = 13;
            this.FiltersTitle.Text = "- Filters -";
            this.FiltersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FiltersContrastMore
            // 
            this.FiltersContrastMore.Enabled = false;
            this.FiltersContrastMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersContrastMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersContrastMore.Location = new System.Drawing.Point(54, 229);
            this.FiltersContrastMore.Name = "FiltersContrastMore";
            this.FiltersContrastMore.Size = new System.Drawing.Size(778, 64);
            this.FiltersContrastMore.TabIndex = 14;
            this.FiltersContrastMore.Text = "Contrast more";
            this.FiltersContrastMore.UseVisualStyleBackColor = true;
            this.FiltersContrastMore.Click += new System.EventHandler(this.FiltersContrastMore_Click);
            // 
            // FiltersBorderEnhancement
            // 
            this.FiltersBorderEnhancement.Enabled = false;
            this.FiltersBorderEnhancement.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBorderEnhancement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBorderEnhancement.Location = new System.Drawing.Point(54, 299);
            this.FiltersBorderEnhancement.Name = "FiltersBorderEnhancement";
            this.FiltersBorderEnhancement.Size = new System.Drawing.Size(778, 64);
            this.FiltersBorderEnhancement.TabIndex = 15;
            this.FiltersBorderEnhancement.Text = "Border enhancement";
            this.FiltersBorderEnhancement.UseVisualStyleBackColor = true;
            this.FiltersBorderEnhancement.Click += new System.EventHandler(this.FiltersBorderEnhancement_Click);
            // 
            // FiltersBlur
            // 
            this.FiltersBlur.Enabled = false;
            this.FiltersBlur.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBlur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBlur.Location = new System.Drawing.Point(54, 369);
            this.FiltersBlur.Name = "FiltersBlur";
            this.FiltersBlur.Size = new System.Drawing.Size(778, 64);
            this.FiltersBlur.TabIndex = 16;
            this.FiltersBlur.Text = "Blur";
            this.FiltersBlur.UseVisualStyleBackColor = true;
            this.FiltersBlur.Click += new System.EventHandler(this.FiltersBlur_Click);
            // 
            // FiltersBorderDetect
            // 
            this.FiltersBorderDetect.Enabled = false;
            this.FiltersBorderDetect.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBorderDetect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBorderDetect.Location = new System.Drawing.Point(54, 439);
            this.FiltersBorderDetect.Name = "FiltersBorderDetect";
            this.FiltersBorderDetect.Size = new System.Drawing.Size(778, 64);
            this.FiltersBorderDetect.TabIndex = 17;
            this.FiltersBorderDetect.Text = "Border detect";
            this.FiltersBorderDetect.UseVisualStyleBackColor = true;
            this.FiltersBorderDetect.Click += new System.EventHandler(this.FiltersBorderDetect_Click);
            // 
            // FiltersBorderDetectMore
            // 
            this.FiltersBorderDetectMore.Enabled = false;
            this.FiltersBorderDetectMore.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBorderDetectMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBorderDetectMore.Location = new System.Drawing.Point(54, 509);
            this.FiltersBorderDetectMore.Name = "FiltersBorderDetectMore";
            this.FiltersBorderDetectMore.Size = new System.Drawing.Size(778, 64);
            this.FiltersBorderDetectMore.TabIndex = 18;
            this.FiltersBorderDetectMore.Text = "Border detect more";
            this.FiltersBorderDetectMore.UseVisualStyleBackColor = true;
            this.FiltersBorderDetectMore.Click += new System.EventHandler(this.FiltersBorderDetectMore_Click);
            // 
            // FiltersPush
            // 
            this.FiltersPush.Enabled = false;
            this.FiltersPush.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersPush.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersPush.Location = new System.Drawing.Point(54, 579);
            this.FiltersPush.Name = "FiltersPush";
            this.FiltersPush.Size = new System.Drawing.Size(778, 64);
            this.FiltersPush.TabIndex = 19;
            this.FiltersPush.Text = "Push";
            this.FiltersPush.UseVisualStyleBackColor = true;
            this.FiltersPush.Click += new System.EventHandler(this.FiltersPush_Click);
            // 
            // FiltersSharpeness
            // 
            this.FiltersSharpeness.Enabled = false;
            this.FiltersSharpeness.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersSharpeness.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersSharpeness.Location = new System.Drawing.Point(54, 649);
            this.FiltersSharpeness.Name = "FiltersSharpeness";
            this.FiltersSharpeness.Size = new System.Drawing.Size(778, 64);
            this.FiltersSharpeness.TabIndex = 20;
            this.FiltersSharpeness.Text = "Sharpeness";
            this.FiltersSharpeness.UseVisualStyleBackColor = true;
            this.FiltersSharpeness.Click += new System.EventHandler(this.FiltersSharpeness_Click);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ButtonExit.Location = new System.Drawing.Point(16, 1000);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(196, 64);
            this.ButtonExit.TabIndex = 21;
            this.ButtonExit.Text = "Exit";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 1000);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "label1";
            // 
            // RayTracer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonExit);
            this.Controls.Add(this.FiltersSharpeness);
            this.Controls.Add(this.FiltersPush);
            this.Controls.Add(this.FiltersBorderDetectMore);
            this.Controls.Add(this.FiltersBorderDetect);
            this.Controls.Add(this.FiltersBlur);
            this.Controls.Add(this.FiltersBorderEnhancement);
            this.Controls.Add(this.FiltersContrastMore);
            this.Controls.Add(this.FiltersTitle);
            this.Controls.Add(this.RenderButton);
            this.Controls.Add(this.PathText);
            this.Controls.Add(this.LoadButton);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.StatusText);
            this.Controls.Add(this.RenderPicture);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RayTracer";
            this.Padding = new System.Windows.Forms.Padding(448, 0, 0, 0);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chicken RayTracer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.RenderPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox RenderPicture;
        private System.Windows.Forms.Label StatusText;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label PathText;
        private System.Windows.Forms.Button RenderButton;
        private System.Windows.Forms.Label FiltersTitle;
        private System.Windows.Forms.Button FiltersContrastMore;
        private System.Windows.Forms.Button FiltersBorderEnhancement;
        private System.Windows.Forms.Button FiltersBlur;
        private System.Windows.Forms.Button FiltersBorderDetect;
        private System.Windows.Forms.Button FiltersBorderDetectMore;
        private System.Windows.Forms.Button FiltersPush;
        private System.Windows.Forms.Button FiltersSharpeness;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Label label1;
    }
}

