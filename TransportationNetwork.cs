//2024 CAB301 Assignment 3 
//TransportationNetwok.cs
//Assignment3B-TransportationNetwork

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class TransportationNetwork
{

    private string[]? intersections; //array storing the names of those intersections in this transportation network design
    private int[,]? distances; //adjecency matrix storing distances between each pair of intersections, if there is a road linking the two intersections

    public string[]? Intersections
    {
        get {return intersections;}
    }

    public int[,]? Distances
    {
        get { return distances; }
    }


    //Read information about a transportation network plan into the system
    //Preconditions: The given file exists at the given path and the file is not empty
    //Postconditions: Return true, if the information about the transportation network plan is read into the system, the intersections are stored in the class field, intersections, and the distances of the links between the intersections are stored in the class fields, distances;
    //                otherwise, return false and both intersections and distances are null.
    public bool ReadFromFile(string filePath)
    {
        //To be completed by students
        FileStream fs = null;
        List<string> memFile = new List<string>();
        try
        {
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (TextReader tx = new StreamReader(fs))
            {
                String line;
                while ((line = tx.ReadLine()) != null)
                {
                    memFile.Add(line.Replace(" ", ""));
                }
            }
        }
        finally
        {
            if (fs != null)
            {
                fs.Dispose();
            }
        }
        List<string> intersectionsTemp = new List<string>();
        foreach (String line in memFile)
        {
            string[] input = line.Split(',');
            if (input.Length < 3)
            {
                intersections = null;
                distances = null;
                return false;
            }
            intersectionsTemp.Add(input[0]);
            intersectionsTemp.Add(input[1]);
        }
        intersectionsTemp.Sort();
        intersections = intersectionsTemp.Distinct().ToArray();
        int len = intersections.Count();
        int[,] distTemp = new int[len, len];
        for (int i = 0; i < len; i++)
        {
            for(int j = 0; j < len; j++)
            {
                if (i == j)
                {
                    distTemp[i, j] = 0;
                }
                else
                {
                    distTemp[i, j] = int.MaxValue;
                }
            }
        }
        foreach (String line in memFile)
        {
            string[] input = line.Split(',');
            int xPos = Array.FindIndex(intersections, row => row.Contains(input[0]));
            int yPos = Array.FindIndex(intersections, row => row.Contains(input[1]));
            distTemp[xPos, yPos] = int.Parse(input[2]);
        }
        distances = distTemp;
        return true;
    }


    //Display the transportation network plan with intersections and distances between intersections
    //Preconditions: The given file exists at the given path and the file is not empty
    //Postconditions: The transportation netork is displayed in a matrix format
    public void DisplayTransportNetwork()
    {
        Console.Write("       ");
        for (int i = 0; i < intersections?.Length; i++)
        {
                    Console.Write(intersections[i].ToString().PadRight(5) + "  ");
        }
        Console.WriteLine();


        for (int i = 0; i < distances?.GetLength(0); i++)
        {
            Console.Write(intersections[i].ToString().PadRight(5) + "  ");
            for (int j = 0; j < distances?.GetLength(1); j++)
            {
                if (distances[i, j] == Int32.MaxValue)
                    Console.Write("INF  " + "  ");
                else
                    Console.Write(distances[i, j].ToString().PadRight(5)+"  ");
            }
            Console.WriteLine();
        }
    }


    //Check if this transportation network is strongly connected. A transportation network is strongly connected, if there is a path from any intersection to any other intersections in thihs transportation network. 
    //Precondition: Transportation network plan data have been read into the system.
    //Postconditions: return true, if this transpotation netork is strongly connected; otherwise, return false. This transportation network remains unchanged.
    public bool IsConnected()
    {
        //To be completed by students
        int len = intersections.Count();
        int[,] connections = FindAllShortestDistances();
        for(int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                if (connections[i, j] != int.MaxValue)
                {
                    continue;
                }
                else return false;
            }
        }
        return true;
    }

    
    
    //Find the shortest path between a pair of intersections
    //Precondition: transportation network plan data have been read into the system
    //Postcondition: return the shorest distance between two different intersections; return 0 if there is no path from startVerte to endVertex; returns -1 if startVertex or endVertex does not exists. This transportation network remains unchanged.

    public int FindShortestDistance(string startVertex, string endVertex)
    {
        //To be completed by students
        int len = intersections.Count();
        if (!intersections.Contains(startVertex) || !intersections.Contains(endVertex))
        {
            return -1;
        }
        int startIndex = Array.FindIndex(intersections, row => row.Contains(startVertex));
        int endIndex = Array.FindIndex(intersections, row => row.Contains(endVertex));
        int minDistance(int[] dist,
                    bool[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;
            for (int v = 0; v < len; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }
            return min_index;
        }

        int[] dist = new int[len];
        bool[] vertSel = new bool[len];
        for (int i = 0; i < len; i++)
        {
            dist[i] = int.MaxValue;
            vertSel[i] = false;
        }
        dist[startIndex] = 0; 
        for (int i = 0; i < len - 1; i++)
        {
            int u = minDistance(dist, vertSel);
            vertSel[u] = true;

            for (int j = 0; j < len; j++)
            {
                if (dist[u] + distances[u, j] < dist[j] && dist[u] != int.MaxValue && !vertSel[j] && distances[u, j] != 0)
                {
                    dist[j] = dist[u] + distances[u, j];
                }
            }
        }
        int shortestDistance = dist[endIndex];
        if (shortestDistance == int.MaxValue)
        {
            shortestDistance = 0;
        }

        return shortestDistance;

    }

    //Find the shortest path between all pairs of intersections
    //Precondition: transportation network plan data have been read into the system
    //Postcondition: return the shorest distances between between all pairs of intersections through a two-dimensional int array and this transportation network remains unchanged

    public int[,] FindAllShortestDistances()
    {
        //To be completed by students
        int len = intersections.Count();
        int[,] shortestDistMTX = new int[len, len];
        Array.Copy(distances,shortestDistMTX,distances.Length);
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
                for (int k = 0; k < len; k++)
                {
                    if (shortestDistMTX[j,k] > (shortestDistMTX[j,i] + shortestDistMTX[i,k]) && shortestDistMTX[i,k] != int.MaxValue && shortestDistMTX[j,i] != int.MaxValue)
                    {
                        shortestDistMTX[j, k] = shortestDistMTX[j, i] + shortestDistMTX[i, k];
                    }
                }
            }
        }
        return shortestDistMTX;
    }
}
