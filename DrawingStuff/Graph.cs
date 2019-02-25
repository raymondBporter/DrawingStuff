using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace DrawingStuff
{

    public class Node<NodeData, EdgeData> where EdgeData : class
    {
        public Node(NodeData data) => Data = data;
        public NodeData Data;
        public List<Edge<NodeData, EdgeData>> Neighbors = new List<Edge<NodeData, EdgeData>>();
        public int Degree => Neighbors.Count;

        public bool Visited = false;
        public int Grouping = -1;
        public int Color = -1;
    }

    public struct Edge<NodeData, EdgeData> where EdgeData : class
    {
        public Edge(Node<NodeData, EdgeData> a, Node<NodeData, EdgeData> b, EdgeData data)
        {
            Node0 = a;
            Node1 = b;
            Data = data;
            Visited = false;
        }

        public Edge(Node<NodeData, EdgeData> a, Node<NodeData, EdgeData> b)
        {
            Node0 = a;
            Node1 = b;
            Data = null;
            Visited = false;
        }


        public Node<NodeData, EdgeData> Node0 { get; }
        public Node<NodeData, EdgeData> Node1 { get; }

        public bool IsLoop => Node0 == Node1;

        public EdgeData Data;
        public bool Visited;
    }


    public class Graph<NodeData, EdgeData> where EdgeData : class
    {
        public void AddPair(Node<NodeData, EdgeData> a, Node<NodeData, EdgeData> b, EdgeData edgeData = null)
        {
            Edge<NodeData, EdgeData> edge = new Edge<NodeData, EdgeData>(a, b, edgeData);
            if (!Nodes.Contains(a))
                Nodes.Add(a);
            if (!Nodes.Contains(b))
                Nodes.Add(b);
            a.Neighbors.Add(edge);
            if ( a != b )
                b.Neighbors.Add(edge);
            Edges.Add(edge);
        }


        public void AddNode(Node<NodeData, EdgeData> node)
        {
            if (!Nodes.Contains(node))
                Nodes.Add(node);
        }


        public int Size => Nodes.Count;

        public int NumGroupings { get; private set; }

        public List<Node<NodeData, EdgeData>> Nodes = new List<Node<NodeData, EdgeData>>();
        public List<Edge<NodeData, EdgeData>> Edges = new List<Edge<NodeData, EdgeData>>();

        List<Edge<NodeData, EdgeData>> ShortestPath(Node<NodeData, EdgeData> source, Node<NodeData, EdgeData> dest)
        {
            float[] dist = new float[Nodes.Count];

            for (int i = 0; i < Nodes.Count; i++)
            {
                if ( Nodes[i] == source )
                {
                    dist[i] = 0.0f;
                }
                else
                {
                    dist[i] = float.PositiveInfinity;
                }
            }

            return null;
        }

        bool HasLoop()
        {
            foreach (var edge in Edges)
                if (edge.IsLoop)
                    return true;
            return false;
        }

        public void RemoveNode(Node<NodeData, EdgeData> node)
        {
            Nodes.Remove(node);

            foreach(var edge in node.Neighbors)
            {
                var neighborNode = edge.Node0 == node ? edge.Node1 : edge.Node0;
                neighborNode.Neighbors.Remove(edge);
                Edges.Remove(edge);
            }
        }

        bool IsConnected()
        {
            foreach(var node in Nodes)
            {
                node.Visited = false;
            }

            VisitConnected(Nodes[0]);

            foreach (Node<NodeData, EdgeData> node in Nodes)
            {
                if (!node.Visited)
                    return false;
            }
            return true;
        }

        void VisitConnected(Node<NodeData, EdgeData> node)
        {
            if ( !node.Visited )
            {
                node.Visited = true;

                foreach(var edge in node.Neighbors)
                {
                    VisitConnected(edge.Node0);
                    VisitConnected(edge.Node1);
                }
            }
        }

        public void GroupConnected(Node<NodeData, EdgeData> node, int groupNumber)
        {
            if (node.Grouping == -1)
            {
                node.Grouping = groupNumber;

                foreach (var edge in node.Neighbors)
                {
                    GroupConnected(edge.Node0, groupNumber);
                    GroupConnected(edge.Node1, groupNumber);
                }
            }
        }

        public bool IsBipartite()
        {
            foreach (var node in Nodes)
            {
                node.Color = -1;
            }

            AssignGroupings();
            var groups = new List<Node<NodeData, EdgeData>>(Nodes).GroupBy(node => node.Grouping);
            foreach (var group in groups)
            {
                if (!CheckBipartite(group.First(), 1))
                    return false;
            }
            return true;
        }

        bool CheckBipartite(Node<NodeData, EdgeData> node, int neighborColor)
        {
            if (node.Color == -1)
            {
                node.Color = neighborColor == 0 ? 1 : 0;

                foreach (var edge in node.Neighbors)
                {
                    var neighbor = edge.Node0 == node ? edge.Node1 : edge.Node0;

                    if (neighbor.Color == node.Color || !CheckBipartite(neighbor, node.Color))
                        return false;
                }
            }
            return true;
        }


        public void AssignGroupings()
        {
            foreach (Node<NodeData, EdgeData> node in Nodes)
            {
                node.Grouping = -1;
            }
            int currentGroup = -1; 
            for ( int i = 0; i < Nodes.Count; i++ )
            {
                if ( Nodes[i].Grouping == -1 )
                {
                    currentGroup++;
                    GroupConnected(Nodes[i], currentGroup);
                }
            }
            NumGroupings = currentGroup + 1;
        }
    }
}
