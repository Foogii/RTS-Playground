using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePowerSupply : MonoBehaviour
{
    public GameObject powerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(powerPrefab, position, Quaternion.identity); 
        }
    }
}
