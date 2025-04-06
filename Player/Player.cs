using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject mouseIndicator;

    [SerializeField]
    private InputManager inputManager;

    public GameObject MousePointAt;
    public Entity PlayerChoice;
    public Entity AttObj;
    public bool PressM = false;

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetSelectedMapPosition(out MousePointAt);
        mouseIndicator.transform.position = mousePosition;
        if(MousePointAt != null && Input.GetMouseButtonDown(0) && MousePointAt.layer == 6)
        {
            if (PlayerChoice != null) PlayerChoice.UnClicked();
            PlayerChoice = MousePointAt.GetComponent<Entity>();
            PlayerChoice.Clicked();
        }

    }

    private void DealAttack()
    {

    }

}
