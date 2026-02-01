using UnityEngine;

public class collectible_0 : MonoBehaviour
{
    playerMovement pm;
    public Transform playerSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            pm = collision.GetComponent<playerMovement>();
            if(pm != null)
            {
                pm.ChangeMovementSystem(1);
                pm.transform.position = playerSpawnPoint.position;
                Destroy(gameObject);
            }
        }
    }
}
