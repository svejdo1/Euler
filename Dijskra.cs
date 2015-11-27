using System.Collections.Generic;

namespace Barbar.Euler
{
    public class Dijskra<T>
    {
        Dictionary<T, Dictionary<T, long>> m_Vertices = new Dictionary<T, Dictionary<T, long>>();

        public void AddVertex(T name, Dictionary<T, long> edges)
        {
            m_Vertices[name] = edges;
        }

        public List<T> ShortestPath(T start, T finish)
        {
            checked
            {
                var previous = new Dictionary<T, T>();
                var distances = new Dictionary<T, long>();
                var nodes = new List<T>(10000);

                List<T> path = null;
                foreach (var vertex in m_Vertices)
                {
                    if (EqualityComparer<T>.Default.Equals(vertex.Key, start))
                    {
                        distances[vertex.Key] = 0;
                    }
                    else
                    {
                        distances[vertex.Key] = int.MaxValue;
                    }

                    nodes.Add(vertex.Key);
                }

                while (nodes.Count != 0)
                {
                    nodes.Sort((x, y) => (int)(distances[x] - distances[y]));

                    var smallest = nodes[0];
                    nodes.RemoveAt(0);

                    if (EqualityComparer<T>.Default.Equals(smallest, finish))
                    {
                        path = new List<T>();
                        while (previous.ContainsKey(smallest))
                        {
                            path.Add(smallest);
                            smallest = previous[smallest];
                        }

                        break;
                    }

                    if (distances[smallest] == int.MaxValue)
                    {
                        break;
                    }

                    foreach (var neighbor in m_Vertices[smallest])
                    {
                        var alt = distances[smallest] + neighbor.Value;
                        if (alt < distances[neighbor.Key])
                        {
                            distances[neighbor.Key] = alt;
                            previous[neighbor.Key] = smallest;
                        }
                    }
                }

                return path;
            }
        }
    }
}