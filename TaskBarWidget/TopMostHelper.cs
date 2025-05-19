using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

public static class TopMostHelper
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X, int Y, int cx, int cy,
        uint uFlags);

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private const uint SWP_NOMOVE = 0x0002;
    private const uint SWP_NOSIZE = 0x0001;
    private const uint SWP_SHOWWINDOW = 0x0040;

    public static void ForceTopMost(Form form)
    {
        SetWindowPos(form.Handle, HWND_TOPMOST, 0, 0, 0, 0,
            SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
    }

    public static void StartEnforcingTopMost(Form form, int intervalMs = 500)
    {
        var timer = new Timer { Interval = intervalMs };
        timer.Tick += (s, e) => ForceTopMost(form);
        timer.Start();

        ForceTopMost(form);
    }
}