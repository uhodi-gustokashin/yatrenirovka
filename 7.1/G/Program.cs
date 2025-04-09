using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.IO.Compression;
using System.Linq;
using System.Buffers;
using System.Runtime.Versioning;

class Solver
{
    void Run() 
    {
        var n = input.ReadInt();
        var k = input.ReadInt();
        var lens = new List<Pair<int, int>>[k];
        for (int i = 0; i < k; i++)
        {
            lens[i] = [];
        }
        for (int i = 0; i < n; i++) 
        {
            var x = input.ReadInt();
            var y = input.ReadInt();
            lens[y - 1].Add(new Pair<int, int>{x = x, y = i});
        }

        var s = 0;
        for (int i = 0; i < lens[0].Count; i++)
        {
            s += lens[0][i].x;
        }
        for (int i = 1; i < k; i++) 
        {
            var os = 0;
            for (int j = 0; j < lens[i].Count; j++)
            {
                os += lens[i][j].x;
            }
            if (os != s)
            {
                throw new Exception();
            }
        }
        var can = new int[k][];
        for (int i = 0; i < k; i++)
        {
            can[i] = new int[s + 1];
            Array.Fill(can[i], -1);
            can[i][0] = 0;
            for (int j = 0; j < lens[i].Count; j++)
            {  
                for (int z = s; z >= lens[i][j].x; z--)
                {
                    if (can[i][z] == -1 && can[i][z - lens[i][j].x] != -1)
                    {
                        can[i][z] = j;
                    }
                }
            }
        }
        var al = -1;
        for (int i = 1; i < s; i++)
        {
            bool ok = true;
            for (int j = 0; j < k; j++)
            {
                if (can[j][i] == -1)
                {
                    ok = false;
                    break;
                }
            }
            if (ok)
            {
                al = i;
                break;
            }
        }
        if (al == -1)
        {
            output.WriteLine("NO");
            return;
        }

        output.WriteLine("YES");
        for (int i = 0; i < k; i++)
        {
            var x = al;
            while (x > 0)
            {
                output.WriteLine(lens[i][can[i][x]].y + 1);
                x -= lens[i][can[i][x]].x;
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