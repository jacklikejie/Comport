namespace Comport
{
    enum StateCode
    {
        None = -1,
        自动运行 = 0,
        待机 = 1,
        正常停机 = 2,
        故障停机 = 3,
        待料 = 4,
        满料 = 5
    }

    enum AlarmState
    {
        恢复 = 0,
        发生 = 1,
    }

    enum AlarmLevel
    {
        L,//提示不停机
        M,//提示停机
        H,//故障停机
    }

    enum DownReasonCode
    {
        维护保养 = 1,
        吃饭_休息 = 2,
        换型 = 3,
        设备改造 = 4,
        来料不良 = 5,
        设备校验 = 6,
        首件_点检 = 7,
        品质异常 = 8,
        缺备件 = 9,
        环境异常 = 10,
        设备信息不完善 = 11,
        故障停机 = 12
    }

    enum UnloadingType
    {
        卸料 = 2,
        卸料并报废 = 3,
    }
}