using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


// NO LONGER USED
// Incorporated into THROWN STATE
public class Projectile : MonoBehaviour
{
    private Vector3 start;
    private Rigidbody2D rb;
    private Vector3 worldPosition;
    protected float timePassed;
    public GameObject prefab;
    private GameObject temp;
    
    
    // Start is called before the first frame update
    void Start()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        start = GameObject.FindWithTag("Player").transform.position;

        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed < 2f)
        {
            timePassed += Time.deltaTime;
            transform.position = MathParabola.Parabola(start, worldPosition, 2f, timePassed / 2f);

        }
        else
        {
            Vector3 t = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 10);
            temp = Instantiate(prefab, t, Quaternion.identity);
            
            //temp.transform.parent = GameObject.FindWithTag("Animal");
            Destroy(gameObject);

        }
        
    }
}
