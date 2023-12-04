using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelChange : MonoBehaviour
{
   private Transform tr;
        // public GameObject Player;
        private float PlayerPosX;
        private float PlayerPosY;
        [SerializeField] public basePlayer Instance;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        tr = Instance.GetComponent<Transform>();
        PlayerPosX = Instance.transform.position.x;
        PlayerPosY = Instance.transform.position.y;


        if (PlayerPosX >= -65 && PlayerPosX <= -57 && PlayerPosY <= -29 && PlayerPosY >= -30)
        { //mainfloor exit

            tr.transform.position = new Vector3((float)-61.29, (float)-47.65);
        }

        if (PlayerPosX >= -64 && PlayerPosX <= -58 && PlayerPosY <= -45 && PlayerPosY >= -47)
        { //mainfloor entrance


            tr.transform.position = new Vector3((float)-61, (float)-29.06);
        }

        if (PlayerPosX >= -90 && PlayerPosX <= -86 && PlayerPosY <= -96 && PlayerPosY >= -97)
        { //lowerfloor exit


            tr.transform.position = new Vector3((float)-28.3, (float)-0.26);
        }

        if (PlayerPosX >= -30.47 && PlayerPosX <= -28.6 && PlayerPosY <= 6.8 && PlayerPosY >= -6.22)
        { //lowerfloor entrance


            tr.transform.position = new Vector3((float)-88, (float)-96.07);
        }

        if (PlayerPosX >= 21.8 && PlayerPosX <= 28.6 && PlayerPosY <= 15.55 && PlayerPosY >= 14.34)
        { //basement exit


            tr.transform.position = new Vector3((float)24.88, (float)24.98);
        }

        if (PlayerPosX >= 21.03 && PlayerPosX <= 29.16 && PlayerPosY <= 24.72 && PlayerPosY >= 23.67)
        { //basement entrance


            tr.transform.position = new Vector3((float)25.69, (float)13.16);
        }
    }


}