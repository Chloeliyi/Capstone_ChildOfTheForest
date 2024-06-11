using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class TimeManager : MonoBehaviour
{

    public static TimeManager Instance;

    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;
 
    [SerializeField] private Gradient graddientNightToSunrise;
    [SerializeField] private Gradient graddientSunriseToDay;
    [SerializeField] private Gradient graddientDayToSunset;
    [SerializeField] private Gradient graddientSunsetToNight;
 
    [SerializeField] private Light globalLight;
    
    [SerializeField] private int minutes;
 
    public int Minutes
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    public int hours;
 
    public int Hours
    { get { return hours; } set { hours = value; OnHoursChange(value); } }
 
    [SerializeField] private int days;
 
    public int Days
    { get { return days; } set { days = value; } }
 
    [SerializeField] private float tempSecond;

    [SerializeField] private Texture2D StartSkybox;

    [SerializeField] private Texture2D NextSkybox;

    void Start()
    {
        RenderSettings.skybox.SetFloat("_Blend", 0);
        hours = 19;

        OnHoursChange(hours);

        if (hours >= 8 && hours <= 18)
        {
            Debug.Log("Day");
            StartSkybox = skyboxDay;

            NextSkybox = skyboxSunset;

            RenderSettings.skybox.SetTexture("_Texture1", StartSkybox);

            RenderSettings.skybox.SetTexture("_Texture2", NextSkybox);
        }

        else if (hours >= 18 && hours <= 20 )
        {
            Debug.Log("Sunset");
            StartSkybox = skyboxSunset;

            NextSkybox = skyboxNight;

            RenderSettings.skybox.SetTexture("_Texture1", StartSkybox);

            RenderSettings.skybox.SetTexture("_Texture2", NextSkybox);
        }

        else if (hours >= 20 || hours <= 6)
        {
            Debug.Log("Night");
            StartSkybox = skyboxNight;

            NextSkybox = skyboxSunrise;

            RenderSettings.skybox.SetTexture("_Texture1", StartSkybox);

            RenderSettings.skybox.SetTexture("_Texture2", NextSkybox);
        }

        else if (hours >= 6 && hours <= 8)
        {
            Debug.Log("Sunrise");
            StartSkybox = skyboxSunrise;

            NextSkybox = skyboxDay;

            RenderSettings.skybox.SetTexture("_Texture1", StartSkybox);

            RenderSettings.skybox.SetTexture("_Texture2", NextSkybox);
        }
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    public void Update()
    {
        tempSecond += Time.deltaTime;
 
        if (tempSecond >= 1)
        {
            Minutes += 1;
            tempSecond = 0;
        }
    }
 
    private void OnMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f / (1440f / 4f)) * 360f, Space.World);
        if (value == 30)
        {
            
        }
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
            Debug.Log("Take food damage");
            Debug.Log("Take Water damage");
            GameManager.Instance.TakeFoodDamage(2);
            GameManager.Instance.TakeWaterDamage(2);
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }
 
    private void OnHoursChange(int value)
    {
        if (value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 1f));
            StartCoroutine(LerpLight(graddientNightToSunrise, 1f));
        }
        else if (value == 8)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 1f));
            StartCoroutine(LerpLight(graddientSunriseToDay, 1f));
        }
        else if (value == 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 1f));
            StartCoroutine(LerpLight(graddientDayToSunset, 1f));
        }
        else if (value == 20)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 1f));
            StartCoroutine(LerpLight(graddientSunsetToNight, 1f));

            // Popup here;
            Debug.Log("Night is coming");
        }
    }
 
    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }
 
    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            //RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }
}
