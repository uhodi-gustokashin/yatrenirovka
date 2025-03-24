using System;
using System.IO;
using System.Collections.Generic;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var pq = new SortedSet<(int, int)>();
        for (int i = 0; i < n; i++)
        {
            var x = input.ReadInt();
            if (x == 0)
            {
                var y = input.ReadInt();
                pq.Add((y, i));
            }
            else
            {
                var z = pq.Max;
                pq.Remove(z);
                output.WriteLine(z.Item1);
            }
        }
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

