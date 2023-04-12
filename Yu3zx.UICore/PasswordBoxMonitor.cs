using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Yu3zx.UICore
{
    /// <summary>
    /// WPF密码框帮助类
    /// </summary>
    public class PasswordBoxMonitor : DependencyObject
    {
        public static bool GetIsMonitoring(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMonitoringProperty);
        }

        public static void SetIsMonitoring(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMonitoringProperty, value);
        }

        public static readonly DependencyProperty IsMonitoringProperty =
            DependencyProperty.RegisterAttached("IsMonitoring", typeof(bool), typeof(PasswordBoxMonitor), new UIPropertyMetadata(false, OnIsMonitoringChanged));

        public static int GetPasswordLength(DependencyObject obj)
        {
            return (int)obj.GetValue(PasswordLengthProperty);
        }

        public static void SetPasswordLength(DependencyObject obj, int value)
        {
            obj.SetValue(PasswordLengthProperty, value);
        }

        public static readonly DependencyProperty PasswordLengthProperty =
            DependencyProperty.RegisterAttached("PasswordLength", typeof(int), typeof(PasswordBoxMonitor), new UIPropertyMetadata(0));

        private static void OnIsMonitoringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pb = d as PasswordBox;
            if (pb == null)
            {
                return;
            }
            if ((bool)e.NewValue)
            {
                pb.PasswordChanged += PasswordChanged;
            }
            else
            {
                pb.PasswordChanged -= PasswordChanged;
            }
        }
        static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pb = sender as PasswordBox;
            if (pb == null)
            {
                return;
            }
            SetPasswordLength(pb, pb.Password.Length);
        }


        //==========新增=========
        public static bool GetIsPasswordBindingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindingEnabledProperty);
        }

        public static void SetIsPasswordBindingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        public static readonly DependencyProperty IsPasswordBindingEnabledProperty =
            DependencyProperty.RegisterAttached("IsPasswordBindingEnabled", typeof(bool),
            typeof(PasswordBoxMonitor),
            new UIPropertyMetadata(false, OnIsPasswordBindingEnabledChanged));

        private static void OnIsPasswordBindingEnabledChanged(DependencyObject obj,
                                                              DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;

            if (passwordBox != null)
            {
                passwordBox.PasswordChanged -= PasswordBoxPasswordChanged;

                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
                }

            }
        }

        //when the passwordBox's password changed, update the buffer
        static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;

            if (!String.Equals(GetBindedPassword(passwordBox), passwordBox.Password))
            {
                SetBindedPassword(passwordBox, passwordBox.Password);
            }
        }

        public static string GetBindedPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(BindedPasswordProperty);
        }

        public static void SetBindedPassword(DependencyObject obj, string value)
        {
            obj.SetValue(BindedPasswordProperty, value);
        }

        public static readonly DependencyProperty BindedPasswordProperty =
            DependencyProperty.RegisterAttached("BindedPassword", typeof(string),
            typeof(PasswordBoxMonitor),
            new UIPropertyMetadata(string.Empty, OnBindedPasswordChanged));

        //when the buffer changed, upate the passwordBox's password
        private static void OnBindedPasswordChanged(DependencyObject obj,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;
            if (passwordBox != null)
            {

                passwordBox.Password = e.NewValue == null ? string.Empty : e.NewValue.ToString();
            }
        }

        private static void SetPasswordBoxSelection(PasswordBox passwordBox, int start, int length)
        {
            var select = passwordBox.GetType().GetMethod("Select",
                            BindingFlags.Instance | BindingFlags.NonPublic);

            select.Invoke(passwordBox, new object[] { start, length });
        }

    }
}
