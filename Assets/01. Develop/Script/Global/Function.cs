using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Function
{
    public static bool isAsyncOperationComplete = false;

    //! 컴포넌트를 추가한다.
    public static T AddComponent<T>(GameObject targetObject) where T : Component
    {
        var targetComponent = targetObject.GetComponent<T>();

        if (targetComponent == null)
        {
            targetComponent = targetObject.AddComponent<T>();
        }

        return targetComponent;
    }

    //! 비동기 작업을 대기한다.
    public static IEnumerator WaitAsyncOperation(AsyncOperation asyncOperation, System.Action<AsyncOperation> callBack)
    {
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke(asyncOperation);
        }
    }

    //! 비동기 PlayFabAPI 호출에 대한 응답을 대기한다.
    public static IEnumerator WaitPlayFabAPI(UnityAction playFabAPI)
    {
        isAsyncOperationComplete = false;
        playFabAPI();
        yield return new WaitUntil(() => isAsyncOperationComplete == true);
    }

    //! 함수를 지연 호출한다.
    public static IEnumerator DoLateCallByFrame(UnityAction callback, int frame)
    {
        int frameCount = 0;
        while (frameCount == frame)
        {
            ++frameCount;
            yield return null;
        }
        callback?.Invoke();
    }
    public static IEnumerator DoLateCallByFrame(UnityAction<object> callback, object parameters, int frame)
    {
        int frameCount = 0;
        while (frameCount != frame)
        {
            ++frameCount;
            Debug.Log(frameCount);
            yield return null;
        }
        callback?.Invoke(parameters);
    }
    public static IEnumerator DoLateCallByFrame(UnityAction<object[]> callback, object[] parameters, int frame)
    {
        int frameCount = 0;
        while(frameCount == frame)
        {
            ++frameCount;
            yield return null;
        }
        callback?.Invoke(parameters);
    }

    //! 함수를 지연 호출한다.
    public static IEnumerator DoLateCallByTime(UnityAction<object[]> callback, object[] parameters, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke(parameters);
    }


}
