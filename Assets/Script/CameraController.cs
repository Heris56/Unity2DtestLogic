using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentposX;
    private Vector3 velocity = Vector3.zero;
    private float currentposY;

    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentposX, transform.position.y, transform.position.z), 
            ref velocity, speed);
    }

    public void movetonewRoom(Transform newroom)
    {
        currentposX = newroom.position.x;
    }
}
