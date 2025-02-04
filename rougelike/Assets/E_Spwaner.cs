using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Spwaner : MonoBehaviour
{   [SerializeField]
    private GameObject enemy;
    public float interval=3f;
    public int conter=10;
    // Start is called before the first frame update
    void Start()
    {
        if(conter >0)
        {StartCoroutine(spawn(interval, enemy));}
    }

   private IEnumerator spawn(float interval,GameObject enemy)
   {
    yield return new WaitForSeconds(interval);
    GameObject newEnemy = Instantiate(enemy,new Vector2(Random.Range(-6f,6f),Random.Range(-6f,6f)),Quaternion.identity);
    conter--;
    StartCoroutine(spawn(interval,newEnemy));
   }
    
}
