using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Windows.Forms;

public class Timer : Component
{

    private readonly object _syncObj = new();
    private System.Timers.Timer _coreTimer;

    public Timer() : base()
    {
        _coreTimer = new();
        _coreTimer.AutoReset = true;
        _coreTimer.Interval = 100;
        _coreTimer.Elapsed += (_, _) => OnTick(EventArgs.Empty);
    }

    public Timer(IContainer container) : this()
    {
        ArgumentNullException.ThrowIfNull(container);

        container.Add(this);
    }

    public object? Tag { get; set; }

    public event EventHandler? Tick;


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Enabled = false;
            _coreTimer.Dispose();
        }

        base.Dispose(disposing);
    }

  public virtual bool Enabled
    {
        get => _coreTimer.Enabled;
        set => _coreTimer.Enabled = value;
    }

    [DefaultValue(100)]
    public int Interval
    {
        get => (int)_coreTimer.Interval;
        set
        {
            _coreTimer.Interval = value;
        }
    }

    protected virtual void OnTick(EventArgs e) => Tick?.Invoke(this, e);

    public void Start() => Enabled = true;
    public void Stop() => Enabled = false;

    
}
