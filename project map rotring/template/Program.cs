using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace template
{
    class Program
    {
        struct data
        {
            public double time, totaldistance, walkingdistance, cardistance;
            public List<int> path;
            public long elapsed;
        }
        struct queries
        {
            public float startX, startY, endX, endY;
            public float rad;

        }
        class vertex : IComparable<vertex>
        {
            public int index;
            public double exertedtime;
            public double exerteddist;
            public float x, y;
            public List<nieghbours> nieghbourr = new List<nieghbours>();
            public vertex() { }

            public vertex(int Index, double Exertedtime, double Exerteddist, List<nieghbours> np)
            {
                this.index = Index;
                this.exertedtime = Exertedtime;
                this.exertedtime = Exertedtime;
                this.nieghbourr = np;

            }
            public int CompareTo(vertex that)
            {
                if (this.exertedtime < that.exertedtime)
                { return -1; }
                else if (this.exertedtime > that.exertedtime)
                { return 1; }
                else if (this.exertedtime == that.exertedtime)
                {
                    if (this.exerteddist < that.exerteddist)
                    { return -1; }
                    else if (this.exerteddist > that.exerteddist)
                    { return 1; }
                    else { return 0; }
                }
                return 0;
            }
        }
        class weightededge
        {
            public int start;
            public int end;
            public double timecost;
            public double speed;
            public double distance;

            public weightededge(int start, int end, double timecost, double speed, double distance)
            {
                this.start = start;
                this.end = end;
                this.timecost = timecost;
                this.speed = speed;
                this.distance = distance;
            }
            public weightededge()
            {

            }
        }
        class nieghbours
        {
            public int destnode;
            public weightededge w;
            public nieghbours(int dn, weightededge we)
            {
                this.destnode = dn;
                this.w = we;
            }
        }
        class wightedgraph
        {
            public List<vertex> graph = new List<vertex>();

            public wightedgraph()
            { }
            public wightedgraph(List<vertex> np)
            {
                this.graph = np;
            }
        }
        class dikstra
        {
            bool b = false;
            List<vertex> lv = new List<vertex>();
            vertex vv = new vertex();
            double e;
            public data d1 = new data();
            public data rundijkstra(int src, int goal, wightedgraph G)
            {
                int peforetime = DateTime.Now.Hour;
                double[] d = new double[G.graph.Count];
                double[] dd = new double[G.graph.Count];
                bool[] visited = new bool[G.graph.Count];
                int[] parent = new int[G.graph.Count];
                List<int> ind = new List<int>();
                SortedSet<vertex> q = new SortedSet<vertex>();

                for (int i = 0; i < G.graph.Count; i++)
                {
                    d[i] = i == src ? 0 : int.MaxValue;
                    dd[i] = i == src ? 0 : int.MaxValue;
                }

                q.Add(new vertex(src, 0, 0, G.graph[G.graph.IndexOf(G.graph.First())].nieghbourr));
                ind.Add(0);
                parent[src] = src;

                int t = 0;
                while (q.Count != 0)
                {

                    int u = q.Min().index;
                    e = q.Min.exertedtime;
                    q.Remove(q.Min());

                    for (int i = 0; i < lv.Count; i++)
                    {
                        if (e == lv[i].exertedtime && b == true)
                        {

                            q.Add(lv[i]);
                            lv.RemoveAt(i);
                            b = false;
                            break;

                        }

                    }


                    int x = 0;
                    int z = G.graph[u].nieghbourr.Count();
                    for (int i =0; i <= z-1; i++)
                    {
                        int v = G.graph[u].nieghbourr[x].destnode; //index of current u
                        double weight = G.graph[u].nieghbourr[x].w.timecost; //wieght of the next edge 
                        double distance = G.graph[u].nieghbourr[x].w.distance;
                        if (d[v] > d[u] + weight)
                        {
                            if (d[v] != int.MaxValue)
                            {

                                q.Remove(new vertex(v, d[v], dd[v], G.graph[v].nieghbourr));

                            }
                            // Updating distance of v 
                            d[v] = d[u] + weight;
                            G.graph[v].exertedtime = d[v];
                            dd[v] = dd[u] + distance;
                            parent[v] = u;

                            if (q.Add(new vertex(v, d[v], dd[v], G.graph[v].nieghbourr)) == false)
                            {
                                vv = new vertex(v, d[v], dd[v], G.graph[v].nieghbourr);
                                lv.Add(vv);
                                b = true;

                            }


                        }
                        else if (d[v] == d[u] + weight)
                        {
                            if (dd[v] > dd[u] + distance)
                            {
                                if (dd[v] != int.MaxValue)
                                {
                                    q.Remove(new vertex(v, d[v], dd[v], G.graph[v].nieghbourr));
                                }
                                // Updating distance of v 
                                d[v] = d[u] + weight;
                                dd[v] = dd[u] + distance;
                                parent[v] = u;


                                if (q.Add(new vertex(v, d[v], dd[v], G.graph[v].nieghbourr)) == false)
                                {
                                    vv = new vertex(v, d[v], dd[v], G.graph[v].nieghbourr);
                                    lv.Add(vv);
                                    b = true;

                                }

                            }

                        }
                        t++;
                        x++;
                    }

                }

                d1.time = d[goal] * 60;
                d1.totaldistance = dd[goal];
                printPath(parent, 0, G.graph.Count - 1, G, dd[goal]);
                return d1;
            }
            private void printPath(int[] path, int start, int end, wightedgraph G, double dis)
            {
                int v1 = 0;
                int v = 0;
                double mshy1 = 0;
                double mshy2 = 0;
                double result = 0;




                //prints a path, given a start and end, and an array that holds previous 
                //nodes visited
                d1.path = new List<int>();
                int temp = end;
                Stack<int> s = new Stack<int>();
                while (temp != start)
                {
                    s.Push(temp);
                    temp = path[temp];
                }
                int c = 0;
                int f = s.Count;
                while (f != 0)
                {

                    int rkm = s.Pop();
                    f--;
                    if (c == 0)
                    {
                        v = rkm;
                        c++;

                    }
                    if ( f == 1)
                    {
                        v1 = rkm;

                    }
                    d1.path.Add(rkm);
                }
                int c2 = G.graph[0].nieghbourr.Count();
                for (int i = 0; i <  c2; i++)
                {
                    if (G.graph[0].nieghbourr[i].destnode == v)
                    {
                        mshy1 = G.graph[0].nieghbourr[i].w.distance;
                    }
                }
                int c1 =G.graph[G.graph.Count() - 1].nieghbourr.Count() ;
                for (int i = 0; i < c1 ; i++)
                {
                    if (G.graph[G.graph.Count() - 1].nieghbourr[i].destnode == v1)
                    {
                        mshy2 = G.graph[G.graph.Count() - 1].nieghbourr[i].w.distance;
                    }
                }

                result = mshy1 + mshy2;
                d1.walkingdistance = result;
                d1.cardistance = dis - result;
            }
        }
        class runprog
        {
            public List<vertex> graphconst(string mapnom)
            {
                List<vertex> listofpoints = new List<vertex>();
                List<weightededge> listofedgs = new List<weightededge>();

                FileStream fs = new FileStream(mapnom, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                using (StreamReader reader = File.OpenText(mapnom))
                {
                    int count = Int32.Parse(sr.ReadLine());
                    int a = count;
                    reader.ReadLine();
                    for (int i = 0; i < count; i++)
                    {
                        vertex p = new vertex();
                        string text = reader.ReadLine();
                        string[] bitss = text.Split(' ');
                        p.index = int.Parse(bitss[0]);
                        p.index++;
                        p.x = float.Parse(bitss[1]);
                        p.y = float.Parse(bitss[2]);
                        listofpoints.Add(p);
                    }

                    int countE = Int32.Parse(reader.ReadLine());
                    for (int i = 0; i < countE; i++)
                    {
                        weightededge edge = new weightededge();
                        string textt = reader.ReadLine();
                        string[] bits = textt.Split(' ');
                        edge.start = int.Parse(bits[0]);
                        edge.start++;

                        edge.end = int.Parse(bits[1]);
                        edge.end++;

                        edge.distance = float.Parse(bits[2]);
                        edge.speed = int.Parse(bits[3]);
                        edge.timecost = edge.distance / edge.speed;
                        listofedgs.Add(edge);
                    }

                    for (int i = 0; i < listofedgs.Count(); i++)
                    {
                        int s = listofedgs[i].start;
                        int d = listofedgs[i].end;


                        nieghbours n = new nieghbours(d, listofedgs[i]);
                        nieghbours n2 = new nieghbours(s, listofedgs[i]);

                        listofpoints[s - 1].nieghbourr.Add(n);
                        listofpoints[d - 1].nieghbourr.Add(n2);
                    }

                    return listofpoints;
                }
            }
            public List<data> runqueries(string querynom, List<vertex> nlistofpoints)
            {

                List<data> qeuriesdata = new List<data>();
                queries QE = new queries();
                FileStream fss = new FileStream(querynom, FileMode.Open, FileAccess.Read);
                StreamReader srr = new StreamReader(fss);
                int countQ = Int32.Parse(srr.ReadLine());
                using (StreamReader reader = File.OpenText(querynom))
                {
                    reader.ReadLine();
                    for (int i = 0; i < countQ; i++)
                    {
                        string text = reader.ReadLine();
                        string[] bitss = text.Split(' ');
                        QE.startX = float.Parse(bitss[0]);
                        QE.startY = float.Parse(bitss[1]);
                        QE.endX = float.Parse(bitss[2]);
                        QE.endY = float.Parse(bitss[3]);
                        QE.rad = float.Parse(bitss[4]);

                        vertex v = new vertex();
                        v.x = QE.startX;
                        v.y = QE.startY;
                        v.index = 0;
                        nlistofpoints.Insert(0, v);

                        vertex vv = new vertex();
                        vv.x = QE.endX;
                        vv.y = QE.endY;
                        vv.index = nlistofpoints.Count();
                        nlistofpoints.Add(vv);

                        int cc = nlistofpoints.Count();
                        for (int j = 0; j < nlistofpoints.Count(); j++)
                        {
                            double result = Math.Pow(Math.Pow((nlistofpoints[j].x - QE.startX), 2) + Math.Pow((nlistofpoints[j].y - QE.startY), 2), 0.5);

                            if (QE.rad / 1000 >= result)
                            {
                                weightededge n = new weightededge(0, j, (result / 5), 5, result);
                                nieghbours ne = new nieghbours(j, n);
                                nlistofpoints[0].nieghbourr.Add(ne);


                                weightededge nn = new weightededge(j, 0, (result / 5), 5, result);
                                nieghbours nee = new nieghbours(0, nn);
                                nlistofpoints[j].nieghbourr.Add(nee);
                            }


                            if (j != nlistofpoints.Count() - 1)
                            {
                                double result1 = Math.Pow(Math.Pow((nlistofpoints[j].x - QE.endX), 2) + Math.Pow((nlistofpoints[j].y - QE.endY), 2), 0.5);

                                if (QE.rad / 1000 >= result1)
                                {
                                    weightededge n = new weightededge((nlistofpoints.Count() - 1), j, (result1 / 5), 5, result1);
                                    nieghbours ne = new nieghbours(j, n);
                                    nlistofpoints[(nlistofpoints.Count() - 1)].nieghbourr.Add(ne);
                                    weightededge nn = new weightededge(j, (nlistofpoints.Count() - 1), (result1 / 5), 5, result1);
                                    nieghbours nee = new nieghbours((nlistofpoints.Count() - 1), nn);
                                    nlistofpoints[j].nieghbourr.Add(nee);
                                }

                            }

                        }
                        wightedgraph g = new wightedgraph(nlistofpoints);
                        dikstra DS = new dikstra();
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        data qeurydata = DS.rundijkstra(g.graph.First().index, g.graph.Last().index, g);
                        sw.Stop();
                        long lon = sw.ElapsedMilliseconds;
                        sw.Reset();
                        qeurydata.elapsed = lon;
                        qeuriesdata.Add(qeurydata);

                        for (int m = 0; m < nlistofpoints.Count(); m++)
                        {
                            for (int n = 0; n < nlistofpoints[m].nieghbourr.Count(); n++)
                            {
                                if (nlistofpoints[m].nieghbourr[n].destnode == nlistofpoints.First().index || nlistofpoints[m].nieghbourr[n].destnode == nlistofpoints.Last().index)
                                {
                                    nlistofpoints[m].nieghbourr.RemoveAt(n);
                                }
                            }
                        }
                        nlistofpoints.Remove(nlistofpoints.First());
                        nlistofpoints.Remove(nlistofpoints.Last());

                    }
                }

                //Console.Write(sw.ElapsedMilliseconds + " \n");
                return qeuriesdata;
            }
        }
        static void Main(string[] args)
        {
            runprog rn = new runprog();
            long total = 0;
            List<vertex> listofpoints = rn.graphconst("OLMap.txt");
            List<data> d1 = rn.runqueries("OLQueries.txt", listofpoints);
            FileStream fs = new FileStream("C:\\Users\\Abdo\\Documents\\Visual Studio 2012\\Projects\\template\\template\\bin\\Release\\outputfile.txt", FileMode.Truncate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < d1.Count; i++)
            {
                for (int j = 0; j < d1[i].path.Count - 1; j++)
                {
                    sw.Write((d1[i].path[j] - 1) + " ");
                }
                sw.WriteLine();
                sw.WriteLine(Math.Round(d1[i].time,2) + " mins \n");
                sw.WriteLine(Math.Round(d1[i].totaldistance,2) + " Km \n");
                sw.WriteLine(Math.Round(d1[i].walkingdistance,2) + " Km \n");
                sw.WriteLine(Math.Round(d1[i].cardistance,2) + " Km");

                //sw.WriteLine();
                //sw.WriteLine(d1[i].elapsed+" ms ");
                sw.WriteLine();
                total += d1[i].elapsed;
            }
            sw.WriteLine( total + " ms");
            sw.Close();
            }
        }
    }
