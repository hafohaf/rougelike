using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    [SerializeField] float dmg =1f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Strat()
    {
        
    }
    public void letbulletflying(Vector2 Dir)
    {
        rb.AddForce(Dir * 5000f, ForceMode2D.Impulse);
         void OnTriggerEnter2D(Collider2D collision)
        {
            enemydoing enemy = collision.GetComponent<enemydoing>();
            if(enemy!=null)
            {
                enemy.TakeDmg(damage);
                Destroy(gameObject);
            }

        }
    }
    
}
