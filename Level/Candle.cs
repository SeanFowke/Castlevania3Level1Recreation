using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] GameObject C_Object;
    [SerializeField] GameObject C_Flame;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("knife"))
        {
            Destroy(gameObject);
            Instantiate(C_Flame, gameObject.transform.position, Quaternion.identity);
            Instantiate(C_Object, gameObject.transform.position, Quaternion.identity);
        }
        if (col.CompareTag("Whip"))
        {
            Destroy(gameObject);
            Instantiate(C_Flame, gameObject.transform.position, Quaternion.identity);
            Instantiate(C_Object, gameObject.transform.position, Quaternion.identity);
        }
    }
}
