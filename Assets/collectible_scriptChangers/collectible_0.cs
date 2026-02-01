using UnityEngine;

public class collectible_0 : MonoBehaviour
{
    playerMovement pm;
    public Transform playerSpawnPoint;
    public BoxCollider2D dialogue_three_collider;
    public BoxCollider2D dialogue_four_collider;
    public BoxCollider2D invisible_wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            pm = collision.GetComponent<playerMovement>();
            if(pm != null)
            {
                pm.ChangeMovementSystem(1);
                pm.transform.position = playerSpawnPoint.position;
                dialogue_three_collider.enabled = true;
                dialogue_four_collider.enabled = true;
                invisible_wall.enabled = false;
                pm.MovementDirection = Vector2.zero;
                Destroy(gameObject);
            }
        }
    }
}
