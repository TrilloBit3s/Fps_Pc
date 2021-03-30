using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2 : MonoBehaviour
{
    public float range;
	public Camera mainCamera;

    public Animation Gun2Animation;
    public AnimationClip shootAutoAnim;
    public AnimationClip reloadAnim;
	public AnimationClip drawAnim;
	public AnimationClip miraAnim;
	public AnimationClip backMiraAnim;
	
	public float fireRate;
	private float currentRateToFire;
	public float timeToReload;
	private float currentRateToReload;
	public bool canshoot = true;
	public bool isDraw;

	public AudioSource Gun2Audio;
	public AudioClip shootSound;

	public ParticleSystem MuzzleFlash;
	public ParticleSystem CartridgeEject;

	public GameObject impactTemp;

	public int bullets = 10; 
	public int cartuchos = 3;
	private int startBullets;

    public float intensity;
    public float smooth;
    public bool isMine;
    private Quaternion origin_rotation;	

    void Start()
	{
		startBullets = bullets;

		currentRateToFire = fireRate;
		currentRateToReload = timeToReload;

		origin_rotation = transform.localRotation;	
    }

    void Update()
	{				
		currentRateToFire += Time.deltaTime;
		currentRateToReload += Time.deltaTime;
		
		if(Gun2Animation.IsPlaying(drawAnim.name))
		{
			isDraw = true;			
		}
		else
		{
			isDraw = false;
		}
				
		if(currentRateToReload >= timeToReload)
		{
			canshoot = true;
		}	
		
		//Input.GetButton("Fire1") //Input.GetKey(KeyCode.Mouse0)
		if(Input.GetButton("Fire1") && currentRateToFire >= fireRate && canshoot && isDraw == false && bullets > 0 )
		{
			Shoot();
		}
		
		//use esta linha caso queira que a arma recarregue sozinha quando acabar a munição
	/*	else if
		if(Input.GetButton("Fire1")&& currentRateToFire >= fireRate && canshoot && bullets == 0 && cartuchos > 0 )
		{
			Shoot();
		}
	*/
        if(Input.GetKeyDown(KeyCode.R) && cartuchos > 0)
		{
		    Reload();
    	}
		
		UpdateSway();
    }  

	private void UpdateSway ()
    {
        //controles
        float t_x_mouse = Input.GetAxis("Mouse X");
        float t_y_mouse = Input.GetAxis("Mouse Y");

        if(!isMine)
        {
            t_x_mouse = 0;
            t_y_mouse = 0;
        }

        //calcula a rotação alvo
        Quaternion t_x_adj = Quaternion.AngleAxis(-intensity * t_x_mouse, Vector3.up);
        Quaternion t_y_adj = Quaternion.AngleAxis(intensity * t_y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * t_x_adj * t_y_adj;

        //girar em direção à rotação alvo
        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * smooth);
    }

    void Shoot()
	{
		bullets --;
		MuzzleFlash.Play();
		CartridgeEject.Play();
		currentRateToFire = 0;
		Gun2Animation.clip = shootAutoAnim;
    	Gun2Animation.Play();
		Gun2Audio.clip = shootSound;
	 	Gun2Audio.Play();

        RaycastHit hit;
        if (Physics.Raycast (mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
		{
			Instantiate (impactTemp, hit.point, Quaternion.LookRotation(hit.normal)); 
        }
    }

    void Reload ()
	{
		cartuchos --;
		bullets += startBullets;
		currentRateToReload = 0;
		canshoot = false;
        Gun2Animation.clip = reloadAnim;
		Gun2Animation.Play(PlayMode.StopAll);
    }

	void Mira ()
	{	
		Gun2Animation.clip = miraAnim;
		Gun2Animation.Play();
		Gun2Animation.clip = backMiraAnim;
		Gun2Animation.Play();
	}
}