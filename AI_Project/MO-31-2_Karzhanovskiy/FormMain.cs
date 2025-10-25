using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MO_31_2_Karzhanovskiy.NeuroNet;


namespace MO_31_2_Karzhanovskiy
{
    public partial class FormMain : Form
    {
        private double[] inputPixels;
        private Network network;
        //Конструктор
        public FormMain()
        {
            InitializeComponent();

            inputPixels = new double[15];
            network = new Network();
        }
        //Обработчик события
        private void Changing_State_Pixel_Button_Click(object sender, EventArgs e)
        {
            if(((Button)sender).BackColor == Color.White)
            {
                ((Button)sender).BackColor = Color.Black;
                inputPixels[((Button)sender).TabIndex] = 1d;
            }
            else
            {
                ((Button)sender).BackColor = Color.White;
                inputPixels[((Button)sender).TabIndex] = 0d;
            }
        }
        //созранить в файл обучающий пример
        private void button_SaveTrainSample_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "train.txt";
            string tmpStr = numericUpDownNecessary.Value.ToString();

            for(int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n"; //Переход на новую строку текста
            File.AppendAllText(path, tmpStr); //Добавление текста в tmpStr
        }

        private void button_SaveTestSample_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "test.txt";
            string tmpStr = numericUpDownNecessary.Value.ToString();

            for (int i = 0; i < inputPixels.Length; i++)
            {
                tmpStr += " " + inputPixels[i].ToString();
            }
            tmpStr += "\n";

            File.AppendAllText(path, tmpStr);
        }

        private void buttonRecognize_Click(object sender, EventArgs e)
        {
            network.ForwardPass(network, inputPixels);
            labelOutput.Text = network.Fact.ToList().IndexOf(network.Fact.Max()).ToString();
            labelProbability.Text = (100 * network.Fact.Max()).ToString("0.00") + "%";
        }

        private void buttonTrain_Click(object sender, EventArgs e)
        {
            network.Train(network);
            MessageBox.Show("Обучение успешно завершено. ", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
