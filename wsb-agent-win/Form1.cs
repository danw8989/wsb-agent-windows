using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wsb_agent_win
{
    public partial class Form1 : Form
    {
        public VendingMachine machine;

        public Form1()
        {
            machine = ProductGenerator.Generate();
            
            InitializeComponent();
            progressBar1.Maximum = machine.SlotDepthness;
            progressBar1.Value = progressBar1.Maximum;
            Text = "Automat na ulicy " + machine.Adress + " Kod:[" + machine.Code + "]";
            foreach ( var item in machine.Slots )
            {
                comboBox1.Items.Add(item.Name);
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tmp = machine.BuyProduct(comboBox1.SelectedIndex);
            if ( tmp != "" )
            {
                listBox1.Items.Add(tmp);
                if (progressBar1.Value > 0)
                    progressBar1.Value--;
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            progressBar1.Value = machine.Slots[comboBox1.SelectedIndex].Count;
        }
    }
}
