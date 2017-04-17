using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    Animator anim;

    bool isFaded = false;
    public bool IsFaded { get { return isFaded; } }
    

    void Start ()
    {
        anim = GetComponent<Animator>();
	}

    public void FadeToBlack()
    {
        anim.SetTrigger("FadeBlack");
    }
    public void FadeAway()
    {
        anim.SetTrigger("FadeAway");
    }
}
