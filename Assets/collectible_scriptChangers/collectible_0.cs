using UnityEngine;

public class collectible_0 : MonoBehaviour
{
    playerMovement pm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            pm = collision.GetComponent<playerMovement>();
            if(pm != null)
            {
                pm.ChangeMovementSystem(1);
                Destroy(gameObject);
            }
        }
    }
}
