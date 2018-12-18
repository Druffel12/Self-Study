using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggerAI : MonoBehaviour
{
    bool hasTool = false;
    bool hasOre = false;


    public int speed;

    int toolCount;
    int toolLife = 0;

    float cost;

    int trial = 0;

    Vector3 myTarget;

    SimulationManager simMan;

    public List<System.Action> optionsList;

    public List<System.Action> optionsList2;
    
	// Use this for initialization
	void Start ()
    {
        GameObject Manager = GameObject.Find("GameManager");

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
        myTarget = GameObject.FindGameObjectWithTag("Forrest").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, speed);

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
                myTarget = GameObject.FindGameObjectWithTag("ToolBin").transform.position;

                transform.position = Vector3.MoveTowards(transform.position, myTarget, speed);

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
        myTarget = GameObject.FindGameObjectWithTag("Wood").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, speed);
        
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
        myTarget = GameObject.FindGameObjectWithTag("WoodBin").transform.position;

        transform.position = Vector3.MoveTowards(transform.position, myTarget, speed);

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
