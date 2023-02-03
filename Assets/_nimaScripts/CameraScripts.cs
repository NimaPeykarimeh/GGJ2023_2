using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScripts : MonoBehaviour
{
    [SerializeField] GameObject player;
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

    private void Start()
    {
        cameraHolder = transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        cameraOffset.x = (Input.mousePosition.x - (Screen.width / 2)) / (Screen.width / 2) * maxOffsetX;
        cameraOffset.y = (Input.mousePosition.y - (Screen.height / 2)) / (Screen.height / 2) * maxOffsetY + 2;
        cameraOffset.x = Mathf.Clamp(cameraOffset.x, -8.5f, 8.5f);
        cameraOffset.y = Mathf.Clamp(cameraOffset.y, 0, 4);

    }

    private void LateUpdate()
    {
        

        
    }

    private void FixedUpdate()
    {
        if (isFollowing)
        {
            cameraHolder.transform.position = Vector3.Lerp(cameraHolder.transform.position, player.transform.position + cameraOffset, cameraSpeed * Time.fixedDeltaTime);
        }

        /*
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
        */
    }


}
