using System;
using System.IO;

namespace MO_31_2_Karzhanovskiy.NeuroNet
{
    class InputLayer
    {
        //поля
        private double[,] trainset; //100 изображений в обучающей выборке
        private double[,] testset; //10 изображений в тестовой выборке

        //свойства
        public double[,] Trainset { get => trainset; }
        public double[,] Testset { get => testset; }

        //Конструктор
        public InputLayer(NetworkMode nm)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string[] tmpArrStr; //временный массив строк
            string[] tmpStr; //временный массив элементов в строке

            switch (nm)
            {
                case NetworkMode.Train:
                    tmpArrStr = File.ReadAllLines(path + "train.txt"); //считываем все строки из файла
                    trainset = new double[tmpArrStr.Length, 16]; //определяем массив обучающей выборки
                    for (int i = 0; i < tmpArrStr.Length; i++) //цикл перебора строк обучающей выборки
                    {
                        tmpStr = tmpArrStr[i].Split(' '); //разбиение i-ой строки на массив отдельных цифр
                        for (int j = 0; j < 16; j++) //цикл заполнения i-ой строки обучающей выборки
                        {
                            trainset[i, j] = double.Parse(tmpStr[j]); //строковое значение числа преобразуется с двойной точностью
                        }
                    }
                    Shuffling_Array_Rows(trainset); //перетасовка обучающей выборки
                    break;

                case NetworkMode.Test:
                    tmpArrStr = File.ReadAllLines(path + "test.txt"); //считываем все строки из файла
                    testset = new double[tmpArrStr.Length, 16]; //определяем массив тестовой выборки
                    for (int i = 0; i < tmpArrStr.Length; i++) //цикл перебора строк тестовой выборки
                    {
                        tmpStr = tmpArrStr[i].Split(' '); //разбиение i-ой строки на массив отдельных цифр
                        for (int j = 0; j < 16; j++) //цикл заполнения i-ой строки тестовой выборки
                        {
                            testset[i, j] = double.Parse(tmpStr[j]); //строковое значение числа преобразуется с двойной точностью
                        }
                    }
                    Shuffling_Array_Rows(testset); //перетасовка тестовой выборки
                    break;
            }
        }
        public void Shuffling_Array_Rows(double[,] arr)
        {
            //написать дома
        }
        //метод Фишера-Йетса
    }
}
