using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    private Transform target;


    private GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/prefabs/Bullet.prefab", typeof(GameObject));

    private float bulletTimer = 1;
    private float attackTimer = 1;


    public Attack(NPC npc, StateMachine stateMachine) : base(npc, stateMachine)
    {

    }
    public override void Enter()
    {
        base.Enter();
        npc.SetColour(Color.red);
        npc.GetComponent<NavMeshAgent>().speed = npc.speed = npc.attackSpeed;
        target = GameObject.Find("Trigger").transform;
        Debug.Log("Here on the attack state ");
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (npc != null && target != null)
        {
            float distance = Vector3.Distance(npc.transform.position, target.transform.position);
            if (distance > 3)
            {
                //so npc stops rotating towards players rotation when going back to patrol
                //rigidbody2D.transform.rotation = Quaternion.identity;
                Debug.Log(npc.name + "is further than 3 so needs to go chase");
                Debug.Log("exit attack");
                stateMachine.ChangeState(npc.chase);
            }
            else if (distance > 6)
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
    public void DamagePlayer()
    {
        target.GetComponent<PlayerManager>().ApplyDamage();
    }
    private void CloseRangeAttack()
    {
        if (npc.InFOV(target.transform.position) && Vector3.Distance(npc.transform.position, target.transform.position) < 2f)
        {

            if (attackTimer == 1)
            {
                Debug.Log("Applying damage to player");
                DamagePlayer();
            }
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = 1;
            }

        }
        else
        {
            attackTimer = 1;
        }

        if (Vector3.Distance(npc.transform.position, target.transform.position) > 2.5f)
            npc.Nav.SetDestination(target.transform.position);
        else
            npc.Nav.speed = 0;

        Vector3 playerDirection = (target.transform.position - npc.transform.position).normalized;
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


