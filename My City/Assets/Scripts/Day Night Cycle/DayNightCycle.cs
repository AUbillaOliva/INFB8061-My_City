using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time")]
    [Tooltip("Day Length in Minutes")]
    [SerializeField]
    private float _targetDayLength = 0.5f; // Length of day in minutes
    public float targetDayLength
    {
        get
        {
            return _targetDayLength;
        }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }

    [SerializeField]
    private int _dayNumber = 0;
    public int dayNumber // Tracks the days passed
    {
        get
        {
            return _dayNumber;
        }
    }

    [SerializeField]
    private int _yearNumber = 0; // Tracks the years passed
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }

    private float _timeScale = 100f;
    [SerializeField]
    private int _yearLenght = 100;
    public float yearLength
    {
        get
        {
            return _yearLenght;
        }
    }

    public bool pause = false;

    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            AdjustSunRotation();
            AdjustMoonRotation();
            SunIntensity();
            MoonIntensity();
            AdjustSunColor();
            AdjustMoonColor();
        }
        if(IsDay())
        {
            // FindObjectOfType<AudioManager>().Play("DayCitySounds");
            // FindObjectOfType<AudioManager>().Stop("Crickets");
        } else if(!IsDay())
        {
            // FindObjectOfType<AudioManager>().Play("Crickets");
            // FindObjectOfType<AudioManager>().Stop("DayCitySounds");
            
        }
        
    }

    private void UpdateTimeScale()
    {
        _timeScale = 24 / (_targetDayLength / 60);
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400; // Second in a day
        if(_timeOfDay > 1) // New day
        {
            _dayNumber++;
            _timeOfDay -= 1;

            if (_dayNumber > _yearLenght) // New year
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    public bool IsDay()
    {
        // FindObjectOfType<AudioManager>().Play("");
        if (_timeOfDay < 0.75f && _timeOfDay > 0.28f)
        {
            return true;
        }
        return false;
    }

    [Header("Sun Light")]
    [SerializeField]
    private Transform dailyRotation;
    [SerializeField]
    private Light sun;
    private float sunIntensity;
    [SerializeField]
    private float sunBaseIntensity = 1f;
    [SerializeField]
    private float sunVariation = 1.5f;

    private void SunIntensity()
    {
        sunIntensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        sunIntensity = Mathf.Clamp01(sunIntensity);

        sun.intensity = sunIntensity * sunVariation + sunBaseIntensity;
    }

    private void AdjustSunRotation()
    {
        float sunAngle = timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, sunAngle));
    }

    [SerializeField]
    private Gradient sunColor;
    private void AdjustSunColor()
    {
        sun.color = sunColor.Evaluate(sunIntensity);
    }

    [Header("Moon Light")]
    [SerializeField]
    private Transform moonlyRotation;
    [SerializeField]
    private Light moon;
    private float moonIntensity;
    [SerializeField]
    private float moonBaseIntensity = 1f;
    [SerializeField]
    private float moonVariation = 1.5f;

    private void MoonIntensity()
    {
        moonIntensity = Vector3.Dot(moon.transform.forward, Vector3.down);
        moonIntensity = Mathf.Clamp01(moonIntensity);

        moon.intensity = moonIntensity * moonVariation + moonBaseIntensity;
    }

    private void AdjustMoonRotation()
    {
        float moonAngle = timeOfDay * 360f;
        moonlyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, moonAngle));

    }

    [SerializeField]
    private Gradient moonColor;
    private void AdjustMoonColor()
    {
        moon.color = moonColor.Evaluate(moonIntensity);
    }
}
