using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yaga.Storage;
using System;

public class PushEntity {
    public bool notification_scheduled;
    public StorableData<PushDataEntity> data;

    public PushEntity()
    {
        notification_scheduled = false;
        data = new StorableData<PushDataEntity>("PushDataEntity");
    }
}

[Serializable]
public class PushDataEntity
{
    public bool set_natification;

    public PushDataEntity()
    {
        set_natification = false;
    }
}
