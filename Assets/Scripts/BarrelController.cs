using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private Renderer rnd;
    private Color[] colors = {Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow };


    private void Start()
    {
        rnd = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HitCheck()) ChangeColor();
        }
    }

    private bool HitCheck()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.name == "WoodBarrel")
            {
                return true;
            }
        }
        return false;
    }

    private void ChangeColor()
    {
        rnd.material.color = colors[Random.Range(0, colors.Length)];
    }
}