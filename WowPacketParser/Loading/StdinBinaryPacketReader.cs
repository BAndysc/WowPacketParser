using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading;

public class StandardInputBinaryPacketReader : IPacketReader
{
    private readonly BinaryReader _reader;
    private bool first = true;

    private static Stream? stdin;

    public StandardInputBinaryPacketReader(Encoding encoding)
    {
        _reader = new BinaryReader(Console.OpenStandardInput(), encoding);
    }

    public void Dispose()
    {
        _reader.Dispose();
    }

    public bool CanRead()
    {
        return first;
    }

    public Packet Read(int number, string fileName)
    {
        int opcode;
        int length;
        DateTime time;
        Direction direction;
        byte[] data;
        StringBuilder writer = null;
        int cIndex = 0;

        first = false;
        switch (_reader.ReadUInt32())
        {
            case 0x47534d53:
                direction = Direction.ServerToClient;
                break;
            case 0x47534d43:
                direction = Direction.ClientToServer;
                break;
            case 0x4e425f53:
                direction = Direction.BNServerToClient;
                break;
            default:
                direction = Direction.BNClientToServer;
                break;
        }

        cIndex = _reader.ReadInt32(); // session id, connection index
        var tickCount = _reader.ReadInt64();
        time = DateTimeOffset.FromUnixTimeMilliseconds(tickCount).DateTime;
        time = DateTime.SpecifyKind(time, DateTimeKind.Utc);
        time = TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.Local);

        int additionalSize = _reader.ReadInt32();
        length = _reader.ReadInt32();
        _reader.ReadBytes(additionalSize);
        opcode = _reader.ReadInt32();
        data = _reader.ReadBytes(length - 4);

        ClientVersion.SetVersion(time);

        return new Packet(data, opcode, time, direction, number, writer, Path.GetFileName(fileName))
        {
            ConnectionIndex = cIndex
        };
    }

    public long GetTotalSize()
    {
        return 1;
    }

    public long GetCurrentSize()
    {
        return 0;
    }
}