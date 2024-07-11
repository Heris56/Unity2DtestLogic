using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform prev;
    [SerializeField] private Transform next;
    [SerializeField] private CameraController cam;
    private PlayerMovement player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.tag == "Player")
            {
                if (collision.transform.position.x < transform.position.x)
                {
                    cam.movetonewRoom(next);
                }
                else
                {
                    cam.movetonewRoom(prev);
                }

                if (next == null)
                {
                    throw new Exception();
                }
            }
        }
        catch(Exception e)
        {
            player.transform.position = new Vector2(prev.position.x, prev.position.y);
        }
        
    }
}
