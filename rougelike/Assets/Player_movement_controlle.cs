using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement_controlle : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] float force=50f;
    [SerializeField] float maxSpeed=5f;
    [SerializeField] float friction=0.9f;

    [Header("Dash")]
    [SerializeField] float dashForce=100f;
    [SerializeField] float dashDuration=0.15f;
    [SerializeField] float dashCD=0.5f;

    [Header("Attack")]
    [SerializeField] float akkackDuration=0.15f;
    [SerializeField] float akkackCD=0.5f;
    [SerializeField] GameObject melee;
    [SerializeField] GameObject attackrichtung;
    [SerializeField] GameObject aimCross;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootpoint;

    public Camera maincamera;
    
    
    private bool canAttack=true;
    //private bool isAttacking;
    

    public Rigidbody2D rb;
    private Vector2 inputDircection;
    private Vector3 mausepos;
    
    private bool isDashing;
    private bool canDash=true;
    private bool canshoot=true;
    public float rotZ;
    
    private void Start()
    {
        
        //rb = GetComponent<Rigidbody2D>();
        // melee = GetComponentInChildren<Collider2D>();
        melee.SetActive(false);
        
    }
    
    private void Update()
    {
        GetInput();
       
    }
    private void FixedUpdate()
    {
        if (!isDashing) ApplyMovementForce();
        ApplyFriction();
    }

    void GetInput()
    {
        inputDircection= new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
        mausepos= maincamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation=mausepos-gun.transform.position;
       // Debug.Log($"mauseposX: {mausepos.x}");
        //Debug.Log($"mauseposY: {mausepos.y}");
        rotZ= Mathf.Atan2(rotation.y,rotation.x)*Mathf.Rad2Deg;
        gun.transform.rotation=Quaternion.Euler(0,0,rotZ);
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(PerformDash());
        }
        if(Input.GetMouseButton(0)&& canshoot)
        {
            StartCoroutine(Performshoot());
        }
    }

    void ApplyMovementForce()
    {
        if(inputDircection.magnitude>0)
        {
            rb.AddForce(inputDircection*force*Time.fixedDeltaTime);
            Vector2 atkDir =inputDircection!=Vector2.zero?
                        inputDircection:
                        (rb.velocity.normalized!=Vector2.zero?
                        rb.velocity.normalized:Vector2.up);
            Vector3 atkDir3D= new Vector3(-atkDir.x,-atkDir.y,0);
            attackrichtung.transform.rotation=Quaternion.LookRotation(Vector3.forward,atkDir3D);
            
            
            
            if(rb.velocity.magnitude>maxSpeed)
            {
                rb.velocity=rb.velocity.normalized*maxSpeed;
            }
        }
    }

    void ApplyFriction()
    {
        if(inputDircection.magnitude<0.1f)
        {
            rb.velocity*=friction;
        }
    }
    System.Collections.IEnumerator PerformDash()
    {
        GetComponent<Collider2D>().enabled = false;
        canDash=false;
        isDashing=true;
        
        Vector2 dashDir =inputDircection!=Vector2.zero?
                        inputDircection:
                        (rb.velocity.normalized!=Vector2.zero?
                        rb.velocity.normalized:Vector2.up);
        rb.velocity=Vector2.zero;
        rb.AddForce(dashDir * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);
        GetComponent<Collider2D>().enabled = true;
        rb.velocity = Vector2.zero;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
        
    }
    System.Collections.IEnumerator PerformAkt()
    {
        melee.SetActive(true);
        canAttack=false;
       // isAttacking=true;
        //Debug.Log("akt\n");
        
        yield return new WaitForSeconds(akkackDuration);
        melee.SetActive(false);
        //isAttacking=false;
        yield return new WaitForSeconds(akkackCD);
        canAttack=true;

    }
    System.Collections.IEnumerator Performshoot()
    {   
        canshoot=false;
        GameObject bullets = Instantiate(bullet,new Vector2(shootpoint.transform.position.x,shootpoint.transform.position.y),Quaternion.identity );
        
        bullet bulle = bullets.GetComponent<bullet>();
        bulle.letbulletflying(mausepos);
        yield return new WaitForSeconds(0.05f);
        canshoot=true;

    }
}
