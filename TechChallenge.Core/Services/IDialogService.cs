﻿namespace TechChallenge.Core.Services
{
    public interface IDialogService
    {
        void ShowAlert(string message, string title = null, string okText = null);
    }
}
