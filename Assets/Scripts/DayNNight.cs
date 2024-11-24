using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNNight : MonoBehaviour
{
    public Light sunLight; 
    public Gradient skyColor;
    public AnimationCurve lightIntensityCurve; 

    [Range(0, 24)] public float timeOfDay = 12; 
    public float dayDurationInSeconds = 120;
    private float timeSpeed; 

    private void Start()
    {
        timeSpeed = 24f / dayDurationInSeconds; 
    }

    private void Update()
    {
        timeOfDay += timeSpeed * Time.deltaTime;
        if (timeOfDay > 24) timeOfDay -= 24; 

        // Зміна напрямку світла
        float sunRotation = (timeOfDay / 24f) * 360f; // Від 0 до 360 градусів
        sunLight.transform.rotation = Quaternion.Euler(new Vector3(sunRotation - 90, 170, 0));

        // Зміна кольору неба
        RenderSettings.ambientLight = skyColor.Evaluate(timeOfDay / 24f);

        // Зміна інтенсивності світла
        sunLight.intensity = lightIntensityCurve.Evaluate(timeOfDay / 24f);
    }

}
