using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    PlanetStats planeta;
    private static Main instance;
    public List<PlanetStats> player_planets = new List<PlanetStats>();
    public List<PlanetStats> all_planets = new List<PlanetStats>();
    public TMP_Text player_text;
    private int playerid;
    int totalfood = 0;
    int totaleconomy = 0;
    int totalresearch = 0;
    int totalpops = 0;
    int c_food = 0;
    int c_economy = 0;
    int c_research = 0;
    int c_pops = 0;
    public bool selecting = false;
    private float game_time = 0f;
    private float delta_time = 0f;
    private bool time_toggled = false;
    private int current_month = 1;
    private int current_year = 1;
    float timescale = 1f;
    private List<string> planetNames = new List<string>();
    private bool start = false;
    public static Main GetInstance()
    {
        return instance;
    }
    private void Start()
    {
        instance = this;
        UpdatePlanetList();
        GameObject go = GameObject.Find("building_check");
        go.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (player_planets != null && start == false)
        {
            int planets_count;
            planets_count = all_planets.Count;
            int r = UnityEngine.Random.Range(1, planets_count);
            Debug.Log(r);
            GameObject k = GameObject.Find(r.ToString());
            PlanetStats p = k.GetComponent<PlanetStats>();
            if (p != null && !player_planets.Contains(p))
            {
                player_planets.Add(p);
                foreach (PlanetStats planet in player_planets)
                {
                    planet.owner_id = 1;
                    planet.is_cored = true;
                    planet.is_colonized = true;
                    planet.pops = 3;
                    planet.research = 1;
                    start = true;
                }
            }
        }
        if (time_toggled == true)
        {
            timescale = 1f;
        }
        else if (time_toggled == false)
        {
            timescale = 0f;
        }
            delta_time = Time.deltaTime * timescale;
            game_time += delta_time;
            if (game_time >= 3f)
            {
                handle_month();
                current_month++;
                if (current_month > 12)
                {
                    current_month = 1;
                    current_year++;
                }
                game_time = 0f;
            Debug.Log(delta_time);
            }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (time_toggled == true)
            {
                time_toggled = false;
                Debug.Log(time_toggled);
            }
            else if (time_toggled == false)
            {
                time_toggled = true;
                Debug.Log(time_toggled);
            }
        }
        if (all_planets != null)
        {
            player_planet_claiming();
        }
        if(player_planets != null)
        {
            player_stats();
            player_text.text = "Food: " + totalfood + " Economy: " + totaleconomy + " Research: " + totalresearch + " Population: " + totalpops + " Total Food: " + c_food + " Total Economy: " + c_economy; 
        }
        if(Input.GetKeyDown(KeyCode.Backspace)) 
        {
            Debug.Log(Main.GetInstance().selecting);        
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
                if (planeta != null && planeta.tag != "is_selected")
                {
                    GameObject gop = GameObject.FindGameObjectWithTag("is_selected");
                    {
                        if(gop != null)
                        {
                            gop.tag = "Untagged";
                        }
                    }
                    planeta.tag = "is_selected";
                    Debug.Log(planeta.tag);
                }
            }
            else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                GameObject gop = GameObject.FindGameObjectWithTag("is_selected");
                if (gop != null && !EventSystem.current.IsPointerOverGameObject())
                {
                    gop.tag = "Untagged";
                }
                GameObject go = GameObject.FindGameObjectWithTag("planet_stats");
                TMP_Text text = go.GetComponent<TMP_Text>();
                text.text = "";
                Debug.Log(planeta.tag);
            }
        }
        if(planeta != null)
        {
            if (planeta.tag == "is_selected")
            {
                GameObject go = GameObject.FindGameObjectWithTag("planet_stats");
                TMP_Text text = go.GetComponent<TMP_Text>();
                text.text = "Name: " + planeta.name_ + "\nFood: " + planeta.food + "\nEconomy: " + planeta.economy + "\nResearch: " + planeta.research + "\nPopulation: " + planeta.pops + "\nIs Claimed: " + planeta.cored + "\nIs Colonized: " + planeta.colonized;
            }
        }
    }
    public void LoadPlanetNames()
    {
        TextAsset planetNamesFile = Resources.Load<TextAsset>("planet_names");
        if (planetNamesFile != null)
        {
            string[] namesArray = planetNamesFile.text.Split(new[] { "\", \"" }, System.StringSplitOptions.None);
            foreach (string name in namesArray)
            {
                planetNames.Add(name.Trim('"'));
            }
        }
        else
        {
            Debug.LogWarning("planet_names.txt not found in Resources folder.");
        }
    }
    public string GetRandomPlanetName()
    {
        if (planetNames.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, planetNames.Count);
            return planetNames[randomIndex];
        }
        else
        {
            return "Default Planet Name";
        }
    }
    void handle_month()
    {
        c_food += totalfood;
        c_economy += totaleconomy;
        foreach (PlanetStats planeta in player_planets)
        {
            if(planeta.pops >= 1)
            {
                float popsf;
                float foodf;
                float economyf;
                popsf = planeta.pops;
                foodf = planeta.food;
                economyf = planeta.economy;
                popsf += UnityEngine.Mathf.Floor(((foodf + economyf + 3)/3));
                planeta.pops = (int)popsf;
                c_pops += planeta.pops;
            }
        }
        totalpops = c_pops;
        GameObject go = GameObject.Find("game_time");
        go.GetComponent<TMP_Text>().text = "Y. " + current_year + " M. " + current_month;
    }
    public void pause()
    {
            if (time_toggled == true)
            {
                time_toggled = false;
                Debug.Log(time_toggled);
            }
            else if (time_toggled == false)
            {
                time_toggled = true;
                Debug.Log(time_toggled);
            }
    }
    void player_planet_claiming()
    {
        foreach (PlanetStats planet in all_planets)
        {
            if(planet.is_colonized == true)
            {
                if(!player_planets.Contains(planet))
                {
                    player_planets.Add(planet);
                }
            }
        }
    }
    void player_stats()
    {
        foreach (PlanetStats planet in player_planets)
        {
            if (planet.is_in_list == false)
            {
                totalfood += planet.food;
                totaleconomy += planet.economy;
                totalresearch += planet.research;
                totalpops += planet.pops;
            }
            planet.is_in_list = true;
        }
    }
    void UpdatePlanetList()
    {
        all_planets.Clear();
        all_planets.AddRange(FindObjectsOfType<PlanetStats>());
    }
}
