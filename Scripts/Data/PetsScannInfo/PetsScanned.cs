using System.Collections;
using System.Collections.Generic;
using System;
using Yaga.Storage;

public class PetsScannedStorage
{
    [Serializable]
    public class PetsScanned
    {
        public Dictionary<string, bool> opened_pets;

        public PetsScanned()
        {
            opened_pets = new Dictionary<string, bool>();
        }
    }

    public StorableData<PetsScanned> storage;

    public int OpenedPetsCount()
    {
        int cnt = 0;
        foreach(var pair in storage.content.opened_pets)
        {
            if (pair.Value) cnt++;
        }
        return cnt;
    }

    public PetsScannedStorage(List<string> names)
    {
        storage = new StorableData<PetsScanned>("PetsScannedStorage_cats");
        if(storage.content.opened_pets.Count == 0)
        {
            foreach(string name in names)
            {
                storage.content.opened_pets.Add(name, false);
            }
            storage.Store();
        }
    }
}


