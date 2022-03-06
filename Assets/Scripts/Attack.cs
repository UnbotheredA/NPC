using UnityEditor;
using UnityEngine;

public class Attack : State
{
    private Transform target;

    private GameObject closeRangeNPC;
    private GameObject farRangeNPC;
    private GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/prefabs/Bullet.prefab", typeof(GameObject));

    private float bulletTimer = 1;

    private Rigidbody2D rigidbody2D;

    public Attack(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        npc.speed = 5;
        closeRangeNPC = GameObject.Find("OfficalCloseRangeNPC");
        Debug.Log(closeRangeNPC);
        rigidbody2D = closeRangeNPC.GetComponent<Rigidbody2D>();
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        closeRangeNPC.GetComponent<BoxCollider2D>().isTrigger = false;
        //closeRangeNPCS = GameObject.FindGameObjectsWithTag("CloseRangeEnemy");
        farRangeNPC = GameObject.Find("FarRangeTest");
        target = GameObject.Find("Trigger").transform;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        Debug.Log("Here on the attack state ");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (npc != null & closeRangeNPC != null && target != null)
        {
            float distance = Vector3.Distance(npc.transform.position, target.transform.position);
            if (distance > 6)
            {
                //this if statement will never be true untill both npcs are > six??
                rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                //so npc stops rotating towards players rotation when going back to patrol
                rigidbody2D.transform.rotation = Quaternion.identity;
                Debug.Log(npc.name + "is further than 6 so needs to go patrol");
                Debug.Log("exit attack");
                stateMachine.ChangeState(npc.patrol);
            }
            else if (npc.health < 3)
            {
                Debug.Log(npc.health);
                stateMachine.ChangeState(npc.hide);
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (npc && target != null)
        {
            CloseRangeAttack();
            float distance = Vector3.Distance(farRangeNPC.transform.position, target.transform.position);
            if (distance <= 6f)
            {
                FarRangeAttack();
            }
        }
    }
    private void CloseRangeAttack()
    {
        Vector3 playerDirection = (target.transform.position - npc.transform.position).normalized;
        rigidbody2D.MovePosition(closeRangeNPC.transform.position + playerDirection * npc.speed * Time.deltaTime);
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        var rotationOfPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
        rigidbody2D.transform.rotation = rotationOfPlayer;
    }

    //TODO sometimes it seems like the npc shoots two bullets at a time or just once at a time
    public void FarRangeAttack()
    {
        //Timer so that bullets are Instantiated at a reasonable rate
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0f)
        {
            Vector3 positionOfBulletInstantiate = new Vector3(farRangeNPC.transform.position.x, farRangeNPC.transform.position.y, farRangeNPC.transform.position.z);
            MonoBehaviour.Instantiate(prefab, positionOfBulletInstantiate, Quaternion.identity);
            Vector3 playerDirection = (target.transform.position - npc.transform.position).normalized;
            //far range npc to rotate and face the player while shooting
            float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
            Quaternion rotationOfPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
            farRangeNPC.transform.rotation = rotationOfPlayer;
            bulletTimer = 1f;
        }
    }
}


