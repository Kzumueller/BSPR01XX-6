using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Timer
{
    // total time for this timer
    public float time;

    public Timer(float time)
    {
        this.time = time;
    }

    // takes time passed since last call of this function (usually TIme.deltaTime) and returns whether time has run out
    public bool Finished(float deltaTime) => 0 >= (time -= deltaTime);
}
