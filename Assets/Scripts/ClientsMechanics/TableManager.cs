using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public Table[] tables;

    private void Awake()
    {
        tables = FindObjectsOfType<Table>();
    }

    public Table GetNearestFreeTable(Vector3 position)
    {
        Table nearestTable = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Table table in tables)
        {
            if (!table.isOccupied)
            {
                float distance = Vector3.Distance(position, table.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestTable = table;
                }
            }
        }

        return nearestTable;
    }
}
