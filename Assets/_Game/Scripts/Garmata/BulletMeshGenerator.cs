using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BulletMeshGenerator : MonoBehaviour
{
    [SerializeField] private int _resolution = 15; 
    [SerializeField] private float _radius = 0.25f;
    [SerializeField] private float _randomness = 0.3f;

    private Mesh _mesh;

    private void Start()
    {
        GenerateRandomSphere();
    }

    private void GenerateRandomSphere()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        Vector3[] vertices = new Vector3[(_resolution + 1) * (_resolution + 1)];
        int[] triangles = new int[_resolution * _resolution * 6];

        int vertIndex = 0;
        int triIndex = 0;

        for (int i = 0; i <= _resolution; i++)
        {
            float latitude = Mathf.PI * i / _resolution;

            for (int j = 0; j <= _resolution; j++)
            {
                float longitude = 2 * Mathf.PI * j / _resolution;

                Vector3 vertex = new Vector3(
                    Mathf.Sin(latitude) * Mathf.Cos(longitude),
                    Mathf.Cos(latitude),
                    Mathf.Sin(latitude) * Mathf.Sin(longitude)
                ) * _radius;

                float randomFactor = Random.Range(1,1 + _randomness);
                vertex = vertex.normalized * _radius * randomFactor;

                vertices[vertIndex] = vertex;

                if (i < _resolution && j < _resolution)
                {
                    triangles[triIndex] = vertIndex;
                    triangles[triIndex + 1] = vertIndex + 1;
                    triangles[triIndex + 2] = vertIndex + _resolution + 1;

                    triangles[triIndex + 3] = vertIndex + 1;
                    triangles[triIndex + 4] = vertIndex + _resolution + 2;
                    triangles[triIndex + 5] = vertIndex + _resolution + 1;

                    triIndex += 6;
                }

                vertIndex++;
            }
        }

        _mesh.Clear();

        _mesh.vertices = vertices;
        _mesh.triangles = triangles;

        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _mesh.Optimize();
    }
}
