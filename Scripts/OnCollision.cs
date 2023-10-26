using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollision : MonoBehaviour
{
    public PlayerMovement m_char;
    public Animator m_animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "Ground")
        {
            Debug.Log("Player or Ground");
            return;
        }
        Debug.Log("Collision Detected with: " + collision.collider.tag);
        m_char.OnCharacterColliderHit(collision.collider);
    }

    
}
