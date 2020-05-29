using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TurnManager : MonoBehaviourPun 
{


    //string for player/npc tag, list for members of team
    static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();

    //stores the team that is currently active
    static Queue<string> turnKey = new Queue<string>();

    //Queue for team whose turn it is, goes through all members of team
    static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();

	// Use this for initialization
	void Start () 
	{


    }
	
	// Update is called once per frame
	void Update () 
	{
        //if no turns qued, queue for a turn
        if (turnTeam.Count == 0)
        {
         
                InitTeamTurnQueue();
              
        }
	}

    static void InitTeamTurnQueue()
    {
        //finds first term in Queue(team that is active)
        List<TacticsMove> teamList = units[turnKey.Peek()];

        //Add each member of active team to turn Queue
        foreach (TacticsMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        
        StartTurn();
    }

    public static void StartTurn()
    {
        //makes sure team isn't empty
        if (turnTeam.Count > 0)
        {
          //  turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        TacticsMove unit = turnTeam.Dequeue();
        unit.EndTurn();

        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(TacticsMove unit)
    {
        List<TacticsMove> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticsMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }
}
