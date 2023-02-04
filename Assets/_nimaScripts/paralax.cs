using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    float lenght, startPos;
    [SerializeField] GameObject _camera;
    [SerializeField] float parallaxEffect;

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = transform.position.x;
        //lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = _camera.transform.position.x * (1 - parallaxEffect);
        float dist = _camera.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        /*
        if (temp > startPos + lenght *2 + 5)
        {
            startPos += lenght *2;
        }
        else if (temp < startPos - lenght *2 - 5)
        {
            startPos -= lenght *2 ;
        }
        */
    }
}
