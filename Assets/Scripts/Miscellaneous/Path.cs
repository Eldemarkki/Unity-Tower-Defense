using UnityEngine;

public class Path : MonoBehaviour
{
    public Transform[] points;

    private void OnDrawGizmos()
    {
        if (points != null && points.Length >= 2)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector3 p1 = points[i].position;
                Vector3 p2 = points[i + 1].position;
                Gizmos.DrawLine(p1, p2);
            }
        }
    }
}