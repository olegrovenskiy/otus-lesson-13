// See https://aka.ms/new-console-template for more information
using System;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics;
using System.Text.Json;

Console.WriteLine("Hello, World!");

string path = @"C:\Users\o.rovenskiy\source\repos\otus-lesson-13\otus-lesson-13\otus-lesson-13\ser.csv";
string pathJson = @"C:\Users\o.rovenskiy\source\repos\otus-lesson-13\otus-lesson-13\otus-lesson-13\ser.json";


var dd = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };

// Tasks from  1 to 8 

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
Console.WriteLine("Time  for CSV serialization " + stopwatch.ElapsedMilliseconds + "msec");



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
Console.WriteLine("Time for console output  " + stopwatch.ElapsedMilliseconds + "msec");


stopwatch.Reset();
stopwatch.Start();

for (int i = 0; i < 10000; i++)
{
    using (FileStream fs = new FileStream(pathJson, FileMode.OpenOrCreate))
    {

        await JsonSerializer.SerializeAsync<F>(fs, dd);
      //  Console.WriteLine("JSON Data has been saved to file");
    }
}

stopwatch.Stop();
//смотрим сколько миллисекунд было затрачено на выполнение
Console.WriteLine("Time for JSON serialization  " + stopwatch.ElapsedMilliseconds + "msec");



// Tasks from 9 to 10

// Десириализация JSON

stopwatch.Reset();
stopwatch.Start();

for (int i = 0; i < 10000; i++)
{
    using (FileStream fs = new FileStream(pathJson, FileMode.OpenOrCreate))
    {
        F? ffJson = await JsonSerializer.DeserializeAsync<F>(fs);
        // Console.WriteLine($"i1: {ffJson?.i1}  i5: {ffJson?.i5}");
    }
}
stopwatch.Stop();
//смотрим сколько миллисекунд было затрачено на выполнение
Console.WriteLine("Time for JSON deserialization  " + stopwatch.ElapsedMilliseconds + "msec");



// Десириализация CSV

stopwatch.Reset();
stopwatch.Start();
for (int i = 0; i < 10000; i++)
{
    var ddDesir = new F();
    CsvSerial<F> test1 = new CsvSerial<F>(ddDesir, path);
    await test1.CsvDeserializationAsync();

}

stopwatch.Stop();
//смотрим сколько миллисекунд было затрачено на выполнение
Console.WriteLine("Time for CSV deserialization  " + stopwatch.ElapsedMilliseconds + "msec");

//.WriteLine("Desir " + ddDesir.i5);


Console.ReadKey();
