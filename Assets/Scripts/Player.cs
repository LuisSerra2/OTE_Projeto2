
using UnityEngine;


[SelectionBase]
public class Player : Singleton<Player> {

    public enum GameState {
        Idle,
        ChooseColor,
        Player,
        Restart
    }

    public GameState _GameState;

    public float speed = 10f;

    int playerMovemetnIndex;

    Rigidbody rb;

    private void OnEnable() {
        WebCammController.Instance.ON_FIST_EVENT += MoveDownEvent;
        WebCammController.Instance.ON_RPALM_EVENT += MoveUpEvent;
    }


    private void OnDisable() {
        WebCammController.Instance.ON_FIST_EVENT -= MoveUpEvent;
        WebCammController.Instance.ON_RPALM_EVENT -= MoveUpEvent;
    }
    private void MoveUpEvent() {
        playerMovemetnIndex = 0;
    }

    private void MoveDownEvent() {
        playerMovemetnIndex = 1;
    }

    private void Update() {
        if (UIManager.Instance.CanPlay()) {
            PlayerMovementHandler();
        }
    }

    public void PlayerMovementHandler() {
        float step = speed * Time.fixedDeltaTime;

        switch (playerMovemetnIndex) {
            case 0:
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, 4), step);
                break;
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, -2), step);
                break;
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
