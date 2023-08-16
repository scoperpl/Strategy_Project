using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coring : MonoBehaviour
{
    PlanetStats yourScript = new PlanetStats();
    bool clickedVariable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPosition2D = new Vector2(clickPosition.x, clickPosition.y);

        // Wykonaj raycast, aby wykry� klikni�cie na obiekcie.
        RaycastHit2D hit = Physics2D.Raycast(clickPosition2D, Vector2.zero);

        if (hit.collider != null)
        {
            // Znaleziono obiekt, kt�ry zosta� klikni�ty.
            GameObject clickedObject = hit.collider.gameObject;

            // Sprawd� zmienn� na klikni�tym obiekcie.
            yourScript = clickedObject.GetComponent<PlanetStats>();
            if (yourScript != null)
            {

            }
        }
    }
    public void coring()
    {
        if (yourScript.is_selected == true && yourScript.is_cored == false)
        {
            yourScript.is_cored = true;
        }
    }
}
