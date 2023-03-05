using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime; // MAKE SURE UR USING THIS!!!!!!!!!!!!!!!
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NovelManager : MonoBehaviour
{
    private static NovelManager instance;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Other UI")]

    [SerializeField] private GameObject bgPanel;
    [SerializeField] private GameObject[] choices;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject startButton;

    private Story currentStory; // The Story object you use to advance line by line.

    public TextAsset testingScript; //This is just here to feed the JSON file to start dialogue


    public InputAction submitAction; // This is the submit action to advance dialogue + select choices. if don't know what the new input system is, you can use old one.
    // CERTAIN THINGS IN THIS SCRIPT WILL NOT WORK IF YOU USE OLD INPUT SYSTEM.

    // Current State
    public bool dialogueIsPlaying { get; private set; }

    private Coroutine displayLineCoroutine;

    private bool isLineScolling = false;
    
    private bool desireSkipLineScrolling = false;




    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Multiple instances of NovelManager detected");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        startPanel.SetActive(true);
        StartCoroutine(SelectStart());
        AudioManager.GetInstance().SwitchTheme("Frigid");
        //StartDialogue(testingScript);

        foreach(GameObject button in choices)
        {
            button.SetActive(false);
        }
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        if(!dialogueIsPlaying) { return; }

        if (currentStory.currentChoices.Count == 0 && !isLineScolling)
        {
            ContinueStory();
        } else if(isLineScolling) //If cannot continue, try to skip line scrolling. THIS IS ONLY NEEDED IF YOU WANT LINE SKIPPING.
        {
            desireSkipLineScrolling = true;
        }
    }

    public void StartDialogue(TextAsset inkJSON) // gets the ink json file
    {
        startPanel.SetActive(false);
        AudioManager.GetInstance().SwitchTheme(" ");

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;

        ContinueStory();
    }

    void EndDialogue()
    {
        dialogueIsPlaying = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //dialogueText.text = "";
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {

            if (isLineScolling)
            {
                return;
            }

            if (displayLineCoroutine != null) StopCoroutine(displayLineCoroutine);

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            HandleTags(currentStory.currentTags);
        }
        else
        {
            EndDialogue();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        HideChoices();

        isLineScolling = true;

        foreach (char letter in line.ToCharArray()) 
        {

            if (desireSkipLineScrolling)
            {
                desireSkipLineScrolling = false;
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            dialogueText.maxVisibleCharacters++;
            AudioManager.GetInstance().PlaySound("Text_Scroll");
            yield return new WaitForSeconds(0.03f);
        }

        DisplayChoices();
        isLineScolling = false;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            // split tag into key and value parts
            string[] splitTag = tag.Split(':');

            if(splitTag.Length != 2 ) 
            {
                Debug.LogError("tag could not be parsed correctly. Tag: " + splitTag);
            }

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();


            // handle tags
            switch (tagKey)
            {
                case "speaker":
                    nameText.text = (tagValue == "NONE") ? " " : tagValue + ':'; //If "NONE", don't add a question mark
                    break;
                case "expression":
                    string[] expressionValue = tagValue.Split('_');
                    ImageManager.GetInstance().SetExpression(expressionValue[0], tagValue);
                    break;
                case "splash":
                    ImageManager.GetInstance().SetSplash(tagValue);
                    break;
                case "playSound":
                    AudioManager.GetInstance().PlaySound(tagValue);
                    break;
                case "stopSound":
                    AudioManager.GetInstance().StopSound(tagValue);
                    break;
                case "switchTheme":
                    AudioManager.GetInstance().SwitchTheme(tagValue);
                    break;
                default:
                    Debug.Log("Tag was parsed but cannot be interpretted. Tag Key: " + tagKey);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices requested than can be supported. Number of choices requested: " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice option in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choices[index].GetComponentInChildren<TextMeshProUGUI>().text = option.text; //set text of button
            index++;
        }

        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private void HideChoices()
    {
        foreach(GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }


    public void MakeChoice(int choiceIndex)
    {
        if(isLineScolling) { return; }

        currentStory.ChooseChoiceIndex(choiceIndex);

        ContinueStory();
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    // Subscribe to submitAction event
    private void OnEnable()
    {
        submitAction.Enable();
        submitAction.started += OnSubmit;
    }

    private void OnDisable() 
    {
        submitAction.started -= OnSubmit;
        submitAction.Disable();
    }

    private IEnumerator SelectStart()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(startButton);
    }
}
