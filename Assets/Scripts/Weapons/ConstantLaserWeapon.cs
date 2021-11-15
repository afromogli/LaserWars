using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ConstantLaserWeapon : MonoBehaviour
{
    private Vector3[] vertices = new Vector3[2] { new Vector3(0f, 0f, 0f), new Vector3(0f, 100f, 1000f) };
    private int[] indices = new int[2] { 0, 1 };
    private Color[] colors;
    private MeshFilter filter;

    // Start is called before the first frame update
    void Start()
    {
        colors = new Color[] { Color.red, Color.red };

        Mesh mesh = new Mesh();
        mesh.name = "My laser mesh";
        mesh.vertices = vertices;
        mesh.SetColors(colors);
        mesh.SetIndices(indices, MeshTopology.Lines, 0, true);

        filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = mesh;
    }


    // Update is called once per frame
    void Update()
    {
        vertices[1] = transform.forward * 1000;
        filter.mesh.vertices = vertices;
    }

    //private void OnDrawGizmos()
    //{
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(vertices[0], vertices[1]);
    //}
}
