using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    [SerializeField] int id;

    private void OnMouseDown()
    {
        EventManager.OpenDoor(id);
    }
}
