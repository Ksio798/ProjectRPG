using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public bool hold;
    public float distance = 1f;
    RaycastHit2D hit;
    public Transform holdPoint;
    public LayerMask weapom;


    public GunController Gun;
    public Collider2D hitObject;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hold = false;
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                //    Physics2D.queriesStartInColliders = false;
                //    hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance,weapom);

                //    if (hit.collider)
                //    {
                //        hold = true;
                //    }


                if (hitObject != null)
                    hold = true;
            }
        }
        if (hold)
        {
            hitObject.gameObject.transform.position = holdPoint.position;
            
           WeaponData W = hitObject.transform.GetComponent<WeaponData>();
            if(W!=null)
            {
                Gun.SetNewWeapon(W);
            }

            // hit.collider.gameObject.transform.position = holdPoint.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {

        if  ((weapom.value &  1<<collision.gameObject.layer)>0)
        {

            hitObject = collision;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        if (hitObject==collision)
        {

            hitObject = null;
        }
    }

}




