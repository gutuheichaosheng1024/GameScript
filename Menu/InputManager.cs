using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Camera SceneCarema;

    private Vector3 lastPosition;

    [SerializeField]
    private LayerMask PlacementMask;

    public event Action Onclicked, OnExit;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Onclicked?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnExit?.Invoke();
        }
    }

    public bool IsPointerOverUI() => EventSystem.current.IsPointerOverGameObject();

    public Vector3 GetSelectedMapPosition(out GameObject HitObject)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = SceneCarema.nearClipPlane;
        Ray ray = SceneCarema.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 100, PlacementMask))
        {
            lastPosition = hit.point;
            HitObject = hit.collider.gameObject;
        }
        else HitObject = null;
        return lastPosition;
    }
}
