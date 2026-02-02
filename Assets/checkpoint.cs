using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private playerMovement pm;
    public BoxCollider2D checkpointCollider;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pm = collision.GetComponent<playerMovement>();
            if(pm != null)
            {
                pm.currentSpawnPoint = this.transform;
                checkpointCollider.enabled = false;
            }
        }
    }
}
