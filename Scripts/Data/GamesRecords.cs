using System;
using System.Collections.Generic;
using Yaga.Storage;

public class GamesRecords
{
    [Serializable]
    class GamesRecordsEntity
    {
        public Dictionary<string, Minigames.GameRecords> records;
        public bool tap_tap_tutor_done;
        public bool zig_zag_tutor_done;

        public bool star_minigames_need;

        public GamesRecordsEntity()
        {
            records = new Dictionary<string, Minigames.GameRecords>();
            records.Add("zigzag", new Minigames.GameRecords());
            records.Add("taptap", new Minigames.GameRecords());

            tap_tap_tutor_done = false;
            zig_zag_tutor_done = false;
            star_minigames_need = false;
        }
    }

    public GamesRecords()
    {
        records = new StorableData<GamesRecordsEntity>("game_records");
    }

    StorableData<GamesRecordsEntity> records;
    public bool tutor_tap;

    public bool StarMinigameNeed
    {
        get { return records.content.star_minigames_need; }
        set
        {
            records.content.star_minigames_need = value;
            records.Store();
        }
    }


    public Minigames.GameRecords Record(string game)
    {
        return records.content.records[game];
    }

    public bool tapTapTutorDone
    {
        set { records.content.tap_tap_tutor_done = value; records.Store(); }
        get { return records.content.tap_tap_tutor_done; }
    }

    public bool zigZagTutorDone
    {
        set { records.content.zig_zag_tutor_done = value; records.Store(); }
        get { return records.content.zig_zag_tutor_done; }
    }


    public void setRecords(string game, Minigames.GameRecords rec)
    {
        records.content.records[game] = rec;
        records.Store();
        tutor_tap = false;
    }
}
