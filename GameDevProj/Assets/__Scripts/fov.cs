using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fov : MonoBehaviour
{
    [SerializeField]private Mesh mesh;
    [SerializeField]private GameObject daddy;
    public float angle;
    private void Start(){
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        daddy = this.transform.parent.gameObject;
        
    }
    private void Update(){
        float Fov = 120f;
        Vector3 Origin = daddy.transform.position;
       

        Vector3 OriginLocal = transform.InverseTransformPoint(Origin);
        //Debug.Log("Origin !!!!!!!!!!! " + Origin);
        int rayCount = 360;
        if(daddy.GetComponent<baseGaurd>().dir == direction.north){
           angle = 150f;
        }
        else if(daddy.GetComponent<baseGaurd>().dir == direction.east){ angle = -120f;}
        float angleIncrease = Fov/rayCount;
        float viewDistance = 10f;


        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0]=OriginLocal;
       int vertexIndex = 1;
       int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i ++){
            Vector3 vertex;
           
            RaycastHit2D hit = Physics2D.Raycast(Origin, GetVectorFromAngle(angle) ,viewDistance);
            if(hit.collider == null){
                //Debug.Log("miss");
                vertex = OriginLocal +(GetVectorFromAngle(angle) * viewDistance);
            }
            else{
                //Debug.Log("hit");
                //Debug.Log(hit.collider);
                vertex = transform.InverseTransformPoint(hit.point);
                

                
                //vertex = hit.barycentricCoordinate;
               // Debug.Log(hit.point);
                //Debug.Log(hit.transform.position);
                if(hit.collider.gameObject.tag == "Player"){
                    var b_gaurd = daddy.GetComponent<baseGaurd>();
                    b_gaurd.path = TileManager.FindPath(b_gaurd.character.tile, hit.collider.gameObject.GetComponent<baseUnit>().tile);
                    b_gaurd.move();
                }
            }
            vertices[vertexIndex] = vertex;

            if(i>0){

            triangles[triangleIndex + 0] = 0;
            triangles[triangleIndex + 1] = vertexIndex -1;
            triangles[triangleIndex + 2] = vertexIndex;
            triangleIndex +=3;
        }
            vertexIndex++;
            angle -= angleIncrease;

        }

       

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        //Debug.Log("mesh placed");

    }

    private static Vector3 GetVectorFromAngle(float angle) {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
