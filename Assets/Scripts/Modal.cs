using UnityEngine;
using System;

public class Modal : MonoBehaviour
{
    Rect m_windowRect;
    Action m_next;
    Action m_home;
    Action m_quit;
    string m_title;
    string m_msg;

    static public void MessageBox(string title, string msg, Action next)
    {
        GameObject go = new GameObject("Modal");
        Modal dlg = go.AddComponent<Modal>();
        dlg.Init(title, msg, next);
    }

    void Init(string title, string msg, Action next)
    {
        m_title = title;
        m_msg = msg;
        m_next = next;
    }

    void OnGUI()
    {
        const int maxWidth = 480;
        const int maxHeight = 360;

        int width = Mathf.Min(maxWidth, Screen.width - 20);
        int height = Mathf.Min(maxHeight, Screen.height - 20);
        m_windowRect = new Rect(
            (Screen.width - width) / 2,
            (Screen.height - height) / 2,
            width,
            height);

        m_windowRect = GUI.Window(0, m_windowRect, WindowFunc, m_title);
    }

    void WindowFunc(int windowID)
    {
        const int border = 10;
        const int width = 80;
        const int height = 35;
        const int spacing = 10;

        Rect l = new Rect(
            border,
            border + spacing,
            m_windowRect.width - border * 2,
            m_windowRect.height - border * 2 - height - spacing);
        GUI.Label(l, m_msg);

        Rect b = new Rect(
            m_windowRect.width - width - border,
            m_windowRect.height - height - border,
            width,
            height);

        if (GUI.Button(b, "NEXT"))
        {
            Destroy(this.gameObject);
            m_next();
        }

        Rect bb = new Rect(
            m_windowRect.width - 2 * width - 2 * border,
            m_windowRect.height - height - border,
            width,
            height);

        if (GUI.Button(bb, "HOME"))
        {
            Destroy(this.gameObject);
            Application.LoadLevel(0);
        }

        Rect bbb = new Rect(
            m_windowRect.width - 3 * width - 3 * border,
            m_windowRect.height - height - border,
            width,
            height);

        if (GUI.Button(bbb, "QUIT"))
        {
            Destroy(this.gameObject);
            Application.Quit();
        }
    }
}
