namespace MusicApp
{
    partial class Display
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Display));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.PauseButton = new System.Windows.Forms.Button();
            this.ShuffleButton = new System.Windows.Forms.Button();
            this.DataView = new System.Windows.Forms.DataGridView();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Album = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.musicAppDBDataSet = new MusicApp.MusicAppDBDataSet();
            this.filesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.musicAppDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filesTableAdapter = new MusicApp.MusicAppDBDataSetTableAdapters.FilesTableAdapter();
            this.IndexButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicAppDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicAppDBDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(218, 0);
            this.TitleLabel.MaximumSize = new System.Drawing.Size(1920, 47);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(595, 47);
            this.TitleLabel.TabIndex = 2;
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PlayButton
            // 
            this.PlayButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PlayButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayButton.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.PlayButton.Location = new System.Drawing.Point(-1, -1);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(74, 49);
            this.PlayButton.TabIndex = 5;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // PauseButton
            // 
            this.PauseButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.PauseButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseButton.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.PauseButton.Location = new System.Drawing.Point(70, -1);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(74, 49);
            this.PauseButton.TabIndex = 6;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = false;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ShuffleButton
            // 
            this.ShuffleButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ShuffleButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShuffleButton.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ShuffleButton.Location = new System.Drawing.Point(140, -1);
            this.ShuffleButton.Name = "ShuffleButton";
            this.ShuffleButton.Size = new System.Drawing.Size(74, 49);
            this.ShuffleButton.TabIndex = 7;
            this.ShuffleButton.Text = "Shuffle";
            this.ShuffleButton.UseVisualStyleBackColor = false;
            this.ShuffleButton.Click += new System.EventHandler(this.ShuffleButton_Click);
            // 
            // DataView
            // 
            this.DataView.AllowUserToAddRows = false;
            this.DataView.AllowUserToDeleteRows = false;
            this.DataView.AllowUserToResizeRows = false;
            this.DataView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataView.AutoGenerateColumns = false;
            this.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DataView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataView.ColumnHeadersHeight = 20;
            this.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.titleDataGridViewTextBoxColumn,
            this.artistDataGridViewTextBoxColumn,
            this.Album,
            this.genreDataGridViewTextBoxColumn,
            this.playedDataGridViewTextBoxColumn});
            this.DataView.Cursor = System.Windows.Forms.Cursors.Default;
            this.DataView.DataSource = this.filesBindingSource1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataView.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataView.EnableHeadersVisualStyles = false;
            this.DataView.GridColor = System.Drawing.SystemColors.HotTrack;
            this.DataView.Location = new System.Drawing.Point(2, 48);
            this.DataView.Name = "DataView";
            this.DataView.ReadOnly = true;
            this.DataView.RowHeadersVisible = false;
            this.DataView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DataView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataView.Size = new System.Drawing.Size(893, 387);
            this.DataView.TabIndex = 1;
            this.DataView.VirtualMode = true;
            this.DataView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataView_CellDoubleClick);
            this.DataView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataView_DataError);
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // artistDataGridViewTextBoxColumn
            // 
            this.artistDataGridViewTextBoxColumn.DataPropertyName = "Artist";
            this.artistDataGridViewTextBoxColumn.HeaderText = "Artist";
            this.artistDataGridViewTextBoxColumn.Name = "artistDataGridViewTextBoxColumn";
            this.artistDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Album
            // 
            this.Album.DataPropertyName = "Album";
            this.Album.HeaderText = "Album";
            this.Album.Name = "Album";
            this.Album.ReadOnly = true;
            // 
            // genreDataGridViewTextBoxColumn
            // 
            this.genreDataGridViewTextBoxColumn.DataPropertyName = "Genre";
            this.genreDataGridViewTextBoxColumn.HeaderText = "Genre";
            this.genreDataGridViewTextBoxColumn.Name = "genreDataGridViewTextBoxColumn";
            this.genreDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // playedDataGridViewTextBoxColumn
            // 
            this.playedDataGridViewTextBoxColumn.DataPropertyName = "Played";
            this.playedDataGridViewTextBoxColumn.HeaderText = "Played";
            this.playedDataGridViewTextBoxColumn.Name = "playedDataGridViewTextBoxColumn";
            this.playedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // filesBindingSource1
            // 
            this.filesBindingSource1.DataMember = "Files";
            this.filesBindingSource1.DataSource = this.musicAppDBDataSet;
            // 
            // musicAppDBDataSet
            // 
            this.musicAppDBDataSet.DataSetName = "MusicAppDBDataSet";
            this.musicAppDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // filesTableAdapter
            // 
            this.filesTableAdapter.ClearBeforeFill = true;
            // 
            // IndexButton
            // 
            this.IndexButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.IndexButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.IndexButton.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IndexButton.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.IndexButton.Location = new System.Drawing.Point(819, -1);
            this.IndexButton.Name = "IndexButton";
            this.IndexButton.Size = new System.Drawing.Size(74, 49);
            this.IndexButton.TabIndex = 8;
            this.IndexButton.Text = "Index";
            this.IndexButton.UseVisualStyleBackColor = false;
            this.IndexButton.Click += new System.EventHandler(this.IndexButton_Click);
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(892, 429);
            this.Controls.Add(this.IndexButton);
            this.Controls.Add(this.ShuffleButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.DataView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Display";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PatrickTunes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Display_FormClosed);
            this.Load += new System.EventHandler(this.Display_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicAppDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicAppDBDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel;
        public System.Windows.Forms.Button PlayButton;
        public System.Windows.Forms.Button PauseButton;
        public System.Windows.Forms.Button ShuffleButton;
        public MusicAppDBDataSet musicAppDBDataSet;
        public System.Windows.Forms.DataGridView DataView;
        private System.Windows.Forms.BindingSource filesBindingSource;
        private System.Windows.Forms.BindingSource musicAppDBDataSetBindingSource;
        private MusicAppDBDataSet musicAppDBDataSet1;
        private System.Windows.Forms.BindingSource filesBindingSource1;
        private MusicAppDBDataSetTableAdapters.FilesTableAdapter filesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn skippedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn artistDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Album;
        private System.Windows.Forms.DataGridViewTextBoxColumn genreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn playedDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button IndexButton;
    }
}