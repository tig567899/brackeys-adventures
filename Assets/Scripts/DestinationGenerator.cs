using UnityEngine;
using System.Collections.Generic;

public class DestinationGenerator : MonoBehaviour
{
    public int maxX, maxY;
    public GameObject objToSpawn;
    private List<GameObject> objects = new List<GameObject>();

    void Start()
    {
        GenerateDestinations();
    }

    private void GenerateDestinations()
    {
        for (int x = -maxX; x <= maxX; x += 5)
        {
            for (int y = -maxY; y <= maxY; y += 5)
            {
                GameObject newDestination = Instantiate(objToSpawn);
                newDestination.transform.position = new Vector3(x, y, -1);
                objects.Add(newDestination);
            }
        }
    }

}
