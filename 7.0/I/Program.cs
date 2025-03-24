using System;
using System.IO;
using System.Collections.Generic;

class Solver
{

    void Run() 
    {
        var n = input.ReadInt();
        var g = new List<int>[n];
        for (int i = 0; i < n; i++) 
        {
            g[i] = new();
        }

        for (int i = 0; i < n - 1; i++) 
        {
            var x = input.ReadInt() - 1;
            var y = input.ReadInt() - 1;
            g[x].Add(y);
            g[y].Add(x);
        }

        Action<int, int> dfs = null;
        var sz = new int[n];
        dfs = (int v, int p) =>
        {
            sz[v] = 1;
            foreach (int to in g[v])
            {
                if (to != p)
                {
                    dfs(to, v);
                    sz[v] += sz[to];
                }
            }
        };

        dfs(0, -1);
        foreach (int i in sz)
        {
            output.Write($"{i} ");
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
        //   new FileStream("input.txt", FileMode.Open),
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

