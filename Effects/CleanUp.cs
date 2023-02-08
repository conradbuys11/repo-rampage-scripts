using UnityEngine;

public class CleanUp : MonoBehaviour
{
    //Amount of time you want the object to last in the world.
    //Set in the inspector.
    public float lifeTime;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if(lifeTime == 0)
        {
            lifeTime = 1;
        }
        //Increases time until lifetime is matched then it destoryes what it is attached to.
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
