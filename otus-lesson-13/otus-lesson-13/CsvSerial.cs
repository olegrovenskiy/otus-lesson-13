using System;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Text;
using System.Runtime.ConstrainedExecution;

class CsvSerial<T>

{

    public T dd { get; set; }
    public string Path { get; set; }

    public CsvSerial (T fff, string path)
    {
        dd = fff;
        Path = path;
        
    }

    public async Task CsvSerializationAsync()
    {

    //    string path = @"C:\Users\o.rovenskiy\source\repos\otus-lesson-13\otus-lesson-13\otus-lesson-13\ser.csv";
        Type myType = typeof(T);

        string ser = "";
        int j = 0;


        foreach (var u in myType.GetProperties())
        {
            if (j != 0)
                ser = ser + ",";
            j++;
            //Console.WriteLine(u.Name);
            var ff = myType.GetProperty(u.Name);
            var ff1 = ff?.GetValue(dd);
           // Console.WriteLine(ff1);
            ser = ser + u.Name + "=" + ff1;
        }

       // Console.WriteLine(ser);

        using (FileStream fstream = new FileStream(Path, FileMode.OpenOrCreate))
        {
            // ����������� ������ � �����
            byte[] buffer = Encoding.Default.GetBytes(ser);
            // ������ ������� ������ � ����
            await fstream.WriteAsync(buffer, 0, buffer.Length);
           // Console.WriteLine("����� ������� � ����");
        }



    }

    public async Task CsvDeserializationAsync()
    {
        Type myType = typeof(T);

        using (FileStream fstream = File.OpenRead(Path))
        {
            // �������� ������ ��� ���������� ������ �� �����
            byte[] buffer = new byte[fstream.Length];
            // ��������� ������
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            // ���������� ����� � ������
            string textFromFile = Encoding.Default.GetString(buffer);
           // Console.WriteLine($"����� �� ����� csv: {textFromFile}");

            string[] words = textFromFile.Split(new char[] { ',' });
            int j = 0;

            foreach (var u in myType.GetProperties())
            {

                string[] value = words[j].Split(new char[] { '=' });

                var ff = myType.GetProperty(u.Name);
                ff?.SetValue(dd, int.Parse(value[1]));

                 j++;
            }





        }





    }

}













