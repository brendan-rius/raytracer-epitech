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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.PositionX = new System.Windows.Forms.NumericUpDown();
            this.PositionY = new System.Windows.Forms.NumericUpDown();
            this.PositionZ = new System.Windows.Forms.NumericUpDown();
            this.RotationZ = new System.Windows.Forms.NumericUpDown();
            this.RotationY = new System.Windows.Forms.NumericUpDown();
            this.RotationX = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.RenderPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationX)).BeginInit();
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
            this.StatusText.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.LoadButton.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.LoadButton.Location = new System.Drawing.Point(576, 16);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(256, 48);
            this.LoadButton.TabIndex = 3;
            this.LoadButton.Text = "Load .obj file";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // PathText
            // 
            this.PathText.BackColor = System.Drawing.Color.Transparent;
            this.PathText.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PathText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.PathText.Location = new System.Drawing.Point(55, 16);
            this.PathText.Name = "PathText";
            this.PathText.Size = new System.Drawing.Size(512, 48);
            this.PathText.TabIndex = 4;
            this.PathText.Text = "No file selected";
            this.PathText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RenderButton
            // 
            this.RenderButton.Enabled = false;
            this.RenderButton.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RenderButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.RenderButton.Location = new System.Drawing.Point(54, 755);
            this.RenderButton.Name = "RenderButton";
            this.RenderButton.Size = new System.Drawing.Size(778, 64);
            this.RenderButton.TabIndex = 5;
            this.RenderButton.Text = "Render scene";
            this.RenderButton.UseVisualStyleBackColor = true;
            this.RenderButton.Click += new System.EventHandler(this.RenderButton_Click);
            // 
            // FiltersTitle
            // 
            this.FiltersTitle.BackColor = System.Drawing.Color.Transparent;
            this.FiltersTitle.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersTitle.Location = new System.Drawing.Point(55, 271);
            this.FiltersTitle.Name = "FiltersTitle";
            this.FiltersTitle.Size = new System.Drawing.Size(778, 48);
            this.FiltersTitle.TabIndex = 13;
            this.FiltersTitle.Text = "- Filters -";
            this.FiltersTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FiltersContrastMore
            // 
            this.FiltersContrastMore.Enabled = false;
            this.FiltersContrastMore.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersContrastMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersContrastMore.Location = new System.Drawing.Point(54, 322);
            this.FiltersContrastMore.Name = "FiltersContrastMore";
            this.FiltersContrastMore.Size = new System.Drawing.Size(778, 56);
            this.FiltersContrastMore.TabIndex = 14;
            this.FiltersContrastMore.Text = "Contrast more";
            this.FiltersContrastMore.UseVisualStyleBackColor = true;
            this.FiltersContrastMore.Click += new System.EventHandler(this.FiltersContrastMore_Click);
            // 
            // FiltersBorderEnhancement
            // 
            this.FiltersBorderEnhancement.Enabled = false;
            this.FiltersBorderEnhancement.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBorderEnhancement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBorderEnhancement.Location = new System.Drawing.Point(54, 384);
            this.FiltersBorderEnhancement.Name = "FiltersBorderEnhancement";
            this.FiltersBorderEnhancement.Size = new System.Drawing.Size(778, 56);
            this.FiltersBorderEnhancement.TabIndex = 15;
            this.FiltersBorderEnhancement.Text = "Border enhancement";
            this.FiltersBorderEnhancement.UseVisualStyleBackColor = true;
            this.FiltersBorderEnhancement.Click += new System.EventHandler(this.FiltersBorderEnhancement_Click);
            // 
            // FiltersBlur
            // 
            this.FiltersBlur.Enabled = false;
            this.FiltersBlur.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBlur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBlur.Location = new System.Drawing.Point(54, 446);
            this.FiltersBlur.Name = "FiltersBlur";
            this.FiltersBlur.Size = new System.Drawing.Size(778, 56);
            this.FiltersBlur.TabIndex = 16;
            this.FiltersBlur.Text = "Blur";
            this.FiltersBlur.UseVisualStyleBackColor = true;
            this.FiltersBlur.Click += new System.EventHandler(this.FiltersBlur_Click);
            // 
            // FiltersBorderDetect
            // 
            this.FiltersBorderDetect.Enabled = false;
            this.FiltersBorderDetect.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBorderDetect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBorderDetect.Location = new System.Drawing.Point(54, 508);
            this.FiltersBorderDetect.Name = "FiltersBorderDetect";
            this.FiltersBorderDetect.Size = new System.Drawing.Size(778, 56);
            this.FiltersBorderDetect.TabIndex = 17;
            this.FiltersBorderDetect.Text = "Border detect";
            this.FiltersBorderDetect.UseVisualStyleBackColor = true;
            this.FiltersBorderDetect.Click += new System.EventHandler(this.FiltersBorderDetect_Click);
            // 
            // FiltersBorderDetectMore
            // 
            this.FiltersBorderDetectMore.Enabled = false;
            this.FiltersBorderDetectMore.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersBorderDetectMore.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersBorderDetectMore.Location = new System.Drawing.Point(54, 570);
            this.FiltersBorderDetectMore.Name = "FiltersBorderDetectMore";
            this.FiltersBorderDetectMore.Size = new System.Drawing.Size(778, 56);
            this.FiltersBorderDetectMore.TabIndex = 18;
            this.FiltersBorderDetectMore.Text = "Border detect more";
            this.FiltersBorderDetectMore.UseVisualStyleBackColor = true;
            this.FiltersBorderDetectMore.Click += new System.EventHandler(this.FiltersBorderDetectMore_Click);
            // 
            // FiltersPush
            // 
            this.FiltersPush.Enabled = false;
            this.FiltersPush.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersPush.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersPush.Location = new System.Drawing.Point(54, 632);
            this.FiltersPush.Name = "FiltersPush";
            this.FiltersPush.Size = new System.Drawing.Size(778, 56);
            this.FiltersPush.TabIndex = 19;
            this.FiltersPush.Text = "Push";
            this.FiltersPush.UseVisualStyleBackColor = true;
            this.FiltersPush.Click += new System.EventHandler(this.FiltersPush_Click);
            // 
            // FiltersSharpeness
            // 
            this.FiltersSharpeness.Enabled = false;
            this.FiltersSharpeness.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiltersSharpeness.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.FiltersSharpeness.Location = new System.Drawing.Point(54, 694);
            this.FiltersSharpeness.Name = "FiltersSharpeness";
            this.FiltersSharpeness.Size = new System.Drawing.Size(778, 56);
            this.FiltersSharpeness.TabIndex = 20;
            this.FiltersSharpeness.Text = "Sharpeness";
            this.FiltersSharpeness.UseVisualStyleBackColor = true;
            this.FiltersSharpeness.Click += new System.EventHandler(this.FiltersSharpeness_Click);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Font = new System.Drawing.Font("Cicle Gordita", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label1.Location = new System.Drawing.Point(54, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(778, 48);
            this.label1.TabIndex = 22;
            this.label1.Text = "- Camera position -";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label3.Location = new System.Drawing.Point(55, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 48);
            this.label3.TabIndex = 27;
            this.label3.Text = "x";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label4.Location = new System.Drawing.Point(316, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 48);
            this.label4.TabIndex = 28;
            this.label4.Text = "y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label5.Location = new System.Drawing.Point(577, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 48);
            this.label5.TabIndex = 29;
            this.label5.Text = "z";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label8.Location = new System.Drawing.Point(54, 169);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(778, 48);
            this.label8.TabIndex = 30;
            this.label8.Text = "- Camera rotation (in °)-";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PositionX
            // 
            this.PositionX.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionX.Location = new System.Drawing.Point(109, 123);
            this.PositionX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PositionX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.PositionX.Name = "PositionX";
            this.PositionX.Size = new System.Drawing.Size(201, 42);
            this.PositionX.TabIndex = 37;
            this.PositionX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PositionX.ThousandsSeparator = true;
            // 
            // PositionY
            // 
            this.PositionY.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionY.Location = new System.Drawing.Point(370, 123);
            this.PositionY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PositionY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.PositionY.Name = "PositionY";
            this.PositionY.Size = new System.Drawing.Size(201, 42);
            this.PositionY.TabIndex = 38;
            this.PositionY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PositionY.ThousandsSeparator = true;
            // 
            // PositionZ
            // 
            this.PositionZ.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PositionZ.Location = new System.Drawing.Point(631, 123);
            this.PositionZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PositionZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.PositionZ.Name = "PositionZ";
            this.PositionZ.Size = new System.Drawing.Size(201, 42);
            this.PositionZ.TabIndex = 39;
            this.PositionZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PositionZ.ThousandsSeparator = true;
            this.PositionZ.Value = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            // 
            // RotationZ
            // 
            this.RotationZ.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotationZ.Location = new System.Drawing.Point(631, 230);
            this.RotationZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.RotationZ.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.RotationZ.Name = "RotationZ";
            this.RotationZ.Size = new System.Drawing.Size(201, 42);
            this.RotationZ.TabIndex = 45;
            this.RotationZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RotationZ.ThousandsSeparator = true;
            // 
            // RotationY
            // 
            this.RotationY.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotationY.Location = new System.Drawing.Point(370, 230);
            this.RotationY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.RotationY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.RotationY.Name = "RotationY";
            this.RotationY.Size = new System.Drawing.Size(201, 42);
            this.RotationY.TabIndex = 44;
            this.RotationY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RotationY.ThousandsSeparator = true;
            // 
            // RotationX
            // 
            this.RotationX.Font = new System.Drawing.Font("Cicle Gordita", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RotationX.Location = new System.Drawing.Point(109, 230);
            this.RotationX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.RotationX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.RotationX.Name = "RotationX";
            this.RotationX.Size = new System.Drawing.Size(201, 42);
            this.RotationX.TabIndex = 43;
            this.RotationX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RotationX.ThousandsSeparator = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label2.Location = new System.Drawing.Point(577, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 48);
            this.label2.TabIndex = 42;
            this.label2.Text = "z";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label6.Location = new System.Drawing.Point(316, 225);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 48);
            this.label6.TabIndex = 41;
            this.label6.Text = "y";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Cicle Gordita", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.label7.Location = new System.Drawing.Point(55, 225);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 48);
            this.label7.TabIndex = 40;
            this.label7.Text = "x";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // RayTracer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.RotationZ);
            this.Controls.Add(this.RotationY);
            this.Controls.Add(this.RotationX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PositionZ);
            this.Controls.Add(this.PositionY);
            this.Controls.Add(this.PositionX);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
            ((System.ComponentModel.ISupportInitialize)(this.PositionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PositionZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RotationX)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown PositionX;
        private System.Windows.Forms.NumericUpDown PositionY;
        private System.Windows.Forms.NumericUpDown PositionZ;
        private System.Windows.Forms.NumericUpDown RotationZ;
        private System.Windows.Forms.NumericUpDown RotationY;
        private System.Windows.Forms.NumericUpDown RotationX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}

