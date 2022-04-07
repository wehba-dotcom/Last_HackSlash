using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovment : MonoBehaviour
{
    CharacterController controller;
    Transform cam;
    public float speed = 6f;
    float gravity = 10;
    float verticalVelocity = 10;
    public float jumpValue = 10;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main.transform;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isSprint = Input.GetKey(KeyCode.LeftShift);
        float sprint = isSprint ? 2.5f : 1;
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }

        Vector3 moveDirection = new Vector3(horizontalInput * speed, 0, verticalInput * speed);
        anim.SetFloat("Speed", Mathf.Clamp(moveDirection.magnitude, 0, 0.5f) + (isSprint ? 0.5f : 0));

        if (controller.isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
                verticalVelocity = jumpValue;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        if (moveDirection.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        moveDirection = cam.TransformDirection(moveDirection);
        moveDirection = new Vector3(moveDirection.x * speed * sprint, verticalVelocity, moveDirection.z * speed * sprint);
        controller.Move(moveDirection * Time.deltaTime);
    }


    public void DoAttack()
    {
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = true;
        StartCoroutine(HideCollider());
    }

    IEnumerator HideCollider()
    {
        yield return new WaitForSeconds(0.5f);
        transform.Find("Collider").GetComponent<BoxCollider>().enabled = false;
    }
}
