using System;
using System.IO;
using System.Collections.Generic;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var m = input.ReadInt();
        var a = new int[n][];
        var d = new int[n][];
        for (int i = 0; i < n; i++) 
        {
            a[i] = new int[m];
            d[i] = new int[m];

            for (int j = 0; j < m; j++)
            {
                a[i][j] = input.ReadInt();
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++) 
            {
                if (i > 0 && d[i - 1][j] > d[i][j]) 
                {
                    d[i][j] = d[i - 1][j];
                }
                if (j > 0 && d[i][j - 1] > d[i][j])
                {
                    d[i][j] = d[i][j - 1];
                }
                d[i][j] += a[i][j];
            }
        }

        output.WriteLine(d[n - 1][m - 1]);
        var x = n - 1;
        var y = m - 1;

        var ans = new List<char>();
        while (x != 0 || y != 0) 
        {
            if (x == 0 || (y > 0 && d[x - 1][y] < d[x][y - 1]))
            {
                ans.Add('R');
                y--;
            }
            else
            {
                ans.Add('D');
                x--;
            }
        }
        ans.Reverse();
        foreach (char c in ans) 
        {
            output.Write(c);
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

