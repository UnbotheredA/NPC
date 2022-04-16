using UnityEditor;
using UnityEngine;

public class Attack : State
{
    private Transform target;


    private GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/prefabs/Bullet.prefab", typeof(GameObject));

    private float bulletTimer = 1;


    public Attack(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        npc.speed = npc.attackSpeed;
        target = GameObject.Find("Trigger").transform;
        Debug.Log("Here on the attack state ");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (npc != null && target != null)
        {
            float distance = Vector3.Distance(npc.transform.position, target.transform.position);
            if (distance > 6)
            {
                //so npc stops rotating towards players rotation when going back to patrol
                //rigidbody2D.transform.rotation = Quaternion.identity;
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
            if (npc.GetComponent<CloseRangeNPC>() != null) 
            {
                CloseRangeAttack();
            }
            if (npc.GetComponent<FarRangeNPC>() != null) 
            {
                FarRangeAttack();
            }
        }
    }
    private void CloseRangeAttack()
    {
        Vector3 playerDirection = (target.transform.position - npc.transform.position).normalized;
        npc.rb.MovePosition(npc.transform.position + playerDirection * npc.speed * Time.deltaTime);
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        var rotationOfPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
        npc.rb.transform.rotation = rotationOfPlayer;
    }

    //TODO sometimes it seems like the npc shoots two bullets at a time or just once at a time
    public void FarRangeAttack()
    {
        //Timer so that bullets are Instantiated at a reasonable rate
        bulletTimer -= Time.deltaTime;
        Vector3 playerDirection = (target.transform.position - npc.transform.position).normalized;
        //far range npc to rotate and face the player while shooting
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        Quaternion rotationOfPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
        npc.transform.rotation = rotationOfPlayer;
        if (bulletTimer <= 0f)
        {
            //change farRangeNPC to npc on the postionOfBullet
            Vector3 positionOfBulletInstantiate = npc.transform.position;
            MonoBehaviour.Instantiate(prefab, positionOfBulletInstantiate, Quaternion.identity);
            bulletTimer = 1f;
        }
    }
}


