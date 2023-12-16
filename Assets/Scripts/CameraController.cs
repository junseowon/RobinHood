using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float cameraSpeed;
    public float cameraDistance;

    void Update()
    {
        Vector2 dir = player.transform.position - this.transform.position;
        Vector2 moveVector = new Vector2(dir.x * cameraSpeed * Time.deltaTime, (dir.y + cameraDistance) * cameraSpeed * Time.deltaTime);
        this.transform.Translate(moveVector);
    }
}
