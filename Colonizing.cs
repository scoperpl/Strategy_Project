using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Colonizing : MonoBehaviour
{
    bool is_enabled = true;
    public Button button;
    public TMP_InputField inputField;
    PlanetStats yourScript = new PlanetStats();
    // Start is called before the first frame update
    void Start()
    {
        inputField.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (is_enabled == true)
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
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            Debug.Log(is_enabled);
            Debug.Log(yourScript.tag);
            Debug.Log(yourScript.id);
            Debug.Log(yourScript.is_cored);
        }
        
    }
    public void colonizing()
    {
        if (yourScript.tag == "is_selected" && yourScript.is_cored == true && yourScript.is_colonized == false)
        {
            is_enabled = false;
            button.gameObject.SetActive(true);
            inputField.gameObject.SetActive(true);
            yourScript.is_colonized = true;
            yourScript.owner_id = 1;
            yourScript.pops = 1;
        }
    } 
    public void confirm()
    {
            yourScript.name_ = inputField.text;
            inputField.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            inputField.text = "";
            is_enabled = true;
    }
    public void rename()
    {
        
    }
}
