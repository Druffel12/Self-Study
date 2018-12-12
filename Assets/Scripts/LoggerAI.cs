using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerAI : MonoBehaviour
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
        optionsList.Add(gatherWood);
        toolCount = GetComponent<SimulationManager>().toolCount;

        optionsList2.Add(chopWood);
        optionsList2.Add(gatherWood);
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        optionsList[trial]();
        if(toolLife == 0)
        {
            Debug.Log("Tool Has Broken for Logger");
            hasTool = false;
        }
	}

    void chopWood()
    {
        //cost 4
        myTarget = GameObject.FindGameObjectWithTag("Mine").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

        if (transform.position == myTarget)
        {
            cost += 4;
            toolLife -= 1;
            dropOffWood();
        }
    }

    void getTool()
    {
        
        if (toolCount > 0 && toolLife == 0)
        {
            while (transform.position != myTarget)
            {
                //T possibly replace coordinates with a more advanced path finding idea
                myTarget = GameObject.FindGameObjectWithTag("Forge").transform.position;

                transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

                //after Ai reaches target
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

    void gatherWood()
    {
        //cost 8
        myTarget = GameObject.FindGameObjectWithTag("Rocks").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);
        
        //ran when AI reachs target
        if (transform.position == myTarget)
        {
            cost += 8;
            hasOre = true;
            dropOffWood();
        }
    }

    void dropOffWood()
    {
        //cost 2
        myTarget = GameObject.FindGameObjectWithTag("Forge").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, 2);

        //ran when AI reachs target position
        if (transform.position == myTarget)
        {
            simMan.Wood += 1;
            cost += 1;
            Debug.Log("Logger");
            Debug.Log(cost);
        }
    }

}
