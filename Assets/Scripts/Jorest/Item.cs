using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float cooldownSeconds =6f;
    private float cooldown ;
    private bool toggled = false;
    public GameObject SnackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = cooldownSeconds;
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
            cooldown = cooldownSeconds;
        }
        
        
        if (Input.GetKeyDown(KeyCode.K) && toggled==false)
        {
            toggled = true;
            GameObject newSnack = Instantiate(SnackPrefab, transform.position, Quaternion.identity);
        }

    }
}
