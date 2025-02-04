using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemydoing : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] float force=50f;
    [SerializeField] float maxSpeed=4.5f;
    
    [Header("nockback")] 
    [SerializeField] float nockbackForce=200f;
    [SerializeField] float nockbackDurration=1f;
    [SerializeField] float nockbackCD=0.1f;

    private bool cannockback=true;
    private bool isnockingback;
    [Header("Health")]
    [SerializeField] float Health,maxHealth =3f;

    Vector2 movetoward;

    public Rigidbody2D rb;
    
    Transform target;
    

    // Start is called before the first frame update
    void Start()
    {
        Health= maxHealth;
        target=GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            movetoward=(target.position-transform.position).normalized;
        }
        
    }
    void FixedUpdate()
    {
        if(target&&!isnockingback)
        {
           rb.AddForce(movetoward*force);
           if(rb.velocity.magnitude>maxSpeed)
            {
                rb.velocity=rb.velocity.normalized*maxSpeed;
            }
        }
    }
    public void TakeDmg(float damage)
    {
        Health-=damage;
        if(cannockback )
        {
            StartCoroutine(PerformNockback());
        }
        
        if(Health<=0)
        {
            Destroy(gameObject);
        }
    }
    System.Collections.IEnumerator PerformNockback()
    {
        cannockback=false;
        isnockingback=true;
        rb.velocity=Vector2.zero;
        rb.AddForce(-movetoward*nockbackForce,ForceMode2D.Impulse);
        yield return new WaitForSeconds(nockbackDurration);
        rb.velocity = Vector2.zero;
        isnockingback=false;
        yield return new WaitForSeconds(nockbackCD);
        cannockback=true;
    }  
}
