using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool doubleJumpEnabled;
    [SerializeField] private bool deflectEnabled;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(player.GetInstanceID());
        /*
        TODO: Configure abilities (as components on the player, most likely) as enabled or disabled
        */
    }
}
