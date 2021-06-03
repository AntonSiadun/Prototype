using Photon.Pun;
using UnityEngine;

namespace NewScripts
{
     public class PlayerCharacterStatusSelector : MonoBehaviourPunCallbacks,IPunInstantiateMagicCallback
     {
          public void OnPhotonInstantiate(PhotonMessageInfo info)
          {
               GameObject newGameObject = gameObject;
               
               if (photonView.IsMine==false)
               {
                    
                    newGameObject.tag = "Enemy";
                    Debug.Log("Enemy Instantiated");
               }
               else
               {
                    newGameObject.tag = "Local";
                    Debug.Log("Local Player Instantiated");
               }
               GameRules.SetStatusByType(this.gameObject);
          }
     }
}
