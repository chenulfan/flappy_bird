using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private const float CAMERA_Y_POSITION = 0;
    private const float CAMERA_Z_POSITION = -10;
    public GameObject m_Bird;

    void Update () {
        transform.position = new Vector3(m_Bird.transform.position.x, CAMERA_Y_POSITION,  CAMERA_Z_POSITION);   
    }
}
