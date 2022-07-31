using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Function
{
    public static bool isAsyncOperationComplete = false;

    //! ������Ʈ�� �߰��Ѵ�.
    public static T AddComponent<T>(GameObject targetObject) where T : Component
    {
        var targetComponent = targetObject.GetComponent<T>();

        if (targetComponent == null)
        {
            targetComponent = targetObject.AddComponent<T>();
        }

        return targetComponent;
    }

    //! �񵿱� �۾��� ����Ѵ�.
    public static IEnumerator WaitAsyncOperation(AsyncOperation asyncOperation, System.Action<AsyncOperation> callBack)
    {
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke(asyncOperation);
        }
    }

    //! �񵿱� PlayFabAPI ȣ�⿡ ���� ������ ����Ѵ�.
    public static IEnumerator WaitPlayFabAPI(UnityAction playFabAPI)
    {
        isAsyncOperationComplete = false;
        playFabAPI();
        yield return new WaitUntil(() => isAsyncOperationComplete == true);
    }

    //! �Լ��� ���� ȣ���Ѵ�.
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

    //! �Լ��� ���� ȣ���Ѵ�.
    public static IEnumerator DoLateCallByTime(UnityAction<object[]> callback, object[] parameters, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke(parameters);
    }


}
