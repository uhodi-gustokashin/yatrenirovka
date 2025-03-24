using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration.Assemblies;

class Solver
{
    class Tree
    {
        public Tree? l = null, r = null;
        public int x;
    }

    Tree add(Tree? t, int x)
    {
        if (t == null)
        {
            var z = new Tree();
            z.x = x;
            return z;
        }
        if (x < t.x)
        {
            t.l = add(t.l, x);
        }
        else
        {
            t.r = add(t.r, x);
        }
        return t;
    }

    bool contains(Tree? t, int x)
    {
        if (t == null)
        {
            return false;
        }
        if (t.x == x)
        {
            return true;
        }
        if (t.x > x)
        {
            return contains(t.l, x);
        }
        return contains(t.r, x);
    }

    void print(Tree? t, int d)
    {
        if (t == null)
        {
            return;
        }
        print(t.l, d + 1);
        for (int i = 0; i < d; i++) {
            output.Write('.');
        }
        output.WriteLine(t.x);
        print(t.r, d + 1);
    }

    void Run() 
    {
        Tree? t = null;
        while (true)
        {
            var x = input.ReadToken();
            if (x.Length == 0)
            {
                break;
            }
            if (x == "ADD")
            {
                var y = input.ReadInt();
                if (contains(t, y))
                {
                    output.WriteLine("ALREADY");
                }
                else
                {
                    t = add(t, y);
                    output.WriteLine("DONE");
                }
            }
            else if (x == "SEARCH")
            {
                var y = input.ReadInt();
                if (contains(t, y))
                {
                    output.WriteLine("YES");
                }
                else
                {
                    output.WriteLine("NO");
                }
            }
            else
            {
                print(t, 0);
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

