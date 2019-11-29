using HCFramework;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancerAnimation : MonoBehaviour
{
    Animator m_Animator;
    Renderer m_ObjectRenderer;
    public Texture[] textures;
    int rand;
    float dsTravelled;
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float speed;
    GameObject cell;
    Vector3 rotation, position;
    bool doRotate = false;
    Transform tr;
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_ObjectRenderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        SetTexture();
        tr = FindObjectOfType<Camera>().transform;

        if (Random.Range(0, 100) < 50)
        {
            pathCreator = GameObject.FindGameObjectWithTag("BloodPath").GetComponent<PathCreator>();
        }
        else
        {
            pathCreator = GameObject.FindGameObjectWithTag("ReversePath").GetComponent<PathCreator>();
        }
        position = new Vector3(((float)Random.Range(-70, 70) / (float)100), ((float)Random.Range(-70, 70) / (float)100), ((float)Random.Range(-70, 70) / (float)100));
        cell = this.transform.FindChildGameObject("crab", false);
        Debug.Log(cell.name);
        cell.transform.position = position;
        speed = ((float)Random.Range(80,90) / (float)100);
        cell.transform.localScale = Vector3.one * ((float)Random.Range(100, 350) / (float)100);
    }

    void Update()
    { float distance = Mathf.Abs((transform.position - tr.position).magnitude);
        if ( distance>1)
        {
            dsTravelled += speed * Time.deltaTime*((distance-1.5f)/5);
            transform.position = pathCreator.path.GetPointAtDistance(dsTravelled, end);
        }
        transform.LookAt(tr);
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    SwitchDefendtoAttack();
        //}

        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    SwithAttackToDefend();
        //}

        //if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        //{
        //    m_Animator.SetBool("take_Hit", false);
        //}

    }

    void SetTexture()
    {
        rand = Random.Range(0, 20);
        if (rand > 10)
        { rand = 1; }
        else
        { rand = 0; }

        Debug.Log("Random" + rand);
        m_ObjectRenderer.material.SetTexture("_MainTex", textures[rand]);

    }

    void TakeHit()
    {

        m_Animator.SetBool("take_Hit", true);

    }

    void SwithAttackToDefend()
    {
        m_Animator.SetBool("attack_Defend", true);
        m_Animator.SetBool("defend_Attack", false);
    }

    void SwitchDefendtoAttack()
    {

        m_Animator.SetBool("attack_Defend", false);
        m_Animator.SetBool("defend_Attack", true);

    }

    void DestroyThisObject()
    {
        m_Animator.SetBool("destroy", true);
        Destroy(gameObject, 2);
        // Debug.Log("destroyed");
    }

    void ReturnToAttackMode()
    {

        m_Animator.SetBool("return_To_Attack", true);
        //m_Animator.SetBool("take_Hit", false);
    }




    //void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.tag == "Bullet")
    //    {

    //        //Debug.Log("hii" + collision.gameObject.GetComponent<BulletScipt>().right_Bullet);

    //        TakeHit();

    //        if (collision.gameObject.GetComponent<BulletScipt>().right_Bullet == 0)
    //        {
    //            DestroyThisObject();
    //            Debug.Log("destroyed");

    //        }
    //        else
    //        {
    //            ReturnToAttackMode();
    //        }


    //        Destroy(collision.gameObject);

    //    }

    //}

}
