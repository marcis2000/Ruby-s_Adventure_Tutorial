using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if(position.x < 2f) 
        {
            position.x = position.x + 0.01f;
        }
        if (position.y > -1f)
        {
            position.y = position.y - 0.005f;
        }
        transform.position = position;
    }
}
