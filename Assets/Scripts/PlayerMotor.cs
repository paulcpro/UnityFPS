using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Camera mCam;

    private Vector3 mVelocity;

    private Vector3 mRotation;

    private Vector3 mCameraRotation;

    private Rigidbody mRb; //Responsable des déplacements (Component Rigidibody)

    #endregion //Fields

    private void Start()
    {
        this.mRb = GetComponent<Rigidbody>(); //Initialisation du personnage responsable des déplacements
    }

    //Déplacement du joueur
    public void Move(Vector3 pVelocity)
    {
        this.mVelocity = pVelocity; //Assignation de la vélocité à notre personnage
    }

    //Rotation de la souris
    public void Rotate(Vector3 pMouseRotation)
    {
        this.mRotation = pMouseRotation;
    }

    //Rotation de la caméra du joueur
    public void RotateCamera(Vector3 pCameraRotation)
    {
        this.mCameraRotation = pCameraRotation;
    }

    /// <summary>
    /// Délai d'appel à cette méthode fixe donc tous les tant de secondes conrairement à la méthode Update qui elle dépend de la framerate
    /// </summary>
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformRotation()
    {
        //On envoi en argument un quaternion qui est la représentation d'une rotation (x,y,z,w) soit la rotation en plus
        //La méthode Euler va permettre de convertir un Vector3 en Quaternion
        this.mRb.MoveRotation(this.mRb.rotation * Quaternion.Euler(this.mRotation));

        this.mCam.transform.Rotate(-this.mCameraRotation); //Si non négative, commandes souris inversé
    }

    private void PerformMovement()
    {
        //Vérification que le joueur n'appuie pas sur aucune touche pour se déplacer
        if(this.mVelocity != Vector3.zero)
        {
            //On envoi la position actuelle du joueur + la direction et la vitesse multiplié par le temps actuel du mouvement
            //Soit: pos actu + direction avec vitesse * le temps
            this.mRb.MovePosition(this.mRb.position + this.mVelocity * Time.fixedDeltaTime);
        }

    }

}
