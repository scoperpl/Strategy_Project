using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coring : MonoBehaviour
{
    PlanetStats yourScript = new PlanetStats();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPosition2D = new Vector2(clickPosition.x, clickPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(clickPosition2D, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                yourScript = clickedObject.GetComponent<PlanetStats>();
                if (yourScript != null)
                {

                }
            }
        }
    }
    public void coring()
    {
        if (yourScript.tag == "is_selected" && yourScript.is_cored == false)
        {
            yourScript.is_cored = true;
            yourScript.tag = "is_selected";
        }
    }
}
