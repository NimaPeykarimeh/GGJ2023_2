using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Vector3 cameraOffset;
    [SerializeField] Vector3 _cameraOffset;
    [SerializeField] Transform roomPos;
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float cameraSpeed;
    [SerializeField] bool isFollowing;
    [SerializeField] float cameraSizeInRoom;
    [SerializeField] float cameraSizeDefault;
    [SerializeField] float maxOffsetX;
    [SerializeField] float maxOffsetY;


    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    private void Start()
    {

        
    }


    private void FixedUpdate()
    {

        cameraOffset.x =  (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * maxOffsetX;
        cameraOffset.y = (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * maxOffsetY +2;
        cameraOffset.x = Mathf.Clamp(cameraOffset.x, -8.5f, 8.5f);
        cameraOffset.y = Mathf.Clamp(cameraOffset.y, 0, 4);


        if (isFollowing)
        {
            cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, playerPos.position + cameraOffset,cameraSpeed * Time.fixedDeltaTime);
        }
        if (playerPos.position.x < -11)
        {
            isFollowing = false;
            cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position,new Vector3(roomPos.position.x,roomPos.position.y,-10), cameraSpeed *5 * Time.fixedDeltaTime);

            if (GetComponent<Camera>().orthographicSize > cameraSizeInRoom)
            {
                GetComponent<Camera>().orthographicSize -= Time.deltaTime * 2;
            }
            if (GetComponent<Camera>().orthographicSize < cameraSizeInRoom)
            {
                GetComponent<Camera>().orthographicSize = cameraSizeInRoom;
            }
            //GetComponent<Camera>().orthographicSize = cameraSizeInRoom;

        }
        else if(playerPos.position.x >= -11)
        {
            isFollowing = true;

            if (GetComponent<Camera>().orthographicSize < cameraSizeDefault)
            {
                GetComponent<Camera>().orthographicSize += Time.deltaTime * 2;
            }
            if (GetComponent<Camera>().orthographicSize > cameraSizeDefault)
            {
                GetComponent<Camera>().orthographicSize = cameraSizeDefault;
            }
            //GetComponent<Camera>().orthographicSize = cameraSizeDefault;
        }
    }


}
