using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RangeModifier : MonoBehaviour
{
    private Transform closestEnemy;
    public float Radius;
    public float lookSpeed;

    // Update is called once per frame
    void Update()
    {
        FindClosestEnemy();
        RotatoEnemy();
    }

    private void FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Radius);
        closestEnemy = null;

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                float distance = Vector2.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hitCollider.transform;
                }
            }
        }
    }

    void RotatoEnemy()
    {
        if (closestEnemy != null)
        {
            Vector3 targetting = closestEnemy.position - transform.position;
            float aimAngle = Mathf.Atan2(targetting.y, targetting.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * lookSpeed);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
