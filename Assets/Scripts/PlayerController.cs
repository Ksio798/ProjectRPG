using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.SceneManagement;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
public enum PlayerType
{
    Egor,
    Dimitry,
    Maxim,
    Alex
}

public class PlayerController : BaseCharecter
{
   
    float shildCount = 5;
    public float ShildCount { get { return shildCount; } }
    public PlayerType CurrentptayerType = PlayerType.Egor;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject crossHair;
    public LayerMask ObMask;
    float hitDistanse = 0.3f;
    Collider2D currentCollider;
    GameObject interactingObject;
    public PlayerUIController playerUIController;
    int medicineChestCount = 3;
  


    public Inventory Inventory;
    public int MedicineChestCount
    {
        get { return medicineChestCount; }
        set
        {
            if (value <= Inventory.MaxMedicineChestCount)
            {
                medicineChestCount = value;
              //  Debug.Log(medicineChestCount);
            }
            else
            {
                medicineChestCount = Inventory.MaxMedicineChestCount;
                int a = value;
                int b =a- Inventory.MaxMedicineChestCount ;
           
               // Debug.Log(b);
                CarInventory.MedChestCount += b;

               // Debug.Log(medicineChestCount + "ErrorS");
            }


        } }
    public override void Start()
    {
        base.Start();
        if (playerUIController != null)
        {
            playerUIController.SetHp(stats.MaxHealth, health);

            playerUIController.SetShild(stats.MaxShield, shildCount);
            playerUIController.SetMedicineCount(medicineChestCount);
        }
        rb = GetComponent<Rigidbody2D>();
    }

   
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
        //RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector3.left, hitDistanse, ObMask);
        //RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector3.right, hitDistanse, ObMask);
        //RaycastHit2D hitTop = Physics2D.Raycast(transform.position, Vector3.up, hitDistanse, ObMask);
        //RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector3.down, hitDistanse, ObMask);
        //RaycastHit2D hitRightDown = Physics2D.Raycast(transform.position, Vector3.down + Vector3.right, hitDistanse, ObMask);
        //RaycastHit2D hitRightUp = Physics2D.Raycast(transform.position, Vector3.up + Vector3.right, hitDistanse, ObMask);
        //RaycastHit2D hitLefttUp = Physics2D.Raycast(transform.position, Vector3.up + Vector3.left, hitDistanse, ObMask);
        //RaycastHit2D hitLefttDown = Physics2D.Raycast(transform.position, Vector3.down + Vector3.left, hitDistanse, ObMask);
        //if (hitLeft && hInput < 0)
        //{
        //    hInput = 0;
        //}
        //if (hitRight && hInput > 0)
        //{
        //    hInput = 0;
        //}
        //if (hitTop && vInput > 0)
        //{
        //    vInput = 0;
        //}
        //if (hitDown && vInput < 0)
        //{
        //    vInput = 0;
        //}
        //if (hitRightDown && vInput < 0 && hInput > 0)
        //{
        //    vInput = 0;
        //    hInput = 0;
        //}
        //if (hitRightUp && vInput > 0 && hInput > 0)
        //{
        //    vInput = 0;
        //    hInput = 0;
        //}
        //if (hitLefttUp && vInput > 0 && hInput < 0)
        //{
        //    vInput = 0;
        //    hInput = 0;
        //}
        //if (hitLefttDown && vInput < 0 && hInput < 0)
        //{
        //    vInput = 0;
        //    hInput = 0;
        //}
        Vector2 moveInput = new Vector2(hInput, vInput);
        moveVelocity = moveInput.normalized * stats.Speed;
    }
    void FixedUpdate()
    {
        if (CanMove)
            rb.velocity = moveVelocity * Time.fixedDeltaTime;
        else
            rb.velocity = Vector2.zero;
       
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
        if (collision == currentCollider)
            currentCollider = null;
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
            playerUIController.SetMedicineCount(medicineChestCount);
        }
        interactable.Interact(transform);
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
           // TakeDamage(1);
        }
    }
    void HealingByMedicineChest()
    {
        if (medicineChestCount!=0)
        {
            float a = Mathematics.GetPercent(Inventory.HealingPercentByMedicineChest, stats.MaxHealth);
            medicineChestCount--;
            Healing(a);
            playerUIController.SetMedicineCount(medicineChestCount);
        }
    }
    public void Healing(float hp)
    {
        if (health + hp <= stats.MaxHealth)
        {
            health += hp;
        }
        else
            health = stats.MaxHealth;
    }
    public override void Die()
    {
       base.Die();
        //health = -1;
        //SceneManager.LoadScene(0);
    }
    public override void TakeDamage(float Dmg)
    {
        if(shildCount == 0)
        {
        base.TakeDamage(Dmg);
           
        }
        else if(shildCount>=Dmg)
        {
            shildCount -= Dmg;
          
        }
        else
        {
            Dmg -= shildCount;
            base.TakeDamage(Dmg);
           
        }
        playerUIController.SetHp(stats.MaxHealth, health);
        playerUIController.SetShild(stats.MaxShield, shildCount);
    }
  
}
           
