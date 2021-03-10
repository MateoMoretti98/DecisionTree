using UnityEngine;
using System.Collections;

public class EnviromentData : MonoBehaviour
{
    //TODO:
    //Esta clase va a contener toda la información necesaria 
    //para que el aldeano pueda decidir que hacer (acceden a esta info haciendo algo como
    //EnviromentData.Instance.foodQty).
    //Por ejemplo acá pueden poner:
    //1. Cuanta madera tiene (la necesita para construir)
    //2. Cuanta comida tiene (la necesita para vivir)
    //3. Estado del clima (si llueve no debería salir de su casa porque se enferma)
    //4. Momento del día (de noche es peligroso salir)	

    public int wood;
    public int food;
    public int stamina;
    public int housesbuilded;
    public bool rain;
    public bool night;
    float timer;
    public GameObject maplight;
    public Light lt;
    public GameObject rainEffect;


    private void Update()
    {
        Timer();
        timer += Time.deltaTime;
    }

    public void Timer()
    {
        if (timer >= 15) night = true;
        if (timer >= 25) night = false;
        if (timer >= 30) rain = true;
        if (timer >= 35)
        {
            rain = false;
            timer = 0;
        }

        if (night == true)      
            maplight.transform.rotation = Quaternion.Euler(-19.084f, -132.74f, 0);
        else
            maplight.transform.rotation = Quaternion.Euler(28.4f, -132.74f, 0);

        if (rain == true)
        {
            lt.color = Color.gray;
            rainEffect.SetActive(true);
        }
        else
        {
            lt.color = Color.white;
            rainEffect.SetActive(false);
        }



    }

    //La siguiente region les va a servir para
    //acceder al aldeano desde sus nodos 'Accion' (deberán heredar de los nodos 'Accion'		
    //y en el método Execute() hacer algo como 'EnviromentData.Instance.citizen.DoSomething()')
    #region DONT TOUCH THIS
    public Citizen citizen;
	public static EnviromentData Instance { get; private set; }
	void Awake()
	{
		Instance = this;
	}
#endregion
}
