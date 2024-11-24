using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rating : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rateCanvas;
    [HideInInspector] public List<int> rates;

    public void GetTotalRate(int x)
    {
        rates.Add(x);
        int sum = 0;
        foreach (int number in rates)
        {
            sum += number;
        }

        double average = (double)sum / rates.Count;
        double roundedAverage = Math.Round(average, 1);

        rateCanvas.text = $"Rating: {roundedAverage}";
    }
}
