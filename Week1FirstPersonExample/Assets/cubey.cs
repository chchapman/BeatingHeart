using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubey : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        EventManager.biggerEvent += bigCubey;
        EventManager.smallerEvent += smolCubey;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void bigCubey()
    {
        transform.localScale *= 2;
    }

    void smolCubey()
    {
        transform.localScale *= .5f;
    }
    private void OnDisable()
    {
        EventManager.biggerEvent -= bigCubey;
        EventManager.smallerEvent -= smolCubey;
    }
}
