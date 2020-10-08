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


    public PlayerType CurrentptayerType = PlayerType.Egor;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject crossHair;
    public LayerMask ObMask;
    float hitDistanse = 0.3f;
    Collider2D currentCollider;
    GameObject interactingObject;
    public PlayerUIController playerUIController;




    public Inventory Inventory;

    protected override void Start()
    {
        base.Start();
        if (OneSavePanel.SaveNum != -1)
        {
            LoadSave(); 
        }

       // Inventory.Ammo = 10;
       // playerUIController = FindObjectOfType<PlayerUIController>();
       // Debug.Log((playerUIController == null)+"    "+CurrentptayerType);
        if (playerUIController != null)
        {
            UpdateUI();
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
        if (crossHair != null)
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
            crossHair.transform.position = Vector2.Lerp(crossHair.transform.position, aim, 10);
        }



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
            Inventory.MedicineChestCount += (interactable as IMedicineChest).Count;
            playerUIController.SetMedicineCount(Inventory.MedicineChestCount);
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
        if (Inventory.MedicineChestCount != 0 && stats.health < stats.MaxHealth)
        {
            float a = Mathematics.GetPercent(Inventory.HealingPercentByMedicineChest, stats.MaxHealth);
            Inventory.MedicineChestCount--;
            Healing(a);
            playerUIController.SetMedicineCount(Inventory.MedicineChestCount);
        }
    }
    public void Healing(float hp)
    {
        if (stats.health + hp <= stats.MaxHealth)
        {
            stats.health += hp;
        }
        else
            stats.health = stats.MaxHealth;
        playerUIController.SetHp(stats.MaxHealth, stats.health);
    }
    public override void Die()
    {
        base.Die();
        //health = -1;
        //SceneManager.LoadScene(0);
    }
    public override void TakeDamage(float Dmg)
    {
        if (Inventory.ShildCount == 0)
        {
            base.TakeDamage(Dmg);

        }
        else if (Inventory.ShildCount >= Dmg)
        {
            Inventory.ShildCount -= Dmg;

        }
        else
        {
            Dmg -= Inventory.ShildCount;
            base.TakeDamage(Dmg);

        }
        playerUIController.SetHp(stats.MaxHealth, stats.health);


    }
    void LoadSave()
    {
        transform.position = new Vector2(SaveController.saves[OneSavePanel.SaveNum].PlayerPosX, SaveController.saves[OneSavePanel.SaveNum].PlayerPosY);
        SaveHelper.loadStats(SaveHelper.GetStats(CurrentptayerType), stats);
        SaveHelper.LoadInv(SaveHelper.GetInv(CurrentptayerType), Inventory);
        if ((PlayerType)SaveController.saves[OneSavePanel.SaveNum].PlayerType != CurrentptayerType)
        {

            gameObject.SetActive(false);
        }
    }
    public void UpdateUI()
    {

        playerUIController.SetHp(stats.MaxHealth, stats.health);
        playerUIController.SetMedicineCount(Inventory.MedicineChestCount);
        playerUIController.SetBullet(Inventory.Ammo);
        playerUIController.SetMoney(CarInventory.MoneyCount);
        playerUIController.SetMutagenCount(CarInventory.MutagenCount);
        playerUIController.SetSanorinCount(CarInventory.SanorinCount);
    }

}

