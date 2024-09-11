using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public GameObject holding;

    [SerializeField] float range;
    [SerializeField] LayerMask pickUps;

    public void OnPickUp()
    {
        RaycastHit hit;
        if(Physics.BoxCast(Camera.main.transform.position, new Vector3(1, 1, 1), Camera.main.transform.forward, out hit, Camera.main.transform.rotation, range, pickUps))
        {
            if (hit.collider.gameObject.CompareTag("pickUp"))
            {
                Debug.Log("pickup");
            }
        }
    }
}
