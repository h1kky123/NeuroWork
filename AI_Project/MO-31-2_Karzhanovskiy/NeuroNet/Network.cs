
namespace MO_31_2_Karzhanovskiy.NeuroNet
{
    class Network
    {
        //все слои сети
        private InputLayer input_layer = null;
        private HiddenLayer hidden_layer1 = new HiddenLayer(70, 15, NeuronType.Hidden, nameof(hidden_layer1));
        private HiddenLayer hidden_layer2 = new HiddenLayer(33, 70, NeuronType.Hidden, nameof(hidden_layer2));
        private OutputLayer output_layer = new OutputLayer(10, 33, NeuronType.Output, nameof(output_layer));

        private double[] fact = new double[10]; //массив фактического выхода сети
        private double[] e_error_avr; //среднее значение энергии ошибки эпохи обучения

        //Свойства
        public double[] Fact { get => fact; } //массив фактического выхода сети
        public double[] E_error_avr { get => e_error_avr; set => e_error_avr = value; } 
        //Конструктор
        public Network() { }

        public void ForwardPass (Network net, double[] netInput)
        {
            net.hidden_layer1.Data = netInput;
            net.hidden_layer1.Recognize(null, net.hidden_layer2);
            net.hidden_layer2.Recognize(null, net.output_layer);
            net.output_layer.Recognize(net, null);
        }

        //обучение
        public void Train(Network net)
        {
            net.input_layer = new InputLayer(NetworkMode.Train); //инициализация входного слоя
            int epoches = 15; //количество эпох обучения
            double tmpSumError; //временная переменная ошибок
            double[] errors; //вектор сигнала ошибки входного слоя
            double[] temp_gsum1; //вектор градиента 1-го скрытого слоя
            double[] temp_gsum2; //векто градиента 2-го скрытого слоя

            e_error_avr = new double[epoches];
            for(int k = 0; k < epoches; k++) //перебор эпох обучения
            {
                e_error_avr[k] = 0; //обнуляем массив средней энергии эпохи обучения для каждого прохода
                net.input_layer.Shuffling_Array_Rows(net.input_layer.Trainset); //перетасовка обучающей выборки
                for(int i = 0; i < net.input_layer.Trainset.GetLength(0); i++)
                {
                    double[] tmpTrain = new double[15];
                    for (int j = 0; j < tmpTrain.Length; j++)
                        tmpTrain[j] = net.input_layer.Trainset[i, j + 1];

                    //Прямой проход
                    ForwardPass(net, tmpTrain);

                    //вычисление ошибки по итерации
                    tmpSumError = 0; //для каждого обучающего образа значние ошибки этого образа обнуляем
                    errors = new double[net.fact.Length]; //переопределение массива сигнала ошибки выходного слоя
                    for(int x = 0; x < errors.Length; x++)
                    {
                        if (x == net.input_layer.Trainset[i, 0]) //если номер выходного нейрона совпадает с желаемым результатом
                            errors[x] = 1.0 - net.fact[x];
                        else
                            errors[x] = -net.fact[x]; //errors[x] = 0.0 - net.fact[x]

                        tmpSumError += errors[x] * errors[x] / 2;
                    }
                    e_error_avr[k] = tmpSumError / errors.Length; //суммарное значение энергии ошибки k-го прохода  

                    //обратный проход и коррекция весов
                    temp_gsum2 = net.output_layer.BackwardPass(errors);
                    temp_gsum1 = net.hidden_layer2.BackwardPass(temp_gsum2);
                    net.hidden_layer1.BackwardPass(temp_gsum1);
                }
                e_error_avr[k] /= net.input_layer.Trainset.GetLength(0);
            }
            net.input_layer = null; //обнуление(уборка) входного слоя

            //Запись скорректированных весов в память
            net.hidden_layer1.WeightInitialize(MemoryMode.SET, nameof(hidden_layer1) + "_memory.csv");
            net.hidden_layer2.WeightInitialize(MemoryMode.SET, nameof(hidden_layer2) + "_memory.csv");
            net.output_layer.WeightInitialize(MemoryMode.SET, nameof(output_layer) + "_memory.csv");
        }


    }
}
