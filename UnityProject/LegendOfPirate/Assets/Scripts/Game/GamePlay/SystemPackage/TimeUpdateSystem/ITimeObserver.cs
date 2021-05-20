using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeObserver
{
    int GetTickInterval();
    int GetTickCount();
    int GetTotalSeconds(); // -1表示一直执行
    void OnStart();
    void OnTick(int count);
    void OnFinished();
    void OnPause();
    void OnResume();
    bool ShouldRemoveWhenMapChanged();
}
