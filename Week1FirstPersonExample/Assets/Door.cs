using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int id = 0;

    private void Start()
    {

        EventManager.OpenDoorEvent += OpenDoor;
    }

    void OpenDoor(int id)
    {
        if(this.id == id)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
