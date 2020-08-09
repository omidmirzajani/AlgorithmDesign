using System;
using System.Collections.Generic;

namespace A8
{
    class Program
    {
        static void Main(string[] args)
        {
            MyGraph g = new MyGraph(5);
            g.addEdge(0, 1, 1);
            g.addEdge(0, 2, 1);
            g.addEdge(0, 3, 1);
            g.addEdge(3, 4, 1);

            foreach (var item in g.findPathDFS(0, 4,new List<long>()))
            {
                Console.WriteLine(item);
            }
        }
    }
}
