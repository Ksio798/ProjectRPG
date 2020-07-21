using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : BaseCharecter
{
    public float Speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject crossHair;
    public LayerMask ObMask;
    float hitDistanse = 0.3f;
    Collider2D currentCollider;
    GameObject interactingObject;
    
    int medicineChestCount = 0;
    public int MedicineChestCount
    {
        get { return medicineChestCount; }
        set
        {
            if (value <= DataBase.MaxMedicineChestCount)
            {
                medicineChestCount = value;
                Debug.Log(medicineChestCount);
            }
            else
            {
                int a = value;
                int b = DataBase.MaxMedicineChestCount - medicineChestCount;
                a -= b;
                medicineChestCount = DataBase.MaxMedicineChestCount;
                Debug.Log(medicineChestCount + "ErrorS");
            }


        } }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Interact();
        UseSkills();
        moveCrossHair();
       


    }
    void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, hitDistanse, ObMask);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, hitDistanse, ObMask);
        RaycastHit2D hitTop = Physics2D.Raycast(transform.position, Vector3.up, hitDistanse, ObMask);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector3.down, hitDistanse, ObMask);
        RaycastHit2D hitRightDown = Physics2D.Raycast(transform.position, Vector3.down + Vector3.right, hitDistanse, ObMask);
        RaycastHit2D hitRightUp = Physics2D.Raycast(transform.position, Vector3.up + Vector3.right, hitDistanse, ObMask);
        RaycastHit2D hitLefttUp = Physics2D.Raycast(transform.position, Vector3.up + Vector3.left, hitDistanse, ObMask);
        RaycastHit2D hitLefttDown = Physics2D.Raycast(transform.position, Vector3.down + Vector3.left, hitDistanse, ObMask);
        if (hitLeft && hInput < 0)
        {
            hInput = 0;
        }
        if (hitRight && hInput > 0)
        {
            hInput = 0;
        }
        if (hitTop && vInput > 0)
        {
            vInput = 0;
        }
        if (hitDown && vInput < 0)
        {
            vInput = 0;
        }
        if (hitRightDown && vInput < 0 && hInput > 0)
        {
            vInput = 0;
            hInput = 0;
        }
        if (hitRightUp && vInput > 0 && hInput > 0)
        {
            vInput = 0;
            hInput = 0;
        }
        if (hitLefttUp && vInput > 0 && hInput < 0)
        {
            vInput = 0;
            hInput = 0;
        }
        if (hitLefttDown && vInput < 0 && hInput < 0)
        {
            vInput = 0;
            hInput = 0;
        }
        Vector2 moveInput = new Vector2(hInput, vInput);
        moveVelocity = moveInput.normalized * Speed;
    }
    void FixedUpdate()
    {
       rb.velocity = moveVelocity * Time.fixedDeltaTime;
       
       // rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    private void moveCrossHair()
    {
        if (crossHair!=null)
        {

        Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aim.z = 0;
        float dist = Vector3.Distance(aim, transform.position);





        if (dist > 2)
        {

            Vector3 offset = aim - transform.position;
            offset = offset.normalized;

            aim.x = offset.x * 2 + transform.position.x;
            aim.y = offset.y * 2 + transform.position.y;

        }
        crossHair.transform.position = Vector2.Lerp(crossHair.transform.position, aim,10); 
        }

        //if (aim.magnitude> 0.0f)
        //{
        //   aim.Normalize();
        //    aim *= 0.5f;
        //    crossHair.transform.localPosition = aim;
        //    crossHair.SetActive(true);


        //}
        //else
        //    {
        //        crossHair.SetActive(false);
        //    }
        
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentCollider != collision)
        {
            currentCollider = collision;
            IInteractable interactable = collision.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (!interactable.InteractingByKeyPressing) // если с объектом взаимодействуют бещ нажатия клавиши, то вызывать действие interaction ( см. Ниже)
                {
                    interaction(interactable);
                }
                else
                {
                   

                    // иначе запомнить объект столкновения
                    interactingObject = collision.gameObject;
                 
                }

            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (currentCollider != collision)
        {
            currentCollider = collision;
            IInteractable interactable = collision.transform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (!interactable.InteractingByKeyPressing) // если с объектом взаимодействуют бещ нажатия клавиши, то вызывать действие interaction ( см. Ниже)
                {
                    interaction(interactable);
                }
                else
                {


                    // иначе запомнить объект столкновения
                    interactingObject = collision.gameObject;

                }

            }
        }
    }
     void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject == interactingObject)
        {
            interactingObject = null;
        }
    }
    void interaction(IInteractable interactable)
    {
        if (interactable is IMedicineChest)
        {
            MedicineChestCount += (interactable as IMedicineChest).Count;
        }
        interactable.Interact();
    }
    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactingObject != null)
        {
            interaction(interactingObject.GetComponent<IInteractable>());
        }
        
    }
    void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HealingByMedicineChest();
        }
    }
    void HealingByMedicineChest()
    {
        if (medicineChestCount!=0&& health!= MaxHealth)
        {
            float a = Mathematics.GetPercent(DataBase.HealingPercentByMedicineChest, MaxHealth);
            Healing(a);
        }
    }
    public void Healing(float hp)
    {
        if (health + hp <= MaxHealth)
        {
            health += hp;
        }
        else
            health = MaxHealth;
    }
    public override void Die()
    {
        base.Die();
        health = -1;
        SceneManager.LoadScene(0);
    }
}
           
