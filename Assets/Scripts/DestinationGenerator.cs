using UnityEngine;
using System.Collections.Generic;
using static System.Math;

public class DestinationGenerator : MonoBehaviour
{
    public GameObject destination;
    private List<List<GameObject>> objects = new List<List<GameObject>>();

    private const int mapEdgeY = 50;
    private const int mapEdgeX = 90;

    private const float minGap = 10f;

    void Start()
    {
        GenerateDestinations();
        GenerateConnections();
    }

    private void GenerateDestinations()
    {
        objects.Add(new List<GameObject> { GenerateDestinationAt(-mapEdgeX, 0) });

        for (int x = 1; x <= 9; x++)
        {
            List<GameObject> currentLayer = new List<GameObject>();

            List<GameObject> previousLayer = objects[x - 1];
            int destinationCount = Min(7, Max(2, Random.Range(previousLayer.Count, previousLayer.Count + 3)));

            List<float> yCoords = GenerateYCoordinatesForColumn(destinationCount);

            for (int i = 0; i < destinationCount; i++)
            {
                GameObject newDest = GenerateDestinationAt(-mapEdgeX + 20 * x, yCoords[i]);
                //newDest.GetComponent<DestinationSelection>().SetActive(false);

                currentLayer.Add(newDest);
            }
            objects.Add(currentLayer);
        }
    }

    private GameObject GenerateDestinationAt(float x, float y)
    {
        GameObject newDestination = Instantiate(destination);
        newDestination.transform.position = new Vector3(x, y, 0);
        return newDestination;
    }

    private List<float> GenerateYCoordinatesForColumn(int nodes)
    {
        List<float> toReturn = new List<float>();

        // Dist from max to center = nodes/2 * 30f;
        for (int i = 0; i < nodes; i++)
        {
            toReturn.Add(minGap * (nodes - 1) / 2 - minGap * i);
        }

        return toReturn;
    }

    private void GenerateConnections()
    {
        // First column is special
        for (int j = 0; j < objects[1].Count; j++)
        {
            objects[0][0].GetComponent<DestinationSelection>().AddChildNode(objects[1][j]);
        }
        for (int i = 1; i < objects.Count - 1; i++)
        {
            /* 
                Case 1: Same number of nodes

                Attach each node to its predecessor, and, with the exception of the first
                and last node in the list, each will have a 50% chance to attach to its neighbours.

                In addition, have a 10% chance to have a magical connection to any node in the list
                if the higher than 5.
            */

            if (objects[i].Count == objects[i + 1].Count)
            {
                for (int j = 0; j < objects[i].Count; j++)
                {
                    objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j]);

                    bool attachToTopNeighbour = Random.Range(0f, 1f) > 0.5;
                    bool attachToBottomNeighbour = Random.Range(0f, 1f) > 0.5;

                    if (j != 0 && (j == objects[i].Count - 1 || attachToTopNeighbour))
                    {
                        objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j - 1]);
                    }

                    // Ensure there's always two paths to go.
                    if (j != objects[i].Count - 1 && (j == 0 || !attachToTopNeighbour || attachToBottomNeighbour))
                    {
                        objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j + 1]);
                    }

                    // TODO: Add magical paths
                }
            }

            /*
                Case 2: One more node than previous

                Attach each node to its corresponding and lower neighbours. No randomization needed here.
            */
            if (objects[i].Count == objects[i + 1].Count - 1)
            {
                for (int j = 0; j < objects[i].Count; j++)
                {
                    objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j]);
                    objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j + 1]);

                    // TODO: Add magical paths
                }
            }

            /*
                Case 3: Two more nodes than previous

                We shift the next array up (in practice, not in actuality). Then, attach each node to its corresponding ones,
                the top and bottom nodes to the excluded extreme nodes, and then attach each node to its upper and lower 
                neighbours at 50% rates.
            */

            if (objects[i].Count == objects[i + 1].Count - 2)
            {
                for (int j = 0; j < objects[i].Count; j++)
                {
                    objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j + 1]);

                    bool attachToTopNeighbour = Random.Range(0f, 1f) > 0.5 || j == 0;
                    bool attachToBottomNeighbour = Random.Range(0f, 1f) > 0.5 || j == objects[i].Count - 1;

                    if (attachToTopNeighbour)
                    {
                        objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j]);
                    }

                    // Ensure there's always two paths to go.
                    if (!attachToTopNeighbour || attachToBottomNeighbour)
                    {
                        objects[i][j].GetComponent<DestinationSelection>().AddChildNode(objects[i + 1][j + 2]);
                    }
                }
            }
        }
    }
}
