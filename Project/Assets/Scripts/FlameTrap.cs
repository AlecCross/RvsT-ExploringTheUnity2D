using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameTrap : MonoBehaviour
{
    Animator anim;
    TrapState State
    {
        get { return (TrapState)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        State = TrapState.DoesMotBurning;
        StartCoroutine(SwitchWorkTrap());
    }

    // Update is called once per frame
    IEnumerator SwitchWorkTrap()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            State = TrapState.IsBurning;
            GetComponent<CapsuleCollider2D>().enabled = true;
            yield return new WaitForSeconds(4f);
            State = TrapState.DoesMotBurning;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
    public enum TrapState
    {
        DoesMotBurning,
        IsBurning
    }
}
