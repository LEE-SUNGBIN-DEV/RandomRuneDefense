using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static partial class Function
{
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

    //! �Լ��� ���� ȣ���Ѵ�.
    public static IEnumerator DoLateCall(UnityAction<object[]> callback, float delay, object[] parameters)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke(parameters);
    }


}
