using System;
using System.IO;

class Solver
{
    void Run() 
    {
        int n = input.ReadInt();
        var a = new int[n][];
        for (int i = 0; i < n; i++)
        {
            a[i] = new int[3];
            for (int j = 0; j < 3; j++) {
                a[i][j] = input.ReadInt();
            }
        }

        var d = new int[n + 1];
        Array.Fill(d, (int)1e9);
        d[0] = 0;
        for (int i = 0; i < n; i++) 
        {
            for (int j = 0; i + j + 1 <= n && j < 3; j++) 
            {
                d[i + j + 1] = Math.Min(d[i + j + 1], d[i] + a[i][j]);
            }
        }

        output.WriteLine(d[n]);
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

