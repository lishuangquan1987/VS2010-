using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LuaInterface;

namespace 解析lua中的table
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
    }
}
