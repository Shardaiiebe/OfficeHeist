using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float fov;
    public float viewDistance;
    public LayerMask layerMask;

    public delegate void OnPlayerDetected();
    public OnPlayerDetected onPlayerDetectedCallback;

    private void Update()
    {
        Collider2D target = Physics2D.OverlapCircle(transform.position, viewDistance, layerMask);
        if (target != null && target.CompareTag("Player"))
        {
            Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
            float angle = Vector2.Angle(transform.right, directionToTarget);

            if (angle < fov / 2f)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, layerMask);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    if (onPlayerDetectedCallback != null)
                    {
                        onPlayerDetectedCallback.Invoke();
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector3 directionLeft = Quaternion.Euler(0, 0, fov / 2f) * -transform.right;
        Vector3 directionRight = Quaternion.Euler(0, 0, -fov / 2f) * -transform.right;

        Gizmos.DrawLine(transform.position, transform.position + directionLeft * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + directionRight * viewDistance);

        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
