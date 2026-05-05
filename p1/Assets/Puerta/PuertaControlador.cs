using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaControlador : MonoBehaviour
{
    public Animator pueltita;

    //open dor pli
    [ContextMenu("Abrilo pelotudo")]
    public void AbrirPuerta() {
        print("abrio la puelta");
        pueltita.SetBool("ABieltO",true);
    }
    //ta cerra la puelta
    [ContextMenu("Tenes que cerrar la puerta")]
    public void CerrarPuerta()
    {
        print("cerro la puelta");
        pueltita.SetBool("ABieltO",false);
    }
    //puerta afk
    [ContextMenu("la puelta")]
    public void PuertaCerrada()
    {
        print("puelta dolmida");
    }
}
