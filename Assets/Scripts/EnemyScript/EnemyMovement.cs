using UnityEngine;

public class CreepMovement : MonoBehaviour
{
    public Transform[] pathPoints;
    public float speed = 8f;
    private int currentPointIndex = 0;

    void Start()
    {
        if (pathPoints == null || pathPoints.Length == 0)
        {
            Debug.LogError("No path points assigned to the CreepMovement script.");
        }
    }

    void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (currentPointIndex < pathPoints.Length)
        {
            Vector3 targetPosition = pathPoints[currentPointIndex].position;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}