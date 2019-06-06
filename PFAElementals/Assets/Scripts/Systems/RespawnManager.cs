using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RespawnManager : MonoBehaviour {


    [SerializeField] Transform[] teamOneStartingSpawns;
    [SerializeField] Transform[] teamTwoStartingSpawns;
    [SerializeField] Monolith teamOneMonolith;
    [SerializeField] Monolith teamTwoMonolith;

    [SerializeField] List<Monolith> teamOneSpawn;
    [SerializeField] List<Monolith> teamTwoSpawn;

    Material map;
    private bool isMoving = false;
    private float target;
    [SerializeField] float movingSpeed = 2f;
    [SerializeField] float maxGlowThickness = 0.03f;



    // Use this for initialization
    void Start ()
    {
       
        map = GameObject.Find("Ground").GetComponent<Renderer>().material;

        teamOneSpawn.Clear();
        teamTwoSpawn.Clear();
        for (int i = 0; i < teamOneStartingSpawns.Length; i++)
        {
            teamOneSpawn.Add(Instantiate(teamOneMonolith, teamOneStartingSpawns[i].transform.position, Quaternion.identity));
        }
        for (int i = 0; i < teamTwoStartingSpawns.Length; i++)
        {
            teamTwoSpawn.Add(Instantiate(teamTwoMonolith, teamTwoStartingSpawns[i].transform.position, Quaternion.identity));
        }

        for (int i = 0; i < teamOneSpawn.Count - 1; i++)
        {
            teamOneSpawn[i].GetComponent<Monolith>().SetDamageableOff();
        }

        for (int i = 0; i < teamTwoSpawn.Count - 1; i++)
        {
            teamTwoSpawn[i].GetComponent<Monolith>().SetDamageableOff();
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            // position
            map.SetFloat("Vector1_3ECABBA8", Mathf.Lerp(map.GetFloat("Vector1_3ECABBA8"), -target * 0.5f, Time.deltaTime * movingSpeed));

            // épaisseur
            if (Mathf.Approximately(-target * 0.5f, map.GetFloat("Vector1_3ECABBA8")))
            {
                isMoving = false;
            }
        }
    }

    public void AddMonolithToListOne(Monolith Monolith)
    {
        teamOneSpawn.Add(Monolith);
    }
    public void AddMonolithToListTwo(Monolith Monolith)
    {
        teamTwoSpawn.Add(Monolith);
    }


    public Transform GetMonolith(int team)
    {
        if (team == 1)
        {
            if (teamOneSpawn.Count > 1)
            {
                return teamOneSpawn[teamOneSpawn.Count - 2].GetSpawner();
            }
            else
            {
                return teamOneSpawn[teamOneSpawn.Count - 1].GetSpawner();
            }

        }
        else if (team == 2)
        {
            if (teamTwoSpawn.Count > 1)
            {
                return teamTwoSpawn[teamTwoSpawn.Count - 2].GetSpawner();
            }
            else
            {
                return teamTwoSpawn[teamTwoSpawn.Count - 1].GetSpawner();
            }
        }
        else
        {
            return null;
        }
    }

    public void RemoveMonolith(int team)
    {
        if (team == 1)
        {
            if(teamOneSpawn.Count > 1)
            {
                teamOneSpawn.Remove(teamOneSpawn[teamOneSpawn.Count - 1]);
                teamOneSpawn[teamOneSpawn.Count - 1].GetComponent<Monolith>().SetDamageableOn();
            }
            else
            {
                SceneManager.LoadScene("RestartMenu");
            }

        }

        if (team == 2)
        {
            if(teamTwoSpawn.Count >1)
            {
                teamTwoSpawn.Remove(teamTwoSpawn[teamTwoSpawn.Count - 1]);
                teamTwoSpawn[teamTwoSpawn.Count - 1].GetComponent<Monolith>().SetDamageableOn();
            }
            else
            {
                SceneManager.LoadScene("RestartMenu");
            }
        }
    }

    public void AddMonolith(int team, Transform zone)
    {
        if (team == 1)
        {
            teamOneSpawn.Add(Instantiate(teamOneMonolith, zone.position, Quaternion.identity));
            teamOneSpawn[teamOneSpawn.Count - 2].GetComponent<Monolith>().SetDamageableOff();
        }

        if (team == 2)
        {
            teamTwoSpawn.Add(Instantiate(teamTwoMonolith, zone.position, Quaternion.identity));
            teamTwoSpawn[teamTwoSpawn.Count - 2].GetComponent<Monolith>().SetDamageableOff();
        }

        target = zone.position.x;
        isMoving = true;
    }


    public Monolith GetMonolithIndex(int team, int index)
    {
        if (team == 1 && teamOneSpawn[index]!= null)
        {
            return (teamOneSpawn[index]);
        }

        else if (team == 2 && teamTwoSpawn[index] != null)
        {
            return (teamTwoSpawn[index]);
        }

        else
        {
            return null;
        }
    }

    public int GetListCount (int team)
    {
        if (team == 1)
        {
            return teamOneSpawn.Count;
        }
        else if (team == 2)
        {
            return teamTwoSpawn.Count;
        }

        else
        {
            return 0;
        }
    }
}
