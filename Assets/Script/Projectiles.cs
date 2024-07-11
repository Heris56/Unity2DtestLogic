using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifetime;

    private BoxCollider2D boxcollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movespeed = speed * Time.deltaTime * direction;
        transform.Translate(movespeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxcollider.enabled = false;
        anim.SetTrigger("explode");
    }

    public void setDirection(float direction)
    {
        lifetime = 0;
        this.direction = direction;
        gameObject.SetActive(true);
        hit = false;
        boxcollider.enabled = true;
        float localscaleX = transform.localScale.x;
        if (Mathf.Sign(localscaleX) != direction)
            localscaleX = -localscaleX;

        transform.localScale = new Vector3(localscaleX, transform.localScale.y, transform.localScale.z);
    }

    public void deactivate()
    {
        gameObject.SetActive(false);
    }
}
