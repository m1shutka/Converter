using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Converter
{
    public partial class Form1 : Form
    {
        //Объект класса Управление.
        Control_ ctl = new Control_();

        public Form1()
        {
            InitializeComponent();
        }

        //Обработчик события нажатия командной кнопки.
        private void button_Click(object sender, EventArgs e)
        {
            //ссылка на компонент, на котором кликнули мышью
            System.Windows.Forms.Button but = (System.Windows.Forms.Button)sender;
            //номер выбранной команды
            int j = Convert.ToInt16(but.Tag.ToString());
            DoCmnd(j);
        }

        //Выполнить команду.
        private void DoCmnd(int j)
        {
            if (j == 19) { textBox2.Text = ctl.DoCmnd(j); }
            else
            {
                if (ctl.St == Control_.State.Преобразовано)
                { 
                    textBox1.Text = ctl.DoCmnd(18);
                }
                textBox1.Text = ctl.DoCmnd(j);
                textBox2.Text = "";
            }
        }

        //Обновляет состояние командных кнопок по основанию с.сч.исходного числа.
        private void UpdateButtons()
        {
            //просмотреть все компоненты формы
            foreach (Control i in this.Controls)
            {
                if (i is System.Windows.Forms.Button)
                {
                    int j = Convert.ToInt16(i.Tag.ToString());
                    if (j < trackBar1.Value)
                    {
                        i.Enabled = true;
                    }
                    if ((j >= trackBar1.Value) && (j <= 15))
                    {
                        i.Enabled = false;
                    }
                }
            }
        }

        //Изменяет значение основания с.сч. исходного числа.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
            this.UpdateP1();
        }

        //Изменяет значение основания с.сч. исходного числа.
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = Convert.ToByte(numericUpDown1.Value);
            this.UpdateP1();
        }

        //Выполняет необходимые обновления при смене ос. с.сч.р1.
        private void UpdateP1()
        {
            label3.Text = "" + trackBar1.Value;
            ctl.Pin = trackBar1.Value;
            this.UpdateButtons();
            textBox1.Text = ctl.DoCmnd(18);
            textBox2.Text = "";
        }

        //Изменяет значение основания с.сч. результата.
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            numericUpDown2.Value = trackBar2.Value;
            this.UpdateP2();
        }

        //Изменяет значение основания с.сч. результата.
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trackBar2.Value = Convert.ToByte(numericUpDown2.Value);
            this.UpdateP2();
        }

        //Выполняет необходимые обновления при смене ос. с.сч.р2.
        private void UpdateP2()
        {
            ctl.Pout = trackBar2.Value;
            textBox2.Text = ctl.DoCmnd(19);
            label4.Text = "" + trackBar2.Value;
        }

        //Пункт меню Выход.
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Пункт меню Справка.
        /*private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        //Пункт меню История.
        private void историяToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 history = new Form2();
            history.Show();
            if (ctl.history.Count() == 0)
            {
                history.textBox1.AppendText("История пуста");
                return;
            }
            //Ообразить историю.
            for (int i = 0; i < ctl.history.Count(); i++)
            {
                history.textBox1.AppendText(ctl.history[i].ToString());
            }
        }*/

        //Обработка алфавитно-цифровых клавиш.
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int i = -1;
            if (e.KeyChar >= 'A' && e.KeyChar <= 'F') i = (int)e.KeyChar - 'A' + 10;
            if (e.KeyChar >= 'a' && e.KeyChar <= 'f') i = (int)e.KeyChar - 'a' + 10;
            if (e.KeyChar >= '0' && e.KeyChar <= '9') i = (int)e.KeyChar - '0';
            if (e.KeyChar == '.') i = 16;
            if ((int)e.KeyChar == 8) i = 17;
            if ((int)e.KeyChar == 13) i = 19;
            if ((i < ctl.Pin) || (i >= 16)) DoCmnd(i);
        }

        //Обработка клавиш управления.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) DoCmnd(18);
            if (e.KeyCode == Keys.Execute) DoCmnd(19);
            if (e.KeyCode == Keys.Decimal) DoCmnd(16);
        }
    }
}
