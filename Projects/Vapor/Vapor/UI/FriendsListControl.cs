using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Vapor
{
    class FriendsListControl : FlowLayoutPanel
    {

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FriendsListControl
            // 
            this.AutoScroll = true;
            this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ResumeLayout( false );

        }
    }
}
