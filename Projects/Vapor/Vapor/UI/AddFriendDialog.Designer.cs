﻿namespace Vapor
{
    partial class AddFriendDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFriendDialog));
            this.txtFriend = new Vapor.VaporTextBox();
            this.vaporLabel1 = new Vapor.VaporLabel();
            this.btnOK = new Vapor.VaporButton();
            this.btnCancel = new Vapor.VaporButton();
            this.SuspendLayout();
            // 
            // txtFriend
            // 
            this.txtFriend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.txtFriend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFriend.ForeColor = System.Drawing.Color.White;
            this.txtFriend.Location = new System.Drawing.Point(12, 43);
            this.txtFriend.Name = "txtFriend";
            this.txtFriend.Size = new System.Drawing.Size(303, 20);
            this.txtFriend.TabIndex = 0;
            // 
            // vaporLabel1
            // 
            this.vaporLabel1.ForeColor = System.Drawing.Color.White;
            this.vaporLabel1.Location = new System.Drawing.Point(12, 9);
            this.vaporLabel1.Name = "vaporLabel1";
            this.vaporLabel1.Size = new System.Drawing.Size(303, 31);
            this.vaporLabel1.TabIndex = 1;
            this.vaporLabel1.Text = "Please enter the account name or SteamID of the friend you wish to add";
            this.vaporLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(240, 69);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(58)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(12, 69);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // AddFriendDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 104);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.vaporLabel1);
            this.Controls.Add(this.txtFriend);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            this.MaximizeBox = false;
            this.Name = "AddFriendDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Friend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VaporTextBox txtFriend;
        private VaporLabel vaporLabel1;
        private VaporButton btnOK;
        private VaporButton btnCancel;
    }
}