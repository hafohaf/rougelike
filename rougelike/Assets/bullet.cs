using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    [SerializeField] float dmg =1f;
    public Rigidbody2D rb;
    public float life=3f;
    // Start is called before the first frame update
    void  Awake()
    {
        Destroy(gameObject,life);
    }
    void Strat()
    {
      
    }
    public void letbulletflying(Vector3 Dir)
    {
        Vector3 dir=Dir-transform.position;
        Vector3 rotation =transform.position-Dir;
        rb.velocity= new Vector2(dir.x,dir.y).normalized*10;
        float rot =Mathf.Atan2(rotation.y,rotation.x)*Mathf.Rad2Deg;
        transform.rotation=Quaternion.Euler(0,0,rot);
    }
    private void OnTriggerEnter2D(Collider2D collision)
        {
            enemydoing enemy = collision.GetComponent<enemydoing>();
            if(enemy!=null)
            {
                enemy.TakeDmg(dmg);
                Destroy(gameObject);
            }

        }
}
