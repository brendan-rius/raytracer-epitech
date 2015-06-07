namespace RT_2_poule
{
    partial class Form1
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
            this.RenderScene = new System.Windows.Forms.Button();
            this.SaveScene = new System.Windows.Forms.Button();
            this.LoadFile = new System.Windows.Forms.Button();
            this.OpenFile = new System.Windows.Forms.OpenFileDialog();
            this.SaveFile = new System.Windows.Forms.SaveFileDialog();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.RenderPicture = new System.Windows.Forms.PictureBox();
            this.FileName = new System.Windows.Forms.Label();
            this.Exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.RenderPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // RenderScene
            // 
            this.RenderScene.Enabled = false;
            this.RenderScene.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RenderScene.Location = new System.Drawing.Point(936, 1028);
            this.RenderScene.Name = "RenderScene";
            this.RenderScene.Size = new System.Drawing.Size(256, 48);
            this.RenderScene.TabIndex = 0;
            this.RenderScene.Text = "Render";
            this.RenderScene.UseVisualStyleBackColor = true;
            this.RenderScene.Click += new System.EventHandler(this.RenderScene_Click);
            // 
            // SaveScene
            // 
            this.SaveScene.Enabled = false;
            this.SaveScene.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveScene.Location = new System.Drawing.Point(1652, 1028);
            this.SaveScene.Name = "SaveScene";
            this.SaveScene.Size = new System.Drawing.Size(256, 48);
            this.SaveScene.TabIndex = 1;
            this.SaveScene.Text = "Save";
            this.SaveScene.UseVisualStyleBackColor = true;
            this.SaveScene.Click += new System.EventHandler(this.SaveScene_Click);
            // 
            // LoadFile
            // 
            this.LoadFile.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadFile.Location = new System.Drawing.Point(210, 1028);
            this.LoadFile.Name = "LoadFile";
            this.LoadFile.Size = new System.Drawing.Size(192, 48);
            this.LoadFile.TabIndex = 2;
            this.LoadFile.Text = "Load file";
            this.LoadFile.UseVisualStyleBackColor = true;
            this.LoadFile.Click += new System.EventHandler(this.LoadFile_Click);
            // 
            // OpenFile
            // 
            this.OpenFile.FileName = ".poule";
            this.OpenFile.Filter = "Poule files (*.poule)|*.poule";
            // 
            // SaveFile
            // 
            this.SaveFile.FileName = ".png";
            this.SaveFile.Filter = "Image (.png)|*.png";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(1198, 1028);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(448, 48);
            this.ProgressBar.TabIndex = 3;
            // 
            // RenderPicture
            // 
            this.RenderPicture.BackColor = System.Drawing.SystemColors.ControlDark;
            this.RenderPicture.Location = new System.Drawing.Point(64, 16);
            this.RenderPicture.Name = "RenderPicture";
            this.RenderPicture.Size = new System.Drawing.Size(1792, 1008);
            this.RenderPicture.TabIndex = 4;
            this.RenderPicture.TabStop = false;
            // 
            // FileName
            // 
            this.FileName.BackColor = System.Drawing.Color.Transparent;
            this.FileName.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileName.Location = new System.Drawing.Point(408, 1028);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(522, 48);
            this.FileName.TabIndex = 5;
            this.FileName.Text = "File path";
            this.FileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Exit
            // 
            this.Exit.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Location = new System.Drawing.Point(12, 1028);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(192, 48);
            this.Exit.TabIndex = 6;
            this.Exit.Text = "Exit";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.RenderPicture);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.LoadFile);
            this.Controls.Add(this.SaveScene);
            this.Controls.Add(this.RenderScene);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.RenderPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RenderScene;
        private System.Windows.Forms.Button SaveScene;
        private System.Windows.Forms.Button LoadFile;
        private System.Windows.Forms.OpenFileDialog OpenFile;
        private System.Windows.Forms.SaveFileDialog SaveFile;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.PictureBox RenderPicture;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Button Exit;
    }
}

