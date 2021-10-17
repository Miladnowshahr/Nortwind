using System;
// Define the EventArgs-derived type for this event.
public class FooEventArgs : EventArgs { }
public class TypeWithLotsOfEvents
{
    
    // Define a private instance field that references a collection.
    // The collection manages a set of Event/Delegate pairs.
    // NOTE: The EventSet type is not part of the FCL, it is my own type.
    private readonly EventSet m_eventSet = new EventSet();
    
    // The protected property allows derived types access to the collection.
    protected EventSet EventSet { get { return m_eventSet; } }
    
    #region Code to support the Foo event (repeat this pattern for additional events)

    // Define the members necessary for the Foo event.
    // 2a. Construct a static, read-only object to identify this event.
    // Each object has its own hash code for looking up this
    // event's delegate linked list in the object's collection.
    protected static readonly EventKey s_fooEventKey = new EventKey();
 
    // 2d. Define the event's accessor methods that add/remove the
    // delegate from the collection.
    public event EventHandler<FooEventArgs> Foo
    {
        add { m_eventSet.Add(s_fooEventKey, value); }
        remove { m_eventSet.Remove(s_fooEventKey, value); }
    }
    
    // 2e. Define the protected, virtual On method for this event.
    protected virtual void OnFoo(FooEventArgs e)
    {
        m_eventSet.Raise(s_fooEventKey, this, e);
    }
    // 2f. Define the method that translates input to this event.
    public void SimulateFoo() { OnFoo(new FooEventArgs()); }
    #endregion
}





public sealed class Program
{
    public static void Main()
    {
        TypeWithLotsOfEvents twle = new TypeWithLotsOfEvents();
        // Add a callback here
        twle.Foo += HandleFooEvent;
        // Prove that it worked
        twle.SimulateFoo();
    }
    private static void HandleFooEvent(object sender, FooEventArgs e)
    {
        Console.WriteLine("Handling Foo Event here...");
    }
}