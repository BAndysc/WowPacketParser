﻿using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("gameobject")]
    public sealed class GameObjectModel : IDataModel
    {
        [DBFieldName("guid", true, true)]
        public string GUID;

        [DBFieldName("id")]
        public uint? ID;

        [DBFieldName("map")]
        public uint? Map;

        [DBFieldName("zoneId")]
        public uint? ZoneID;

        [DBFieldName("areaId")]
        public uint? AreaID;

        [DBFieldName("spawnMask")]
        public uint? SpawnMask;

        [DBFieldName("phaseMask", TargetedDatabase.Zero, TargetedDatabase.WarlordsOfDraenor)]
        public uint? PhaseMask;

        [DBFieldName("PhaseId", TargetedDatabase.Cataclysm)]
        public string PhaseID;

        [DBFieldName("PhaseGroup")]
        public uint? PhaseGroup;

        [DBFieldName("position_x")]
        public float? PositionX;

        [DBFieldName("position_y")]
        public float? PositionY;

        [DBFieldName("position_z")]
        public float? PositionZ;

        [DBFieldName("orientation")]
        public float? Orientation;

        [DBFieldName("rotation", 4, true)]
        public float?[] Rotation;

        [DBFieldName("spawntimesecs")]
        public int? SpawnTimeSecs;

        [DBFieldName("animprogress")]
        public uint? AnimProgress;

        [DBFieldName("state")]
        public uint? State;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
