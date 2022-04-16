using UnityEngine;

public class CalmAnger : MonoBehaviour
{
    private Animator mAnimator;
    public Material mat;
    public Transform other;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
        other = GameObject.Find("Trigger").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (mAnimator != null)
        {
            var dist = Vector3.Distance(transform.position, other.transform.position);
            if (dist >= 7.5f)
            {
                mAnimator.SetTrigger("TrCalm");
            }
            else if (dist <= 7f)
            {
                mAnimator.SetTrigger("TrAnger");
                gameObject.GetComponent<MeshRenderer>().material = mat;
            }
        }
    }
}
//Behaviour is seperate to where the states are set.
// mAnimator.SetBool("Attack",true);
// mAnimator.SetTrigger("IsInRange");