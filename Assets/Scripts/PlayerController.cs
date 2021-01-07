using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    #region Fields

    //Attribut qui sert à rendre visible notre champs dans l'interface Unity
    [SerializeField]
    private float mSpeed = 3f;

    [SerializeField]
    private float mMouseSensitivityX = 3f;

    [SerializeField]
    private float mMouseSensitivityY = 3f;

    private PlayerMotor mMotor;

    #endregion //Fields

    //Sert à initialiser notre script
    private void Start()
    {
        this.mMotor = GetComponent<PlayerMotor>(); // Sert à stocker le script PlayerMotor dans notre champs PlayerMotor
    }

    //Sert à détecter sur quelles touches l'utilisateur appuie
    private void Update()
    {
        //Calculer la vélocité (vitesse) du mouvement de notre joueur
        float lXMove = Input.GetAxisRaw("Horizontal"); //Vers droite/gauche
        float lZMove = Input.GetAxisRaw("Vertical"); //Vers avant/arriere

        Vector3 lMoveHorizontal = transform.right * lXMove; //Précise à Unity sur quel axe on va calculer le déplacement (Transform.right) donc Horizontal
        Vector3 lMoveVertical = transform.forward * lZMove;

        Vector3 lVelocity = (lMoveVertical + lMoveHorizontal).normalized * this.mSpeed; //Les deux vecteurs réunis forme la direction où on veut aller multiplié par la vitesse ce qui forme la vélocité

        this.mMotor.Move(lVelocity);

        //Calcul de la rotation du joueur en un Vector3 en fonction de l'axe de la souris
        float lYRotation = Input.GetAxisRaw("Mouse X"); //La rotation se fait en fonction de l'axe vertical soit Y Rotation

        //Arg x y z, on modifie y car on est toujours en fonction de cet Axe pour bouger en horizontal
        Vector3 lMouseRotation = new Vector3(0, lYRotation, 0) * this.mMouseSensitivityX;

        this.mMotor.Rotate(lMouseRotation);

        //Calcul de la rotation de la caméra en un Vector3 en fonction de l'axe de la souris
        float lXRotation = Input.GetAxisRaw("Mouse Y"); //La rotation se fait en fonction de l'axe vertical soit Y Rotation

        //Arg x y z, on modifie y car on est toujours en fonction de cet Axe pour bouger en horizontal
        Vector3 lCameraRotation = new Vector3(lXRotation, 0, 0) * this.mMouseSensitivityY;

        this.mMotor.RotateCamera(lCameraRotation);
    }

}
