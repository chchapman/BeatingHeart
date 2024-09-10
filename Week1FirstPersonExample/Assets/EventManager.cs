using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action biggerEvent;
    public static event Action smallerEvent;

    public static event Action<int> OpenDoorEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(biggerEvent != null)
            {
                biggerEvent();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            smallerEvent?.Invoke();
        }
    }

    public static void OpenDoor(int id)
    {
        OpenDoorEvent?.Invoke(id);
    }
}
