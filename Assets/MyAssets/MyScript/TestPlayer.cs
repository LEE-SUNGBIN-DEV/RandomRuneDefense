using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TestPlayer : MonoBehaviourPunCallbacks
{
    private void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * 2.0f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * 2.0f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * 2.0f * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
            }
        }
    }
}
