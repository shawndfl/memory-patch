using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MemoryPatch
{
    public partial class CustomTreeview : TreeView
    {
        private Button _button;
        public CustomTreeview()
        {
            InitializeComponent();
            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;            

            TreeNode root = Nodes.Add("Test");
            TreeNode child = root.Nodes.Add("Child1");
            TreeNode grandChild = child.Nodes.Add("Child2");
            ExpandAll();


            _button = new Button();
            _button.Text = "test button";
            _button.Click += new EventHandler(b_Click);            
            //b.DrawToBitmap(_buttonImage, new Rectangle(0,0,b.Width, b.Height));
        }

        void b_Click(object sender, EventArgs e)
        {
            
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            
            e.Node.Nodes.Add("New Node");            
            ExpandAll();
        }

        protected override void OnResize(EventArgs e)
        {            
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }       

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            base.OnDrawNode(e);            
            
            Font f = new Font(FontFamily.GenericSerif, 12); 
            Pen pen = new Pen(Color.Black);
            PointF p  = new PointF(e.Bounds.Left, e.Bounds.Top);
            e.Graphics.DrawString(e.Node.Text, f, pen.Brush, p );            
        }
    }
}
