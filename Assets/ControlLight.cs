using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeLightRedToWhite()
    {
        Light  light = gameObject.GetComponent(typeof(Light)) as Light;
    }
}
