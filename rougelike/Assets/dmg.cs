using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmg : MonoBehaviour
{   
    public float damage=1f;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        enemydoing enemy = collision.GetComponent<enemydoing>();
        if(enemy!=null)
        {
            enemy.TakeDmg(damage);
        }

    }
}
