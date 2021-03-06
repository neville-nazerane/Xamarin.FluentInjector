﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.FluentInjector.Utilities
{
    public interface IPageControl
    {
        object Page { get; }

        Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task<string> DisplayPromptAsync(string title,
                                        string message,
                                        string accept = "OK",
                                        string cancel = "Cancel",
                                        string placeholder = null,
                                        int maxLength = -1,
                                        Keyboard keyboard = null,
                                        string initialValue = "");
        Task<Page> NavigateAsync<T>(Action<T> addData = null);
        Task PopAsync();
        Task PopModalAsync();
        Task PopToRootAsync();
        Task<Page> PushModalAsync<T>(Action<T> addData = null);
        Page ResolvePage<T>(Action<T> addData = null);
    }
}
