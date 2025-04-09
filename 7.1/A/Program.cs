using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var m = input.ReadInt();
        var a = new Pair<int, int>[n];
        for (int i = 0; i < n; i++)
        {
            a[i] = new Pair<int, int> {
                x = input.ReadInt(),
                y = i
            };
        }
        Array.Sort(a);
        var b = new Pair<int, int>[m];
        for (int i = 0; i < m; i++)
        {
            b[i] = new Pair<int, int> {
                x = input.ReadInt(),
                y = i
            };
        }
        Array.Sort(b);
        var ans = new int[n];
        int j = 0;
        int cc = 0;
        for (int i = 0; i < a.Length; i++)
        {
            while (j < b.Length && a[i].x >= b[j].x)
            {
                j++;
            }
            if (j < b.Length)
            {
                cc++;
                ans[a[i].y] = b[j].y + 1;
                j++;
            }
        }
        output.WriteLine(cc);
        for (int i = 0; i < n; i++)
        {
            output.Write($"{ans[i]} ");
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
    public X x;
    public Y y;

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