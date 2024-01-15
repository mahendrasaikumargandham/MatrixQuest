using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Missions missions;
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        Vector3 position;

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        missions.Mission1 = data.Mission1;
        missions.Mission2 = data.Mission2;
        missions.Mission3 = data.Mission3;
        missions.Mission4 = data.Mission4;
        missions.Mission5 = data.Mission5;
        missions.Mission6 = data.Mission6;

    }
}
