using System.Diagnostics;
using System.Linq;
using static DrawingStuff.FloatMath;


namespace DrawingStuff
{

    internal class GraphTestWorld
    {
        Node<NodeData, Visual> FocusedNode = null;

        class NodeData
        {
            public NodeData(Vector2 position, Visual visual)
            {
                Visual = visual;
            }
            public float Radius;
            public Visual Visual;
        }

        public GraphTestWorld(Camera camera)
        {
                    Scene = new Scene(camera);

        /*

        int numNodes = 45;
        int numEdges = 44;


        for (int i = 0; i < numNodes; i++)
        {
            Graph.AddNode(new Node<NodeData, Visual>(new NodeData(new Vector2(-2 + 4.0f * Rand(1), -2 + 4.0f * Rand(1)), null)));
        }

        for (int i = 0; i < numEdges; i++)
        {
            int rand1 = (int)Rand(numNodes - 0.0000000001f);
            int rand2;
            do { rand2 = (int)Rand(numNodes - 0.0000000001f); }
            while ( rand1 == rand2 );


            Graph.AddPair(Graph.Nodes[rand1], Graph.Nodes[rand2],
                VisualFactory.LineVisual(Graph.Nodes[rand1].Data.Position, Graph.Nodes[rand2].Data.Position, Color4.Green, 0.6f));

        }

        float RadiusGrowth = 0.0125f;
        float RadiusBase = 0.025f;

        for (int i = 0; i < numNodes; i++)
        {
            Graph.Nodes[i].Data.Radius = RadiusBase + Graph.Nodes[i].Neighbors.Count * RadiusGrowth;
            Graph.Nodes[i].Data.Visual =
            VisualFactory.CircleVisual(Graph.Nodes[i].Data.Radius, VisualFactory.greenMaterial, 0.5f);
        }

        /*
        Graph.AssignGroupings();
        for (int i = 0; i < numNodes; i++)
        {
            if (Graph.Nodes[i].Grouping < 3)
            {
                Graph.Nodes[i].Data.Visual.Material = new ColorMaterial(
                new Color4((Graph.Nodes[i].Grouping + 1) / 3.0f, 0, 0, 1.0f),
                Primitive.Triangles);
            }
            else if (Graph.Nodes[i].Grouping < 6)
            {
                Graph.Nodes[i].Data.Visual.Material = new ColorMaterial(
                    new Color4(0, 0, (Graph.Nodes[i].Grouping - 2) / 3.0f, 1.0f),
                    Primitive.Triangles);
            }
            else if (Graph.Nodes[i].Grouping < 9)
            {
                Graph.Nodes[i].Data.Visual.Material = new ColorMaterial(
                    new Color4(0, (Graph.Nodes[i].Grouping - 5) / 3.0f, 0, 1.0f),
                    Primitive.Triangles);
            }
            else
            {
                float c = (float)Graph.Nodes[i].Grouping / Graph.NumGroupings;
                Graph.Nodes[i].Data.Visual.Material = new ColorMaterial(
                    new Color4(c, c, c, 1.0f),
                    Primitive.Triangles);
            }

        }


        //CompleteGraph(8);

        Node<NodeData, Visual>[] nodes = new Node<NodeData, Visual>[6]
        {
            new Node<NodeData, Visual>(new NodeData(new Vector2(-1, -1), VisualFactory.CircleVisual(0.074f, VisualFactory.greenMaterial, 0.5f))),
            new Node<NodeData, Visual>(new NodeData(new Vector2(-1,  1), VisualFactory.CircleVisual(0.074f, VisualFactory.greenMaterial, 0.5f))),
            new Node<NodeData, Visual>(new NodeData(new Vector2( 1,  1), VisualFactory.CircleVisual(0.074f, VisualFactory.greenMaterial, 0.5f))),
            new Node<NodeData, Visual>(new NodeData(new Vector2( 1, -1), VisualFactory.CircleVisual(0.074f, VisualFactory.greenMaterial, 0.5f))),
            new Node<NodeData, Visual>(new NodeData(new Vector2( 1, -0.5f), VisualFactory.CircleVisual(0.074f, VisualFactory.greenMaterial, 0.5f))),
            new Node<NodeData, Visual>(new NodeData(new Vector2( 0.5f, -1), VisualFactory.CircleVisual(0.074f, VisualFactory.greenMaterial, 0.5f)))
        };



        Graph.AddPair(nodes[0], nodes[3], VisualFactory.LineVisual(nodes[0].Data.Position, nodes[1].Data.Position, Color4.Green, 0.6f));
        Graph.AddPair(nodes[0], nodes[4], VisualFactory.LineVisual(nodes[1].Data.Position, nodes[2].Data.Position, Color4.Green, 0.6f));
        Graph.AddPair(nodes[1], nodes[5], VisualFactory.LineVisual(nodes[2].Data.Position, nodes[3].Data.Position, Color4.Green, 0.6f));
        Graph.AddPair(nodes[1], nodes[3], VisualFactory.LineVisual(nodes[3].Data.Position, nodes[0].Data.Position, Color4.Green, 0.6f));
        Graph.AddPair(nodes[2], nodes[4], VisualFactory.LineVisual(nodes[3].Data.Position, nodes[1].Data.Position, Color4.Green, 0.6f));
        Graph.AddPair(nodes[2], nodes[3], VisualFactory.LineVisual(nodes[3].Data.Position, nodes[1].Data.Position, Color4.Green, 0.6f));


        foreach (Node<NodeData, Visual> node in Graph.Nodes)
        {
            node.Data.Radius = 0.074f;
        }
        */


        /*
        if (Graph.IsBipartite())
        {

            for (int i = 0; i < Graph.Nodes.Count; i++)
            {
                if (Graph.Nodes[i].Color == 0)
                {
                    Graph.Nodes[i].Data.Visual.Material = new ColorMaterial(
                    new Color4(1, 0, 0, 1.0f),
                    Primitive.Triangles);
                }
                else if (Graph.Nodes[i].Color == 1)
                {
                    Graph.Nodes[i].Data.Visual.Material = new ColorMaterial(
                        new Color4(0, 0, 1, 1.0f),
                        Primitive.Triangles);
                }
            }
        }




*/
        CycleGraph(NumNodes);


        }

