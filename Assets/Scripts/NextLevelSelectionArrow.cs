using UnityEngine;
using UnityEngine.UI;

public class NextLevelSelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound; //The sound we play when we move the arrow up/down
    [SerializeField] private AudioClip interactSound; //The sound we play when an option is selected
    [SerializeField] private float arrowOffset = 10f;
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Change position of the selected arrow
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            ChangePosition(1);  

        //Interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);


        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        //Assign the Y position of the current option to the arrow (basically moving it up/down)
        float offsetX = options[currentPosition].position.x + arrowOffset;
        rect.position = new Vector3(offsetX, rect.position.y, 0);

    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //Access the button component on each option and call it's function
           options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
