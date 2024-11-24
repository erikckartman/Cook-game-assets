using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SpawnProducts
{
    public string productName;
    public Button spawnButton;
    public GameObject productPrefab;
    public double priceOfProduct;
}

public class Market : MonoBehaviour
{
    [SerializeField] private List<SpawnProducts> spawners;
    [HideInInspector]public Vector3 spawnPosition;
    [SerializeField] private KeyCode keyToOpen;
    [SerializeField] private GameObject buttonPanel;
    public bool openCloseMarket = false;

    private void Awake()
    {
        spawnPosition = new Vector3(transform.position.x, transform.position.y + (transform.localScale.y / 2), transform.localScale.z);

        foreach (var spawner in spawners)
        {
            if (spawner.spawnButton != null)
            {
                var buttonHandler = spawner.spawnButton.gameObject.AddComponent<SpawnerButtonScript>();

                buttonHandler.Setup(spawner.productPrefab, spawnPosition, spawner.priceOfProduct);

                spawner.spawnButton.onClick.AddListener(buttonHandler.SpawnProduct);
            }
            else
            {
                Debug.LogWarning($"Product button {spawner.productName} = null!");
            }
        }
    }

    private void Update()
    {
        if(openCloseMarket && Input.GetKeyDown(keyToOpen) && !buttonPanel.activeInHierarchy)
        {
            buttonPanel.SetActive(true);
        }
        else if(!openCloseMarket || openCloseMarket && Input.GetKeyDown(keyToOpen) && buttonPanel.activeInHierarchy)
        {
            buttonPanel.SetActive(false);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            openCloseMarket = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        openCloseMarket = false;
    }
}