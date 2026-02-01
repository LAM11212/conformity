using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public Transform playerSpawnPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            playerMovement pm = collision.collider.GetComponent<playerMovement>();
            if(pm != null)
            {
                pm.transform.position = playerSpawnPoint.position;
            }
        }
    }
}
