using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputHandler : MonoBehaviour
{
    private Player m_player;

    // Start is called before the first frame update
    void Awake()
    {
        m_player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputV = Vector2.zero;

        inputV.x = Input.GetAxis("Horizontal");
        inputV.y = Input.GetAxis("Vertical");

        m_player.SetInputVector(inputV.y);
    }
}
