using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Buildings;

public class PlanetStats : MonoBehaviour
{
        private PlanetStats planeta;
        private TMP_Text targetText;
        public int id;
        public int owner_id;
        public string name_;
        public bool is_cored = false;
        public bool is_colonized = false;
        public int food;
        public int economy;
        public int pops;
        public int research;
        public string cored="";
        public string colonized="";
        public bool is_selected;
        public string targetTag = "planet_stats";
        public bool is_in_list = false;
        public List<IBuilding> buildings_list = new List<IBuilding>();
    // Start is called before the first frame update
    void Start()
    {
        Main.GetInstance().LoadPlanetNames();
        GameObject text = GameObject.FindWithTag("planet_stats");
        targetText = text.GetComponent<TMP_Text>();
        id = Int32.Parse(gameObject.name);
        owner_id = 0;
        name_ = Main.GetInstance().GetRandomPlanetName();
        is_cored = false;
        is_colonized = false;
        food = UnityEngine.Random.Range(1,10);
        economy = UnityEngine.Random.Range(1, 10);
        pops = 0;
        research = 0;
        Buildings.Farms farms_building = new Buildings.Farms();
        Buildings.GlassFarm glassfarm_building = new Buildings.GlassFarm();
        Buildings.Mine mine_building = new Buildings.Mine();
        Buildings.Factory factory_building = new Buildings.Factory();

        buildings_list.Add(farms_building);
        buildings_list.Add(glassfarm_building);
        buildings_list.Add(mine_building);
        buildings_list.Add(factory_building);
    }
    void UpdatePlanetList()
    {
        Main mainScript = FindObjectOfType<Main>();
        if (mainScript != null)
        {
            List<PlanetStats> allPlanets = mainScript.all_planets;

            allPlanets.Add(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (is_cored == false)
        {
            cored = "no";
        }
        else
        {
            cored = "yes";
        }
        if (is_colonized == false)
        {
            colonized = "no";
        }
        else
        {
            colonized = "yes";
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            is_selected = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UnityEngine.Vector2 clickPosition2D = new UnityEngine.Vector2(clickPosition.x, clickPosition.y);

            RaycastHit2D hit = Physics2D.Raycast(clickPosition2D, UnityEngine.Vector2.zero);

            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;
                planeta = clickedObject.GetComponent<PlanetStats>();
                if (planeta == null)
                {
                    gameObject.tag = "Untagged";
                }
            }
        }
    }
}
