using System.Collections.Generic;

namespace A5
{
    class Node
    {
        public string sub;                     // a substring of the input string
        public List<int> ch = new List<int>(); // vector of child nodes

        public Node()
        {
            sub = "";
        }

        public Node(string sub, params int[] children)
        {
            this.sub = sub;
            ch.AddRange(children);
        }
    }
}
