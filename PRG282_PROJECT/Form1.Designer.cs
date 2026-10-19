namespace PRG282_PROJECT
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
            this.dgvAnimals = new System.Windows.Forms.DataGridView();
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSpecies = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtScore = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSpecies = new System.Windows.Forms.Label();
            this.labelAge = new System.Windows.Forms.Label();
            this.labelRecoveryScore = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSummary = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAnimals
            // 
            this.dgvAnimals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimals.Location = new System.Drawing.Point(449, 71);
            this.dgvAnimals.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAnimals.Name = "dgvAnimals";
            this.dgvAnimals.ReadOnly = true;
            this.dgvAnimals.RowHeadersWidth = 51;
            this.dgvAnimals.Size = new System.Drawing.Size(552, 325);
            this.dgvAnimals.TabIndex = 0;
            this.dgvAnimals.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAnimals_CellClick);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(16, 223);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(0, 16);
            this.lblID.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(203, 71);
            this.txtID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(132, 22);
            this.txtID.TabIndex = 6;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(203, 111);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(132, 22);
            this.txtName.TabIndex = 7;
            // 
            // txtSpecies
            // 
            this.txtSpecies.Location = new System.Drawing.Point(203, 158);
            this.txtSpecies.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSpecies.Name = "txtSpecies";
            this.txtSpecies.Size = new System.Drawing.Size(132, 22);
            this.txtSpecies.TabIndex = 8;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(203, 209);
            this.txtAge.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(132, 22);
            this.txtAge.TabIndex = 9;
            // 
            // txtScore
            // 
            this.txtScore.Location = new System.Drawing.Point(203, 270);
            this.txtScore.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtScore.Name = "txtScore";
            this.txtScore.Size = new System.Drawing.Size(132, 22);
            this.txtScore.TabIndex = 10;
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(44, 71);
            this.labelID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(64, 16);
            this.labelID.TabIndex = 11;
            this.labelID.Text = "Animal ID";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(44, 111);
            this.labelName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(44, 16);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Name";
            // 
            // labelSpecies
            // 
            this.labelSpecies.AutoSize = true;
            this.labelSpecies.Location = new System.Drawing.Point(44, 161);
            this.labelSpecies.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSpecies.Name = "labelSpecies";
            this.labelSpecies.Size = new System.Drawing.Size(57, 16);
            this.labelSpecies.TabIndex = 13;
            this.labelSpecies.Text = "Species";
            // 
            // labelAge
            // 
            this.labelAge.AutoSize = true;
            this.labelAge.Location = new System.Drawing.Point(44, 209);
            this.labelAge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAge.Name = "labelAge";
            this.labelAge.Size = new System.Drawing.Size(32, 16);
            this.labelAge.TabIndex = 14;
            this.labelAge.Text = "Age";
            // 
            // labelRecoveryScore
            // 
            this.labelRecoveryScore.AutoSize = true;
            this.labelRecoveryScore.Location = new System.Drawing.Point(44, 270);
            this.labelRecoveryScore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRecoveryScore.Name = "labelRecoveryScore";
            this.labelRecoveryScore.Size = new System.Drawing.Size(105, 16);
            this.labelRecoveryScore.TabIndex = 15;
            this.labelRecoveryScore.Text = "Recovery Score";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(72, 448);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 28);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Add ";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(328, 448);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save changes";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(545, 448);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 28);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSummary
            // 
            this.btnSummary.Location = new System.Drawing.Point(901, 448);
            this.btnSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSummary.Name = "btnSummary";
            this.btnSummary.Size = new System.Drawing.Size(100, 28);
            this.btnSummary.TabIndex = 19;
            this.btnSummary.Text = "Summary";
            this.btnSummary.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(720, 448);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(235, 367);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(37, 369);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(132, 22);
            this.txtSearch.TabIndex = 22;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(33, 350);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(87, 16);
            this.lblSearch.TabIndex = 23;
            this.lblSearch.Text = "Search by ID:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSummary);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.labelRecoveryScore);
            this.Controls.Add(this.labelAge);
            this.Controls.Add(this.labelSpecies);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.txtScore);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtSpecies);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.dgvAnimals);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAnimals;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSpecies;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtScore;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSpecies;
        private System.Windows.Forms.Label labelAge;
        private System.Windows.Forms.Label labelRecoveryScore;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSummary;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}

