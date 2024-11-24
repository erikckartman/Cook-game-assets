using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerButtonScript : MonoBehaviour
{
    private MoneySystem moneySystem;
    private GameObject productPrefab;
    private double price;
    private Vector3 spawnPosition;

    public void Setup(GameObject prefab, Vector3 position, double getPrice)
    {
        productPrefab = prefab;
        spawnPosition = position;
        price = getPrice;
    }

    private void Awake()
    {
        moneySystem = FindAnyObjectByType<MoneySystem>();
    }

    public void SpawnProduct()
    {
        if (productPrefab != null && moneySystem != null)
        {
            if(moneySystem.HaveEnoughMoney(price))
            {
                Instantiate(productPrefab, spawnPosition, Quaternion.identity);
                moneySystem.SpendMoney(price);
            }
            else
            {
                Debug.Log($"You don't have enough money");
            }
        }
        else
        {
            Debug.LogWarning("Prefab = null!");
        }
    }
}
