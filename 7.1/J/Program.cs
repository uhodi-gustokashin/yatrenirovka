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
        public string name;
        public int x;
    }

    void Run() 
    {
        var n = input.ReadInt();
        var m = input.ReadInt();
        var a = new Item[n];
        for (int i = 0; i < n; i++)
        {
            a[i].name = input.ReadToken();
            a[i].x = input.ReadInt();
        }   
        Array.Sort(a, (x, y) => x.x.CompareTo(y.x));
        var an = new List<string>();
        var ax = 0L;
        var d = new long[n + 1][];
        const int MX = 10000;
        const long BAD = long.MaxValue;
        for (int i = 0; i <= n; i++) 
        {
            d[i] = new long[MX + 1];
            Array.Fill(d[i], BAD);
        }
        d[0][0] = 0;
        for (int i = 0; i < n; i++) 
        {
            var c = 1L;
            if (a[i].x > m)
            {
                c = BAD;
                for (int j = a[i].x - m; j <= MX; j++)
                {
                    if (d[i][j] != BAD)
                    {
                        c = Math.Min(c, d[i][j] + 1);
                    }
                }
            }

            if (c == BAD)
            {
                break;
            }
            an.Add(a[i].name);
            ax += c;
            for (int j = 0; j <= MX; j++)
            {
                if (d[i][j] == BAD)
                {
                    continue;
                }
                d[i + 1][j] = Math.Min(d[i + 1][j], d[i][j]);
                if (j + a[i].x <= MX)
                {
                    d[i + 1][j + a[i].x] = Math.Min(d[i + 1][j + a[i].x], d[i][j] + c);
                }
            }
        }
        output.WriteLine($"{an.Count} {ax}");
        an.Sort();
        foreach (var s in an)
        {
            output.WriteLine(s);
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