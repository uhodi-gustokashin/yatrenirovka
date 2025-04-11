using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.IO.Compression;
using System.Linq;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var a = new List<Pair<int, int>>();
        var ev = new List<Pair<int, int>>();
        for (int i = 0; i < n; i++)
        {
            var s = input.ReadToken();
            var p = new Pair<int, int>();
            for (int j = 0; j < s.Length; j++)
            {
                if (s[j] == 'S')
                {
                    if (j % 2 == 0)
                    {
                        p.x++;
                    }
                    else
                    {
                        p.y++;
                    }
                }
            }
            if (s.Length % 2 == 0)
            {
                ev.Add(p);
            }            
            else
            {
                a.Add(p);
            }
        }

        if (a.Count == 0)
        {
            var s = 0;
            foreach (var p in ev)
            {
                s += p.x;
            }
            output.WriteLine(s);
            return;
        }
        var ans = 0;
        foreach (var p in ev)
        {
            ans += Math.Max(p.x, p.y);
        }
        a.Sort((p1, p2) => (p1.x - p1.y).CompareTo(p2.x - p2.y));
        a.Reverse();
        var l = 0;
        var r = a.Count - 1;
        while (l <= r)
        {
            ans += a[l].x;
            l++;
            if (l <= r)
            {
                ans += a[r].y;
                r--;
            }
        }
        output.WriteLine(ans);
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