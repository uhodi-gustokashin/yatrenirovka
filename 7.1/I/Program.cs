using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.IO.Compression;
using System.Linq;

class Solver
{
    struct Item
    {
        public int v, c, p, id;
    }

    void Run() 
    {
        var n = input.ReadInt();
        var s = input.ReadInt();
        var a = new Item[n];
        var ss = 0;
        for (int i = 0; i < n; i++)
        {
            a[i].v = input.ReadInt();
            a[i].c = input.ReadInt();
            a[i].p = input.ReadInt();
            a[i].id = i;
            ss += a[i].v;
        }
        Array.Sort(a, (x, y) => y.p.CompareTo(x.p));
        var d = new int[n + 1][];
        for (int i = 0; i < n + 1; i++)
        {
            d[i] = new int[ss + 1];
            Array.Fill(d[i], -1);
        }
        d[0][0] = 0;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j <= ss; j++) {
                if (d[i][j] == -1)
                {
                    continue;
                }
                d[i + 1][j] = Math.Max(d[i + 1][j], d[i][j]);
                if (a[i].v + j <= ss && a[i].v + j - s <= a[i].p)
                {
                    d[i + 1][j + a[i].v] = Math.Max(d[i + 1][j + a[i].v], d[i][j] + a[i].c);
                }
            }
        }

        var mi = 0;
        var mj = 0;
        for (int i = 0; i <= n; i++)
        {
            for (int j = 0; j <= ss; j++)
            {
                if (d[i][j] > d[mi][mj])
                {
                    mi = i;
                    mj = j;
                }
            }
        }
        var mm = d[mi][mj];

        var ans = new List<int>();
        while (mi > 0)
        {
            if (d[mi][mj] != d[mi - 1][mj])
            {
                ans.Add(a[mi - 1].id);
                mj -= a[mi - 1].v;
            }
            mi--;
        }        
        output.WriteLine($"{ans.Count} {mm}");
        ans.Sort();
        foreach (var i in ans)
        {
            output.Write($"{i + 1} ");
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

struct Pair<X, Y> : IComparable<Pair<X, Y>>
where X: IComparable<X>
where Y: IComparable<Y>
{
    public X x { get; set; }
    public Y y { get; set; }

    public int CompareTo(Pair<X, Y> other)
    {
        int cx = this.x.CompareTo(other.x);
        if (cx != 0)
        {
            return cx;
        }
        return this.y.CompareTo(other.y);
    }
}