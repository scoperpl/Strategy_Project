using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class PlanetStats : MonoBehaviour
{
    private TMP_Text text;
        public int id;
        public int owner_id;
        public string name_;
        public bool is_cored = false;
        public bool is_colonized = false;
        public int food;
        public int economy;
        public int pops;
        public int research;
        string cored="";
        string colonized="";
        public bool is_selected;
    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<TMP_Text>();
        id = Int32.Parse(gameObject.name);
        owner_id = 0;
        name_ = " ";
        is_cored = false;
        is_colonized = false;
        food = UnityEngine.Random.Range(1,10);
        economy = UnityEngine.Random.Range(1, 10);
        pops = 0;
        research = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            is_selected = false;
        }
        if (is_cored == true)
        {
            cored = "yes";
        }
        else if (is_cored == false)
        {
            cored = "no";
        }
        if (is_colonized == true)
        {
            colonized = "yes";
        }
        else if (is_colonized == false)
        {
            colonized = "no";
        }
        if (is_selected == true)
        {
            text.text = "Name: " + name_ + "\nFood: " + food + "\nEconomy: " + economy + "\nPopulation: " + pops + "\nResearch Value: " + research + "\nClaimed: " + cored + "\nColonized: " + colonized;
        }
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UnityEngine.Vector2 clickPosition2D = new UnityEngine.Vector2(clickPosition.x, clickPosition.y);

            // Wykonaj raycast, aby wykryæ klikniêcie na obiekcie.
            RaycastHit2D hit = Physics2D.Raycast(clickPosition2D, UnityEngine.Vector2.zero);
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (hit.collider == null)

                {

                    is_selected = false;

                    text.text = "";

                }

                else if (hit.collider != null)

                {

                    GameObject clickedObject = hit.collider.gameObject;

                    PlanetStats yourScript = clickedObject.GetComponent<PlanetStats>();

                    if (yourScript != null)

                    {

                        int idcheck = yourScript.id;

                        if (id != idcheck)

                        {

                            is_selected = false;

                        }

                    }
                } 
            }
        }

    }
    private void OnMouseDown()
    {
      is_selected = true;
    }
}
