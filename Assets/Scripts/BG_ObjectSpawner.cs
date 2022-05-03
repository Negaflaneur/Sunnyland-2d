using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects;

    void Start()
    {
        int randObject = Random.Range(0, objects.Length);
        Instantiate(objects[randObject], transform.position, Quaternion.identity);
    }
}
