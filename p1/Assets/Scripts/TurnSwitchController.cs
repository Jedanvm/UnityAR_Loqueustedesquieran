using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TurnSwitchController : MonoBehaviour
{
    public ActionBasedSnapTurnProvider SnapTurnProvider;
    public ActionBasedContinuousTurnProvider ContinuousTurnProvider;

    public bool SnapTurnActive = true;

    private void Awake(){
        SwitchTurn();
    }

    [ContextMenu("Ejecutar -> SwitchTurn")]

    public void SwitchTurn(){
        if (SnapTurnActive == true){
            SnapTurnProvider.turnAmount = 0;
            ContinuousTurnProvider.turnSpeed = 60;
            SnapTurnActive = false;
        }else if (SnapTurnActive == false){
            SnapTurnProvider.turnAmount = 15;
            ContinuousTurnProvider.turnSpeed = 0;
            SnapTurnActive = true;
        }
    }
}
