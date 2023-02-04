using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class lightRandomizer : MonoBehaviour
{
    // Start is called before the first frame update
    Light2D _light;
    [SerializeField] Vector2 _lightRange;
    [SerializeField] float speed;
    [SerializeField] float targetIntensity;
    bool flag;
    void Start()
    {
        targetIntensity = Random.Range(_lightRange[0], _lightRange[1]);
        _light = GetComponent<Light2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if ( Mathf.Abs(_light.intensity - targetIntensity) > 0.1)
        {
            _light.intensity = Mathf.Lerp(_light.intensity, targetIntensity, speed * Time.fixedDeltaTime); 
        }
        else
        {
            targetIntensity = Random.Range(_lightRange[0], _lightRange[1]);
        }
        
  
 
    }
}
