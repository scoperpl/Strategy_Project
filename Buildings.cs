using JetBrains.Annotations;
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

public class Buildings : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    public Button farm_button;
    public Button glassfarm_button;
    public Button factory_button;
    public Button mine_button;
    public Image planet_check;
    bool is_clicked = false;
    // Start is called before the first frame update
    void Start()
    {
        farm_button.gameObject.SetActive(false);
        glassfarm_button.gameObject.SetActive(false);
        factory_button.gameObject.SetActive(false);
        mine_button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyUp(KeyCode.L)) 
       {
            Debug.Log(dropdown.value); 
       }
        dropdown_handler();
    }

    public void dropdown_handler()
    {
        GameObject go = GameObject.FindGameObjectWithTag("is_selected");
        if (dropdown.options!= null && go != null)
        {
            PlanetStats planet = go.GetComponent<PlanetStats>();
            if (dropdown.value == 0)
            {
                farm_button.gameObject.SetActive(true);
                glassfarm_button.gameObject.SetActive(true);
                factory_button.gameObject.SetActive(false);
                mine_button.gameObject.SetActive(false);
            }
            else if (dropdown.value == 1)
            {
                factory_button.gameObject.SetActive(true);
                mine_button.gameObject.SetActive(true);
                farm_button.gameObject.SetActive(false);
                glassfarm_button.gameObject.SetActive(false);
            }
            else
            {
                farm_button.gameObject.SetActive(false);
                glassfarm_button.gameObject.SetActive(false);
                factory_button.gameObject.SetActive(false);
                mine_button.gameObject.SetActive(false);
            }
        }
        else
        {
        farm_button.gameObject.SetActive(false);
        glassfarm_button.gameObject.SetActive(false);
        factory_button.gameObject.SetActive(false);
        mine_button.gameObject.SetActive(false);
        }
    }

    public interface IBuilding
    {
        string GetCategory();
        bool IsBuilded();
        float GetCost();
        int GetLevel();
    }

    public class Building : IBuilding
    {
        public string category;
        public bool builded;
        public float cost;
        public int level;

        public string GetCategory()
        {
            return category;
        }

        public bool IsBuilded()
        {
            return builded;
        }

        public float GetCost()
        {
            return cost;
        }

        public int GetLevel()
        {
            return level;
        }
    }
    public class Farms : Building
    {
        public float natural_food;
        public float static_food;

        public Farms()
        {
            category = "food_industry";
            builded = false;
            cost = 100;
            level = 1;
            natural_food = 0.10f;
            static_food = 0;
        }
    }

    public class GlassFarm : Building
    {
        public float natural_food;
        public float static_food;

        public GlassFarm()
        {
            category = "food_industry";
            builded = false;
            cost = 1000;
            level = 1;
            natural_food = 0;
            static_food = 5;
        }
    }
    
    public class Factory : Building
    {
        public float natural_economy;
        public float static_economy;

        public Factory()
        {
            category = "buisness_industry";
            builded = false;
            cost = 2500;
            level = 1;
            natural_economy = 0;
            static_economy = 10;
        }
    }
    public class Mine : Building
    {
        public float natural_economy;
        public float static_economy;
        public Mine()
        {
            category = "buisness_industry";
            builded = false;
            cost = 5000;
            level = 1;
            natural_economy = 0.2f;
            static_economy = 0;
        }
    }
    public void farmb()
    {
        bool farmclicked = false;
        if (is_clicked == false && farmclicked == false)
        {
            farmclicked = true;
            Color color;
            color = farm_button.colors.normalColor;
            farm_button.image.color = farm_button.colors.pressedColor;
            is_clicked = true;
        }
        else if (is_clicked == true && farmclicked == true)
        {
            farmclicked = false;
            Color color;
            color = farm_button.colors.normalColor;
            farm_button.image.color = color;
            is_clicked = false;
        }
    }
    public void glassfarmb()
    {
        if(is_clicked == false)
        { 
            Color color;
            color = glassfarm_button.colors.normalColor;
            glassfarm_button.image.color = glassfarm_button.colors.pressedColor;
            is_clicked = true;
        }
    }
}
