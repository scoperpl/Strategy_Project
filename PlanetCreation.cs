using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetCreation : MonoBehaviour
{
    private Vector2 r;
    public GameObject prefabToSpawn;
    public LayerMask collisionLayer;
    public TMP_Text planet_stats;
    public Main Main;
    public int liczba_planet = 8;
    int k;
    // Start is called before the first frame update
    void Start()
    {
        for (k = 1; k < liczba_planet+1; k++)
        {
            r = GetRandomSpawnPosition();
            planet_creation(r);
            
        }
    }

    private void planet_creation(Vector2 spawnPosition)
    {
        GameObject newObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        newObject.name = k.ToString();
        float random = Random.Range(0.4f, 0.7f);
        newObject.transform.localScale= new Vector3(random,random,random);
        Renderer renderer = newObject.GetComponent<Renderer>();
        Color randomColor = GenerateRandomColor();
        renderer.material.color = randomColor;
        while (CheckForCollision(newObject))
        {
            spawnPosition = GetRandomSpawnPosition();
            newObject.transform.position = spawnPosition;
        }
    }
    private Color GenerateRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
    private bool CheckForCollision(GameObject obj)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(obj.transform.position, obj.GetComponent<Collider2D>().bounds.extents.magnitude, collisionLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != obj)
            {
                return true; 
            }
        }

        return false;
    }

    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(-5, 10), Random.Range(-3, 4));
    }

// Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) 
        {
            planet_stats.text = "";
        }
     
    }
    private void OnMouseDown()
    {
        
    }
}