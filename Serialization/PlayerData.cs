using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public bool Mission1;
    public bool Mission2;
    public bool Mission3;
    public bool Mission4;
    public bool Mission5;
    public bool Mission6;

    public PlayerData(Player player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        Mission1 = player.missions.Mission1;
        Mission2 = player.missions.Mission2;
        Mission3 = player.missions.Mission3;
        Mission4 = player.missions.Mission4;
        Mission5 = player.missions.Mission5;
        Mission6 = player.missions.Mission6;

        Debug.Log("Saved");
    }
}
