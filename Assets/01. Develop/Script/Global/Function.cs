using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Function
{
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

    //! 함수를 지연 호출한다.
    public static IEnumerator DoLateCall(UnityAction<object[]> callback, float delay, object[] parameters)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke(parameters);
    }


}
