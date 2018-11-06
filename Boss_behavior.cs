using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss_behavior : MonoBehaviour {

    public Rigidbody2D bullet;
    public float bullet_speed = 2.0f;
    public int Boss_health = 200;
    public int cur_Bosshealth;
    public float walk_run_dis = 0.1f;
    float dis;
    bool isdeath = false;
    int chose_act = 0;
    float e_timer = 6.0f;
    float e_timer_1 = 3.0f;
    float e_timer_2 = 5.0f;
    Animator e_animator;
    GameObject player;
    //CharacterController e_cc;
	// Use this for initialization
	void Start ()
    {
        cur_Bosshealth = Boss_health;
        player = GameObject.FindGameObjectWithTag("Player");
        e_animator = GetComponent<Animator>();
       // e_cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        dis = Vector2.Distance(this.transform.position, player.transform.position);
        if(cur_Bosshealth>0)
        {
            Activity();
        }
        else if(!isdeath)
        {
            e_animator.SetTrigger("death");
            isdeath = true;
        }
	}

    void Activity()
    {
        float speed_run = 0.03f;
        float speed_walk = 0.05f;
        float speed_hang = 0.07f;
        Vector3 velocity = Vector3.zero;
        Vector3 newposition = Vector3.zero;
        float x_e = this.transform.position.x;
        float x_p = player.transform.position.x;
        if (x_p > x_e)
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
        else
            this.transform.localEulerAngles = new Vector3(0, -180, 0);
        if (cur_Bosshealth > 120)
        {
            if (dis > 10)
            {
                if(e_timer_2<=0)
                {
                    chose_act = Random.Range(0, 2);
                    e_timer_2 = 5.0f;
                }
                // e_animator.SetTrigger("idle_1");
                if (chose_act == 0)
                {
                    e_animator.SetTrigger("run");
                    // e_cc.SimpleMove();
                    if (this.transform.localEulerAngles.y == 0)
                        newposition = this.transform.position + new Vector3(walk_run_dis, 0, 0);
                    else
                        newposition = this.transform.position + new Vector3(-walk_run_dis, 0, 0);
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, newposition, ref velocity, speed_run);
                    e_timer_2 = e_timer_2 - Time.deltaTime;
                }
                else
                {
                    e_animator.SetTrigger("skull_2");
                    if (this.transform.localEulerAngles.y == 0)
                    {
                        Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(bullet_speed, 0);
                    }
                    else
                    {
                        Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                        bulletInstance.velocity = new Vector2(-bullet_speed, 0);
                    }

                }
            }
            if(dis<=10&&dis>5)
            {
                e_timer -= Time.deltaTime;
                Debug.Log(e_timer);
                e_animator.SetTrigger("walk");
                if (e_timer <= 6&&e_timer>4)
                {
                    if (this.transform.localEulerAngles.y == 0)
                        newposition = this.transform.position + new Vector3(walk_run_dis, 0, 0);
                    else
                        newposition = this.transform.position + new Vector3(-walk_run_dis, 0, 0);
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, newposition, ref velocity, speed_hang);
                }
                if(e_timer <= 4 && e_timer > 2)
                {
                    if (this.transform.localEulerAngles.y == 0)
                        newposition = this.transform.position + new Vector3(-walk_run_dis, 0, 0);
                    else
                        newposition = this.transform.position + new Vector3(walk_run_dis, 0, 0);
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, newposition, ref velocity, speed_hang);
                }
                if (e_timer <= 2 && e_timer > 0)
                {
                    if (this.transform.localEulerAngles.y == 0)
                        newposition = this.transform.position + new Vector3(walk_run_dis, 0, 0);
                    else
                        newposition = this.transform.position + new Vector3(-walk_run_dis, 0, 0);
                    this.transform.position = Vector3.SmoothDamp(this.transform.position, newposition, ref velocity, speed_hang);
                }
                if(e_timer<=0)
                {
                    e_animator.ResetTrigger("idle_1");
                    e_animator.ResetTrigger("walk");
                    e_animator.ResetTrigger("run");
                    e_animator.SetTrigger("skill_2");
                }
                if (e_timer <= -0.25)
                    e_timer = 6.0f;
            }
            if(dis<=5)
            {
                e_animator.ResetTrigger("idle_1");
                e_animator.ResetTrigger("walk");
                e_animator.ResetTrigger("run");
                //e_animator.SetTrigger("idle_1");
                //e_animator.SetTrigger("skill_1");
                e_timer_1 = e_timer_1 - Time.deltaTime;
                Debug.Log(e_timer_1);
                if (e_timer_1 <= 0)
                {
                    e_animator.SetTrigger("skill_1");
                    e_timer_1 = 3.0f;
                }
            }
               
        }
        if (cur_Bosshealth <= 25)
        {

        }
    }
}
