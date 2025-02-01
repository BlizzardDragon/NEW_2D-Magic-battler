using System;
using Entity.Core;
using FishNet.Object;
using FishNet.Serializing;
using UnityEngine;
using VampireSquid.Common.Connections;

public interface IEntity
{
    int Id { get; }
    NetworkedPresence Presence { get; }
    bool IsInitialized { get; }

    bool        ContainsModule<TDependency>();
    bool        TryGetModule<TDependency>(out TDependency dependency);
    TDependency AddModule<TDependency>(TDependency        dependency);
    TDependency GetModule<TDependency>();
}

//do not remove, this is used for network broadcasting & rpc calls of fishnet
public static class EntityNetworking
{
    public static void WriteEntity(this Writer writer, IEntity entity)
    {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        //writer.WriteString(Environment.StackTrace);
#endif
        
        var nob = entity?.GetModule<NetworkObject>();
        
        var exists = nob != null;

        if (!exists) Debug.LogError("Entity is null or does not contain a NetworkObject");

        writer.WriteBoolean(exists);
        writer.WriteNetworkObject(nob);
    }

    public static IEntity ReadEntity(this Reader reader)
    {
        var stack = "Unknown Stack Trace";
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        //stack = reader.ReadString();
#endif
        
        var exists = reader.ReadBoolean();
        var no     = reader.ReadNetworkObject();

        if (!exists) return null;
        
        try
        {
            return no.GetComponent<MonoEntity>();
        }
        catch (Exception e)
        {
            Debug.LogError($"Network object :\"{no}\" is invalid MonoEntity: {stack}");
            Debug.LogError(e);
        }

        return null;
    }
}
