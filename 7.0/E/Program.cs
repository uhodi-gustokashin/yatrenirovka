using System;
using System.IO;
using System.Collections.Generic;

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
        var d = new int[n + 1][];
        for (int i = 0; i <= n; i++) {
            d[i] = new int[n + 1];
            Array.Fill(d[i], (int)1e9);
        }
        d[0][0] = 0;
        for (int i = 0; i < n; i++) 
        {
            for (int j = 0; j <= i; j++) 
            {
                int nj = a[i] > 100 ? j + 1 : j;
                d[i + 1][nj] = Math.Min(d[i + 1][nj], d[i][j] + a[i]);
                if (j > 0)
                {
                    d[i + 1][j - 1] = Math.Min(d[i + 1][j - 1], d[i][j]);
                }
            }
        }

        var y = 0;
        for (int i = 1; i <= n; i++) 
        {
            if (d[n][i] <= d[n][y])
            {
                y = i;
            }
        }
        output.WriteLine(d[n][y]);
        var k1 = y;
        var ans = new List<int>();
        for (int i = n - 1; i >= 0; i--) 
        {
            int py = a[i] > 100 ? y - 1 : y;
            if (py >= 0 && d[i][py] + a[i] == d[i + 1][y])
            {
                y = py;
            }
            else
            {
                y++;
                ans.Add(i);
            }
        }
        output.WriteLine($"{k1} {ans.Count}");
        ans.Reverse();
        foreach (int i in ans)
        {
            output.Write(i + 1);
            output.Write(' ');
        }
        output.WriteLine();
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

