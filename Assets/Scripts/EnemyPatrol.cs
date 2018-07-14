using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 5;

    public float waitTime = .3f;

    public float turnSpeed = 90;

    public Transform pathHolder;

    private void Start()
    {
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
        }

        StartCoroutine(FollowPath(waypoints));
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        float desiredAngle = AngleBetweenTwoPoints(transform.position, targetWaypoint);
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, desiredAngle));

        //Do this loop while the player has not been spotted by this enemy instance
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if(transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        float desiredAngle = AngleBetweenTwoPoints(transform.position, lookTarget);

        Quaternion desiredRotation = Quaternion.Euler(new Vector3(0f, 0f, desiredAngle));

        while (Quaternion.Angle(transform.rotation, desiredRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 prevPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .1f);
            Gizmos.DrawLine(prevPosition, waypoint.position);
            prevPosition = waypoint.position;
        }
        Gizmos.DrawLine(prevPosition, startPosition);
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return (Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg) + 90;
    }
}
