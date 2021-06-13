using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float cooldown = 60.0f;
    private bool toggled = false;
    public GameObject SnackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (toggled == true)
        {
            cooldown -= Time.deltaTime;
        }
            
            
          
        if (cooldown <= 0.0f)
        {
            toggled = false;
        }
        
        
        if (Input.GetKeyDown("Jump"))
        {
            toggled = true;
            GameObject newSnack = Instantiate(SnackPrefab, transform.position, Quaternion.identity);
        }

    }
}
