using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.Messaging.Messages;
using TechChallenge.Core.Models;

namespace TechChallenge.Core.Utils
{
    public class SelectedComicChangedMessage : ValueChangedMessage<Comic>
    {
        public SelectedComicChangedMessage(Comic selectedComic) : base(selectedComic)
        {
        }
    }
}
