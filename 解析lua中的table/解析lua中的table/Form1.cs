using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LuaInterface;
using WeifenLuo.WinFormsUI.Docking;

namespace 解析lua中的table
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ListView listView1 = new ListView();
        DockPanel dockPanel = new DockPanel();
        Sunisoft.IrisSkin.SkinEngine skin;
        private void Form1_Load(object sender, EventArgs e)
        {
            #region~显示窗体
            

            //this.Controls.Add(dockPanel);
            //dockPanel.Dock = DockStyle.Fill;
            //dockPanel.Size = new Size(this.Width - 150, this.Height);
            this.dockPanel1.Size = new Size(this.Width - 140, this.Height);
            this.panel1.Size = new Size(150, this.Height);
            

            DockContent f = new DockContent();
            f.Text = "UUT";

            f.CloseButtonVisible = false;
            f.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            
            //f.IsMdiContainer = true;

            
            f.Size = dockPanel1.Size;
            f.Show(dockPanel1);

            f.Controls.Add(listView1);
            listView1.Dock = DockStyle.Fill;
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            //listView1.Size = dockPanel.Size;
            
            #endregion
            lua = new Lua();
            lua.DoFile("test.lua");
            ShowItem(lua);
            ColumnHeader[] columheaders = new ColumnHeader[11];
            string[] headnames=new string[]{"Index","Item_name","lower","upper","UUT1","UUT2","UUT3","UUT4","UUT5","UUT6","Remark"};
            for (int i = 0; i < columheaders.Length; i++)
            {
                columheaders[i] = new ColumnHeader();
                columheaders[i].Text = headnames[i];
            }
            this.listView1.Columns.AddRange(columheaders);
            ListViewItem[] listviewitems = new ListViewItem[names.Count];
            for (int i = 0; i < listviewitems.Length; i++)
            {
                listviewitems[i] = new ListViewItem();
                listviewitems[i].Text =i.ToString("d2");
                listviewitems[i].SubItems.AddRange(new string[] { names.ToArray()[i], lower.ToArray()[i], upper.ToArray()[i] });
            }
            this.listView1.Items.AddRange(listviewitems);
           
        }
        Lua lua;
        List<string> names = new List<string>();
        List<string> lower = new List<string>();
        List<string> upper = new List<string>();
        public void ShowItem(Lua lua)
        {
            
            LuaTable luatable = lua["items"] as LuaTable;
            for (int i = 1; i <= luatable.Values.Count; i++)
            {
                LuaTable luatable1 = luatable[i] as LuaTable;
                LuaTable luatable2 = luatable1["sub"] as LuaTable;
                for (int j = 1; j <= luatable2.Values.Count; j++)
                {
                    
                    LuaTable luatable3 = luatable2[j] as LuaTable;
                   
                    if (luatable3["visible"].ToString() == "0" || luatable3["visible"].ToString()== null)
                        continue;
                    names.Add(luatable3["name"].ToString());
                    object _lower = luatable3["lower"];
                    object _upper = luatable3["upper"];
                    if (_lower == null)
                        _lower = "NA";
                    if (_upper == null)
                        _upper = "NA";
                    upper.Add(_upper.ToString());
                    lower.Add(_lower.ToString());

                    //lower.Add((luatable3["lower"] as object).ToString());
                    //upper.Add((luatable3["upper"] as object).ToString());
                }
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.dockPanel1.Size = new Size(this.Width - 140, this.Height);
            this.panel1.Size = new Size(150, this.Height);

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F5)
                return;
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "请选择皮肤文件";
            op.Filter = "SSK文件|*.ssk";
            if (op.ShowDialog() != DialogResult.OK)
                return;
            skin = new Sunisoft.IrisSkin.SkinEngine(this,op.FileName);
            skin.AddForm(this);
            this.Refresh();
        }
    }
}
