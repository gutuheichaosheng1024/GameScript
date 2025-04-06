using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator, cellIndicator;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private Grid grid;
    private GameObject HitObject;

    [SerializeField]
    private ObjectsDatabaseSo database;
    private int selectObjectIndex = -1;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private AudioSource audioSource;


    private GridData floorData,funitureData;
    private Renderer preViewRender;

    private List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        funitureData = new GridData();
        preViewRender = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        selectObjectIndex = database.objectsData.FindIndex(data => data.ID==ID);
        if(selectObjectIndex < 0)
        {
            Debug.LogError($"No such ID Like {ID}");
            return;
        }
        gridVisualization.SetActive(true);
        cellIndicator.SetActive(true);
        inputManager.Onclicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceStructure()
    {
        if (inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition(out HitObject);
        Vector3Int gridPos = grid.WorldToCell(mousePosition);

        bool placementAvailable = CheckPlacementAvailable(gridPos,selectObjectIndex);
        if (!placementAvailable) return;
        audioSource.Play();
        GameObject PlaceObject = Instantiate(database.objectsData[selectObjectIndex].Perfab);
        gameObjects.Add(PlaceObject);
        PlaceObject.transform.position = grid.CellToWorld(gridPos);
        GridData selectData = database.objectsData[selectObjectIndex].ID == 0 ? floorData : funitureData;
        selectData.AddObjectAt(gridPos, database.objectsData[selectObjectIndex].Size, database.objectsData[selectObjectIndex].ID, gameObjects.Count- 1);
    }

    private bool CheckPlacementAvailable(Vector3Int gridPos, int selectObjectIndex)
    {
        GridData selectData = database.objectsData[selectObjectIndex].ID == 0 ? floorData : funitureData;


        return selectData.CanPlaceObjectAt(gridPos, database.objectsData[selectObjectIndex].Size);
    }

    private void StopPlacement()
    {
        selectObjectIndex = -1;
        gridVisualization.SetActive(false);
        cellIndicator.SetActive(false);
        inputManager.Onclicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
    }

    private void Update()
    {
        if (selectObjectIndex < 0) return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition(out HitObject);
        Vector3Int gridPos = grid.WorldToCell(mousePosition);

        bool placementAvailable = CheckPlacementAvailable(gridPos, selectObjectIndex);
        preViewRender.material.color = placementAvailable ? Color.white : Color.red;

        mouseIndicator.transform.position = mousePosition;
        cellIndicator.transform.position = grid.CellToWorld(gridPos);
    }
}
