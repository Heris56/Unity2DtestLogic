
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playermove;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermove = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown && playermove.canAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[Findfireball()].transform.position = firepoint.position;
        fireballs[Findfireball()].GetComponent<Projectiles>().setDirection(Mathf.Sign(transform.localScale.x));
        // pool fireballs
    }

    private int Findfireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
