using System;
using System.IO;
using System.Collections.Generic;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var p = new SortedDictionary<string, string>();
        var d = new SortedDictionary<string, int>();
        var g = new SortedDictionary<string, List<string>>();
        for (int i = 0; i < n - 1; i++)
        {
          var x = input.ReadToken();
          var y = input.ReadToken();
          
          p[x] = y;

          if (!g.ContainsKey(y))
          {
            g[y] = [];
          }
          g[y].Add(x);

          d[x] = 0;
          d[y] = 0;
        }
        string root = "";
        foreach (string s in d.Keys)
        {
          if (!p.ContainsKey(s))
          {
            root = s;
          }
        }

        Action<string, int> dfs = null;
        dfs = (string v, int cd) => 
        {
          d[v] = cd;
          if (!g.ContainsKey(v))
          {
            return;
          }
          foreach (string to in g[v])
          {
            dfs(to, cd + 1);
          }
        };

        dfs(root, 0);
        foreach (var (x, y) in d)
        {
          output.WriteLine($"{x} {y}");
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

  public string ReadToken()
  {
    var length = PrepareToken();
    return new string(buffer.AsSpan(0, length));
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

