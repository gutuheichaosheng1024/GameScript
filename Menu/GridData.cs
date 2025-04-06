using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new Dictionary<Vector3Int, PlacementData>();
    

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize,int ID,int placementIndex)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData Data = new PlacementData(positionToOccupy, ID, placementIndex);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position))
            {
                throw new Exception($"Dictionary already contains this cell positon in {position}");
            }
            placedObjects[position] = Data;
        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnval = new();
        for(int x = 0; x < objectSize.x; x++)
        {
            for(int y = 0; y < objectSize.y; y++)
            {
                returnval.Add(gridPosition + new Vector3Int(x,0,y));
            }
        }
        return returnval;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var position in positionToOccupy)
        {
            if (placedObjects.ContainsKey(position)) return false;
        }
        return true;
    }
}


public class PlacementData
{
    public List<Vector3Int> occupiedPositions;

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placeObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlaceObjectIndex = placeObjectIndex;
    }

    public int ID { get; private set; }
    public int PlaceObjectIndex {  get; private set; }
}