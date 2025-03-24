using System;
using System.IO;

class Solver
{
    void Run() {
        var n = input.ReadInt();
        var d = new int[n + 1][];
        for (int i = 0; i <= n; i++)
        {
            d[i] = new int[3];
        }

        d[0][0] = 1;
        for (int i = 1; i <= n; i++)
        {
            d[i][0] = d[i - 1][0] + d[i - 1][1] + d[i - 1][2];
            d[i][1] = d[i - 1][0];
            d[i][2] = d[i - 1][1];
        }

        output.WriteLine(d[n][0] + d[n][1] + d[n][2]);
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