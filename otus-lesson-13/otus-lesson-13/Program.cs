// See https://aka.ms/new-console-template for more information
using System;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;

Console.WriteLine("Hello, World!");


var dd = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };

CsvSerial<F> test = new CsvSerial<F>(dd);

await test.CsvSerializationAsync();



Console.ReadKey();
