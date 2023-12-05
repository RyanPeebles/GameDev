using UnityEngine;

public class fov : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private GameObject daddy;
    [SerializeField] private baseGaurd b_gaurd;
    public float angle;
    public playerControl player;
    public List<GameObject> hitList;
    public float Fov;
    public int rayCount;
    public GameObject eye;
    public float startAngle;
    public float viewDistance;

    //public int q = 1;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //daddy = this.transform.parent.gameObject;
        b_gaurd = daddy.GetComponent<baseGaurd>();

    private void Update(){
       if(this.b_gaurd.TYPE == type.gaurd){
        if(this.b_gaurd.dir == direction.east){
         angle = 70f;
        }
        if(this.b_gaurd.dir == direction.north){
            angle = 160f;
        }
        if(this.b_gaurd.dir == direction.west){
            this.angle = 250f;
        }
        if(this.b_gaurd.dir == direction.south){
            angle = 340f;
        }
       
         Fov = 140f;
       }
     
        Vector3 Origin = new Vector3();
        if(this.b_gaurd.TYPE == type.wizard){
         Origin = (eye.transform.position);
         this.angle = startAngle;
        Fov = 360f;
        rayCount = 360;
        viewDistance = 5f;
         
        }
        if(this.b_gaurd.TYPE == type.gaurd){
         Origin = daddy.transform.position;
         rayCount = 360;
         viewDistance = 10f;

        }

        Vector3 OriginLocal = transform.InverseTransformPoint(Origin);
        
        
      
        float angleIncrease = Fov/rayCount;
        


        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = OriginLocal;
        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit2D hit = Physics2D.Raycast(Origin, GetVectorFromAngle(angle), viewDistance);
            if (hit.collider == null)
            {
                // angle = 150;
                vertex = OriginLocal + (GetVectorFromAngle(angle) * viewDistance);
            }
            else
            {

                vertex = transform.InverseTransformPoint(hit.point);



                //vertex = hit.barycentricCoordinate;

                if (hit.collider.gameObject.tag == "Player")
                {
                    GameObject p = hit.collider.gameObject;

                    if(!this.hitList.Contains(p)){
                    this.hitList.Add(p);
                    }
                    //this.b_gaurd.playerSpotted = true;
                   
                    this.b_gaurd.FinalTarget = p.GetComponent<baseUnit>().tile;
                   if(p.GetComponent<movement>().stealth == stealth.stealthMode){
                        if(this.b_gaurd.path == null){
                    this.b_gaurd.path = TileManager.FindPath(this.b_gaurd.tile, this.b_gaurd.FinalTarget);
                        
                    if(this.b_gaurd.path != null){
                    this.b_gaurd.move();
                    }
                        }
                        }
                    }
                  
                    
                    //continue;
                    //b_gaurd.move();
                

            }
            vertices[vertexIndex] = vertex;

            if (i > 0)
            {

                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;

        }

        if(this.hitList.Any()){
            player = p.GetComponent<playerControl>();
            this.b_gaurd.playerSpotted = true;
            this.hitList.RemoveAt(0);
            player.isSeen = this.b_gaurd.playerSpotted;
        }
        else{
            this.b_gaurd.playerSpotted = false;
            player.isSeen = this.b_gaurd.playerSpotted;
        }
       

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;


    }
  

    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
