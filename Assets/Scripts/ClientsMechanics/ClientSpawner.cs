using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private float spawnInterwal;
    [SerializeField] private Transform enterExit;
    [SerializeField] private int maxLenght;

    private void Start()
    {
        InvokeRepeating("Spawn", 0f, spawnInterwal);
    }

    private void Spawn()
    {
        Customer[] clients = FindObjectsOfType<Customer>();

        if(clients.Length < maxLenght)
        {
            Instantiate(customer, enterExit.position, Quaternion.identity);
        }
    }
}
