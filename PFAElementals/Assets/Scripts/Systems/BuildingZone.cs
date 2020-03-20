using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingZone : MonoBehaviour
{
    [SerializeField] private RespawnManager respawnManager;
    [SerializeField] private int team = 1;
    [SerializeField] private int index = 0;


    private bool totemed = false;
    private bool isToteming = false;


    // Update is called once per frame
    void LateUpdate()
    {
        if (respawnManager.GetListCount(team) >= (index + 1))
        {
            if (respawnManager.GetMonolithIndex(team, index) != null)
            {
                totemed = true;
            }
        } 
        else
        {
            totemed = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.SetBuildingZone(this as BuildingZone);
            if (!totemed)
            {
                player.CanBuild(gameObject.transform);
            }
            else if (totemed)
            {
                player.CanNotBuild();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.CanNotBuild();
            player.UnSetBuildingZone();
        }
    }

    public void SetTeamAndIndex (int _team, int _index)
    {
        team = _team;
        index = _index;
    }

    public void Totemed()
    {
        totemed = true;
    }
}
