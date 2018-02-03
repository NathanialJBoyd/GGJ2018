using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public int level;
    public int drinkValue = 100;
	public int addMoneyValue;
    public int moneyTarget = 2000;
	public int failureMax;
	public int timesFailed = 0;
	public int toolLevel;
	public int garnishLevel;
    public Glass Glass;

	public GameObject moneyUI;
	public GameObject bottles;
	public GameObject garnish;
	public GameObject tools;
	public GameObject alienSpawnPoint;
	public GameObject alienPrefab;
	public GameObject angryBoss;

	private GameObject tempAlien;
	private Level currentLevel;
	private bool garnishIn;
	private bool toolIn;

	void Start() {

		newAlien ();
        UIManager.Instance.ManagerSpeechBubble.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {

		if ((level >= garnishLevel)&&(!garnishIn))
			garnishEnter ();
		if ((level >= toolLevel)&&(!toolIn))
			toolEnter();

	}

	public void success(){
		timesFailed = 0;
		level++;
		tempAlien.GetComponent<SliderScript> ().slideOut = true;// this destroys the alien!
		moneyUI.GetComponent<MoneyManager> ().money += addMoneyValue;
        if (moneyUI.GetComponent<MoneyManager>().money >= moneyTarget)
        {
            SceneManager.LoadScene("Credits");
        }
        else
        {
            newAlien();
        }
	}

	public void failed(){
		timesFailed++;
        addMoneyValue /= 2;
		if (timesFailed >= failureMax) {
			tempAlien.GetComponent<SliderScript> ().slideOut = true;// this destroys the alien!
			newAlien ();
		}
		angryBoss.GetComponent<SliderScript> ().slideIn = true;

        var managerSpeechBubble = UIManager.Instance.ManagerSpeechBubble;
        managerSpeechBubble.gameObject.SetActive(true);
        StartCoroutine(ShowManagerSpeechBubble());
	}

    IEnumerator ShowManagerSpeechBubble()
    {
        yield return new WaitForFixedUpdate();

        UIManager.Instance.ManagerSpeechBubble.Show(Glass.Contents, GetComponent<AlienRegistry>().GetAlien(currentLevel.Alien).Dialect);
    }

	public void garnishEnter()
	{
		garnish.GetComponent<SliderScript>().slideIn = true;
		garnishIn = true;
	}

	public void toolEnter()
	{
		tools.GetComponent<SliderScript>().slideIn = true;
		toolIn = true;
	}

	public void newAlien(){
        addMoneyValue = drinkValue;
		tempAlien = Instantiate (alienPrefab, alienSpawnPoint.transform);
        var levels = GetComponent<RecipeCatalog>().Levels;
        if (level < levels.Count)
        {
            currentLevel = levels[level];
            Debug.Log("Level: " + level);
        }
        else
        {
            currentLevel = MakeRandomLevel();
            Debug.Log("Random Level");
        }

		timesFailed = 0;

        var alienComponent = tempAlien.GetComponent<Alien>();
        alienComponent.RecipeRequested = GetComponent<RecipeCatalog>().Recipes[currentLevel.RecipeIndex];
        var alienRegistry = GetComponent<AlienRegistry>();
        var currentAlienSpecies = alienRegistry.GetAlien(currentLevel.Alien);
        alienComponent.Species = currentAlienSpecies;
        tempAlien.GetComponent<SpriteRenderer>().sprite = currentAlienSpecies.Image;

        var symbolSequence = alienComponent.RecipeRequested.GetSymbolSequence(currentAlienSpecies.Dialect);
        var clipSequence = symbolSequence.Select(x => alienRegistry.GetSound(currentAlienSpecies, x));
        VOManager.Instance.PlaySeries(clipSequence, 0.75f);
	}

    Level MakeRandomLevel()
    {
        var output = new Level();

        var alienRegistry = GetComponent<AlienRegistry>();
        switch (Random.Range(0, 5))
        {
            case 0:
                output.Alien = AlienType.Gas;
                break;
            case 1:
                output.Alien = AlienType.Qbert;
                break;
            case 2:
                output.Alien = AlienType.Robot;
                break;
            case 3:
                output.Alien = AlienType.Roswell;
                break;
            case 4:
                output.Alien = AlienType.Slug;
                break;
        }

        output.RecipeIndex = Random.Range(0, GetComponent<RecipeCatalog>().Recipes.Count);

        return output;
    }
}