        int NumNodes = 3333;

        public void RelaxNodes(float distance, float dt, int numIterations = 1)
        {
            for (int k = 0; k < numIterations; k++)
            {
                for (int i = 0; i < Graph.Nodes.Count; i++)
                {
                    for (int j = i + 1; j < Graph.Nodes.Count; j++)
                    {
                        Vector2 delta = Graph.Nodes[j].Data.Visual.Transform.d - Graph.Nodes[i].Data.Visual.Transform.d;
                        float lenSq = delta.LengthSq;
                        float len = Sqrt(lenSq);
                        Vector2 dir = delta / len;
                        Vector2 gravity = 0.1f * len * dir * dt;
                        Graph.Nodes[i].Data.Visual.Transform.d += gravity;
                        Graph.Nodes[j].Data.Visual.Transform.d -= gravity;

                        if (lenSq < distance * distance)
                        {
                            Vector2 dispacement = (len - distance) * 0.1f / 2.0f * dir;
                            Graph.Nodes[i].Data.Visual.Transform.d += dispacement;
                            Graph.Nodes[j].Data.Visual.Transform.d -= dispacement;
                        }
                    }
                }
            }
        }

        public void HandleInput(Vector2 mouseWorldPos, float deltaTime, Camera camera)
        {
            if (FocusedNode == null )
            {
                foreach (var node in Graph.Nodes)
                {
                    if ((node.Data.Visual.Transform.d - mouseWorldPos).LengthSq < node.Data.Radius * node.Data.Radius &&
                          OpenTK.Input.Mouse.GetState().IsButtonDown(OpenTK.Input.MouseButton.Left) &&
                          FocusedNode == null)
                    {
                        FocusedNode = node;
                        break;
                    }
                }
            }

            if (!OpenTK.Input.Mouse.GetState().IsButtonDown(OpenTK.Input.MouseButton.Left))
            {
                FocusedNode = null;
            }

            if (FocusedNode != null)
            {
                //  FocusedNode.Data.Position = mouseWorldPos;
                Graph.RemoveNode(FocusedNode);
            }

            if ( OpenTK.Input.Mouse.GetState().IsButtonDown(OpenTK.Input.MouseButton.Left) )
            {
                Graph = new Graph<NodeData, Visual>();
                PathGraph(NumNodes++);
            }

            if (OpenTK.Input.Mouse.GetState().IsButtonDown(OpenTK.Input.MouseButton.Right))
            {
                Graph = new Graph<NodeData, Visual>();
                PathGraph(NumNodes--);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (Node<NodeData, Visual> node in Graph.Nodes)
            {
                Scene.Add(node.Data.Visual);
            }

            foreach (Edge<NodeData, Visual> edge in Graph.Edges)
            {
                Scene.Add(edge.Data);
            }


            foreach (var edge in Graph.Edges)
            {
                edge.Data.Positions[0] = edge.Node0.Data.Visual.Transform.d;
                edge.Data.Positions[1] = edge.Node1.Data.Visual.Transform.d;
            }
            RelaxNodes(3.0f, deltaTime);
        }

        public void Draw(Camera camera, float deltaTime)
        {
            Scene.Draw();
        }



        public void CompleteGraph(int numNodes)
        {
            for (int i = 0; i < numNodes; i++)
            {
                Graph.AddNode(new Node<NodeData, Visual>(new NodeData(new Vector2(-1 + 2.0f * Rand(1), -1 + 2.0f * Rand(1)),
                     VisualFactory.CircleVisual(0.14f, Color4.Black, VisualFactory.FillMaterial, 0.5f))));
                Graph.Nodes[i].Data.Radius = 0.14f;
            }

            for (int i = 0; i < numNodes; i++)
            {
                for (int j = i + 1; j < numNodes; j++)
                {
                    Graph.AddPair(Graph.Nodes[i], Graph.Nodes[j]);
                }
            }
        }


        public void PathGraph(int numNodes)
        {
            for (int i = 0; i < numNodes; i++)
            {
                Graph.AddNode(new Node<NodeData, Visual>(new NodeData(new Vector2(-2 + 4.0f * Rand(1), -2 + 4.0f * Rand(1)),
                     VisualFactory.CircleVisual(0.14f, Color4.Red, VisualFactory.FillMaterial, 0.5f))));
                Graph.Nodes[i].Data.Radius = 0.14f;
            }

            for (int i = 0; i < numNodes-1; i++)
            {
                Graph.AddPair(Graph.Nodes[i], Graph.Nodes[i + 1]);
            }
        }

        public void CycleGraph(int numNodes)
        {
            for (int i = 0; i < numNodes; i++)
            {
                Graph.AddNode(new Node<NodeData, Visual>(new NodeData(new Vector2(-2 + 4.0f * Rand(1), -2 + 4.0f * Rand(1)),
                     VisualFactory.CircleVisual(0.14f, Color4.Blue, VisualFactory.FillMaterial, 0.5f))));
                Graph.Nodes[i].Data.Radius = 0.14f;
            }

            for (int i = 0; i < numNodes; i++)
            {
                Graph.AddPair(Graph.Nodes[i], Graph.Nodes[(i + 1) % numNodes]);
            }
        }


        public void WheelGraph(int numNodes)
        {
            for (int i = 0; i < numNodes; i++)
            {
                Graph.AddNode(new Node<NodeData, Visual>(new NodeData(new Vector2(-2 + 4.0f * Rand(1), -2 + 4.0f * Rand(1)),
                     VisualFactory.CircleVisual(0.14f, Color4.Blue, VisualFactory.FillMaterial, 0.5f))));
                Graph.Nodes[i].Data.Radius = 0.14f;
            }


            for (int i = 1; i < numNodes; i++)
            {
                Graph.AddPair(Graph.Nodes[i], Graph.Nodes[0]);
            }

            for (int i = 1; i < numNodes; i++)
            {
                int j = i + 1 >= numNodes ? 1 : i + 1;

                Graph.AddPair(Graph.Nodes[i], Graph.Nodes[j]);
            }
        }


        private Scene Scene;
        Graph<NodeData, Visual> Graph = new Graph<NodeData, Visual>();
    }

}

