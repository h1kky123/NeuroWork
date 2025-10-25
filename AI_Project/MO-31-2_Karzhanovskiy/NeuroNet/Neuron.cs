using static System.Math;

namespace MO_31_2_Karzhanovskiy.NeuroNet
{
    class Neuron
    {
        //поля
        private NeuronType type; //тип нейрона
        private double[] weights; //его вес
        private double[] inputs; //его входы
        private double output; //выход
        private double derivative; //производная

        //Константы для функции активации

        //Свойства
        public double[] Weights { get => weights; set => weights = value; }
        public double[] Inputs { get => inputs; set => inputs = value; }
        public double Output { get => output; }
        public double Derivative { get => derivative; }
        //Конструктор
        public Neuron(double[] memoryWeights, NeuronType typeNeuron)
        {
            type = typeNeuron;
            weights = memoryWeights;
        }
        public void Activator(double[] i)
        {
            inputs = i; //передача вектора входного сигнала в массив входных данных нейрона
            double sum = weights[0];

            for (int j = 0; j < inputs.Length; j++)
            {
                sum += inputs[j] * weights[j + 1];
            }
            switch (type)
            {
                case NeuronType.Hidden: //Нейрона срытого типа
                    output = logisticFunk(sum);
                    derivative = logisticFunk_Derivativator(sum);
                    break;
                case NeuronType.Output:
                    output = Exp(sum);
                    break;
            }
        }
        //Логистическая функция активации нейрона
        private double logisticFunk(double sum)
        {
            //Защита от переполнения
            if (sum < -700) return 0.0;
            if (sum > 700) return 1.0;

            return 1.0 / (1.0 + Exp(-sum));
        }
        //Производная логистической функции активации нейрона
        private double logisticFunk_Derivativator(double sum)
        {
            double s = logisticFunk(sum);
            return s * (1.0 - s);
        }
    }
}


//Архитектура нейронной сети будет: 15:70:33:10