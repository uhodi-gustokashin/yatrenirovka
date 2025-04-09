using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;

class Solver
{
    void Run() 
    {
        var t = input.ReadInt();
        for (int _ = 0; _ < t; _++) 
        {
            var n = input.ReadInt();
            var a = new int[n];
            for (int i = 0; i < n; i++) 
            {
                a[i] = input.ReadInt();
            }
            var z = new List<Pair<int, int>>();
            z.Add(new Pair<int, int>{x = 1, y = a[0]});
            for (int i = 1; i < n; i++) 
            {
                if (z[^1].x == z[^1].y || a[i] < z[^1].x + 1)
                {
                    z.Add(new Pair<int, int>{x = 1, y = a[i]});
                } 
                else
                {
                    z[^1] = z[^1] with { x = z[^1].x + 1, y = Math.Min(a[i], z[^1].y)};
                }
            }
            output.WriteLine(z.Count);
            foreach (var p in z)
            {
                output.Write($"{p.x} ");
            }
            output.WriteLine();
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