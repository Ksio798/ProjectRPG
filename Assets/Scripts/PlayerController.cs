﻿using System.Collections;
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

    public float offset;
    public PlayerType CurrentptayerType = PlayerType.Egor;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    public GameObject crossHair;
    public LayerMask ObMask;
    float hitDistanse = 0.3f;
    Collider2D currentCollider;
    GameObject interactingObject;
    public PlayerUIController playerUIController;
    float Lastside = 1;

    public float CrossHairRadius=4;

    public Inventory Inventory;

    protected override void Start()
    {
        //  base.Start();
        //if (OneSavePanel.SaveNum != -1)
        //{
        //    LoadSave(); 
        //}


        //if (playerUIController != null)
        //{
        //   // UpdateUI();
        //}

         rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitToMannaRegen());
       
    }

    public void  LoadPlayerCOntrollerData()
    {
        stats.health = stats.MaxHealth;
        if (OneSavePanel.SaveNum != -1)
        {
            LoadSave();
        }


        //if (playerUIController != null)
        //{
        //     UpdateUI();
        //}


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
        if (hInput != 0)
            Lastside = hInput;
                if(Lastside<0)
            transform.localScale = new Vector3(-2, 2, 2);
        else
            transform.localScale = new Vector3(2, 2, 2);
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





            if (dist > CrossHairRadius)
            {

                Vector3 offset = aim - transform.position;
                offset = offset.normalized;

                aim.x = offset.x* CrossHairRadius + transform.position.x;
                aim.y = offset.y* CrossHairRadius + transform.position.y;

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
        if(playerUIController != null)
        playerUIController.SetHp(stats.MaxHealth, stats.health);
    }
    public override void Die()
    {
        FindObjectOfType<PausMenuController>().GameOver(); 
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
          //  Debug.Log(SaveController.saves[OneSavePanel.SaveNum].IsQSave +"     "+ OneSavePanel.SaveNum);
        if(SaveController.saves[OneSavePanel.SaveNum].IsQSave==false)
        {
        transform.position = new Vector2(SaveController.saves[OneSavePanel.SaveNum].PlayerPosX, SaveController.saves[OneSavePanel.SaveNum].PlayerPosY);
        }
        SaveHelper.loadStats(SaveHelper.GetStats(CurrentptayerType), stats);
        SaveHelper.LoadInv(SaveHelper.GetInv(CurrentptayerType), Inventory);
        if ((PlayerType)SaveController.saves[OneSavePanel.SaveNum].PlayerType != CurrentptayerType)
        {

            gameObject.SetActive(false);
        }
    }
    public void UpdateUI()
    {
        playerUIController.SetManna(stats.MaxManna, stats.manna);
        playerUIController.SetHp(stats.MaxHealth, stats.health);
        playerUIController.SetMedicineCount(Inventory.MedicineChestCount);
        playerUIController.SetBullet(Inventory.Ammo);
        playerUIController.SetMoney(CarInventory.MoneyCount);
        playerUIController.SetMutagenCount(CarInventory.MutagenCount);
        playerUIController.SetSanorinCount(CarInventory.SanorinCount);
    }
    protected override IEnumerator WaitToMannaRegen()
    {
        if(playerUIController != null)
        playerUIController.SetManna(stats.MaxManna, stats.manna);
        return base.WaitToMannaRegen();
    }
    //void GunMoving()
    //{
    //    Vector3 pointerPosition = Input.mousePosition;
    //    Vector3 difference = Camera.main.ScreenToWorldPoint(pointerPosition) - transform.position;
    //    float scaleX = Mathf.Sign(transform.parent.localPosition.x);
    //    if (scaleX < 0)
    //        difference = transform.position - Camera.main.ScreenToWorldPoint(pointerPosition);
    //    float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0f, 0f, rotateZ );

    //    Debug.Log("rotatiob");
    //}
}

