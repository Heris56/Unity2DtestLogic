
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundlayer;
    [SerializeField] private LayerMask wallLayer;
    private int stickOnwall;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float walljumpcooldown;
    private float horizontalinput;

    private void Awake()
    {
        // ambil reference untuk rigidbody dan animator dari objek
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalinput = Input.GetAxis("Horizontal");

        // reset jump saat di di ground/lantai
        if (isGrounded())
        {
            stickOnwall = 0;
        }

        // flip char ke kanan
        if (horizontalinput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        
        // flip chara ke kiri
        if (horizontalinput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            jump();
        }

        // set animator param
        anim.SetBool("run", horizontalinput != 0);
        anim.SetBool("grounded", isGrounded());
        
        // wall jump logic
        if(walljumpcooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalinput * speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 2.5f;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                jump();
            }
        }
        else
        {
            walljumpcooldown += Time.deltaTime;
        }
    }

    private void jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalinput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 15, 3);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (stickOnwall < 4)
            {
                if(horizontalinput != 0) 
                {
                    body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 10);
                }
                stickOnwall++;
            }
            walljumpcooldown = 0;
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundlayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalinput == 0 && isGrounded() && !onWall();
    }
}
