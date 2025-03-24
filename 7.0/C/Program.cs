using System;
using System.IO;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var a = new int[n];
        for (int i = 0; i < n; i++) 
        {
            a[i] = input.ReadInt();
        }

        Array.Sort(a);

        var d = new int[n + 1][];
        for (int i = 0; i <= n; i++) 
        {
            d[i] = new int[2];
        }
        d[1][0] = d[1][1] = a[1] - a[0];
        for (int i = 2; i < n; i++) 
        {
            d[i][0] = Math.Min(d[i - 1][0], d[i - 1][1]) + a[i] - a[i - 1];
            d[i][1] = d[i - 1][0];
        }

        output.WriteLine(d[n - 1][0]);
    }    

    static void Main(string[] args)
    {
       new Solver().Run();
       output.Flush();
    }

    static Scanner input = new();
    static StreamWriter output = new(Console.OpenStandardOutput(), bufferSize: 32768);

}

class Scanner
{
  StreamReader input = new(
      Console.OpenStandardInput(),
      bufferSize: 32768);
  char[] buffer = new char[32768];

  public int ReadInt()
  {
    var length = PrepareToken();
    return int.Parse(buffer.AsSpan(0, length));
  }

  private int PrepareToken()
  {
    int length = 0;
    bool readStart = false;
    while (true)
    {
      int ch = input.Read();
      if (ch == -1)
        break;

      if (char.IsWhiteSpace((char)ch))
      {
        if (readStart) break;
        continue;
      }

      readStart = true;
      buffer[length++] = (char)ch;
    }

    return length;
  }
}

