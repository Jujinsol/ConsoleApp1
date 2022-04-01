using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 05, 10, -1, -1 },
            { -1, 05, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 05, -1 },
            { -1, -1, -1, 05, -1, 05 },
            { -1, -1, -1, -1, 05, -1 },
        };

        public void Dijkstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];
            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;
            parent[start] = start;

            while(true)
            {
                int closest = Int32.MaxValue; // 나로부터 가장 가까운 후보와의 거리
                int now = -1; // 가장 가까운 후보의 번호
                for (int i = 0; i < 6; i++)
                {
                    if (visited[i])
                        continue;
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;

                    closest = distance[i];
                    now = i;
                }
                if (now == -1)
                    break;

                visited[now] = true;

                for (int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == -1)
                        continue;
                    if (visited[next])
                        continue;

                    int nextDist = distance[now] + adj[now, next];
                    if (nextDist <= distance[next])
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }
                }
            }
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.Dijkstra(0);
        }
    }
}