using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithAI : MonoBehaviour
{
    bool hasWood = false;
    bool hasMetal = false;

    public int speed;

    int toolStockPile;
    int totalToolsCrafted;
    int metalCount;
    int woodCount;

    Vector3 myTarget;

    SimulationManager simMan;

	// Use this for initialization
	void Start ()
    {
        GameObject Manager = GameObject.Find("GameManager");

        simMan = Manager.GetComponent<SimulationManager>();

        toolStockPile = GetComponent<SimulationManager>().toolCount;

        metalCount = GetComponent<SimulationManager>().Metal;

        woodCount = GetComponent<SimulationManager>().Wood;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(hasWood== true && hasMetal == true)
        {
            makeTool();
        }

        else if (hasWood == false)
        {
            getWood();
        }

        else if(hasMetal == false)
        {
            getMetal();
        }

    }

    void makeTool()
    {
        hasMetal = false;
        hasWood = false;
        toolStockPile++;
    }

    void getWood()
    {
        
        if (metalCount > 0)
        {
            myTarget = GameObject.FindGameObjectWithTag("WoodBin").transform.position;

            while (transform.position != myTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, myTarget, speed);
            }
            hasWood = true;
        }      
    }

    void getMetal()
    {
        if (woodCount > 0)
        {

            myTarget = GameObject.FindGameObjectWithTag("MetalBin").transform.position;

            while (transform.position != myTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, myTarget, speed);
            }
            hasMetal = true;
        }
    }

}
