using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dager : MonoBehaviour
{
    [SerializeField] GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectile, Camera.main.transform.position, Camera.main.transform.rotation);
        }
    }
}
