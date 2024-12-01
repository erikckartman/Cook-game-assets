using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelMaker : MonoBehaviour
{
    [SerializeField] private bool isMakerModeEnabled;
    [SerializeField] private GameObject tablePrefab;
    [SerializeField] private int maxTables;
    [SerializeField] private float yPosition;
    [SerializeField] private float yRotation;
    [SerializeField] private Vector3 areaMin;
    [SerializeField] private Vector3 areaMax;

    [SerializeField]private Table[] tables;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        tables = FindObjectsOfType<Table>();
    }

    private void Update()
    {
        if (isMakerModeEnabled)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 targetPosition = hit.point;

                targetPosition.y = yPosition;

                if (IsWithinPlacementArea(targetPosition))
                {
                    if (Input.GetMouseButtonDown(0) && tables.Length < maxTables)
                    {
                        GameObject table = Instantiate(tablePrefab, targetPosition, Quaternion.Euler(0f, yRotation, 0f));
                        tables = FindObjectsOfType<Table>();
                    }
                    else
                    {
                        Debug.Log("You've placed too much tables");
                    }
                }
            }
        }
    }

    private bool IsWithinPlacementArea(Vector3 position)
    {
        return position.x >= areaMin.x && position.x <= areaMax.x && position.z >= areaMin.z && position.z <= areaMax.z;
    }

    public void TurnOnLevelMaker()
    {
        isMakerModeEnabled = true;
    }

    public void TurnOffLevelMaker()
    {
        isMakerModeEnabled = false;
    }

    public void TurnLevelMaker()
    {
        isMakerModeEnabled = !isMakerModeEnabled;
    }
}
