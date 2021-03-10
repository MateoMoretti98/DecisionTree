using UnityEngine;
using System.Collections;

public class Citizen : MonoBehaviour 
{
	//TODO: 
	//1. Colocar el nodo raíz.
	//2. Hacer lo que sea necesario para updatear las desiciones
	//del aldeano.
#region DONT TOUCH THIS
	public ParticleSystem sleepPos;
	public ParticleSystem getWoodPos;
	public ParticleSystem farmingPos;
	public ParticleSystem buildPos;

	public void GetFood()
	{
		Debug.Log("Decision: Get Food!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(farmingPos);
	}

	public void GetWood()
	{
		Debug.Log("Decision: Get Wood!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(getWoodPos);
	}

	public void BuildHouses()
	{
		Debug.Log("Decision: Build!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(buildPos);
	}

	public void GoToSleep()
	{
		Debug.Log("Decision: Go to Sleep!");
		DeactivateAllParticles();		
		SetPosAndPlayParticle(sleepPos);
	}

	private void DeactivateAllParticles()
	{
		sleepPos.Stop();
		getWoodPos.Stop();
		farmingPos.Stop();
		buildPos.Stop();
	}

	private void SetPosAndPlayParticle(ParticleSystem target)
	{
		transform.position = target.transform.position;
		target.Play();
	}
    #endregion

    private Node initialNode;
    public RouletteWheel<ActionNode> roulette;
    float timer;

    void Awake()
    {
        DecisionTree();
        StartCoroutine(StartNode());
    }

    void Start()
    {
        ActionNode gettingWoodNight = new ActionNode(() =>
        {
            Debug.Log("ROULETTE OPTION 1: Im getting wood at night");
            EnviromentData.Instance.wood += 5;
            EnviromentData.Instance.stamina -= 4;
            GetWood();
        });
        ActionNode harvestingNight = new ActionNode(() =>
        {
            Debug.Log("ROULETTE OPTION 2: Im getting food at night");
            EnviromentData.Instance.food += 2;
            EnviromentData.Instance.stamina -= 4;
            GetFood();
        });
        ActionNode buildHouseNight = new ActionNode(() =>
        {
            Debug.Log("ROULETTE OPTION 3: Im sleeping at night");
            EnviromentData.Instance.stamina += 2;
            GoToSleep();
        });

        roulette = new RouletteWheel<ActionNode>();

        roulette.options.Add(gettingWoodNight);
        roulette.options.Add(harvestingNight);
        roulette.options.Add(buildHouseNight);
    }

    private void DecisionTree()
    {
        ActionNode normalRest = new ActionNode(() =>
        {
            GoToSleep();
            EnviromentData.Instance.stamina += 2;
        });

        ActionNode getWood = new ActionNode(() =>
        {
            GetWood();
            EnviromentData.Instance.wood += 10;
            EnviromentData.Instance.food -= 2;
        });

        ActionNode harvest = new ActionNode(() =>
        {
            GetFood();
            EnviromentData.Instance.food += 10;
        });

        ActionNode build = new ActionNode(() =>
        {
            BuildHouses();
            EnviromentData.Instance.wood -= 5;
            EnviromentData.Instance.food -= 2;
            EnviromentData.Instance.housesbuilded += 1;
        });

        ActionNode workAtNight = new ActionNode(() =>
        {
            roulette.ExecuteRoulette().Execute();        
        });


        QuestionNode wood = new QuestionNode(() => EnviromentData.Instance.wood <= 0, getWood, build);
        QuestionNode food = new QuestionNode(() => EnviromentData.Instance.food == 0, harvest, wood);
        QuestionNode rain = new QuestionNode(() => EnviromentData.Instance.rain == true, normalRest, food);
        QuestionNode stamina = new QuestionNode(() => EnviromentData.Instance.stamina <= 6, normalRest, workAtNight);
        QuestionNode night = new QuestionNode(() => EnviromentData.Instance.night == true, stamina, rain);

        initialNode = night;
    }

    IEnumerator StartNode()
    {
        while (true)
        {
            initialNode.Execute();
            yield return new WaitForSeconds(1);
        }
    }
}
