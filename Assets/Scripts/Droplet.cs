using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droplet : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.down * 10);

        if (transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }
}
