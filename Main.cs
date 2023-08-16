using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> planets = new List<GameObject>();

    private void Start()
    {
        // Uruchom proces zbierania obiektów w centralnym skrypcie
        CollectSpawnedObjects();
    }

    public void CollectSpawnedObjects()
    {
        // Zbierz nowo powsta³e obiekty i dodaj je do listy
        GameObject[] newObjects = GameObject.FindGameObjectsWithTag("planet"); // Zmieñ "YourTag" na odpowiedni tag
        planets.AddRange(newObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
