using UnityEngine;

public class FootprintGenerator : MonoBehaviour
{
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private GameObject footprintRight;
    [SerializeField] private GameObject footprintLeft;

    public void FootprintLeft()
    {
        AddFootprint(leftFoot, footprintLeft);
    }

    public void FootprintRight()
    {
        AddFootprint(rightFoot, footprintRight);
    }

    private void AddFootprint(Transform foot, GameObject footprint)
    {
        Ray ray = new Ray(foot.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.CompareTag("Terrain"))
            {
                var fp = Instantiate(footprint, new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z), Quaternion.Euler(hit.normal.x, foot.eulerAngles.y, hit.normal.z));
                Destroy(fp, 30);
            }
        }
    }
}
