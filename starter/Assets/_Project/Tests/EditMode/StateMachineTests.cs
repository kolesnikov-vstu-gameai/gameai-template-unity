using GameAI.AI;
using NUnit.Framework;

public class StateMachineTests
{
    private enum Guard { Patrol, Alert, Chase }

    [Test]
    public void Fire_KnownTrigger_ChangesState()
    {
        var fsm = new StateMachine<Guard>(Guard.Patrol);
        fsm.AddTransition(Guard.Patrol, "see_player", Guard.Alert);
        Assert.IsTrue(fsm.Fire("see_player"));
        Assert.AreEqual(Guard.Alert, fsm.Current);
    }

    [Test]
    public void Fire_UnknownTrigger_KeepsState()
    {
        var fsm = new StateMachine<Guard>(Guard.Patrol);
        Assert.IsFalse(fsm.Fire("noise"));
        Assert.AreEqual(Guard.Patrol, fsm.Current);
    }
}
