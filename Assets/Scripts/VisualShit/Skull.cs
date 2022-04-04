using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    private float size = 0;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        size += (Time.deltaTime / 6);

        float height = transform.position.y;
        height += (Time.deltaTime / 6);

        this.transform.localScale = new Vector3(size, size, size);
        this.transform.position = new Vector3(transform.position.x, height,
            transform.position.z);
    }
}
