using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float arrivalDistance;
    [SerializeField] private float waitingTime;
    [SerializeField] private TableManager tableManager;
    [SerializeField] private Transform exit; //Model of exit doors should be prefab
    [SerializeField] private string[] names;

    [Header("Client's reactions")]
    [SerializeField] private string clientIsSatisficed;
    [SerializeField] private string clientGetsWrongOrder;
    [SerializeField] private string clientIsTiredOfWaiting;

    private Cooker cooker;
    private Rating rating;

    private Table targetTable;
    private bool isMoving = false;
    private string order;
    private bool isDone = false;
    public float waiting = 0;

    private void Awake()
    {
        if(name != null && names.Length > 0)
        {
            GetName();
        }

        cooker = FindObjectOfType<Cooker>();
        rating = FindObjectOfType<Rating>();
    }

    private void Update()
    {
        if (isMoving && targetTable != null)
        {
            MoveTowardsTable();
        }
        else if(!isMoving && targetTable == null && !isDone)
        {
            FindAndGoToTable();
        }
        else if (isDone)
        {
            GoBack();
        }

        if(targetTable != null)
        {
            TakeOrder();
        }

        if(targetTable != null && targetTable.dish == null)
        {
            waiting += Time.deltaTime;
        }
    }

    private void FindAndGoToTable()
    {
        targetTable = tableManager.GetNearestFreeTable(transform.position);

        if (targetTable != null)
        {
            targetTable.isOccupied = true;
            isMoving = true;
        }
    }

    private void MoveTowardsTable()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTable.transform.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetTable.transform.position) <= arrivalDistance)
        {
            isMoving = false;
            Debug.Log("Клієнт прибув за столик!");
            MakeOrder();
        }
    }

    private void MakeOrder()
    {
        if(cooker.recipes.Count > 0)
        {
            int randomIndex = Random.Range(0, cooker.recipes.Count);
            Recipe orderMade = cooker.recipes[randomIndex];
            order = orderMade.dishName;
            Debug.Log($"{gameObject.name} orders {order}");
        }
    }

    private void TakeOrder()
    {
        if (order != null && targetTable.dish != null && waiting <= waitingTime)
        {
            if(targetTable.dish.name.Contains(order))
            {
                Destroy(targetTable.dish.gameObject);
                Debug.Log($"{gameObject.name} says: {clientIsSatisficed}");
                targetTable.isOccupied = false;
                isDone = true;
                targetTable = null;
                int rateByClient = Random.Range(4, 5);
                rating.GetTotalRate(rateByClient);
            }
            else
            {
                Debug.Log($"{gameObject.name} says: {clientGetsWrongOrder}");
                targetTable.isOccupied = false;
                isDone = true;
                targetTable = null;
                rating.rates.Add(0);
                rating.GetTotalRate(0);
            }
        }
        else if(order != null && targetTable.dish == null && waiting > waitingTime)
        {
            Debug.Log($"{gameObject.name} says: {clientIsTiredOfWaiting}");
            targetTable.isOccupied = false;
            isDone = true;
            targetTable = null;
            rating.GetTotalRate(0);
        }
    }

    private void GoBack()
    {        
        transform.position = Vector3.MoveTowards(transform.position, exit.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, exit.position) <= arrivalDistance)
        {
            Destroy(gameObject);
        }
    }

    private void GetName()
    {
        int index = Random.Range(0, names.Length);
        gameObject.name = names[index];
    }
}