using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenShake : MonoBehaviour
{

    float shakeTimeRemaining, shakePower, shakeFade, shakeRotaiton;
    float rotationMult;


    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.localPosition = new Vector3(xAmount, yAmount, -10f);
            transform.rotation = Quaternion.Euler(0f, 0f, shakeRotaiton * Random.Range(-1f,1f));

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFade * Time.deltaTime);

            shakeRotaiton = Mathf.MoveTowards(shakeRotaiton, 0f, shakeFade * rotationMult * Time.deltaTime);
        }
        else
        {
            transform.localPosition = new Vector3(0,0,-10);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void StartShake(float lenght, float power, float rotation)
    {
        shakeTimeRemaining = lenght;
        shakePower = power;
        shakeFade = power / lenght;
        rotationMult = rotation;
        shakeRotaiton = rotationMult * power;
    }
}
