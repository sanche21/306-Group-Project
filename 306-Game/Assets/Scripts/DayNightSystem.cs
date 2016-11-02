﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNightSystem : MonoBehaviour {

    [SerializeField]
    Stat day;
    [SerializeField]
    Stat night;
    [SerializeField]
    Image overLay;
    [SerializeField]
    float dayLength;
    [SerializeField]
    int numDays;

    bool isDay;
    float timeLimit;

    // Use this for initialization
    void Start()
    {
        isDay = true;
        timeLimit = numDays * 2 * dayLength;
        InvokeRepeating("UpdateTime", 1, 1);
        InvokeRepeating("ChangeTimeType", 0, dayLength);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Awake()
    {
        day.MaxVal = dayLength;
        night.MaxVal = dayLength;
        day.Initialize();
        night.Initialize();
    }


    // Changes the games time from day to night or vice versa
    void ChangeTimeType()
    {
        if (isDay)
        {
            isDay = false;
            // Makes the overlay disappear so that everything appears lighter
            overLay.GetComponent<CanvasGroup>().alpha = 0;
            //Changes from keeping track of how much night is left to how much day is left
            night.Bar.GetComponent<CanvasGroup>().alpha = 0;
            day.Bar.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            isDay = true;
            // Makes the overlay appear so that everything appears darker
            overLay.GetComponent<CanvasGroup>().alpha = 1;
            //Changes from keeping track of how much day is left to how much night is left
            day.Bar.GetComponent<CanvasGroup>().alpha = 0;
            night.Bar.GetComponent<CanvasGroup>().alpha = 1;
        }
        UpdateNPCs();
    }

    // counts how long the day or night has lasted
    // night.MaxVal - night.CurrentVal is how close the night is
    // day.MaxVal - day.CurrentVal is how close the night is
    void UpdateTime()
    {
        if (isDay)
        {
            night.CurrentVal++;
            day.CurrentVal = 0;
        }
        else
        {
            day.CurrentVal++;
            night.CurrentVal = 0;
        }
    }

    //tells NPCs to switch between friendly and enemy forms
    void UpdateNPCs()
    {
        GameObject[] objects;
        objects = GameObject.FindGameObjectsWithTag("NPC");

        for(int i =0; i< objects.Length; i++)
        {
            objects[i].SendMessage("ChangeForm");
        }

    }
}
