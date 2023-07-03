// See https://aka.ms/new-console-template for more information
using System;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;

Console.WriteLine("Hello, World!");

string path = @"C:\Users\o.rovenskiy\source\repos\otus-lesson-13\otus-lesson-13\otus-lesson-13\ser.csv";
var dd = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };


Stopwatch stopwatch = new Stopwatch();
//засекаем время начала операции
stopwatch.Start();

for (int i = 0; i < 10000; i++)

{
    CsvSerial<F> test = new CsvSerial<F>(dd, path);
    await test.CsvSerializationAsync();
}


stopwatch.Stop();
//смотрим сколько миллисекунд было затрачено на выполнение
Console.WriteLine("Time   " + stopwatch.ElapsedMilliseconds + "msec");



stopwatch.Reset();
stopwatch.Start();
using (FileStream fstream = File.OpenRead(path))
{
    // выделяем массив для считывания данных из файла
    byte[] buffer = new byte[fstream.Length];
    // считываем данные
    await fstream.ReadAsync(buffer, 0, buffer.Length);
    // декодируем байты в строку
    string textFromFile = Encoding.Default.GetString(buffer);
    Console.WriteLine($"Текст из файла: {textFromFile}");
}
stopwatch.Stop();
//смотрим сколько миллисекунд было затрачено на выполнение
Console.WriteLine("Time   " + stopwatch.ElapsedMilliseconds + "msec");

Console.ReadKey();
