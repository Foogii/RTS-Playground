using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupplyScript : MonoBehaviour
{
    public bool isPowered = false;
    public bool suppliesPower = false;

    public bool mainPowerSupply; //Big power supply
    public bool medPowerSupply; //Medium power supply
    public bool smPowerSupply; //Small power supply

    public LayerMask whatIsSmPowerSupply; //Small power supply layer
    public LayerMask whatIsMedPowerSupply; //Medium power supply layer
    public LayerMask whatIsMainPowerSupply; //Main power supply layer
    public LayerMask whatIsPlayerPowerSupply; //Player power supply layer

    private SpriteRenderer sprite;

    [Range(0.0f, 10.0f)]
    public float powerRadius;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPowered)
        {
            suppliesPower = false;
            sprite.color = Color.red;
        }

        FindPowerSources();
        SupplyPower();
    }

    public void FindPowerSources()
    {
        Collider2D[] mainPowerSupplies = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsMainPowerSupply); //Big Power Supply
        Collider2D[] medPowerSupplies = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsMedPowerSupply); //Medium Power Supply
        Collider2D[] smPowerSupplies = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsSmPowerSupply); //Small Power Supply
        Collider2D[] playerPowerSupply = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsPlayerPowerSupply); //Player Power Supply


        if(smPowerSupplies.Length <= 0) //If the power source is a small supply
        {
            smPowerSupply = false;
            isPowered = false;
        }

        if (medPowerSupplies.Length <= 0) //If the power source is a medium supply
        {
            medPowerSupply = false;
            isPowered = false;
        }

        if (mainPowerSupplies.Length <= 0) //If the power source is a large/main supply
        {
            mainPowerSupply = false;
            isPowered = false;
        }

        for (int i = 0; i < medPowerSupplies.Length; i++)
        {
            if (medPowerSupplies[i].GetComponent<PowerSupplyScript>().medPowerSupply == true)
            {
                gameObject.layer = 10; //Set layer to small power supply
                smPowerSupply = true;
                isPowered = true;
                sprite.color = Color.cyan;
            }

            medPowerSupplies[i] = null;
        }

        for (int i = 0; i < mainPowerSupplies.Length; i++)
        {
            if (mainPowerSupplies[i].GetComponent<PowerSupplyScript>().mainPowerSupply == true)
            {
                gameObject.layer = 8;  //Set layer to medium power supply
                medPowerSupply = true;
                isPowered = true;
                sprite.color = Color.yellow; 
            }

            mainPowerSupplies[i] = null;
        }

        for (int i = 0; i < playerPowerSupply.Length; i++)
        {
            if (playerPowerSupply[i].GetComponent<PlayerScript>().isMainPower == true)
            {
                gameObject.layer = 9; //Set layer to main power supply
                mainPowerSupply = true;
                isPowered = true;
                sprite.color = Color.green;
            }

            playerPowerSupply[i] = null;
        }
        
    }

    public void SupplyPower()
    {
        if (isPowered)
        {
            Collider2D[] mainPowerSupplies = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsMainPowerSupply);
            Collider2D[] medPowerSupplies = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsMedPowerSupply);
            Collider2D[] smPowerSupplies = Physics2D.OverlapCircleAll(transform.position, powerRadius, whatIsSmPowerSupply);

            for (int i = 0; i < mainPowerSupplies.Length; i++)
            {
                mainPowerSupplies[i].GetComponent<PowerSupplyScript>().isPowered = true;
            }

            for (int i = 0; i < medPowerSupplies.Length; i++)
            {
                medPowerSupplies[i].GetComponent<PowerSupplyScript>().isPowered = true;
            }

            for (int i = 0; i < smPowerSupplies.Length; i++)
            {
                smPowerSupplies[i].GetComponent<PowerSupplyScript>().isPowered = true;
            }
        }
    
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, powerRadius);
    }

}
