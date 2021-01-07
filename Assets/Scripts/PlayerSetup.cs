using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] mComponentsToDisable;

    Camera mSceneCamera;

    private void Start()
    {
        //Verifie qu'on effectue pas l'opération sur notre joueur
        if(isLocalPlayer == false)
        {
            for (int i = 0; i < mComponentsToDisable.Length; i++)
            {
                mComponentsToDisable[i].enabled = false; //Désactivation de toutes les fonctionnalités qui ne nous appartiennent pas
            }

        }

        //Toutes ces lignes seront pour notre joueur à nous
        else
        {
            this.mSceneCamera = Camera.main; //Stock de l'ancienne caméra

            if(this.mSceneCamera != null)
            {
                Camera.main.gameObject.SetActive(false); //Désactivera la caméra de base quand on arrive sur le serveur
            }

        }

    }

    /// <summary>
    /// Dès que l'objet qui possède le script PlayerSetup est désactiver, alors cette méthode sera appelée
    /// </summary>
    private void OnDisable()
    {
        if(this.mSceneCamera != null)
        {
            Camera.main.gameObject.SetActive(true); //On réactive la caméra si on part du serveur
        }

    }

}
