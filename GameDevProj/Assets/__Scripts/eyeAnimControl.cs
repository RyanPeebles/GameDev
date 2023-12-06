using UnityEngine;

public class eyeAnimControl : MonoBehaviour
{
    public bool spotted;
    public Animator anim;
    public bool eyeOpen;
    public bool blinkOpen = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        spotted = this.gameObject.GetComponentInChildren<fov>().b_gaurd.playerSpotted;
        if (spotted)
        {
            eyeOpen = true;
            anim.SetBool("playerNear", true);
        }
        else
        {
            eyeOpen = false;
            anim.SetBool("playerNear", false);
        }
    }
}
