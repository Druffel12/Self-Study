using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinerAI : MonoBehaviour
{
    bool hasTool = false;
    bool hasOre = false;
    
    int toolCount;
    int toolLife = 0;

    float cost;

    int trial = 0;

    Vector3 myTarget;

    GameObject Manager = GameObject.Find("GameManager");

    SimulationManager simMan;

    public List<System.Action> optionsList;

    public List<System.Action> optionsList2;
    
	// Use this for initialization
	void Start ()
    {
        simMan = Manager.GetComponent<SimulationManager>();

        optionsList.Add(getTool);
        optionsList.Add(gatherOre);
        toolCount = GetComponent<SimulationManager>().toolCount;

        optionsList2.Add(mineOre);
        optionsList2.Add(gatherOre);
	}
	
	// Update is called once per frame
	void Update ()
    {
        optionsList[trial]();
        if(toolLife == 0)
        {
            Debug.Log("Tool Has Broken for Miner");
            hasTool = false;
        }
	}

    void getTool()
    {


        if (toolCount > 0 && toolLife == 0)
        {
            while (transform.position != myTarget)
            {

                //T possibly replace coordinates witha more advanced path finding idea
                myTarget = GameObject.FindGameObjectWithTag("Forge").transform.position;
                //myTarget.x -= 3; myTarget.z -= 3;

                transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

                //ran after AI reachs target position
                if (transform.position == myTarget)
                {

                    cost += 2;

                    toolLife += 5;

                    hasTool = true;

                    break;
                }

                else if (toolLife > 0)
                {
                    trial = 0;
                    optionsList2[trial]();
                }

                else
                {
                    trial++;
                    optionsList[trial]();
                }
            }
        }
    }

    void mineOre()
    {
        myTarget = GameObject.FindGameObjectWithTag("Mine").transform.position;
        //myTarget.x -= 3; myTarget.z -= 3;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

        //ran after AI reachs target position 
        if (transform.position == myTarget)
        {
            cost += 4;
            toolLife -= 1;
            dropOffOre();
        }
    }

    void gatherOre()
    {
        myTarget = GameObject.FindGameObjectWithTag("Rocks").transform.position;
        //myTarget.x -= 3; myTarget.z -= 3;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

        //ran after AI reachs target position
        if (transform.position == myTarget)
        {
            cost += 8;
            hasOre = true;
            dropOffOre();
        }
    }

    void dropOffOre()
    {
        myTarget = GameObject.FindGameObjectWithTag("Forge").transform.position;
        //myTarget.x -= 3; myTarget.z -= 3;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

        //ran after AI reachs target position
        if (transform.position == myTarget)
        {
            simMan.Metal += 1;
            cost += 2;
            Debug.Log("Miner");
            Debug.Log(cost);
        }

    }
}
