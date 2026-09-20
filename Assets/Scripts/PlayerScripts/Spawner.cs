using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Chicken_Prefab;
    [SerializeField] private GameObject Cow_Prefab;
    [SerializeField] private GameObject Hog_Prefab;
    [SerializeField] private GameObject Pig_Prefab;
    private int numberofPrefabs;

    [SerializeField]
    public int lower;

    [SerializeField] public int upper;


    [SerializeField] private Vector2 corner1;
    [SerializeField] private Vector2 corner2;

    private void Start()
    {
        numberofPrefabs = Random.Range(lower,upper);
        SpawnPrefabs();
        
    }

    private void SpawnPrefabs()
    {
        for (int i = 0; i < numberofPrefabs; i++)
        {
            int rand = Random.Range(0, 3);
            Vector2 spawnPosition = GetRandomPosition();
            switch (rand)
            {
             case 0:
                 Instantiate(Chicken_Prefab, spawnPosition, Quaternion.identity);
                 break;
             case 1:
                Instantiate(Cow_Prefab, spawnPosition, Quaternion.identity);
                break;
             case 2:
                Instantiate(Hog_Prefab, spawnPosition, Quaternion.identity);
                break;
            case 3:
                Instantiate(Pig_Prefab, spawnPosition, Quaternion.identity);
                break;

            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        float minX = Mathf.Min(corner1.x, corner2.x);
        float maxX = Mathf.Max(corner1.x, corner2.x);
        float minY = Mathf.Min(corner1.y, corner2.y);
        float maxY = Mathf.Max(corner1.y, corner2.y);

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector2(x, y);

    }


}
